namespace chatApp
{
    partial class FileSendingWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSendingWin));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Selectfilebtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Startsendbtn = new System.Windows.Forms.Button();
            this.filenamebox = new System.Windows.Forms.TextBox();
            this.filelengthbox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Selectfilebtn
            // 
            this.Selectfilebtn.Location = new System.Drawing.Point(90, 61);
            this.Selectfilebtn.Name = "Selectfilebtn";
            this.Selectfilebtn.Size = new System.Drawing.Size(104, 35);
            this.Selectfilebtn.TabIndex = 0;
            this.Selectfilebtn.Text = "选择文件";
            this.Selectfilebtn.UseVisualStyleBackColor = true;
            this.Selectfilebtn.Click += new System.EventHandler(this.Selectfilebtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(87, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(87, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件大小";
            // 
            // Startsendbtn
            // 
            this.Startsendbtn.Location = new System.Drawing.Point(90, 239);
            this.Startsendbtn.Name = "Startsendbtn";
            this.Startsendbtn.Size = new System.Drawing.Size(89, 27);
            this.Startsendbtn.TabIndex = 3;
            this.Startsendbtn.Text = "发送";
            this.Startsendbtn.UseVisualStyleBackColor = true;
            this.Startsendbtn.Click += new System.EventHandler(this.Startsendbtn_Click);
            // 
            // filenamebox
            // 
            this.filenamebox.Location = new System.Drawing.Point(208, 128);
            this.filenamebox.Name = "filenamebox";
            this.filenamebox.Size = new System.Drawing.Size(100, 28);
            this.filenamebox.TabIndex = 4;
            // 
            // filelengthbox
            // 
            this.filelengthbox.Location = new System.Drawing.Point(208, 178);
            this.filelengthbox.Name = "filelengthbox";
            this.filelengthbox.Size = new System.Drawing.Size(100, 28);
            this.filelengthbox.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(90, 293);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(218, 21);
            this.progressBar1.TabIndex = 6;
            // 
            // FileSendingWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(439, 396);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.filelengthbox);
            this.Controls.Add(this.filenamebox);
            this.Controls.Add(this.Startsendbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Selectfilebtn);
            this.Name = "FileSendingWin";
            this.Text = "fileSendingWin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileSendingWinClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Selectfilebtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Startsendbtn;
        private System.Windows.Forms.TextBox filenamebox;
        private System.Windows.Forms.TextBox filelengthbox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}