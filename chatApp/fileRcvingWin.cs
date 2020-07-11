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
    public partial class fileRcvingWin : Form
    {
        string userID;
        public string friendID;
        TcpClient toServer;
        int listenPort;
        TcpListener tcpfilelistener;
        bool rcving = false;    //正在接收文件
        Thread rcvfilethread;
        //从外部改变文本框中的text
        public MainWin parentwin = null;
        public string filenamenopath
        {
            get { return filenamebox.Text; }
            set { filenamebox.Text = value; }
        }
        public string filelength
        {
            get { return filelengthbox.Text; }
            set { filelengthbox.Text = value; }
        }
        
        /***********构造函数***************/
        public fileRcvingWin(string userID, string friendID,TcpClient toServer, int listenPort,
            string filenamenopath,string filelength)
        {
            InitializeComponent();
            this.userID = userID;
            this.friendID = friendID;
            this.toServer = toServer;
            this.listenPort = listenPort;
            this.rcvfilethread = new Thread(Rcvfile);

            this.Text = "从" + friendID + "接收文件的窗口";
            this.filenamenopath = filenamenopath;
            this.filelength = filelength;
            this.sendernamebox.Text = friendID; 
        }

        /************同意接收文件**********/
        //发送fra
        private void Rcvbtn_Click(object sender, EventArgs e)
        {
            if (rcving)
            {
                MessageBox.Show("文件已在接受中");
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
                    if (rcvMsg == "n")
                    {
                        MessageBox.Show("好友已离线，无法接收文件");
                    }
                    else
                    {
                        //打开保存文件对话框
                        saveFileDialog1.FileName = filenamenopath;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            //建立并开启tcpfilelistener
                            IPAddress myIP = IPAddress.Any;
                            tcpfilelistener = new TcpListener(myIP, listenPort + 1);
                            tcpfilelistener.Start();

                            //开启文件接收线程
                            rcvfilethread.Start();     
                            rcving = true;

                            //向好友发送：同意接收文件fra
                            TcpClient tcptoFriend = new TcpClient();
                            tcptoFriend.Connect(rcvMsg, listenPort);
                            NetworkStream streamtoFriend = tcptoFriend.GetStream();
                            sendMsg = "fra" + userID;
                            sendByt = Encoding.Unicode.GetBytes(sendMsg);
                            streamtoFriend.Write(sendByt, 0, sendByt.Length);
                            //streamtoFriend.Close();
                            tcptoFriend.Close();
                        }
                        else
                        {
                            MessageBox.Show("请选择保存文件的路径");
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /************拒绝接收文件**********/
        //发送frd
        private void Denybtn_Click(object sender, EventArgs e)
        {
            if (rcving)
            {
                MessageBox.Show("接收已经开始，无法拒绝");
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
                    if (rcvMsg == "n")
                    {
                        MessageBox.Show("好友已离线，无需拒绝接收文件");
                    }
                    else
                    {
                        //以下向好友发送：拒绝接收文件frd
                        TcpClient tcptoFriend = new TcpClient();
                        tcptoFriend.Connect(rcvMsg, listenPort);
                        NetworkStream streamtoFriend = tcptoFriend.GetStream();
                        sendMsg = "frd" + userID;
                        sendByt = Encoding.Unicode.GetBytes(sendMsg);
                        streamtoFriend.Write(sendByt, 0, sendByt.Length);
                        //streamtoFriend.Close();
                        tcptoFriend.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                /*
                //窗口关闭时删除mainwin的fileRcvlist中的这个窗口
                for (int i = 0; i < parentwin.fileRcvList.Count; i++)
                {
                    if ((parentwin.fileRcvList[i] as fileRcvingWin).friendID == friendID)
                    {
                        parentwin.fileRcvList.RemoveAt(i);
                    }
                }*/
                this.Invoke(new Action(() => { this.Dispose(); }));
                
                //清空文件名和大小
                //filenamebox.Clear();
                //filelengthbox.Clear();
            }
        }

        /***********接收文件线程**************/
        private void Rcvfile()
        {
            try
            {
                TcpClient tcpfileclient = tcpfilelistener.AcceptTcpClient();
                NetworkStream streamfromfriend = tcpfileclient.GetStream(); //网络流
                //本地文件流
                FileStream localfilestream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] rcvfilebyt = new byte[1024];  //将文件分成1024字节的字节流接收
                //设置进度条
                int rcvprogress = 0;
                this.Invoke(new Action(() => {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = int.Parse(filelength)/1024 + 1;
                }));

                //循环接收文件
                int readlength;
                do
                {
                    readlength = streamfromfriend.Read(rcvfilebyt, 0, 1024);
                    localfilestream.Write(rcvfilebyt, 0, readlength);
                    Array.Clear(rcvfilebyt, 0, 1024);  //清空

                    rcvprogress += 1;
                    this.Invoke(new Action(() =>        //更新进度条
                    {
                        if (rcvprogress > progressBar1.Maximum)
                            progressBar1.Value = progressBar1.Maximum;
                        else
                            progressBar1.Value = rcvprogress;
                    }));
                    Thread.Sleep(50);  //注意这里
                } while (streamfromfriend.DataAvailable);
                

                localfilestream.Close();
                streamfromfriend.Close();
                tcpfileclient.Close();
                tcpfilelistener.Stop();

                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("文件接收成功");
                    filenamebox.Clear();
                    filelengthbox.Clear();
                    progressBar1.Value = 0;
                    rcving = false;
                }));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Invoke(new Action(()=> { progressBar1.Value = 0; }));
                rcving = false;
            }
            /*
            //窗口关闭时删除mainwin的fileRcvlist中的这个窗口
            for (int i = 0; i < parentwin.fileRcvList.Count; i++)
            {
                if ((parentwin.fileRcvList[i] as fileRcvingWin).friendID == friendID)
                {
                    parentwin.fileRcvList.RemoveAt(i);
                }
            }*/
            this.Invoke(new Action(() => { this.Dispose(); }));
        }

        
        private void FilercvwinClose(object sender, FormClosingEventArgs e)
        {
            if (rcving)
            {
                MessageBox.Show("文件正在接收，请勿关闭窗口");
                e.Cancel = true;
                return;
            }
            
        }
    }
}
