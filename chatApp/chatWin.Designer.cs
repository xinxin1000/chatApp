namespace chatApp
{
    partial class ChatWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatWin));
            this.Chatbox = new System.Windows.Forms.RichTextBox();
            this.Sendchatbox = new System.Windows.Forms.RichTextBox();
            this.Titlelabel = new System.Windows.Forms.Label();
            this.Sendbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Chatbox
            // 
            this.Chatbox.Location = new System.Drawing.Point(159, 94);
            this.Chatbox.Name = "Chatbox";
            this.Chatbox.ReadOnly = true;
            this.Chatbox.Size = new System.Drawing.Size(384, 144);
            this.Chatbox.TabIndex = 1;
            this.Chatbox.Text = "";
            // 
            // Sendchatbox
            // 
            this.Sendchatbox.Location = new System.Drawing.Point(159, 269);
            this.Sendchatbox.Name = "Sendchatbox";
            this.Sendchatbox.Size = new System.Drawing.Size(291, 54);
            this.Sendchatbox.TabIndex = 0;
            this.Sendchatbox.Text = "";
            this.Sendchatbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sendchatbox_keydown);
            // 
            // Titlelabel
            // 
            this.Titlelabel.AutoSize = true;
            this.Titlelabel.BackColor = System.Drawing.Color.Transparent;
            this.Titlelabel.Location = new System.Drawing.Point(134, 59);
            this.Titlelabel.Name = "Titlelabel";
            this.Titlelabel.Size = new System.Drawing.Size(107, 18);
            this.Titlelabel.TabIndex = 2;
            this.Titlelabel.Text = "与...的聊天";
            // 
            // Sendbutton
            // 
            this.Sendbutton.Location = new System.Drawing.Point(468, 290);
            this.Sendbutton.Name = "Sendbutton";
            this.Sendbutton.Size = new System.Drawing.Size(75, 33);
            this.Sendbutton.TabIndex = 3;
            this.Sendbutton.Text = "发送";
            this.Sendbutton.UseVisualStyleBackColor = true;
            this.Sendbutton.Click += new System.EventHandler(this.Sendbutton_Click);
            // 
            // ChatWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(676, 354);
            this.Controls.Add(this.Sendbutton);
            this.Controls.Add(this.Titlelabel);
            this.Controls.Add(this.Sendchatbox);
            this.Controls.Add(this.Chatbox);
            this.Name = "ChatWin";
            this.Text = "ChatWin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chatwinclosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Chatbox;
        private System.Windows.Forms.RichTextBox Sendchatbox;
        private System.Windows.Forms.Label Titlelabel;
        private System.Windows.Forms.Button Sendbutton;
    }
}