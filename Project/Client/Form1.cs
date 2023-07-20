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
using System.Threading;
using System.IO;

namespace _408ProjectStep1_client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        OpenFileDialog file;
        FolderBrowserDialog storageFile;
        string storagePath;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ensures that when form is closed program terminates without crashing
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            // Assign a new socket to client
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;

            int portNum;
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    // Try to connect to ip and port from by gui
                    clientSocket.Connect(IP, portNum);

                    // After connecting send username to server
                    if (textBox_username.Text == "")
                    {
                        // Check if a username is entered if not display a message
                        logs.AppendText("Please enter username!\n");
                    }

                    else
                    {
                        // Send the username to connected server
                        Byte[] buffer = new Byte[64];
                        buffer = Encoding.Default.GetBytes(textBox_username.Text);
                        clientSocket.Send(buffer);
                    }

                    // Wait for the necessary operations at the server side
                    // Server checks if there is another client with the same username
                    System.Threading.Thread.Sleep(1000);

                    // If server doesn't close the socket at server side after getting the username
                    if (isConnected(clientSocket))
                    {
                        // It means that there was not any another client with the same username
                        connected = true;
                        logs.AppendText("Connected to the server!\n");

                        button_connect.Enabled = false;
                        button_browse.Enabled = true;
                        button_requestList.Enabled = true;
                        button_disconnect.Enabled = true;
                        button_copy.Enabled = true;
                        button_delete.Enabled = true;
                        button_folderBrowse.Enabled = true;
                        button_makePublic.Enabled = true;
                        button_publicFileRequest.Enabled = true;                        

                        textBox_filetoCopy.Enabled = true;
                        textBox_filetoDelete.Enabled = true;                        
                        textBox_fileToMakePublic.Enabled = true;                       

                        // Activate the thread that enables the client to recieve message from server
                        // This is used to tell if server is closed
                        Thread checkServerThread = new Thread(Recieve);
                        checkServerThread.Start();
                    }

                    else
                    {
                        // Display the necessary messages if a client with the same username exists at the server side
                        logs.AppendText("Already existing username, could not connect!\n");
                        logs.AppendText("Try again with another username.\n");
                    }
                }

                catch
                {
                    // If client can't connect
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                // If port number fom gui can't be parsed into an integer
                logs.AppendText("Check the port\n");
            }
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            // Enables user to choose the file that is going to be sended 
            // to the server through gui
            file = new OpenFileDialog();

            if (file.ShowDialog() == DialogResult.OK)
            {
                // If a file is selected display a message
                textBox_path.Text = file.FileName;
                logs.AppendText("File selected.\n");
                button_upload.Enabled = true;
            }
        }

        private void fileSend()
        {
            FileInfo info = new FileInfo(file.FileName);

            long bytesSent = 0;

            // Read the file to a buffer as it sends
            using (FileStream fs = File.OpenRead(file.FileName))
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
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a file header, send it first
                // Header contains file name nad file size
                FileInfo info = new FileInfo(file.FileName);

                string fileName = info.Name;
                string filePath = file.FileName;
                string length = (info.Length).ToString();

                string fHeader = "File:";
                fHeader = fHeader + " " + fileName + " " + length;

                Byte[] header = new Byte[1024];
                header = Encoding.Default.GetBytes(fHeader);

                clientSocket.Send(header);

                // Call the function that sends the file content
                fileSend();

                // Display the necessary messages
                logs.AppendText("File is sent to server.\n");
                textBox_path.Text = "";
                button_upload.Enabled = false;
            }

            catch
            {
                // If an error occures while sending, display necessary message
                logs.AppendText("Couldn't upload the file!\n");
            }
        }

        public static bool isConnected(Socket socket)
        {
            // This function tells if a socket is connected to the server's 
            // correcponding socket ie. if the socket is active
            try
            {
                bool part1 = socket.Poll(1000, SelectMode.SelectRead);
                bool part2 = (socket.Available == 0);
                if (part1 && part2)
                    return false;
                else
                    return true;
            }
            catch (SocketException) { return false; }
        }

        private void Recieve()
        {
            // Another thread to check if server is closing
            string username = textBox_username.Text;

            while (connected && !terminating)
            {
                // While connected and not terminating
                try
                {
                    // If the server is closed this will throw an error
                    Byte[] buffer = new Byte[1024];
                    clientSocket.Receive(buffer);

                    string data = Encoding.ASCII.GetString(buffer);

                    if (data.StartsWith("OwnFileList"))
                    {
                        receiveFileList(data);
                    }

                    else if (data.StartsWith("FileDownload"))
                    {
                        fileRecieve(data);
                    }

                    else if(data.StartsWith("PublicFileList"))
                    {
                        receiveFileList(data);
                    }

                    else if (data.StartsWith("Message"))
                    {
                        string mes = data.Split(':')[1];
                        logs.AppendText(mes + "\n");
                    }

                }

                catch
                {
                    // The error is caught here
                    if (!terminating && connected)
                    {
                        // If client is not terminating this error means that server has closed
                        logs.AppendText("The server has closed.\n");
                        clientSocket.Close();
                        connected = false;
                    }

                    button_browse.Enabled = false;
                    button_connect.Enabled = true;
                    button_requestList.Enabled = false;
                    button_disconnect.Enabled = false;
                    button_copy.Enabled = false;
                    button_delete.Enabled = false;
                    button_makePublic.Enabled = false;
                    button_publicFileRequest.Enabled = false;

                    textBox_filetoCopy.Enabled = false;
                    textBox_filetoDelete.Enabled = false;
                    textBox_fileToMakePublic.Enabled = false;
                    textBox_filetoDownload.Enabled = false;
                    textBox_publicFileDownload.Enabled = false;
                    
                }
            }
        }

        private void fileRecieve(string data)
        {
            // Parse the data to get file name, and size
            string[] parsedData = data.Split(' ');
            string fileName = parsedData[1];
            long fileSize = Int64.Parse(parsedData[2]);

            // Get the appropriate file name with respect to already sent files
            string filePath = storagePath + "\\" + fileName;

            long remainingBytes = fileSize;
            bool succesfullyRecieved = true;

            using (StreamWriter sw = File.AppendText(filePath))
            {
                while (remainingBytes > 0)
                {
                    try
                    {
                        // If socket is in a readable state, recieve bytes
                        if (clientSocket.Poll(-1, SelectMode.SelectRead))
                        {
                            Byte[] fileData = new Byte[1024];

                            int recievedBytes = clientSocket.Receive(fileData);

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
        }

        private void receiveFileList(string data)
        {
            int size = Int32.Parse(data.Split(' ')[1]);

            try
            {
                // If socket is in a readable state, recieve bytes
                if (clientSocket.Poll(-1, SelectMode.SelectRead))
                {
                    Byte[] list = new Byte[size];
                    clientSocket.Receive(list);

                    string filelist = Encoding.ASCII.GetString(list);

                    if(filelist == "Empty")
                    {
                        logs.AppendText("No files found.\n");
                    }

                    else
                    {
                        string[] splitList = filelist.Split(',');

                        foreach (string element in splitList)
                        {
                            logs.AppendText(element + "\n");
                        }
                    }                    
                }
            }

            catch
            {
                logs.AppendText("Couldn't get the list of files.\n");
            }

        }

        private void button_requestList_Click(object sender, EventArgs e)
        {
            logs.AppendText("Requesting a list of your uploaded files...\n");

            string request = "RequestOwn:";
            Byte[] req = new Byte[16];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            clientSocket.Close();
            logs.AppendText("Disconnected...\n");

            button_browse.Enabled = false;
            button_connect.Enabled = true;
            button_requestList.Enabled = false;
            button_disconnect.Enabled = false;

            button_download.Enabled = false;
            button_folderBrowse.Enabled = false;
            textBox_storagePath.Text = "";
            button_copy.Enabled = false;
            button_delete.Enabled = false;
            button_makePublic.Enabled = false;
            button_publicFileRequest.Enabled = false;
            button_publicDownload.Enabled = false;

            textBox_filetoCopy.Enabled = false;
            textBox_filetoDelete.Enabled = false;
            textBox_filetoDownload.Enabled = false;
            textBox_fileToMakePublic.Enabled = false;
            textBox_publicFileDownload.Enabled = false;
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            logs.AppendText("File to be copied is " + textBox_filetoCopy.Text + "\n");

            string filename = textBox_filetoCopy.Text;
            string request = "FileCopy:" + filename + ":";

            Byte[] req = new Byte[1024];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);

            textBox_filetoCopy.Text = "";
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            logs.AppendText("File to be deleted is " + textBox_filetoDelete.Text + "\n");

            string filename = textBox_filetoDelete.Text;
            string request = "FileDelete:" + filename + ":";

            Byte[] req = new Byte[1024];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);

            textBox_filetoDelete.Text = "";
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            logs.AppendText("File to be downloaded is " + textBox_filetoDownload.Text + "\n");

            string filename = textBox_filetoDownload.Text;
            string request = "PrivateFileDownload:" + filename + ":";

            Byte[] req = new Byte[1024];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);

            textBox_filetoDownload.Text = "";
        }

        private void button_folderBrowse_Click(object sender, EventArgs e)
        {
            storageFile = new FolderBrowserDialog();

            if (storageFile.ShowDialog() == DialogResult.OK)
            {
                // If a location is selected
                textBox_storagePath.Text = storageFile.SelectedPath;
                storagePath = storageFile.SelectedPath;
                logs.AppendText("Storage path is selected.\n");

                button_download.Enabled = true;
                textBox_filetoDownload.Enabled = true;
                button_publicDownload.Enabled = true;
                textBox_publicFileDownload.Enabled = true;
            }
        }

        private void button_makePublic_Click(object sender, EventArgs e)
        {
            logs.AppendText("File to make public is " + textBox_fileToMakePublic.Text + "\n");

            string filename = textBox_fileToMakePublic.Text;
            string request = "MakePublic:" + filename + ":";

            Byte[] req = new Byte[1024];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);

            textBox_fileToMakePublic.Text = "";
        }

        private void button_publicFileRequest_Click(object sender, EventArgs e)
        {
            logs.AppendText("Requesting a list of public files...\n");

            string request = "RequestPublic:";
            Byte[] req = new Byte[16];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);
        }

        private void button_publicDownload_Click(object sender, EventArgs e)
        {
            logs.AppendText("File to be downloaded is " + textBox_publicFileDownload.Text + "\n");

            string filename = textBox_publicFileDownload.Text;
            string request = "PublicFileDownload:" + filename + ":";

            Byte[] req = new Byte[1024];
            req = Encoding.Default.GetBytes(request);

            clientSocket.Send(req);

            textBox_publicFileDownload.Text = "";
        }
    }
}
