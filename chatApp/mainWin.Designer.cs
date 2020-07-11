namespace chatApp
{
    partial class MainWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.friendListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chatButton = new System.Windows.Forms.Button();
            this.sendFileButton = new System.Windows.Forms.Button();
            this.Logoutbtn = new System.Windows.Forms.Button();
            this.Addfrdbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.FrdIDbox = new System.Windows.Forms.TextBox();
            this.Searchfrdbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // friendListBox
            // 
            this.friendListBox.FormattingEnabled = true;
            this.friendListBox.ItemHeight = 18;
            this.friendListBox.Location = new System.Drawing.Point(59, 86);
            this.friendListBox.Name = "friendListBox";
            this.friendListBox.Size = new System.Drawing.Size(264, 292);
            this.friendListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(56, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择一个在线好友聊天或发送文件";
            // 
            // chatButton
            // 
            this.chatButton.Location = new System.Drawing.Point(401, 86);
            this.chatButton.Name = "chatButton";
            this.chatButton.Size = new System.Drawing.Size(75, 32);
            this.chatButton.TabIndex = 2;
            this.chatButton.Text = "聊天";
            this.chatButton.UseVisualStyleBackColor = true;
            this.chatButton.Click += new System.EventHandler(this.chatButton_Click);
            // 
            // sendFileButton
            // 
            this.sendFileButton.Location = new System.Drawing.Point(401, 136);
            this.sendFileButton.Name = "sendFileButton";
            this.sendFileButton.Size = new System.Drawing.Size(75, 32);
            this.sendFileButton.TabIndex = 3;
            this.sendFileButton.Text = "传文件";
            this.sendFileButton.UseVisualStyleBackColor = true;
            this.sendFileButton.Click += new System.EventHandler(this.sendFileButton_Click);
            // 
            // Logoutbtn
            // 
            this.Logoutbtn.Location = new System.Drawing.Point(565, 111);
            this.Logoutbtn.Name = "Logoutbtn";
            this.Logoutbtn.Size = new System.Drawing.Size(75, 31);
            this.Logoutbtn.TabIndex = 4;
            this.Logoutbtn.Text = "下线";
            this.Logoutbtn.UseVisualStyleBackColor = true;
            this.Logoutbtn.Click += new System.EventHandler(this.Logoutbtn_Click);
            // 
            // Addfrdbtn
            // 
            this.Addfrdbtn.Location = new System.Drawing.Point(539, 346);
            this.Addfrdbtn.Name = "Addfrdbtn";
            this.Addfrdbtn.Size = new System.Drawing.Size(75, 32);
            this.Addfrdbtn.TabIndex = 7;
            this.Addfrdbtn.Text = "添加";
            this.Addfrdbtn.UseVisualStyleBackColor = true;
            this.Addfrdbtn.Click += new System.EventHandler(this.Addfrdbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(398, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "输入学号搜索好友or添加朋友";
            // 
            // FrdIDbox
            // 
            this.FrdIDbox.Location = new System.Drawing.Point(456, 300);
            this.FrdIDbox.Name = "FrdIDbox";
            this.FrdIDbox.Size = new System.Drawing.Size(100, 28);
            this.FrdIDbox.TabIndex = 9;
            // 
            // Searchfrdbtn
            // 
            this.Searchfrdbtn.Location = new System.Drawing.Point(401, 346);
            this.Searchfrdbtn.Name = "Searchfrdbtn";
            this.Searchfrdbtn.Size = new System.Drawing.Size(75, 32);
            this.Searchfrdbtn.TabIndex = 10;
            this.Searchfrdbtn.Text = "搜索";
            this.Searchfrdbtn.UseVisualStyleBackColor = true;
            this.Searchfrdbtn.Click += new System.EventHandler(this.Searchfrdbtn_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(747, 440);
            this.Controls.Add(this.Searchfrdbtn);
            this.Controls.Add(this.FrdIDbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Addfrdbtn);
            this.Controls.Add(this.Logoutbtn);
            this.Controls.Add(this.sendFileButton);
            this.Controls.Add(this.chatButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.friendListBox);
            this.Name = "MainWin";
            this.Text = "MainWin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainformClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox friendListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chatButton;
        private System.Windows.Forms.Button sendFileButton;
        private System.Windows.Forms.Button Logoutbtn;
        private System.Windows.Forms.Button Addfrdbtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FrdIDbox;
        private System.Windows.Forms.Button Searchfrdbtn;
    }
}