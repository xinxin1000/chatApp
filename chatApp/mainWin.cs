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
using System.Collections;
using System.Threading;

//统一C-S间的编码asiic，C-C编码unicode

namespace chatApp
{
    public partial class MainWin : Form
    {
        string userID;
        TcpClient toServer;
        NetworkStream toServerStream;
        int listenPort=9876;       
        public ArrayList chatWinList = new ArrayList();
        public ArrayList fileSendingList = new ArrayList();
        //public ArrayList fileRcvList = new ArrayList();
        Thread listenThread;
        TcpListener chatListener;
        bool listenstart;
        public LoginWin parentform;
        

        //构造函数
        public MainWin(string userID, TcpClient toServer)
        {
            
            this.userID = userID;
            this.toServer = toServer;
            this.toServerStream = toServer.GetStream();
            IPAddress myIP = IPAddress.Any;
            this.chatListener = new TcpListener(myIP, listenPort);
            this.chatListener.Start();
            this.listenThread = new Thread(Listening);
            this.listenThread.Start();
            this.listenstart = true;
            InitializeComponent();
            this.Text = userID + "的主界面";



            /*****************载入通讯录、填入好友状态列表、向好友发送上线通知************/
            StreamWriter streamWriter = new StreamWriter("commuTable\\" + userID + ".txt", true);
            streamWriter.Close();
            StreamReader readerFromLocal = new StreamReader("commuTable\\" + userID + ".txt");
            string line = readerFromLocal.ReadLine();

            while (line!= null)
            {
                if (line != "") //读到一行末尾
                {
                    //向服务器查看好友在线状态
                    string sendMsg = "q" + line;
                    byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
                    toServerStream.Write(sendByt, 0, sendByt.Length);
                    byte[] rcvByt = new byte[1024];
                    int rcvLength = toServerStream.Read(rcvByt, 0, rcvByt.Length);
                    string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
                    if (rcvMsg == "n")
                    {
                        this.friendListBox.Items.Add(line + " 离线");
                    }
                    
                    else
                    {
                        this.friendListBox.Items.Add(line + " 在线");
                        
                        //向在线好友发送上线通知
                        TcpClient toFriend = new TcpClient();
                        toFriend.Connect(rcvMsg, listenPort);
                        NetworkStream toFriendStream = toFriend.GetStream();
                        sendMsg = "ili" + userID;   //命令：ili = i login
                        sendByt = Encoding.Unicode.GetBytes(sendMsg);
                        toFriendStream.Write(sendByt, 0, sendByt.Length);
                        toFriendStream.Close();
                        toFriend.Close();
                    }
                } //endif
                line = readerFromLocal.ReadLine();
            }
            //toServerStream.Close();
            readerFromLocal.Close();
        }

        /*****************聊天*********************/
        private void chatButton_Click(object sender, EventArgs e)
        {
            if (friendListBox.SelectedItem == null)
            {
                MessageBox.Show("请先选择一个好友");
            }
            else if((friendListBox.SelectedItem as string).Contains("离线"))
            {
                MessageBox.Show("请选择一个在线好友");
            }
            else
            {
                string friendID = (friendListBox.SelectedItem as string).Substring(0, 10);
                try
                {
                    //如果已建立过对话框，显示该对话框
                    for(int i = 0; i < chatWinList.Count; i++)
                    {
                        if((chatWinList[i] as ChatWin).friendID == friendID) //聊天窗口类
                        {
                            (chatWinList[i] as ChatWin).Show();
                            (chatWinList[i] as ChatWin).Activate();
                            return;
                        }
                    }
                    //否则建立新的聊天对话框
                    ChatWin newChatWin = new ChatWin(userID, friendID, listenPort, toServer);
                    chatWinList.Add(newChatWin);
                    newChatWin.parentwin = this;
                    newChatWin.Show();
                    newChatWin.Activate();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /******************发送文件****************/
        private void sendFileButton_Click(object sender, EventArgs e)
        {
            if (friendListBox.SelectedItem == null)
            {
                MessageBox.Show("请先选择一个好友");
            }
            else if ((friendListBox.SelectedItem as string).Contains("离线"))
            {
                MessageBox.Show("请选择一个在线好友"); 
            }
            else
            {
                string friendID = (friendListBox.SelectedItem as string).Substring(0, 10);
                try
                {
                    //如果已建立过发送文件窗口，显示该对话框
                    for (int i = 0; i < fileSendingList.Count; i++)
                    {
                        if ((fileSendingList[i] as FileSendingWin).friendID == friendID)    //文件发送窗口类
                        {
                            (fileSendingList[i] as FileSendingWin).Show();
                            (fileSendingList[i] as FileSendingWin).Activate();
                            return;
                        }
                    }
                    // 否则 建立新的发送文件窗口
                    FileSendingWin newFileSendingWin = new FileSendingWin(userID, friendID, listenPort, toServer);
                    fileSendingList.Add(newFileSendingWin);
                    newFileSendingWin.parentwin = this;
                    newFileSendingWin.Show();
                    newFileSendingWin.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /**************添加新朋友*****************/
        private void Addfrdbtn_Click(object sender, EventArgs e)
        {
            string friendID = FrdIDbox.Text;
            //先判断输入是否为有效ID
            bool validfriendID = true;
            if (friendID.Length == 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    if ((friendID[i]-'0')<0 ||(friendID[i]-'0'>9))
                    {
                        validfriendID = false;
                        break;
                    }
                }
            }
            else
                validfriendID = false;
            if (!validfriendID)   //若ID无效
            {
                MessageBox.Show("请输入有效的ID（10位数字）");
                FrdIDbox.Clear();  //清除
                return;
            }
            //检查friendlistbox是否已添加该好友
            for (int i = 0; i < friendListBox.Items.Count; i++)
            {
                if ((friendListBox.Items[i] as string).Contains(friendID))
                {
                    MessageBox.Show(friendID + "已经是你的好友");
                    return;
                }
            }
            if (friendID == userID)
            {
                MessageBox.Show("请不要添加自己");
                return;
            }
            //否则，先向服务器查询对方状态，获得IP
            string sendMsg = "q" + friendID;
            byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
            //NetworkStream toServerStream = toServer.GetStream();
            toServerStream.Write(sendByt, 0, sendByt.Length);
            byte[] rcvByt = new byte[1024];
            int rcvLength = toServerStream.Read(rcvByt, 0, rcvByt.Length);
            string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
            //toServerStream.Close();
            if (rcvMsg == "n")
            {
                MessageBox.Show(friendID + "已离线，请稍后添加");
            }
            else
            {
                //再向friendID发送好友请求rff
                TcpClient tcptoFriend = new TcpClient();
                tcptoFriend.Connect(rcvMsg, listenPort);
                NetworkStream streamtoFriend = tcptoFriend.GetStream();
                sendMsg = "rff" + userID;
                sendByt = Encoding.Unicode.GetBytes(sendMsg);
                streamtoFriend.Write(sendByt, 0, sendByt.Length);
                streamtoFriend.Close();
                tcptoFriend.Close();
                MessageBox.Show("已向" + friendID + "发送好友申请");
            }
        }

        /**************搜索已添加的好友**************/
        private void Searchfrdbtn_Click(object sender, EventArgs e)
        {
            string friendID = FrdIDbox.Text;
            //先判断输入是否为有效ID
            bool validfriendID = true;
            if (friendID.Length == 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    if ((friendID[i] - '0') < 0 || (friendID[i] - '0' > 9))
                    {
                        validfriendID = false;
                        break;
                    }
                }
            }
            else
                validfriendID = false;
            if (!validfriendID) //若ID无效
            {
                MessageBox.Show("请输入有效的ID（10位数字）");
                FrdIDbox.Clear();  //清除
                return;
            }
            //在friendlistbox查找这个ID，并选中
            for (int i = 0; i < friendListBox.Items.Count; i++)
            {
                if ((friendListBox.Items[i] as string).Contains(friendID))
                {
                    friendListBox.SelectedIndex = i;
                    friendListBox.TopIndex = i;
                    return;
                }
            }
            if (friendID == userID)
            {
                MessageBox.Show("请不要搜索自己");
                return;
            }
            //若没有查找到这个ID
            MessageBox.Show(friendID + "还不是你的好友");

        }

        /*************下线***********************/
        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("点击确定将下线,同时关闭本窗口", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //向在线好友发送下线通知
                for (int i = 0; i < friendListBox.Items.Count; i++)
                {
                    if ((friendListBox.Items[i] as string).Contains("在线"))
                    {
                        try
                        {
                            //从服务器获得好友ip
                            string sendstr = "q" + (friendListBox.Items[i] as string).Substring(0, 10);
                            byte[] send = Encoding.ASCII.GetBytes(sendstr);
                            toServerStream.Write(send, 0, send.Length);
                            byte[] rcv = new byte[1024];
                            int length = toServerStream.Read(rcv, 0, 1024);
                            string rcvstr = Encoding.ASCII.GetString(rcv, 0, length);
                            //以下向对方发送：同意加为好友awr
                            TcpClient tcp = new TcpClient();
                            tcp.Connect(rcvstr, listenPort);
                            NetworkStream sm = tcp.GetStream();
                            sendstr = "ilo" + userID;
                            send = Encoding.Unicode.GetBytes(sendstr);
                            sm.Write(send, 0, send.Length);
                            sm.Close();
                            tcp.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                try
                {
                    //向服务器发送下线命令logout+userID
                    string sendMsg = "logout" + userID;
                    byte[] sendByt = Encoding.ASCII.GetBytes(sendMsg);
                    //NetworkStream toServerStream = toServer.GetStream();
                    toServerStream.Write(sendByt, 0, sendByt.Length);
                    byte[] rcvByt = new byte[1024];
                    int rcvLength = toServerStream.Read(rcvByt, 0, 1024);
                    string rcvMsg = Encoding.ASCII.GetString(rcvByt, 0, rcvLength);
                    //toServerStream.Close();
                    //toServer.Close();   //关闭与服务器的tcp

                    if (rcvMsg == "loo") //成功下线
                    {
                        //再关闭监听子线程
                        listenstart = false;
                        chatListener.Stop();   //关闭监听tcp
                        listenThread.Abort(); //退出监听子线程
                        parentform.Show();
                        

                    }
                    else
                    {
                        MessageBox.Show("下线失败，服务器提示：" + rcvMsg);
                    }
                    
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //关闭所有创建的窗口
                for(int i = 0; i < chatWinList.Count; i++)
                {
                    (chatWinList[i] as ChatWin).Close();
                }
                for (int i = 0; i < fileSendingList.Count; i++)
                {
                    (fileSendingList[i] as FileSendingWin).Close();
                }
                /*
                for (int i = 0; i < fileRcvList.Count; i++)
                {
                    (fileRcvList[i] as fileRcvingWin).Close();
                }*/
                this.Dispose(); //关闭本窗口
			}
        }

        /********************监听*****************/
        private void Listening()
        {
            while (listenstart)
            {
                if (!chatListener.Pending()) continue;  //为避免被AcceptTcpClient阻塞，没有连接请求时continue

                TcpClient tcpFromFriend = chatListener.AcceptTcpClient();
                string friendIP = tcpFromFriend.Client.RemoteEndPoint.ToString().Split(':')[0];   //获取对方IP
                NetworkStream stream = tcpFromFriend.GetStream();
                byte[] rcvByt = new byte[1024];
                int rcvlength = stream.Read(rcvByt, 0, 1024);
                string rcvMsg = Encoding.Unicode.GetString(rcvByt, 0,rcvlength);
                //rcvMsg = protocol(3位) + friendID(10位) + ...
                stream.Close();     //关闭流和tcp
                tcpFromFriend.Close();
                string protocol = rcvMsg.Substring(0, 3);
                string friendID = rcvMsg.Substring(3, 10);
                this.Invoke(new Action(() => 
                {
                    /*************收到对方传来的消息,msg=message ***********/
                    if (protocol == "msg")
                    {
                        string chatMsg = rcvMsg.Substring(13);
                        bool firstChat = true;
                        for(int i = 0; i < chatWinList.Count; i++) //已有聊天窗口
                        {
                            if((chatWinList[i] as ChatWin).friendID == friendID)
                            {
                                (chatWinList[i] as ChatWin).Show();
                                (chatWinList[i] as ChatWin).Activate();
                                (chatWinList[i] as ChatWin).AddMsg(chatMsg);
                                firstChat = false;
                                break;
                            }
                        }
                        if (firstChat)  //新建聊天窗口
                        {
                            ChatWin chatwin = new ChatWin(userID, friendID, listenPort, toServer);
                            chatwin.parentwin = this;
                            chatwin.Show();
                            chatwin.AddMsg(chatMsg);
                            chatWinList.Add(chatwin);
                        }
                    }

                    /******收到对方发送文件请求，fsr=filesendrequest****/
                    //rcvdMsg = "fsr" + userID + safefilename + "***" + filelength;
                    else if (protocol == "fsr")
                    {
                        int findposition = rcvMsg.IndexOf("***");
                        string safefilename = rcvMsg.Substring(13, findposition - 13);
                        string filelength = rcvMsg.Substring(findposition + 3);
                        //bool firstrcv = true;
                        /*
                        foreach(object o in fileRcvList)    //已有文件接收窗口
                        {
                            if((o as fileRcvingWin).friendID == friendID)
                            {
                                (o as fileRcvingWin).filenamenopath = safefilename;
                                (o as fileRcvingWin).filelength = filelength;
                                (o as fileRcvingWin).Show();
                                firstrcv = false;
                                break;
                            }
                        }
                        if (firstrcv)   //新建文件接收窗口*/
                        {
                            fileRcvingWin newrcvwin = new fileRcvingWin(userID, friendID, toServer, listenPort, safefilename, filelength);
                            newrcvwin.parentwin = this;
                            newrcvwin.Show();
                            //fileRcvList.Add(newrcvwin);
                        }
                        //MessageBox.Show("来自" + friendID + "的传送文件请求");
                    }
                    /******对方同意接收文件，fra=filereceiveagreeded ****/
                    //此时已有发送文件窗口打开，找到它，发送  rcvMsg = "fra" + userID;
                    else if (protocol == "fra")
                    {
                        foreach (object o in fileSendingList)    
                        {
                            if ((o as FileSendingWin).friendID == friendID)
                            {
                                (o as FileSendingWin).Show();
                                (o as FileSendingWin).Sendthreadstart();
                                break;
                            }
                        }
                    }
                    /********对方拒绝接收文件，frd = filereceivedisagreeded ****/
                    //此时已有发送文件窗口打开,找到它，显示  rcvMsg = "frd" + userID;
                    else if (protocol == "frd")
                    {
                        foreach (object o in fileSendingList)
                        {
                            if ((o as FileSendingWin).friendID == friendID)
                            {
                                (o as FileSendingWin).Show();
                                (o as FileSendingWin).Senddenial();
                                break;
                            }
                        }
                        
                    }

                    /*******收到对方加好友请求，rff = requestforfriend ****/
                    else if (protocol == "rff")
                    {
                        //检查friendlistbox是否已添加该好友，防止发送多次好友请求
                        bool isfriend = false;
                        for (int i = 0; i < friendListBox.Items.Count; i++)
                        {
                            if ((friendListBox.Items[i] as string).Contains(friendID))
                            {
                                isfriend = true;
                                try
                                {
                                    //以下向对方发送：同意加为好友awr
                                    TcpClient tcptoFriend = new TcpClient();
                                    tcptoFriend.Connect(friendIP, listenPort);
                                    NetworkStream streamtoFriend = tcptoFriend.GetStream();
                                    string sendMsg = "awr" + userID;
                                    byte[] sendByt = Encoding.Unicode.GetBytes(sendMsg);
                                    streamtoFriend.Write(sendByt, 0, sendByt.Length);
                                    streamtoFriend.Close();
                                    tcptoFriend.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                break;
                            }
                        }
                        if (!isfriend)  //如果不是好友
                        {
                            string msbx = friendID + "请求加为好友,是否同意";
                            if (MessageBox.Show(msbx, "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {   //同意加为好友
                                try
                                {
                                    //以下向对方发送：同意加为好友awr
                                    TcpClient tcptoFriend = new TcpClient();
                                    tcptoFriend.Connect(friendIP, listenPort);
                                    NetworkStream streamtoFriend = tcptoFriend.GetStream();
                                    string sendMsg = "awr" + userID;
                                    byte[] sendByt = Encoding.Unicode.GetBytes(sendMsg);
                                    streamtoFriend.Write(sendByt, 0, sendByt.Length);
                                    streamtoFriend.Close();
                                    tcptoFriend.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                //将对方加入通讯录，加入friendListBox
                                this.Invoke(new Action(() =>
                                {
                                    friendListBox.Items.Add(friendID + " 在线");
                                    friendListBox.SelectedIndex = friendListBox.Items.Count - 1;  //选中
                                }));
                                StreamWriter localsw = new StreamWriter("commuTable\\" + userID + ".txt", true);
                                localsw.WriteLine(friendID);
                                localsw.Close();
                            }
                            else  //拒绝加为好友
                            {
                                try
                                {
                                    //以下向好友发送：拒绝加为好友dwr
                                    TcpClient tcptoFriend = new TcpClient();
                                    tcptoFriend.Connect(friendIP, listenPort);
                                    NetworkStream streamtoFriend = tcptoFriend.GetStream();
                                    string sendMsg = "dwr" + userID;
                                    byte[] sendByt = Encoding.Unicode.GetBytes(sendMsg);
                                    streamtoFriend.Write(sendByt, 0, sendByt.Length);
                                    streamtoFriend.Close();
                                    tcptoFriend.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
                    /*******收到对方同意加好友，awr = agreewithrequest ****/
                    //rcvMsg = "awr" + userID;
                    else if (protocol == "awr")
                    {
                        //检查friendlistbox是否已添加该好友
                        bool isfriend = false;
                        for (int i = 0; i < friendListBox.Items.Count; i++)
                        {
                            if ((friendListBox.Items[i] as string).Contains(friendID))
                            {
                                isfriend = true;
                                break;
                            }
                        }
                        if (!isfriend)
                        {
                            //将对方加入通讯录，加入friendListBox
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show(friendID + "同意了你的好友请求，可以开始聊天");
                                friendListBox.Items.Add(friendID + " 在线");
                                friendListBox.SelectedIndex = friendListBox.Items.Count - 1;   //选中
                            }));
                            StreamWriter localsw = new StreamWriter("commuTable\\" + userID + ".txt", true); //true在文末追加
                            localsw.WriteLine(friendID);
                            localsw.Close();
                        }
                    }
                    /*******收到对方拒绝加好友，dwr = disagreewithrequest ****/
                    //rcvMsg = "dwr" + userID
                    else if (protocol == "dwr")
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show(friendID + "拒绝了你的好友请求");
                        }));
                    }

                    /*******收到对方上线通知，ili= i-login ****/
                    //rcvMsg = "ili" + userID
                    else if (protocol == "ili")
                    {
                        for(int i = 0; i < friendListBox.Items.Count; i++)
                        {
                            if((friendListBox.Items[i] as string).Contains(friendID))
                            {
                                this.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("好友 "+friendID + "上线");
                                    friendListBox.Items[i] = friendID + " 在线";
                                }));
                                break;
                            }
                        }
                    }
                    /*******收到对方下线通知，ilo= i-logout ****/
                    //rcvMsg = "ilo" + userID
                    else if (protocol == "ilo")
                    {
                        for (int i = 0; i < friendListBox.Items.Count; i++)
                        {
                            if ((friendListBox.Items[i] as string).Contains(friendID))
                            {
                                this.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("好友 " + friendID + "下线");
                                    friendListBox.Items[i] = friendID + " 离线";
                                }));
                                break;
                            }
                        }
                    }
                }));  
            }
        }

        /*************用户关闭窗口时***************/
        private void MainformClosing(object sender, FormClosingEventArgs e)
        {
            Logoutbtn_Click(null, null);
        }
    }
}
