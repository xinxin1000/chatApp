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
    public partial class ChatWin : Form
    {
        public string friendID;
        string userID;
        TcpClient toServer;
        int listenPort;
        public MainWin parentwin = null;
        
        /*******************构造函数********************/
        public ChatWin(string userID,string friendID,int listenPort,TcpClient toServer)
        {
            InitializeComponent();
            this.friendID = friendID;
            this.userID = userID;
            this.toServer = toServer;
            this.listenPort = listenPort;
            this.Text = "与" + friendID + "的聊天窗口";
            Titlelabel.Text = "与" + friendID + "的聊天";
        }

        /******************添加收到的消息******************/
        public void AddMsg(string chatMsg)
        {
            Chatbox.SelectionAlignment = HorizontalAlignment.Left;
            Font font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            Chatbox.SelectionFont = font;
            Chatbox.SelectionColor = Color.Blue;
            string msg = System.DateTime.Now.ToLocalTime().ToString();
            msg = msg + "\n" + ">>> " + chatMsg + "\n";
            Chatbox.AppendText(msg);
            Chatbox.ScrollToCaret();
        }

        /******************发送消息******************/
        private void Sendbutton_Click(object sender, EventArgs e)
        {
            if (Sendchatbox.Text == "")
            {
                MessageBox.Show("请先填写消息");
            }
            else
            {
                try
                {
                    //向服务器确认好友是否在线，得到IP
                    NetworkStream stream = toServer.GetStream();
                    string sendMsg = "q" + friendID;
                    byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
                    stream.Write(sendByt, 0, sendByt.Length);
                    byte[] rcvByt = new byte[1024];
                    int rcvLength = stream.Read(rcvByt, 0, rcvByt.Length);
                    string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
          
                    if (rcvMsg == "n")
                    {
                        MessageBox.Show("好友已离线");
                    }
                    else
                    {
                        //发送该消息
                        sendMsg = "msg" + userID + Sendchatbox.Text;
                        sendByt = Encoding.Unicode.GetBytes(sendMsg);
                        if (sendByt.Length > 1024)
                        {
                            MessageBox.Show("一次输入请不要超过500个字");
                            return;
                        }
                        else
                        {
                            TcpClient tcptoFriend = new TcpClient();
                            tcptoFriend.Connect(rcvMsg, listenPort);
                            NetworkStream streamtoFriend = tcptoFriend.GetStream();
                            streamtoFriend.Write(sendByt, 0, sendByt.Length);
                            streamtoFriend.Close();
                            tcptoFriend.Close();
                            //stream.Close();   会报错

                            //向chatbox添加该消息
                            Chatbox.SelectionAlignment = HorizontalAlignment.Right;
                            Font font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
                            Chatbox.SelectionFont = font;
                            Chatbox.SelectionColor = Color.Green;
                            string msg = System.DateTime.Now.ToLocalTime().ToString();
                            msg = msg + "\n" + ">>> " + Sendchatbox.Text + "\n";
                            Chatbox.AppendText(msg);
                            Chatbox.ScrollToCaret();

                            Sendchatbox.Clear();
                            Sendchatbox.Focus();
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //关闭窗口时删除mainwin的chatWinList中的这个窗口
        private void Chatwinclosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < parentwin.chatWinList.Count; i++)
            {
                if ((parentwin.chatWinList[i] as ChatWin).friendID == friendID)
                {
                    parentwin.chatWinList.RemoveAt(i);
                    parentwin.Show();
                }
            }
        }

        /*************回车响应发送************/
        private void Sendchatbox_keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Sendbutton_Click(null, null);
            }
        }
    }
}
