using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace _408ProjectStep1_server
{
    public partial class Form1 : Form
    {
        // Variables:
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> clientUsernames = new List<string>();
        FolderBrowserDialog storageFile;
        string dataBaseFilePath;

        bool terminating = false;
        bool listening = false;
  
        public Form1()
        {
            // Initialize the form
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();

            logs.AppendText("Please select a storage path to start listening.\n");
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ensures that when form is closed it doesn't crash
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                // Starts listening on the port given from gui
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(100);

                listening = true;
                button_listen.Enabled = false;

                // Create a new thread which is going to accept clients
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                // Display message
                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                // If port number from gui can't be parsed into an interger
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            // Continues to execute while listening on a port
            while (listening)
            {
                try
                {
                    // If there is a client, try to accept it.
                    // If it is accepted get the client's username
                    Socket newClient = serverSocket.Accept();
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);

                    // Parse the username
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // Check if a client with the same username is already connected
                    if(clientUsernames.Contains(username))
                    {
                        // If username exists in the server's connected clients list
                        // Display necessary messages
                        logs.AppendText(username + " tried to connect.\n");
                        logs.AppendText("Already existing username, connection not accepted!\n");

                        // Close the socket from servers side ie.disconnect from client
                        newClient.Shutdown(SocketShutdown.Both);
                        newClient.Close();
                    }

                    else
                    {
                        // If client with the username doesn't exist server is coonected to the accepted client
                        // Add the client and its username to list and display message
                        clientSockets.Add(newClient);
                        clientUsernames.Add(username);
                        logs.AppendText("A client with username " + username + " is connected.\n");
                                  
                        // Create a recieve thread for the client and start it       
                        Thread receiveThread = new Thread(() => Receive(newClient));
                        receiveThread.Start();
                    }                    
                }

                catch
                {
                    // If server can't accept  client
                    if (terminating)
                    {
                        // If server is terminating, make listening false
                        listening = false;
                    }
                    else
                    {
                        // If server is not terminating, display message
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void fileRecieve(Socket thisClient, string data)
        {
            string clientName = clientUsernames[clientSockets.IndexOf(thisClient)];
            string receivePath = storageFile.SelectedPath;
            
            // Parse the data to get file name, and size
            string[] parsedData = data.Split(' ');
            string fileName = parsedData[1];
            long fileSize = Int64.Parse(parsedData[2]);

            // Get the appropriate file name with respect to already sent files
            string newName = nameFile(fileName, receivePath, clientName);
            string filePath = receivePath + "\\" + clientName + newName;

            long remainingBytes = fileSize;
            bool succesfullyRecieved = true;

            using (StreamWriter sw = File.AppendText(filePath))
            {
                while (remainingBytes > 0)
                {
                    try
                    {
                        // If socket is in a readable state, recieve bytes
                        if (thisClient.Poll(-1, SelectMode.SelectRead))
                        {
                            Byte[] fileData = new Byte[1024];

                            int recievedBytes = thisClient.Receive(fileData);

                            // write the recieved content to file
                            string content = Encoding.ASCII.GetString(fileData);

                            sw.Write(content);

                            remainingBytes -= recievedBytes;
                        }                        
                    }

                    catch
                    {
                        logs.AppendText("Couldn't recieve file!\n");
                        succesfullyRecieved = false;
                        break;
                    }
                }

                sw.Flush();
                sw.Close();
            }

            // If file is succesfully recieved update the log file
            if (succesfullyRecieved)
            {
                updateDBFile(filePath, clientName, newName, "Private");

                // Display message saying file is recieved
                logs.AppendText("File recieved from " + clientName + "\n");
            }
        }

        private void updateDBFile(string filePath, string clientName, string newName, string status)
        {
            // Get the size of uploaded file
            FileInfo info = new FileInfo(filePath);
            long size = info.Length;

            // updata database log
            DateTime utcDate = DateTime.UtcNow;
            string updateDB = clientName + "\t" + newName + "\t" + size.ToString() 
                + " bytes\t" + utcDate + " Utc" + "\t" + status;

            // If file exists, adds a new line with the received file's information
            // If it doesn't creates a new database file and then adds a new line
            // with the received file's information
            using (StreamWriter sw2 = File.AppendText(dataBaseFilePath))
            {
                sw2.WriteLine(updateDB);

                sw2.Flush();
                sw2.Close();
            }
        }

        private void clientFileList(string client, Socket thisClient)
        {
            string fileList = "";

            StreamReader file = new StreamReader(dataBaseFilePath);
            string line;

            // Create the data of the file list
            while ((line = file.ReadLine()) != null)
            {
                string[] parsedData = line.Split('\t');
                string uploaderClient = parsedData[0];

                if(uploaderClient == client)
                {
                    line = line.Replace('\t', ' ');
                    line = line.Substring(uploaderClient.Length + 1);
                    fileList += line + ",";
                }
                
            }
            file.Close();

            if (fileList == "")
            {
                logs.AppendText("There are not any files uploaded by " + client + "\n");
                fileList = "Empty";
            }

            else
            {
                logs.AppendText(client + "'s uploaded file list is sent.\n");
            }

            // Send header indicating file list will be sent and its size
            string header = "OwnFileList ";
            string size = fileList.Length.ToString();
            Byte[] buffer = Encoding.Default.GetBytes(header + size + " ");

            thisClient.Send(buffer);            

            // Send the file list
            Byte[] list = Encoding.Default.GetBytes(fileList);
            thisClient.Send(list);
        }

        private void Receive(Socket thisClient)
        {
            // Thread that recieves files from clients
            bool connected = true;
            string name = clientUsernames[clientSockets.IndexOf(thisClient)];

            while (connected && !terminating)
            {
                // Execute while client is connected and server is not terminating
                try
                {
                    Byte[] recieveData = new Byte[1024];
                    thisClient.Receive(recieveData);

                    string data = Encoding.Default.GetString(recieveData);

                    if (data.StartsWith("File:"))
                    {
                        // File will be sent from client, perform necessary actions
                        fileRecieve(thisClient, data);
                    }

                    else if (data.StartsWith("RequestOwn:"))
                    {
                        // client requesting its uploaded file list, do acording actions
                        clientFileList(name, thisClient);
                    }

                    else if (data.StartsWith("PrivateFileDownload:"))
                    {
                        fileDownload(data, name, thisClient , "Private");
                    }

                    else if (data.StartsWith("PublicFileDownload:"))
                    {
                        fileDownload(data, name, thisClient, "Public");

                    }
                    else if (data.StartsWith("FileCopy:"))
                    {
                        fileCopy(name, data, thisClient);
                    }

                    else if (data.StartsWith("FileDelete:"))
                    {
                        fileDelete(name, data, thisClient);
                    }

                    else if (data.StartsWith("MakePublic:"))
                    {
                        makePublic(name, data, thisClient);
                    }

                    else if (data.StartsWith("RequestPublic:"))
                    {
                        publicFileList(name, thisClient);
                    }

                    else
                    {
                        // Happens when client disconnected with disconnect button
                        // Throw exception to disconnect client
                        throw new Exception();
                    }

                }

                catch
                {
                    // If an error is thrown while receiving data
                    if (!terminating)
                    {
                        // If server is not terminating, display message saying client has disconnected
                        logs.AppendText(name + " has disconnected\n");
                    }

                    try
                    {
                        // Disconnect from client, remove the client and its username from list
                        connected = false;
                        thisClient.Close();
                        clientUsernames.RemoveAt(clientSockets.IndexOf(thisClient));
                        clientSockets.Remove(thisClient);
                    }

                    catch
                    {
                        logs.AppendText("Couldn't remove client socket!\n");
                    }

                }
            }
        }

        private void publicFileList(string client, Socket thisClient)
        {
            string fileList = "";

            StreamReader file = new StreamReader(dataBaseFilePath);
            string line;

            // Create the data of the file list
            while ((line = file.ReadLine()) != null)
            {
                string[] parsedData = line.Split('\t');
                string status = parsedData[parsedData.Length - 1];
                string uploaderClient = parsedData[0];

                if (status == "Public")
                {
                    line = line.Replace('\t', ' ');
                    fileList += line + ",";
                }

            }
            file.Close();

            if (fileList == "")
            {
                logs.AppendText("There are no public files.\n");
                fileList = "Empty";
            }

            else
            {
                logs.AppendText("Public file list is sent to " + client + "\n");
            }

            // Send header indicating file list will be sent and its size
            string header = "PublicFileList ";
            string size = fileList.Length.ToString();
            Byte[] buffer = Encoding.Default.GetBytes(header + size + " ");

            thisClient.Send(buffer);
            
            // Send the file list
            Byte[] list = Encoding.Default.GetBytes(fileList);
            thisClient.Send(list);
        }

        private void makePublic(string client, string data, Socket socket)
        {
            string file = data.Split(':')[1];
            string path = storageFile.SelectedPath + "\\" + client + file;
            string message = "";

            if (File.Exists(path))
            {
                // Change the status of the file in DBFile
                List<string> linesList = File.ReadAllLines(dataBaseFilePath).ToList();

                for (int i = 0; i < linesList.Count; i++)
                {
                    if (linesList[i] != "")
                    {
                        string[] partial = linesList[i].Split('\t');
                        string dbName = partial[1];
                        string dbClient = partial[0];
                        if (dbName == file && client == dbClient)
                        {
                            // Update the status of the corresponding database file line
                            partial[partial.Length - 1] = "Public";
                            linesList[i] = string.Join("\t", partial);

                            break;
                        }
                    }
                }

                File.WriteAllLines(dataBaseFilePath, linesList.ToArray());

                message = file + " is now public.";
                logs.AppendText(message + "\n");
            }

            else
            {
                message = "File to make public doesn't exist or you are not authorized!";
                logs.AppendText("File to make public by " + client + " doesn't exist!\n");
            }

            Byte[] buffer = Encoding.Default.GetBytes("Message:" + message + ":");
            socket.Send(buffer);
        }

        private void fileDownload(string data, string client, Socket clientSocket, string type)
        {
            string fileName = data.Split(':')[1];
            string path = "";
            string message = "";

            // If client is downloading its own file set path directly
            if (type == "Private")
            {
                path = storageFile.SelectedPath + "\\" + client + fileName;
            }

            // If it is downloading public file, first check the status of that file
            // if it is really public then set the path
            else if (type == "Public")
            {
                List<string> linesList = File.ReadAllLines(dataBaseFilePath).ToList();

                for (int i = 0; i < linesList.Count; i++)
                {
                    if (linesList[i] != "")
                    {
                        string[] partial = linesList[i].Split('\t');
                        string dbName = partial[1];
                        string dbClient = partial[0];
                        string status = partial[partial.Length - 1];

                        if (dbClient + dbName == fileName && status == "Public")
                        {
                            path = storageFile.SelectedPath + "\\" + fileName;
                            break;
                        }
                    }
                }
            }

            // If the file at the set path exists start sending the file to client
            if (File.Exists(path))
            {
                // Create a file header, send it first
                // Header contains file name nad file size
                FileInfo info = new FileInfo(path);

                string length = (info.Length).ToString();

                string fHeader = "FileDownload";
                fHeader = fHeader + " " + fileName + " " + length;

                Byte[] header = new Byte[1024];
                header = Encoding.Default.GetBytes(fHeader);

                clientSocket.Send(header);

                // send the file
                long bytesSent = 0;

                // Read the file to a buffer as it sends
                using (FileStream fs = File.OpenRead(path))
                {
                    // While there are still bytes left to send
                    while (bytesSent < info.Length)
                    {
                        // If socket is in a writable state, send bytes
                        if (clientSocket.Poll(-1, SelectMode.SelectWrite))
                        {
                            Byte[] toBeSended = new Byte[1024];

                            // Find the place where it left in the file
                            // Read 1024 bytes from there to the buffer 
                            fs.Seek(bytesSent, SeekOrigin.Begin);
                            fs.Read(toBeSended, 0, 1024);

                            int sent = clientSocket.Send(toBeSended);

                            // Update the bytes sent
                            bytesSent += sent;
                        }
                    }
                }

                logs.AppendText(client + " downloaded " + fileName + "\n");
                message = "File downloaded succesfully.";
            }

            else
            {
                logs.AppendText("File to be downloaded by " + client + " doesn't exist!\n");
                message = "Requested file doesn't exist!";
            }

            Byte[] buffer = Encoding.Default.GetBytes("Message:" + message + ":");
            clientSocket.Send(buffer);
        }

        private void fileDelete(string client, string data, Socket socket)
        {
            string file = data.Split(':')[1];
            string filePath = storageFile.SelectedPath + "\\" + client + file;
            string message = "";

            if (File.Exists(filePath))
            {
                List<string> linesList = File.ReadAllLines(dataBaseFilePath).ToList();

                for(int i = 0; i < linesList.Count; i++)
                {
                    if (linesList[i] != "")
                    {
                        string[] partial = linesList[i].Split('\t');
                        string name = partial[1];
                        string clientName = partial[0];
                        if (name == file && client == clientName)
                        {
                            linesList.RemoveAt(i);
                            break;
                        }
                    }
                }
                File.WriteAllLines(dataBaseFilePath, linesList.ToArray());

                File.Delete(filePath);

                message = "File succesfully deleted.";
                logs.AppendText(message + "\n");
            }

            else
            {
                message = "Requested file to be deleted doesn't exist or you are not authorized!";
                logs.AppendText(message + "\n");
            }

            Byte[] buffer = Encoding.Default.GetBytes("Message:" + message + ":");
            socket.Send(buffer);
        }

        private void fileCopy(string client, string data, Socket socket)
        {
            string file = data.Split(':')[1];
            string filePath = storageFile.SelectedPath + "\\" + client + file;

            // get the status of copied file
            string status = "Private";
            List<string> linesList = File.ReadAllLines(dataBaseFilePath).ToList();

            for (int i = 0; i < linesList.Count; i++)
            {
                if (linesList[i] != "")
                {
                    string[] partial = linesList[i].Split('\t');
                    string dbName = partial[1];
                    string dbClient = partial[0];
                    if (dbName == file && client == dbClient)
                    {
                        status = partial[partial.Length - 1];
                        break;
                    }
                }
            }

            string message = "";

            if (File.Exists(filePath))
            {
                string newName = nameFile(file, storageFile.SelectedPath, client);
                string newPath = storageFile.SelectedPath + "\\" + client + newName;

                File.Copy(filePath, newPath);
                

                updateDBFile(newPath, client, newName, status);

                message = "File succesfully copied.";
                logs.AppendText(message + "\n");
            }

            else
            {
                message = "Requested file to be copied doesn't exist or you are not authorized!";
                logs.AppendText(message + "\n");
            }

            Byte[] buffer = Encoding.Default.GetBytes("Message:" + message + ":");
            socket.Send(buffer);            
        }

        private string nameFile(string file, string receivePath, string client)
        {
            string filePath = receivePath + "\\" + client + file;

            // If same file exists in that location add a counter to the end of it
            if (File.Exists(filePath))
            {
                // Display message saying this file exists, saved as something else
                logs.AppendText("Existing file, ");

                int counter = 1;
                string name = "";
                string ext = "";

                // find the counter in filename
                if (file.Contains("-"))
                {
                    int countIdx = file.LastIndexOf('-');
                    int extIdx = file.LastIndexOf('.');
                    string sub = file.Substring(countIdx + 1, extIdx - countIdx - 1);
                    int existingCounter;

                    if(Int32.TryParse(sub, out existingCounter))
                    {
                        counter = existingCounter;
                        name = file.Substring(0, file.LastIndexOf('-'));
                        ext = file.Substring(file.LastIndexOf('.'));
                    }

                }          
                
                else
                {
                    name = file.Substring(0, file.LastIndexOf('.'));
                    ext = file.Substring(file.LastIndexOf('.'));
                }      

                while (File.Exists(filePath))
                {
                    file = name + "-" + counter.ToString() + ext;
                    filePath = receivePath + "\\" + client + file;

                    counter++;
                }

                // Display message saying file will be saved with a counter at the end
                logs.AppendText("will be saved as: " + file + "\n");
                return file;
            }

            return file;
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            // Lets server to choose where the received files will be saved
            // through gui
            storageFile = new FolderBrowserDialog();

            if (storageFile.ShowDialog() == DialogResult.OK)
            {
                // If a location is selected
                textBox_path.Text = storageFile.SelectedPath;
                logs.AppendText("Storage path is selected.\n");

                // Creates a database file in the selected location
                if (!File.Exists(storageFile.SelectedPath + "\\DBFile.txt"))
                {
                    FileStream dbFile;
                    dbFile = File.Create(storageFile.SelectedPath + "\\DBFile.txt");

                    dbFile.Flush();
                    dbFile.Close();
                }
                dataBaseFilePath = storageFile.SelectedPath + "\\DBFile.txt";
                button_listen.Enabled = true;              
            }
        }
    }
}
