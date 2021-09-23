using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace webSniff
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }
        //public Close()
        //{
        //    InitializeComponent();
        //    this.Load += MainForm_Load;
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm_Load");
           // MessageBox.Show("MainForm_Load a");
        }

        //private new void FormClosing(object sender, EventArgs e)
        //{
        //    Console.WriteLine("FormClosing");
        //    MessageBox.Show("FormClosing a");
        //}

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("MainForm_FormClosed");
           // MessageBox.Show("MainForm_FormClosed a");
        }

        private void tryIP_Click(object sender, EventArgs e)
        {
           // Console.WriteLine("tryIP_Click");

            if (pingIP(textBox4IP.Text))
            {
                richTextBox4return.Text += "\n" + textBox4IP.Text + "网络可达";
            }
            else {
                richTextBox4return.Text += "\n" + textBox4IP.Text + "网络不可达";
            }
        }
        List<string> pingContIPList = new List<string>();
        //DateTime sniffIP_Click_Time_B4 = DateTime.Now.AddSeconds(-60);
        //DateTime Async_SniffIP_Time = DateTime.Now;
        int sniffIP_Click_times = 0;
        Thread asynPingJobTh = null;
        private void sniffIP_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime sniffIP_Click_Time = DateTime.Now;
                //if(sniffIP_Click_Time_B4.AddSeconds(5)>sniffIP_Click_Time){
                //    richTextBox4return.Text += "\n" +"检测同网段IP时间间隔不得小于5秒钟！！！";
                //    return;
                //}else
                //{
                //    sniffIP_Click_Time_B4 = sniffIP_Click_Time;
                //}



                if (connectIPMap.Count == 0)
                {
                    richTextBox4return.Text += "\n" + "已经预热完毕，请再次点击！多次点击更加精确！但点击不要过于频繁！";
                }
                else
                {
                    richTextBox4return.Text = "第" + sniffIP_Click_times + "次搜索";
                    richTextBox4return.Text += "\n" + "共有" + connectIPMap.Count + "个可达的IP";
                }
                int k = 0;
                foreach (var item in connectIPMap.ToList())
                {
                    richTextBox4return.Text += "\n" + ++k + "：" + item.Key + "  平均耗时：" + connectIPMap[item.Key][1] / connectIPMap[item.Key][0] + " ms" + " 连接次数：" + connectIPMap[item.Key][0];
                    //Debug.Log(item.Key + "," + item.Value);
                    //pings[item.Key].SendAsyncCancel();
                    //pings[item.Key].Dispose();
                    //pings.Remove(item.Key);

                }
                //if (sniffIP_Click_Time_B4.AddSeconds(3) > sniffIP_Click_Time)
                //{
                //    richTextBox4return.Text = "检测同网段IP时间间隔不得小于3秒钟！！！" + "\n" + richTextBox4return.Text;
                //    return;
                //}
                //else
                //{

                //}
                if (asynPingJobTh != null)
                {
                    if (!asynPingJobTh.IsAlive)
                    {
                        Thread.Sleep(200);
                        foreach (var item in asynPings.ToList())
                        {
                            //Debug.Log(item.Key + "," + item.Value);
                            asynPings[item.Key].SendAsyncCancel();
                            asynPings[item.Key].Dispose();
                            asynPings.Remove(item.Key);
                        }
                        asynPingJobTh = null;
                    }
                    else if (asynPingJobTh == null) { }
                    else { return; }
                }
                //foreach (KeyValuePair<string, List<int>> kvp in connectIPMap)
                //{
                //    richTextBox4return.Text += "\n" + ++k +"：" + kvp.Key + "  平均耗时：" + kvp.Value[1] / kvp.Value[0]+" ms" + " 连接次数：" + kvp.Value[0];
                //}
                // richTextBox4return.Text += "\n" + enableIPList[0].Substring(0,enableIPList[0].LastIndexOf(".")+1);
                //异步
                string func = "异步";
                if (func == "异步")
                {
                    //richTextBox4return.Text += "\n" + "8秒内给出结果，请稍等待";
                    if (asynPings.Count == 0)
                    {
                        //sniffIP_Click_Time_B4 = sniffIP_Click_Time;
                        //if (asynPingJobTh != null && asynPingJobTh.IsAlive) return;
                        sniffIP_Click_times++;
                        asynPingJobTh = new Thread(asynPingJob);
                        asynPingJobTh.IsBackground = true;
                        asynPingJobTh.Start(getAllEnableIP());
                        //asynPingAllJob(getAllEnableIP());
                    }
                    // asynPingJob(getAllEnableIP());

                    //运行时间小于是秒或者检查还未开始,且运行时间小于15秒
                    //while (!(Async_SniffIP_Time.AddSeconds(2) > DateTime.Now && sniffIP_Click_Time.AddSeconds(10) >DateTime.Now))
                    //{
                    //    //首次运行或者超过4秒没运行到检查
                    //    if (Async_SniffIP_Time < sniffIP_Click_Time)
                    //    {

                    //        asynPingJob(getAllEnableIP());
                    //        Thread.Sleep(4000);
                    //    }
                    //    else if (Async_SniffIP_Time.AddMilliseconds(100) < DateTime.Now)
                    //    {
                    //        if (sniffIP_Click_Time.AddSeconds(5) < Async_SniffIP_Time)
                    //        {
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            asynPingJob(getAllEnableIP());
                    //            Thread.Sleep(2000);
                    //        }
                    //    }
                    //    else 
                    //    {
                    //        Thread.Sleep(500);
                    //    }

                    //}

                }
                else
                {
                    //同步
                    synPingJob(getAllEnableIP());
                }
                //while (connectIPMap.Count == 0 || Async_SniffIP_Time.AddMilliseconds(100) < DateTime.Now) 
                //{
                //    Thread.Sleep(450);
                //}
            }
            catch 
            {
            }
        }
        
        //DateTime sniffAllIP_Click_Time_B4 = DateTime.Now.AddSeconds(-180);
        Object sniffAllIP_Clicklock = new object();
        Thread asynPingAllJobTh = null;
        private void sniffAllIP_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime sniffIP_Click_Time = DateTime.Now;



                GC.Collect();
                if (connectAllIPMap.Count == 0)
                {
                    richTextBox4return.Text += "\n" + "已经预热完毕，请再次点击！";
                }
                else
                {
                    richTextBox4return.Text = "还有" + pings.Count + "个未完成ping的连接";

                    richTextBox4return.Text += "\n" + "共有" + connectAllIPMap.Count + "个可达的IP";
                }
                int k = 0;
                foreach (var item in connectAllIPMap.ToList())
                {
                    richTextBox4return.Text += "\n" + ++k + "：" + item.Key + "  平均耗时：" + connectAllIPMap[item.Key][1] / connectAllIPMap[item.Key][0] + " ms" + " 连接次数：" + connectAllIPMap[item.Key][0];
                    //Debug.Log(item.Key + "," + item.Value);
                    //pings[item.Key].SendAsyncCancel();
                    //pings[item.Key].Dispose();
                    //pings.Remove(item.Key);

                }

                //foreach (KeyValuePair<string, List<int>> kvp in connectIPMap)
                //{
                //    richTextBox4return.Text += "\n" + ++k + "：" + kvp.Key + "  平均耗时：" + kvp.Value[1] / kvp.Value[0] + " ms" + " 连接次数：" + kvp.Value[0];
                //}
                // richTextBox4return.Text += "\n" + enableIPList[0].Substring(0,enableIPList[0].LastIndexOf(".")+1);
                //异步
                if (asynPingAllJobTh != null)
                {
                    if (!asynPingAllJobTh.IsAlive)
                    {
                        Thread.Sleep(300);
                        foreach (var item in pings.ToList())
                        {
                            //Debug.Log(item.Key + "," + item.Value);
                            pings[item.Key].SendAsyncCancel();
                            pings[item.Key].Dispose();
                            pings.Remove(item.Key);
                        }
                        asynPingAllJobTh = null;
                        richTextBox4return.Text = "请先清理内存（例如点击深度加速的小火箭）！！" + richTextBox4return.Text;
                    }
                    else if (asynPingAllJobTh == null) { }
                    else { return; }
                }
                //if (sniffAllIP_Click_Time_B4.AddSeconds(180) > sniffIP_Click_Time)
                //{
                //    richTextBox4return.Text = "检测私网IP时间间隔不得小于3分钟！请清理计算机内存！" + "\n" + richTextBox4return.Text;
                //    GC.Collect();
                //    return;
                //}
                //else
                //{

                //}
                //foreach (KeyValuePair<string, Ping> kvp in pings)
                //{
                //    kvp.Value.SendAsyncCancel();
                //    //pings[254].Dispose();


                //}
                //pings.Clear();
                string func = "异步";
                if (func == "异步")
                {
                    //richTextBox4return.Text += "\n" + "8秒内给出结果，请稍等待";
                    lock (sniffAllIP_Clicklock)
                    {
                        if (pings.Count == 0)
                        {
                            Dictionary<string, string> ipType = new Dictionary<string, string>();
                            ipType.Add("A类地址", "10.");
                            ipType.Add("B类地址", "172.");
                            ipType.Add("C类地址", "192.");
                            List<string> enableIPList = getAllEnableIP();
                            //string backHostIP = "";
                            List<string> used = new List<string>();
                            for (int i = 0; i < enableIPList.Count; i++)
                            {
                                string iIP = (enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".")));//.Substring(0, enableIPList[i].LastIndexOf(".") + 1);
                                iIP = iIP.Substring(0, iIP.LastIndexOf("."));

                                if (((iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals(ipType[comboBox4ip.Text])))
                                {
                                    //sniffAllIP_Click_Time_B4 = sniffIP_Click_Time;

                                    asynPingAllJobTh = new Thread(asynPingAllJob);
                                    asynPingAllJobTh.IsBackground = true;
                                    asynPingAllJobTh.Start(iIP);
                                    break;
                                }//
                                if (enableIPList.Count == i + 1)
                                {
                                    richTextBox4return.Text = "不存在" + comboBox4ip.Text + "的网卡\n";
                                }
                            }

                            //asynPingAllJob(getAllEnableIP());
                        }
                    }
                    //运行时间小于是秒或者检查还未开始,且运行时间小于15秒
                    //while (!(Async_SniffIP_Time.AddSeconds(2) > DateTime.Now && sniffIP_Click_Time.AddSeconds(10) >DateTime.Now))
                    //{
                    //    //首次运行或者超过4秒没运行到检查
                    //    if (Async_SniffIP_Time < sniffIP_Click_Time)
                    //    {

                    //        asynPingJob(getAllEnableIP());
                    //        Thread.Sleep(4000);
                    //    }
                    //    else if (Async_SniffIP_Time.AddMilliseconds(100) < DateTime.Now)
                    //    {
                    //        if (sniffIP_Click_Time.AddSeconds(5) < Async_SniffIP_Time)
                    //        {
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            asynPingJob(getAllEnableIP());
                    //            Thread.Sleep(2000);
                    //        }
                    //    }
                    //    else 
                    //    {
                    //        Thread.Sleep(500);
                    //    }

                    //}
                    //while(asynPingAllJobTh.IsAlive)
                    //{
                    //    Application.DoEvents();
                    //    Thread.Sleep(100);
                    //}

                }
                else
                {
                    //同步
                    synPingJob(getAllEnableIP());
                }
                //while (connectIPMap.Count == 0 || Async_SniffIP_Time.AddMilliseconds(100) < DateTime.Now) 
                //{
                //    Thread.Sleep(450);
                //}
            }
            catch 
            {
            
            }
        }
        
        private void tryTelnet_Click(object sender, EventArgs e)
        {
            if (testIPPortConnect(textBox4IP.Text, cvt_int(textBox4Port.Text))) {
                richTextBox4return.Text += "\n" + textBox4IP.Text + ":" + textBox4Port.Text + "端口畅通";
            }
            else
            {
                richTextBox4return.Text += "\n" + textBox4IP.Text + ":" + textBox4Port.Text + "端口不畅通";
            }

        }
        private void tryTracert_Click(object sender, EventArgs e)
        {
            doTryTracert(textBox4IP.Text);
        }
        private void getLocIP_Click(object sender, EventArgs e)
        {
            richTextBox4return.Text += "\n第一个网卡IPv4为" + GetLocalIP() + "\n" + GetLocalAllIP();
        }
        private void getNetCard_Click(object sender, EventArgs e)
        {
            richTextBox4return.Text += getNetworkCard();
        }
        public static int cvt_int(object num)
        {
            try { return (int)Convert.ToDouble(num); }
            catch { return 0; }
        }
        public  bool pingIP(string address)
        {
            int sleepTime = 0;
            pingOK = false;
            Thread th = new Thread(PingJob);
            th.IsBackground = true;
            th.Start(address);
            
            while (!pingOK && sleepTime < 500)//最多等待 n s，如果还没结果，判断本次连接失败
            {
                sleepTime += 10;
                Thread.Sleep(10);
                Application.DoEvents();
            }
            return pingOK;
        }
        static bool pingOK = false;
        void PingJob(object pingIP)
        {
            Ping ping = null;
            try
            {
                ping = new Ping();
               // ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);
                PingReply pingReply = ping.Send(pingIP.ToString(), 500);
                pingOK = pingReply.Status == IPStatus.Success;
            }
            catch
            {
                pingOK = false;
            }
            finally
            {
                if (ping != null) // 2.0 下ping 的一个bug，需要显示转型后释放
                {
                    IDisposable disposable = ping;
                    disposable.Dispose();
                    ping.Dispose();
                }
            }
        }

        bool synPingJob(List<string>  enableIPList)
        {
            Ping ping = null;
            pingOK = false;
            try
            {
                ping = new Ping();
                //PingReply pingReply = null;//ping.Send(pingIP, 100);
               // pingOK = pingReply.Status == IPStatus.Success;
             
                for (int i = 0; i < enableIPList.Count; i++)
                {
                    richTextBox4return.Text += "\n" + "开始搜索：" + enableIPList[i];
                    Thread.Sleep(100);
                    for (int j = 1; j < 254; j++)
                    {
                       
                         PingReply pingReply=ping.Send(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j, 100);
                        if (pingReply.Status == IPStatus.Success)
                        {
                           
                            pingContIPList.Add(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j);
                            richTextBox4return.Text += "\n" + "可连接的IP:" + enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j;
                        }
                    }
                    richTextBox4return.Text += "\n" + "第" + (i+1) + "次搜索结束";

                }
                // ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);
         
                return pingOK;
            }
            catch
            {
                return pingOK;
            }
            finally
            {
                if (ping != null) // 2.0 下ping 的一个bug，需要显示转型后释放
                {
                    
                    IDisposable disposable = ping;
                    disposable.Dispose();
                    ping.Dispose();
                }
            }
        }
        //bool asynPingJob(List<string> enableIPList)
        //{
        //    Ping ping = null;
        //    pingOK = false;
        //    try
        //    {
        //        //ping = new Ping();
        //        //PingReply pingReply = null;//ping.Send(pingIP, 100);
        //        // pingOK = pingReply.Status == IPStatus.Success;
        //        //ping.PingCompleted += PingSuccess;
        //        for (int i = 0; i < enableIPList.Count; i++)
        //        {
        //            //richTextBox4return.Text += "\n" + "开始搜索：" + enableIPList[i];
        //            //Thread.Sleep(100);
        //            for (int j = 1; j < 254; j++)
        //            {
        //                ping = new Ping();
        //                ping.PingCompleted += PingSuccess;
        //                ping.SendAsync(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j, 500, enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j);
        //                //PingReply pingReply = ping.Send(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j, 100);
        //                //if (pingReply.Status == IPStatus.Success)
        //                //{

        //                //    pingContIPList.Add(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j);
        //                //    richTextBox4return.Text += "\n" + "可连接的IP:" + enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j;
        //                //}
        //            }
        //            //richTextBox4return.Text += "\n" + "第" + (i + 1) + "次搜索结束";

        //        }
        //        // ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);

        //        return pingOK;
        //    }
        //    catch
        //    {
        //        return pingOK;
        //    }
        //    finally
        //    {
        //        if (ping != null) // 2.0 下ping 的一个bug，需要显示转型后释放
        //        {
        //            IDisposable disposable = ping;
        //            disposable.Dispose();
        //            ping.Dispose();
        //        }
        //    }
        //}
        Dictionary<string, Ping> asynPings = new Dictionary<string, Ping>();
        void asynPingJob(Object o)
        {
            List<string> enableIPList = (List<string>)o;
            //Ping ping = null;
            pingOK = false;
            try
            {
                //ping = new Ping();
                //PingReply pingReply = null;//ping.Send(pingIP, 100);
                // pingOK = pingReply.Status == IPStatus.Success;
                //ping.PingCompleted += PingSuccess;
                for (int i = 0; i < enableIPList.Count; i++)
                {
                    //richTextBox4return.Text += "\n" + "开始搜索：" + enableIPList[i];
                    //Thread.Sleep(100);
                    for (int j = 1; j < 254; j++)
                    {
                        string IPKey= enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j;
                        asynPings.Add( IPKey,new Ping());
                        asynPings[IPKey].PingCompleted += PingSuccess;
                        asynPings[IPKey].SendAsync(IPKey, 500, IPKey);
                        //PingReply pingReply = ping.Send(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j, 100);
                        //if (pingReply.Status == IPStatus.Success)
                        //{

                        //    pingContIPList.Add(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j);
                        //    richTextBox4return.Text += "\n" + "可连接的IP:" + enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j;
                        //}
                    }
                    //richTextBox4return.Text += "\n" + "第" + (i + 1) + "次搜索结束";

                }
                // ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);

                //return pingOK;
            }
            catch
            {
                //return pingOK;
            }
            finally
            {
               
            }
        }
        //WorkWait workWait = new WorkWait();
        //void asynPingAllJob(Object o)
        //{
        //    List<string> enableIPList =(List<string>)o;
        //    Ping ping = null;
        //    List<Ping> pings = new List<Ping>();
        //    pingOK = false;
        //    try
        //    {
        //        //ping = new Ping();
        //        //PingReply pingReply = null;//ping.Send(pingIP, 100);
        //        // pingOK = pingReply.Status == IPStatus.Success;
        //        //ping.PingCompleted += PingSuccess;
        //        int hh = 0;
        //        for (int i = 0; i < enableIPList.Count; i++)
        //        {
        //            string iIP = (enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".")));//.Substring(0, enableIPList[i].LastIndexOf(".") + 1);
        //            iIP = iIP.Substring(0, iIP.LastIndexOf("."));
        //            //iIP = iIP.Substring(0, iIP.LastIndexOf(".")+1);
        //            if (!(iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("10.")) continue;
        //            //richTextBox4return.Text += "\n" + "开始搜索：" + enableIPList[i];
        //            //Thread.Sleep(10);
        //            for (int k = 0; k < 254; k++)
        //            {
        //                string kIP = iIP + "." + k;
        //                for (int j = 1; j < 254; j++)
        //                {
        //                    workWait.maked();
        //                    workWait.wait();
        //                    hh++;
        //                    string jIP = kIP +"."+j;
        //                    ping = new Ping();
        //                    ping.PingCompleted += PingSuccess;
        //                    ping.SendAsync(jIP, 60, jIP);
        //                    //PingReply pingReply = ping.Send(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j, 100);
        //                    //if (pingReply.Status == IPStatus.Success)
        //                    //{

        //                    //    pingContIPList.Add(enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j);
        //                    //    richTextBox4return.Text += "\n" + "可连接的IP:" + enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".") + 1) + j;
        //                    //}
        //                }
        //            }
        //            //Thread.Sleep(100);
        //            //richTextBox4return.Text += "\n" + "第" + (i + 1) + "次搜索结束";

        //        }
        //        // ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);

        //        //return pingOK;
        //    }
        //    catch
        //    {
        //        //return pingOK;
        //    }
        //    finally
        //    {
        //        if (ping != null) // 2.0 下ping 的一个bug，需要显示转型后释放
        //        {
        //            IDisposable disposable = ping;
        //            disposable.Dispose();
        //            ping.Dispose();
        //        }
        //    }
        //}

        //Dictionary<string, WeakReference> pings = new Dictionary<string, WeakReference>();
        //void asynPingAllJob(Object o)
        //{
        //    List<string> enableIPList = (List<string>)o;
        //    string backHostIP = "";
        //        for (int i = 0; i < enableIPList.Count; i++)
        //        {
        //            string iIP = (enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".")));//.Substring(0, enableIPList[i].LastIndexOf(".") + 1);
        //            iIP = iIP.Substring(0, iIP.LastIndexOf("."));

        //            if (!((iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("10.") || (iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("172.") || (iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("192."))) continue;//)) continue;//

        //            for (int k = 0; k < 255; k++)// for (int k = 254; k >0; k--)//
        //            {
        //                string kIP = iIP + "." + k;
        //                for (int j = 1; j < 255; j++)// for (int j = 254; j > 0; j--)//
        //                {
        //                    backHostIP += "." + k + "." + j;
        //                    //workWait.maked();
        //                    //workWait.wait();
        //                    //hh++;0
        //                    string jIP = kIP + "." + j;
        //                    //ping = new Ping();
        //                    if (!pings.ContainsKey(jIP))
        //                    {
        //                        pings.Add(jIP,  new WeakReference(new Ping()));
        //                        if (pings[jIP].IsAlive) //如果没有被回收
        //                        {
        //                            Ping p=pings[jIP].Target as Ping;
        //                            if (p!=null)
        //                            {
        //                            p.PingCompleted += PingAllIn1Success;
        //                            p.SendAsync(jIP, 600, jIP);
        //                            }
        //                        }
        //                    }
        //                    if (pings.ContainsKey(backHostIP))
        //                    {
        //                        if (pings[jIP].IsAlive) //如果没有被回收
        //                        {
        //                            Ping p = pings[jIP].Target as Ping;
        //                            p.SendAsyncCancel();
        //                            p.Dispose();
        //                        }

                              
        //                        pings.Remove(backHostIP);
        //                    }

        //                }
        //            }
        //            backHostIP = iIP;
        //        }
      
        //}
        Dictionary<string, Ping> pings = new Dictionary<string, Ping>();
       
        void asynPingAllJob(Object o)
        {
            string iIP = (string)o;
            //Dictionary<string, string> ipType = new Dictionary<string, string>();
            //ipType.Add("A类地址","10.");
            //ipType.Add("B类地址", "172.");
            //ipType.Add("C类地址", "192.");
            //List<string> enableIPList = (List<string>)o;
            ////string backHostIP = "";
            //List<string> used = new List<string>();
            //for (int i = 0; i < enableIPList.Count; i++)
            //{
            //    string iIP = (enableIPList[i].Substring(0, enableIPList[i].LastIndexOf(".")));//.Substring(0, enableIPList[i].LastIndexOf(".") + 1);
            //    iIP = iIP.Substring(0, iIP.LastIndexOf("."));

            //    if (!((iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals(ipType[comboBox4ip.Text]))) continue;//|| (iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("172.") || (iIP.Substring(0, iIP.LastIndexOf(".") + 1)).Equals("192.")))
            //    //{

            //    //    continue;
            //    //}//
            //    if (used.Contains(iIP.Substring(0, iIP.LastIndexOf(".") + 1)))
            //    {
            //        continue;
            //    }
            //    used.Add(iIP.Substring(0, iIP.LastIndexOf(".") + 1));
                for (int k = 0; k < 255; k++)// for (int k = 254; k >0; k--)//
                {
                    string kIP = iIP + "." + k;
                    //string kBackHostIP = backHostIP + "." + k;
                    for (int j = 1; j < 255; j++)// for (int j = 254; j > 0; j--)//
                    {
        
                        //workWait.maked();
                        //workWait.wait();
                        //hh++;0
                        //string jBackHostIP =kBackHostIP+"." + j;
                        string jIP = kIP + "." + j;
                        //ping = new Ping();
                        if (!pings.ContainsKey(jIP))
                        {
                            pings.Add(jIP, new Ping());
                            pings[jIP].PingCompleted += PingAllIn1Success;
                            pings[jIP].SendAsync(jIP, 600, jIP);
                         
                            
                        }
                        //if (pings.ContainsKey(backHostIP))
                        //{

                        //    pings[jBackHostIP].SendAsyncCancel();
                        //    pings[jBackHostIP].Dispose();
                        //    pings.Remove(jBackHostIP);
                        //}

                    }
                }
                //backHostIP = iIP;
            //}

        }
        static Object _lock = new object();
         void PingSuccess(object sender, PingCompletedEventArgs e)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;
           
            lock (_lock)
            {
                try
                {
                    //workWait.used();
                    //Async_SniffIP_Time = DateTime.Now;
                    if (e.Reply.Status == IPStatus.Success)
                    {
                        //string temp = e.UserState + "请求结果:" + e.Reply.Status + "(编号" + i + "),耗时：" + e.Reply.RoundtripTime + "ms";
                        //richTextBox4return.Text += "\n"+"IP地址:" + e.UserState + "->请求结果:" + e.Reply.Status + "耗时：" + e.Reply.RoundtripTime + "ms";
                        if (connectIPMap.ContainsKey((e.UserState).ToString()))
                        {

                            connectIPMap[e.UserState.ToString()][0]++;
                            connectIPMap[e.UserState.ToString()][1] += (int)e.Reply.RoundtripTime;

                        }
                        else
                        {
                            List<int> v = new List<int>() { 1, (int)e.Reply.RoundtripTime };
                            connectIPMap.Add(e.UserState.ToString(), v);
                        }



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            
        }
         void PingAllIn1Success(object sender, PingCompletedEventArgs e)
         {

             BackgroundWorker worker = sender as BackgroundWorker;

             lock (_lock)
             {
                 try
                 {
                     //workWait.used();
                     //Async_SniffIP_Time = DateTime.Now;
                     if (e.Reply.Status == IPStatus.Success)
                     {
                         //string temp = e.UserState + "请求结果:" + e.Reply.Status + "(编号" + i + "),耗时：" + e.Reply.RoundtripTime + "ms";
                         //richTextBox4return.Text += "\n"+"IP地址:" + e.UserState + "->请求结果:" + e.Reply.Status + "耗时：" + e.Reply.RoundtripTime + "ms";
                         if (connectAllIPMap.ContainsKey((e.UserState).ToString()))
                         {

                             connectAllIPMap[e.UserState.ToString()][0]++;
                             connectAllIPMap[e.UserState.ToString()][1] += (int)e.Reply.RoundtripTime;

                         }
                         else
                         {
                             List<int> v = new List<int>() { 1, (int)e.Reply.RoundtripTime };
                             connectAllIPMap.Add(e.UserState.ToString(), v);
                         }

                         pings[e.UserState.ToString()].SendAsyncCancel();
                         //pings[254].Dispose();
                         pings.Remove(e.UserState.ToString());

                     }
                 }
                 catch (Exception ex) { Console.WriteLine(ex.ToString()); }
             }

         }
         Dictionary<string, List<int>> connectIPMap = new Dictionary<string, List<int>>();
         Dictionary<string, List<int>> connectAllIPMap = new Dictionary<string, List<int>>(); 
        List<string> getAllEnableIP() 
        {
            List<string> netInfo =new List<string>();
            IPAddress[] dnsIps = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < dnsIps.Length; i++)
            {
                if (dnsIps[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    netInfo .Add( dnsIps[i].ToString());
                }
            }
            return netInfo;
        }
        public static bool testIPPortConnect(string ipAddress, int portNum)
        { //将IP和端口替换成为你要检测的
            //string ipAddress = "192.168.1.1";
            //int portNum = 22;

            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
                IPEndPoint point = new IPEndPoint(ip, portNum);
                using (System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(point);
                    sock.Close();
                    return true;
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        static byte[] PING_BUFFER = new byte[] { 0 };
 
        static int g_nHops = 30;
        static int g_nTimeout = 3000;
        static bool g_bCanceled = false;
 
        public struct ICMP_PARAM
        {
            internal long m_nSendTicks;
            internal IPAddress m_IPAddress;
            internal PingOptions m_PingOptions;
        }
        //void OnPingCompleted(object sender, PingCompletedEventArgs e)
        //{
        //    //ProcessNode(e.Reply.Address, e.Reply.Status);

        //    //if (!this.IsDone)
        //    //{
        //    //    lock (this)
        //    //    {
        //    //        //If the object is disposed the _ping 
        //    //        //member variable is set to null
        //    //        if (_ping == null)
        //    //        {
        //    //            ProcessNode(_destination, IPStatus.Unknown);
        //    //        }
        //    //        else
        //    //        {
        //    //            _options.Ttl += 1;
        //    //            ping.SendAsync(_destination, _timeout,
        //    //                           Tracert.Buffer, _options, null);
        //    //        }
        //    //    }
        //    //}
        //}
         void icmp_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            ICMP_PARAM param = (ICMP_PARAM)e.UserState;
            long nDeltaMS = (DateTime.Now.Ticks - param.m_nSendTicks) / TimeSpan.TicksPerMillisecond;
           richTextBox4return.Text += "\n" +param.m_PingOptions.Ttl+":\t"+((e.Reply.Status == IPStatus.TimedOut) ? "*" : nDeltaMS.ToString())+" ms\t" +e.Reply.Address.ToString();
 
            if (param.m_IPAddress.Equals(e.Reply.Address))
            {
               richTextBox4return.Text += "\n" + "已到达目标地址！";
                g_bCanceled = true;
                return;
            }
 
            if (param.m_PingOptions.Ttl >= g_nHops)
            {
                richTextBox4return.Text += "\n" + "已达到最大跃点计数！";
                g_bCanceled = true;
                return;
            }
 
            Ping icmp = (Ping)sender;
            param.m_PingOptions.Ttl++;
            param.m_nSendTicks = DateTime.Now.Ticks;
            icmp.SendAsync(param.m_IPAddress, g_nTimeout, PING_BUFFER, param.m_PingOptions, param);
        }
 
         void Usage()
        {
             richTextBox4return.Text += "\n" +"Usage: "+Process.GetCurrentProcess().ProcessName+" [-h max_hops] [-w timeout] ip/domain";
        }

         void doTryTracert(string szDomain)
        {

            //string szDomain = "";
            //if (args.Length == 0)
            //{
            //    Usage();
            //    return;
            //}
            //else
            //{
            //    string szTmp = "";
            //    for (int i = 0; i < args.Length; i++)
            //    {
            //        if (args[i].StartsWith("."))
            //        {
            //            if (args[i].Length == 2)
            //            {
            //                if (i + 1 < args.Length)
            //                {
            //                    szTmp = args[i + 1];
            //                }
            //                else
            //                {
            //                    Usage();
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                szTmp = args[i].Substring(2);
            //            }
 
            //            switch (args[i][1])
            //            {
            //                case 'h':
            //                case 'H':
            //                    if (!int.TryParse(szTmp, out g_nHops))
            //                    {
            //                        Usage();
            //                        return;
            //                    }
            //                    break;
 
            //                case 'w':
            //                case 'W':
            //                    if (!int.TryParse(szTmp, out g_nTimeout))
            //                    {
            //                        Usage();
            //                        return;
            //                    }
            //                    break;
 
            //                default:
            //                    Usage();
            //                    return;
            //            }
            //        }
            //        else
            //        {
            //            szDomain = args[i];
            //        }
            //    }
            //}
 
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
 
            ICMP_PARAM param = new ICMP_PARAM();
            param.m_PingOptions = new PingOptions(1, false);
            if (!IPAddress.TryParse(szDomain, out param.m_IPAddress))
            {
                // 解析域名
                try
                {
                    Regex regEx = new Regex("\\d+\\.\\d+\\.\\d+\\.\\d+");
                    IPHostEntry hostEntry = Dns.GetHostEntry(szDomain);
                    foreach (IPAddress ipAddr in hostEntry.AddressList)
                    {
                        if (regEx.IsMatch(ipAddr.ToString()))
                        {
                            param.m_IPAddress = ipAddr;
                            break;
                        }
                    }
 
                    if (param.m_IPAddress == null)
                    {
                        Usage();
                        return;
                    }
 
                    richTextBox4return.Text += "\n" + "正在跟踪到 "+szDomain+"["+ param.m_IPAddress.ToString()+"] 间的路由：";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
            else
            {
               richTextBox4return.Text += "\n" + "正在跟踪到" + param.m_IPAddress.ToString()+" 间的路由：";
            }
 
            Ping icmp = new Ping();
            icmp.PingCompleted += new PingCompletedEventHandler(icmp_PingCompleted);
 
            param.m_nSendTicks = DateTime.Now.Ticks;
            icmp.SendAsync(param.m_IPAddress, g_nTimeout, PING_BUFFER, param.m_PingOptions, param);
 
            while (!g_bCanceled)
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }
        }
 
         void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
          richTextBox4return.Text += "\n" + "程序被终止！";
        }
        //------------------------------------------------------------------------------------------------------
         public string GetLocalIP()
        {
            IPAddress localIp = null;
            try
            {
                IPAddress[] ipArray;
                ipArray = Dns.GetHostAddresses(Dns.GetHostName());
                localIp = ipArray.First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                //Log.WriteLog(ex);
            }
            if (localIp == null)
            {
                localIp = IPAddress.Parse("127.0.0.1");
            }
            return localIp.ToString();
        }
         public string GetLocalAllIP()
         {
             string netInfo = "";
             try
             {
                 IPAddress[] ipArray;
                 ipArray = Dns.GetHostAddresses("");
                 for (int i = 0; i < ipArray.Length; i++)
                {
                    netInfo += string.Format("{0}) [ip:]{1}，  [ip类型:]{2}\r\n", i.ToString(), ipArray[i].ToString(), ipArray[i].AddressFamily);
                }


                 return netInfo;
               
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                 //Log.WriteLog(ex);
                 return "获取所有网卡失败" + ex.ToString();
             }

            
         }
       
         string getNetworkCard() 
         {
            string netInfo ="";
             //3、获取网卡的名字等信息
            netInfo += "\r\n以下是网卡接口数组中的信息。\r\n通过NetworkInterface.GetAllNetworkInterfaces()获取本机所有网卡接口信息:\r\n";
            int count = 0;
            foreach (NetworkInterface netInt in NetworkInterface.GetAllNetworkInterfaces())
            {
               
                count++;
                netInfo += string.Format("{0})接口名:{1}\r\n    接口类型:{2}\r\n    接口MAC:{3}\r\n    接口速度:{4}\r\n    接口描述信息:{5}\r\n", count, netInt.Name, netInt.NetworkInterfaceType, netInt.GetPhysicalAddress().ToString(), netInt.Speed / 1000 / 1000, netInt.Description);
              
                netInfo += "    接口配置的IP地址:\r\n";
                foreach (UnicastIPAddressInformation ipIntProp in netInt.GetIPProperties().UnicastAddresses.ToArray<UnicastIPAddressInformation>())
                {
                    netInfo += string.Format("    接口名:{0}，  ip:{1}，  ip类型:{2}\r\n",netInt.Name, ipIntProp.Address.ToString(),ipIntProp.Address.AddressFamily);
                    
                }
                //单个网卡的IP对象
                IPInterfaceProperties ip = netInt.GetIPProperties();
                GatewayIPAddressInformationCollection gateways = ip.GatewayAddresses;
                foreach (var gateWay in gateways)
                {
                    //如果能够Ping通网关
                    if (pingIP(gateWay.Address.ToString()))
                    {
                        //得到网关地址
                        netInfo += "畅通的网关：" + gateWay.Address.ToString() + "\r\n";
                        //跳出循环
                        //break;
                    }
                    else 
                    {
                        netInfo += "不可达的网关：" + gateWay.Address.ToString() + "\r\n";
                    }
                }
                IPAddressCollection dnsAddresses =ip.DnsAddresses;
                foreach (var DnsAddress in dnsAddresses) 
                {
                     netInfo += "DNS地址：" + DnsAddress.ToString() + "\r\n";
                
                }
                //if (ip.IsDnsEnabled)
                //{
                //    netInfo += "可以上网，DNS地址：" + .ToString() + "\r\n";
                //}
                //else {
                   
                //}


            }
            return netInfo;
         }

         private void getPasNetAddr_Click(object sender, EventArgs e)
         {
             richTextBox4return.Text += "\n" + getImportantIP();
         }
         string getImportantIP() 
         
         {
            string netInfo = "";
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up && item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {//先获取需要的接口。两个条件确定当前正在使用的网口。1、接口状态为up；2、接口类型为Ethernet。
                    
                    foreach (var ipInfo in item.GetIPProperties().UnicastAddresses.ToArray())
                    {
                        if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            netInfo += string.Format("本机正在使用接口为:{0}\r\n本机正在使用的IP为：{1}", item.Name, ipInfo.Address.ToString());
                            break;
                        }
                    }
                }                
            }
 
            //与上面一样，只不过用了Dns.GetHostAddresses()的方法
            IPAddress[] dnsIps = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < dnsIps.Length; i++)
            {
                if (dnsIps[i].AddressFamily==System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    netInfo += "\r\n已启用的IPv4：" + dnsIps[i].ToString();
                }
            }
            return netInfo;
         }

         private void button4RichClear_Click(object sender, EventArgs e)
         {
             richTextBox4return.Text = "";
         }
         Thread urlIsExistTh = null;
         private void button4url_Click(object sender, EventArgs e)
         {
             List<String> urls =new List<string> ();
             urls.Add(richTextBox4url.Text);
             UrlIsExistThStart(urls);
             foreach(string url in urls){
                 if (connectURLMap.ContainsKey(url))
                 {
                     richTextBox4return.Text += "\r\n"+ richTextBox4url.Text +"--"+ connectURLMap[url];
                 }
             }
             //if (UrlIsExist(richTextBox4url.Text)) {
             //    richTextBox4return.Text += richTextBox4url.Text+"-目的地址服务器存在";
             //}
             //else{
             //    richTextBox4return.Text +=  richTextBox4url.Text + "-不可用";
             //}
         }
         private void UrlIsExistThStart(List<String> urls) 
         {
             if (urlIsExistTh == null)
             {
                 urlIsExistTh = new Thread(UrlIsExist);
                 urlIsExistTh.IsBackground = true;
                 urlIsExistTh.Start(urls);
             }
             else if (!urlIsExistTh.IsAlive)
             {
                 urlIsExistTh = new Thread(UrlIsExist);
                 urlIsExistTh.IsBackground = true;
                 urlIsExistTh.Start(urls);
             }
             if (connectURLMap.Count > 100) connectURLMap.Clear();
         }
         Dictionary<string, string> connectURLMap = new Dictionary<string, string>();
         private void UrlIsExist(Object o)
         {
             List<String> urls = (List<String>)o;
             System.Uri u = null;
             foreach(String url in urls){
                   System.Net.HttpWebRequest r =null;
                 try
                 {
                     u = new Uri(url);
                     if (u == null) throw new  Exception();
                     r = System.Net.HttpWebRequest.Create(u) as System.Net.HttpWebRequest;
                     r.Method = "HEAD";
                 }
                 catch 
                 {
                     if (connectURLMap.ContainsKey(url))
                     {
                         connectURLMap[url] = "url格式不正确";
                     }
                     else 
                     {
                         connectURLMap.Add(url, "url格式不正确");
                     }
                     continue;
                     
                 }
                 bool isExist = false;
                
                 try
                 {
                     System.Net.HttpWebResponse s = r.GetResponse() as System.Net.HttpWebResponse;
                     if (s.StatusCode == System.Net.HttpStatusCode.OK)
                     {
                         isExist = true;
                     }
                 }
                 catch (System.Net.WebException x)
                 {
                     try
                     {
                         isExist = ((x.Response as System.Net.HttpWebResponse).StatusCode != System.Net.HttpStatusCode.NotFound);
                     }
                     catch { isExist = (x.Status == System.Net.WebExceptionStatus.Success); }
                 }
                 if (isExist)
                 {
                     if (connectURLMap.ContainsKey(url))
                     {
                         connectURLMap[url] = "目的地址的服务器存活";
                     }
                     else
                     {
                         connectURLMap.Add(url, "目的地址的服务器存活");
                     }
                 }
                 else 
                 {
                     if (connectURLMap.ContainsKey(url))
                     {
                         connectURLMap[url] = "无法连接";
                     }
                     else
                     {
                         connectURLMap.Add(url, "无法连接");
                     }
                 }
             }
             //return isExist;
         }

        private void banIPButton_Click(object sender, EventArgs e)
        {
            IISTool iisTool = new IISTool();
            IEnumerable<string>  siteNames= iisTool.GetSiteNames();
            if (siteNames == null)
            {
                MessageBox.Show("权限不足");
            }
            else {
                foreach (string s in iisTool.GetSiteNames())
                {
                   
                        richTextBox4return.Text += "\n" + s;
                   
                }
               // siteNames.
            }
            if (pingIP(banIPButtonTextBox.Text))
            {
                if (banIPButton.Text.Equals("禁止访问IP"))
                {
                    try
                    {
                        IISTool.banIP(banIPButtonTextBox.Text);
                        richTextBox4return.Text += "\n" + banIPButtonTextBox.Text + "被禁止";
                    }
                    catch
                    {
                        richTextBox4return.Text += "\n" + banIPButtonTextBox.Text + "禁止时出现异常";
                    }
                    banIPButton.Text = "解除禁制IP";
                    banIPButtonTextBox.Enabled = false;
                }
                else 
                {
                    try
                    {
                        IISTool.allowIP(banIPButtonTextBox.Text);
                        richTextBox4return.Text += "\n" + banIPButtonTextBox.Text + "被解除禁止";
                    }
                    catch
                    {
                        richTextBox4return.Text += "\n" + banIPButtonTextBox.Text + "解除禁止时出现异常";
                    }
                    banIPButton.Text = "禁止访问IP";
                    banIPButtonTextBox.Enabled = true;
                }
            }
            else
            {
                richTextBox4return.Text += "\n" + banIPButtonTextBox.Text + "网络不可达";
            }
        }
    }
}
