namespace _408ProjectStep1_client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_browse = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_upload = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_requestList = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_filetoCopy = new System.Windows.Forms.TextBox();
            this.button_copy = new System.Windows.Forms.Button();
            this.textBox_filetoDelete = new System.Windows.Forms.TextBox();
            this.button_delete = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_filetoDownload = new System.Windows.Forms.TextBox();
            this.button_download = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_folderBrowse = new System.Windows.Forms.Button();
            this.textBox_storagePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_makePublic = new System.Windows.Forms.Button();
            this.textBox_fileToMakePublic = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_publicFileRequest = new System.Windows.Forms.Button();
            this.textBox_publicFileDownload = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_publicDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_browse
            // 
            this.button_browse.Enabled = false;
            this.button_browse.Location = new System.Drawing.Point(270, 264);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(75, 32);
            this.button_browse.TabIndex = 0;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(229, 169);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(116, 33);
            this.button_connect.TabIndex = 1;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(107, 118);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(238, 26);
            this.textBox_username.TabIndex = 2;
            this.textBox_username.Text = "simay";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(107, 76);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(238, 26);
            this.textBox_port.TabIndex = 3;
            this.textBox_port.Text = "5555";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(107, 35);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(238, 26);
            this.textBox_ip.TabIndex = 4;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(48, 436);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(831, 246);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "File Selected";
            // 
            // textBox_path
            // 
            this.textBox_path.Enabled = false;
            this.textBox_path.Location = new System.Drawing.Point(107, 232);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(238, 26);
            this.textBox_path.TabIndex = 10;
            // 
            // button_upload
            // 
            this.button_upload.Enabled = false;
            this.button_upload.Location = new System.Drawing.Point(162, 302);
            this.button_upload.Name = "button_upload";
            this.button_upload.Size = new System.Drawing.Size(120, 48);
            this.button_upload.TabIndex = 11;
            this.button_upload.Text = "Upload";
            this.button_upload.UseVisualStyleBackColor = true;
            this.button_upload.Click += new System.EventHandler(this.button_upload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_requestList
            // 
            this.button_requestList.Enabled = false;
            this.button_requestList.Location = new System.Drawing.Point(48, 368);
            this.button_requestList.Margin = new System.Windows.Forms.Padding(2);
            this.button_requestList.Name = "button_requestList";
            this.button_requestList.Size = new System.Drawing.Size(348, 37);
            this.button_requestList.TabIndex = 12;
            this.button_requestList.Text = "Request List of Your Uploaded Files";
            this.button_requestList.UseVisualStyleBackColor = true;
            this.button_requestList.Click += new System.EventHandler(this.button_requestList_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(107, 169);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(116, 33);
            this.button_disconnect.TabIndex = 14;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(424, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Enter the filename to be copied: ";
            // 
            // textBox_filetoCopy
            // 
            this.textBox_filetoCopy.Enabled = false;
            this.textBox_filetoCopy.Location = new System.Drawing.Point(428, 217);
            this.textBox_filetoCopy.Name = "textBox_filetoCopy";
            this.textBox_filetoCopy.Size = new System.Drawing.Size(313, 26);
            this.textBox_filetoCopy.TabIndex = 16;
            // 
            // button_copy
            // 
            this.button_copy.Enabled = false;
            this.button_copy.Location = new System.Drawing.Point(761, 214);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(118, 32);
            this.button_copy.TabIndex = 17;
            this.button_copy.Text = "Copy";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // textBox_filetoDelete
            // 
            this.textBox_filetoDelete.Enabled = false;
            this.textBox_filetoDelete.Location = new System.Drawing.Point(428, 269);
            this.textBox_filetoDelete.Name = "textBox_filetoDelete";
            this.textBox_filetoDelete.Size = new System.Drawing.Size(313, 26);
            this.textBox_filetoDelete.TabIndex = 18;
            // 
            // button_delete
            // 
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(761, 266);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(118, 32);
            this.button_delete.TabIndex = 19;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(424, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Enter the filename to be deleted: ";
            // 
            // textBox_filetoDownload
            // 
            this.textBox_filetoDownload.Enabled = false;
            this.textBox_filetoDownload.Location = new System.Drawing.Point(428, 113);
            this.textBox_filetoDownload.Name = "textBox_filetoDownload";
            this.textBox_filetoDownload.Size = new System.Drawing.Size(313, 26);
            this.textBox_filetoDownload.TabIndex = 21;
            // 
            // button_download
            // 
            this.button_download.Enabled = false;
            this.button_download.Location = new System.Drawing.Point(761, 110);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(118, 32);
            this.button_download.TabIndex = 22;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(317, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Enter your own filename to be downloaded: ";
            // 
            // button_folderBrowse
            // 
            this.button_folderBrowse.Enabled = false;
            this.button_folderBrowse.Location = new System.Drawing.Point(761, 58);
            this.button_folderBrowse.Name = "button_folderBrowse";
            this.button_folderBrowse.Size = new System.Drawing.Size(118, 32);
            this.button_folderBrowse.TabIndex = 24;
            this.button_folderBrowse.Text = "Browse";
            this.button_folderBrowse.UseVisualStyleBackColor = true;
            this.button_folderBrowse.Click += new System.EventHandler(this.button_folderBrowse_Click);
            // 
            // textBox_storagePath
            // 
            this.textBox_storagePath.Enabled = false;
            this.textBox_storagePath.Location = new System.Drawing.Point(428, 61);
            this.textBox_storagePath.Name = "textBox_storagePath";
            this.textBox_storagePath.Size = new System.Drawing.Size(313, 26);
            this.textBox_storagePath.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(424, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(301, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "Choose a folder for the downloaded files: ";
            // 
            // button_makePublic
            // 
            this.button_makePublic.Enabled = false;
            this.button_makePublic.Location = new System.Drawing.Point(761, 318);
            this.button_makePublic.Name = "button_makePublic";
            this.button_makePublic.Size = new System.Drawing.Size(118, 32);
            this.button_makePublic.TabIndex = 27;
            this.button_makePublic.Text = "Make Public";
            this.button_makePublic.UseVisualStyleBackColor = true;
            this.button_makePublic.Click += new System.EventHandler(this.button_makePublic_Click);
            // 
            // textBox_fileToMakePublic
            // 
            this.textBox_fileToMakePublic.Enabled = false;
            this.textBox_fileToMakePublic.Location = new System.Drawing.Point(428, 321);
            this.textBox_fileToMakePublic.Name = "textBox_fileToMakePublic";
            this.textBox_fileToMakePublic.Size = new System.Drawing.Size(313, 26);
            this.textBox_fileToMakePublic.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(424, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(253, 20);
            this.label9.TabIndex = 29;
            this.label9.Text = "Enter the filename to make public: ";
            // 
            // button_publicFileRequest
            // 
            this.button_publicFileRequest.Enabled = false;
            this.button_publicFileRequest.Location = new System.Drawing.Point(428, 368);
            this.button_publicFileRequest.Margin = new System.Windows.Forms.Padding(2);
            this.button_publicFileRequest.Name = "button_publicFileRequest";
            this.button_publicFileRequest.Size = new System.Drawing.Size(348, 37);
            this.button_publicFileRequest.TabIndex = 30;
            this.button_publicFileRequest.Text = "Request List of Public Files";
            this.button_publicFileRequest.UseVisualStyleBackColor = true;
            this.button_publicFileRequest.Click += new System.EventHandler(this.button_publicFileRequest_Click);
            // 
            // textBox_publicFileDownload
            // 
            this.textBox_publicFileDownload.Enabled = false;
            this.textBox_publicFileDownload.Location = new System.Drawing.Point(428, 165);
            this.textBox_publicFileDownload.Name = "textBox_publicFileDownload";
            this.textBox_publicFileDownload.Size = new System.Drawing.Size(313, 26);
            this.textBox_publicFileDownload.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(295, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "Enter public filename to be downloaded: ";
            // 
            // button_publicDownload
            // 
            this.button_publicDownload.Enabled = false;
            this.button_publicDownload.Location = new System.Drawing.Point(761, 162);
            this.button_publicDownload.Name = "button_publicDownload";
            this.button_publicDownload.Size = new System.Drawing.Size(118, 32);
            this.button_publicDownload.TabIndex = 33;
            this.button_publicDownload.Text = "Download";
            this.button_publicDownload.UseVisualStyleBackColor = true;
            this.button_publicDownload.Click += new System.EventHandler(this.button_publicDownload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 708);
            this.Controls.Add(this.button_publicDownload);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_publicFileDownload);
            this.Controls.Add(this.button_publicFileRequest);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_fileToMakePublic);
            this.Controls.Add(this.button_makePublic);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_storagePath);
            this.Controls.Add(this.button_folderBrowse);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.textBox_filetoDownload);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.textBox_filetoDelete);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.textBox_filetoCopy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_requestList);
            this.Controls.Add(this.button_upload);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.button_browse);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_upload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_requestList;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_filetoCopy;
        private System.Windows.Forms.Button button_copy;
        private System.Windows.Forms.TextBox textBox_filetoDelete;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_filetoDownload;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_folderBrowse;
        private System.Windows.Forms.TextBox textBox_storagePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_makePublic;
        private System.Windows.Forms.TextBox textBox_fileToMakePublic;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_publicFileRequest;
        private System.Windows.Forms.TextBox textBox_publicFileDownload;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_publicDownload;
    }
}

