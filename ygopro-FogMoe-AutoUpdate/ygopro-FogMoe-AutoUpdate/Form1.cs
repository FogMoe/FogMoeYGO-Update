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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string thisVersion = label1.Text;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["toolVer"] = thisVersion;
                var response = client.UploadValues("http://verify.fogmoe.top/verifyVer.php", values);
                var responseString = Encoding.Default.GetString(response);
                if (responseString == "False")
                {
                    Visible = false;
                    MessageBox.Show("当前更新器" + thisVersion + "，有新版本可以使用！");
                    System.Diagnostics.Process.Start("https://diy.fog.moe");
                    Close();
                }

            }
        }
    }
}
