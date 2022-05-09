using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ygopro_FogMoe_AutoUpdate
{
    public partial class Form1 : Form
    {
        string downloadNode;
        string nodeUrl;
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
                    MessageBox.Show("网络连接失败啦！");
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接失败啦！");
                Application.Exit();
            }
            comboBox1.SelectedItem = comboBox1.Items[1];
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
                    MessageBox.Show("当前更新器" + thisVersion + "，有新版本可以使用！");
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
                nodeUrl = "https://ghproxy.fsofso.com/https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
            }
            else if(downloadNode == "国外")
            {
                nodeUrl = "https://github.com/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
            }
            else
            {
                nodeUrl = "https://archive.fastgit.org/scarletkc/ygopro-FogMoe-card-database/archive/refs/heads/main.zip";
            }
            if (true)
            {
                label2.Text = "当前状态: 运行中";
                /*try
                {
                    CoreClass.DownloadFile(nodeUrl, gameFolderPath + "\\expansions");
                    CoreClass.Unzip(gameFolderPath + "\\expansions\\ygopro-FogMoe-card-database-main.zip");
                    CoreClass.DeleteFile(gameFolderPath + "\\expansions\\ygopro-FogMoe-card-database-main.zip");
                    label2.Text = "当前状态: 完成了";
                    MessageBox.Show("更新完成啦！");
                }
                catch (Exception)
                {
                    MessageBox.Show("发生错误了: 无法更新！");
                    label2.Text = "当前状态: 失败了";
                }*/
                ////////////////////
                    CoreClass.DownloadFile(nodeUrl, gameFolderPath + "\\expansions");
                    CoreClass.Unzip(gameFolderPath + "\\expansions\\FogMoe-Temp-YGO.zip");
                    CoreClass.DeleteFile(gameFolderPath + "\\expansions\\FogMoe-Temp-YGO.zip");
                    label2.Text = "当前状态: 完成了";
                    MessageBox.Show("更新完成啦！");



                    MessageBox.Show("发生错误了: 无法更新！");
                    label2.Text = "当前状态: 失败了";
                ////////////////////
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
    }
    //This program is made by Kc [https://github.com/scarletkc]
}
