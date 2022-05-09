using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ygopro_FogMoe_AutoUpdate
{
    internal class CoreClass
    {
        //download file
        public static void DownloadFile(string url, string path)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.DownloadFile(url, path + "\\FogMoe-Temp-YGO.zip");
            }
        }
        //Unzip
        
        
    
        //Delete file
        public static void DeleteFile(string path)
        {
            System.IO.File.Delete(path);
        }
    }
    //This program is made by Kc [https://github.com/scarletkc]
}
