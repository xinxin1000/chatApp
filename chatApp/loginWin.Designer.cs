namespace chatApp
{
    partial class LoginWin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWin));
            this.userIDlabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userIDbox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userIDlabel
            // 
            this.userIDlabel.AutoSize = true;
            this.userIDlabel.BackColor = System.Drawing.Color.Transparent;
            this.userIDlabel.Location = new System.Drawing.Point(84, 125);
            this.userIDlabel.Name = "userIDlabel";
            this.userIDlabel.Size = new System.Drawing.Size(44, 18);
            this.userIDlabel.TabIndex = 0;
            this.userIDlabel.Text = "学号";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.Location = new System.Drawing.Point(84, 186);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(44, 18);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "密码";
            // 
            // userIDbox
            // 
            this.userIDbox.Location = new System.Drawing.Point(221, 125);
            this.userIDbox.Name = "userIDbox";
            this.userIDbox.Size = new System.Drawing.Size(100, 28);
            this.userIDbox.TabIndex = 2;
            this.userIDbox.Text = "2014010528";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(221, 175);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 28);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.Text = "net2018";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(151, 263);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 32);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "登录";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // LoginWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(541, 374);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.userIDbox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userIDlabel);
            this.Name = "LoginWin";
            this.Text = "LoginWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loginwinclosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userIDlabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userIDbox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button loginButton;
    }
}

