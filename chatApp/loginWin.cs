using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace chatApp
{
    public partial class LoginWin : Form
    {
        TcpClient toServer;
        string userID;
        NetworkStream toServerStream;

        //构造函数
        public LoginWin()
        {
            InitializeComponent();
        }

        /****************登录****************/
        private void loginButton_Click(object sender, EventArgs e)
        {
            toServer = new TcpClient();
            userID = userIDbox.Text;
            
            try
            {
                toServer.Connect("166.111.140.52", 8000);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toServerStream = toServer.GetStream();

            //向服务器发送学号和密码
            string sendMsg = userIDbox.Text + "_" + passwordBox.Text;
            byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
            toServerStream.Write(sendByt, 0, sendByt.Length);
            byte[] rcvByt = new byte[1024];
            int rcvLength = toServerStream.Read(rcvByt, 0, rcvByt.Length);
            string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
            //stream.Close();
            //是否成功登录
            if (rcvMsg == "lol")
            {
                try
                {
                    MainWin mainForm = new MainWin(userID, toServer);
                    this.Hide();
                    mainForm.parentform = this;
                    mainForm.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Exit();
                }
            }
        }

        /*************关闭窗口**************/
        private void Loginwinclosing(object sender, FormClosingEventArgs e)
        {
            //关闭网络流和tcp连接
            toServerStream.Close();
            toServer.Close();
        }
    }
}
