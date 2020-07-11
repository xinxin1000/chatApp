namespace chatApp
{
    partial class fileRcvingWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fileRcvingWin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sendernamebox = new System.Windows.Forms.TextBox();
            this.filenamebox = new System.Windows.Forms.TextBox();
            this.Rcvbtn = new System.Windows.Forms.Button();
            this.Denybtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.filelengthbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(70, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "发件人";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(70, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "文件名";
            // 
            // sendernamebox
            // 
            this.sendernamebox.Location = new System.Drawing.Point(173, 41);
            this.sendernamebox.Name = "sendernamebox";
            this.sendernamebox.Size = new System.Drawing.Size(100, 28);
            this.sendernamebox.TabIndex = 2;
            // 
            // filenamebox
            // 
            this.filenamebox.Location = new System.Drawing.Point(173, 92);
            this.filenamebox.Name = "filenamebox";
            this.filenamebox.Size = new System.Drawing.Size(100, 28);
            this.filenamebox.TabIndex = 3;
            // 
            // Rcvbtn
            // 
            this.Rcvbtn.Location = new System.Drawing.Point(70, 199);
            this.Rcvbtn.Name = "Rcvbtn";
            this.Rcvbtn.Size = new System.Drawing.Size(75, 31);
            this.Rcvbtn.TabIndex = 4;
            this.Rcvbtn.Text = "接收";
            this.Rcvbtn.UseVisualStyleBackColor = true;
            this.Rcvbtn.Click += new System.EventHandler(this.Rcvbtn_Click);
            // 
            // Denybtn
            // 
            this.Denybtn.Location = new System.Drawing.Point(70, 254);
            this.Denybtn.Name = "Denybtn";
            this.Denybtn.Size = new System.Drawing.Size(75, 31);
            this.Denybtn.TabIndex = 5;
            this.Denybtn.Text = "拒绝";
            this.Denybtn.UseVisualStyleBackColor = true;
            this.Denybtn.Click += new System.EventHandler(this.Denybtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(173, 199);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(208, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(65, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "文件大小";
            // 
            // filelengthbox
            // 
            this.filelengthbox.Location = new System.Drawing.Point(173, 142);
            this.filelengthbox.Name = "filelengthbox";
            this.filelengthbox.Size = new System.Drawing.Size(100, 28);
            this.filelengthbox.TabIndex = 8;
            // 
            // fileRcvingWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(471, 402);
            this.Controls.Add(this.filelengthbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Denybtn);
            this.Controls.Add(this.Rcvbtn);
            this.Controls.Add(this.filenamebox);
            this.Controls.Add(this.sendernamebox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "fileRcvingWin";
            this.Text = "fileRcvingWin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FilercvwinClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sendernamebox;
        private System.Windows.Forms.TextBox filenamebox;
        private System.Windows.Forms.Button Rcvbtn;
        private System.Windows.Forms.Button Denybtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filelengthbox;
    }
}