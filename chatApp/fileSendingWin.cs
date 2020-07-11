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
using System.Threading;

namespace chatApp
{
    public partial class FileSendingWin : Form
    {
        public string friendID;
        string friendIP;
        string userID;
        TcpClient toServer;
        int listenPort;
        bool sending;   //正在发送
        //bool selected;    //已选择文件
        string safefilename;
        string filelength;
        FileInfo fileinfo;
        Thread sendfilethread;

        public MainWin parentwin = null;

        EventWaitHandle ready = new AutoResetEvent(false);
        EventWaitHandle go = new AutoResetEvent(false);

        

        /***********构造函数*************/
        public FileSendingWin(string userID,string friendID,int listenPort, TcpClient toServer)
        {
            InitializeComponent();
            this.userID = userID;
            this.friendID = friendID;
            this.toServer = toServer;
            this.listenPort = listenPort;
            sending = false;
            //selected = false;
            sendfilethread = new Thread(Sendfile);
            sendfilethread.Start();
            this.Text = "向" + friendID + "发送文件的窗口";
            
        }

        /************选择文件************/
        private void Selectfilebtn_Click(object sender, EventArgs e)
        {
            if (sending)
            {
                MessageBox.Show("正在发送文件，请稍后选择下次发送的文件");
            }
            else
            {
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    safefilename = openFileDialog1.SafeFileName;    //文件名和扩展名
                    fileinfo = new FileInfo(openFileDialog1.FileName);
                    filelength = fileinfo.Length.ToString();
                    this.Invoke(new Action(()=>
                    {
                        filenamebox.Text = safefilename;
                        filelengthbox.Text = filelength + "B";
                    }));
                    //selected = true;
                }
            }
        }

        /*********发送文件申请 fsr*****************/
        private void Startsendbtn_Click(object sender, EventArgs e)
        {
            if (sending)
            {
                MessageBox.Show("文件正在发送");
            }
            else if (filenamebox.Text=="")
            {
                MessageBox.Show("请先选择文件");
            }
            else
            {
                try
                {
                    //以下向服务器查询好友状态，获得IP
                    NetworkStream stream = toServer.GetStream();
                    string sendMsg = "q" + friendID;
                    byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
                    stream.Write(sendByt, 0, sendByt.Length);
                    byte[] rcvByt = new byte[1024];
                    int rcvLength = stream.Read(rcvByt, 0, rcvByt.Length);
                    string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
                    //stream.Close();
                    friendIP = rcvMsg;      //记录对方IP
                    if (rcvMsg == "n")
                    {
                        MessageBox.Show("好友已离线");
                        this.Invoke(new Action(()=> {
                            filenamebox.Clear();
                            filelengthbox.Clear();
                            //selected = false;
                        }));
                    }
                    else
                    {
                        //以下向好友发送：文件发送请求fsr
                        //sending = true;
                        TcpClient tcptoFriend = new TcpClient();
                        tcptoFriend.Connect(rcvMsg, listenPort);
                        NetworkStream streamtoFriend = tcptoFriend.GetStream();
                        sendMsg = "fsr" + userID + safefilename + "***" + filelength;
                        sendByt = Encoding.Unicode.GetBytes(sendMsg);
                        streamtoFriend.Write(sendByt, 0, sendByt.Length);
                        //streamtoFriend.Close();
                        tcptoFriend.Close();
                        MessageBox.Show("正在等待对方同意接收文件");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*********对方拒绝接收文件 对外接口*********/
        //由Listening线程收到frd后 调用
        public void Senddenial()
        {
            this.Invoke(new Action(() => {
                MessageBox.Show("对方拒接接收文件"+safefilename);
            }));
            sending = false;
        }

        /*********对方同意接收文件 开始发送文件线程的对外接口********/
        //由Listening线程收到fra后 调用
        public void Sendthreadstart()
        {
            ready.WaitOne();
            go.Set();
        }

        /***********发送文件线程****************/
        private void Sendfile()
        {
            while (true)
            {
                ready.Set();
                go.WaitOne();
                try
                {
                    //循环发送文件
                    TcpClient tcptoFriend = new TcpClient();
                    tcptoFriend.Connect(friendIP, listenPort + 1); //9877端口收发文件
                    NetworkStream streamtoFriend = tcptoFriend.GetStream();     //网络流
                                                                                //本地文件流                                                            
                    FileStream localfilestream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    byte[] sendfilebyt = new byte[1024]; //将文件分成1024字节的字节流发送
                                                         //设置进度条                                     
                    int sendprogress = 0;
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Value = 0;
                        progressBar1.Maximum = (int)(fileinfo.Length / 1024) + 1;
                    }));

                    int readlength;
                    while ((readlength = localfilestream.Read(sendfilebyt, 0, 1024)) > 0)
                    {
                        streamtoFriend.Write(sendfilebyt, 0, readlength);
                        streamtoFriend.Flush(); //刷新
                        Array.Clear(sendfilebyt, 0, 1024);  //清空
                        sendprogress += 1;
                        this.Invoke(new Action(() =>        //更新进度条
                        {
                            if (sendprogress > progressBar1.Maximum)
                                progressBar1.Value = progressBar1.Maximum;
                            else
                                progressBar1.Value = sendprogress;
                        }));
                    }
                    localfilestream.Close();    //关闭本地文件流
                    streamtoFriend.Close();     //关闭网络流和tcp连接
                    tcptoFriend.Close();

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("文件发送成功");
                        //filenamebox.Clear();
                        //filelengthbox.Clear();
                        //selected = false;
                        sending = false;
                        progressBar1.Value = 0;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    sending = false;
                    progressBar1.Value = 0;
                }
            }   
        }

        //窗口关闭时删除mainwin的filesendinglist中的这个窗口
        private void FileSendingWinClosing(object sender, FormClosingEventArgs e)
        {
            if (sending)
            {
                MessageBox.Show("文件正在发送，请勿关闭窗口");
                e.Cancel = true;
                return;
            }
            for(int i = 0; i < parentwin.fileSendingList.Count; i++)
            {
                if((parentwin.fileSendingList[i] as FileSendingWin).friendID == friendID)
                {
                    parentwin.fileSendingList.RemoveAt(i);
                    parentwin.Show();
                }
            }
            sendfilethread.Abort();
        }
    }
}
