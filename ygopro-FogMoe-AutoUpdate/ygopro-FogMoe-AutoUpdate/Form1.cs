using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ygopro_FogMoe_AutoUpdate
{
    public partial class Form1 : Form
    {
        int needUpdateCard = 0; //0是未检查，1是有更新，2是无更新
        string downloadNode;
        string nodeUrl;
        string checkUpdateUrl;
        public string gameFolderPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ping baidu
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply pingReply = ping.Send("baidu.com");
                if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    //success
                }
                else
                {
                    MessageBox.Show("网络连接失败啦！请联系Kc处理喵~ ");
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接失败啦！请联系Kc处理喵~ ");
                Application.Exit();
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string thisVersion = label1.Text;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["toolVer"] = thisVersion;
                var response = client.UploadValues("http://verify.fogmoe.top/toolVer.php", values);
                var responseString = Encoding.Default.GetString(response);
                if (responseString == "False")
                {
                    Visible = false;
                    MessageBox.Show("当前更新器" + thisVersion + "，有新版本可以使用，请下载！");
                    System.Diagnostics.Process.Start("https://diy.fog.moe");
                    Close();
                }
                else if (responseString == "True")
                {
                    //no update
                }
                    

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            gameFolderPath = textBox1.Text;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (downloadNode == "国内")
            {
                nodeUrl = "https://ygodiydata.github.fogmoe.top/";
                checkUpdateUrl = "https://ghproxy.futils.com/https://github.com/scarletkc/ygopro-FogMoe-card-database/blob/main/Version.txt";
            }
            else if(downloadNode == "国外")
            {
                nodeUrl = "https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
                checkUpdateUrl = "https://github.com/scarletkc/ygopro-FogMoe-card-database/raw/main/Version.txt";
            }
            else
            {
                nodeUrl = "https://gh2.yanqishui.work/https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
                checkUpdateUrl = "https://raw.iqiq.io/scarletkc/ygopro-FogMoe-card-database/main/Version.txt";
            }
            if (needUpdateCard == 1)
            {
                label2.Text = "当前状态: 更新中";
                //try
                //{
                    CoreClass.DownloadFile(nodeUrl, gameFolderPath);
                    CoreClass.Unzip(gameFolderPath);
                    CoreClass.DeleteFile(gameFolderPath + "\\FogMoe-Temp-YGO.zip");
                    CoreClass.CopyFilesRecursively(gameFolderPath + "\\FogMoeYGO-Card-Database-main", gameFolderPath + "\\expansions");
                    label2.Text = "当前状态: 完成了";
                    MessageBox.Show("更新完成啦！");
                /*}
                catch (Exception)
                {
                    MessageBox.Show("发生错误了: 无法更新！请更换下载节点重试或联系Kc处理喵~ ");
                    label2.Text = "当前状态: 失败了";
                }  */             
            }
            else if (needUpdateCard == 0)
            {
                MessageBox.Show("请先点击检查版本更新哦！");
            }
            else if (needUpdateCard == 2)
            {
                MessageBox.Show("您的本地FogMoe卡片数据版本已经是最新的了，不需要更新哦！");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            downloadNode = comboBox1.GetItemText(comboBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://diy.fog.moe");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=tbRrClio");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (downloadNode == "国内")
            {
                nodeUrl = "https://ygodiydata.github.fogmoe.top/";
                checkUpdateUrl = "https://ghproxy.futils.com/https://github.com/scarletkc/ygopro-FogMoe-card-database/blob/main/Version.txt";
            }
            else if (downloadNode == "国外")
            {
                nodeUrl = "https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
                checkUpdateUrl = "https://github.com/scarletkc/ygopro-FogMoe-card-database/raw/main/Version.txt";
            }
            else
            {
                nodeUrl = "https://gh2.yanqishui.work/https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
                checkUpdateUrl = "https://raw.iqiq.io/scarletkc/ygopro-FogMoe-card-database/main/Version.txt";
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("请先选择YGOPro目录路径文件夹！");
            }
            else
            {
                try
                {
                    label2.Text = "当前状态: 检查中";
                    string localDataVersion = "[没有发现FogMoe卡片数据本地版本文件，您可能是第一次使用呢，请点击更新卡片数据！]";
                    if (File.Exists(gameFolderPath + "\\expansions\\Version.txt"))
                    {
                        localDataVersion = CoreClass.ReadTextFromFile(gameFolderPath + "\\expansions\\Version.txt");
                    }
                    else
                    {
                        localDataVersion = "[没有发现FogMoe卡片数据本地版本文件，您可能是第一次使用呢，请点击更新卡片数据！]";
                    }
                    string cardDataVersion = CoreClass.ReadTextFromUrl(checkUpdateUrl);
                    MessageBox.Show("FogMoe卡片数据最新版本是: " + cardDataVersion + " ,本地版本是: " + localDataVersion);
                    if (localDataVersion == cardDataVersion)
                    {
                        label2.Text = "当前状态: 已最新";
                        needUpdateCard = 2;
                    }
                    else
                    {
                        label2.Text = "当前状态: 需更新";
                        needUpdateCard = 1;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("发生错误了: 无法检查！请更换下载节点重试或联系Kc处理喵~ ");
                    label2.Text = "当前状态: 失败了";
                    needUpdateCard = 0;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    //This program is made by Kc [https://github.com/scarletkc]
}
