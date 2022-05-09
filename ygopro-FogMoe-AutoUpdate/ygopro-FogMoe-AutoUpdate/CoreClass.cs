using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Net;

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
        public static void Unzip(string path)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path + "\\FogMoe-Temp-YGO.zip"))
            {
                ZipFile.ExtractToDirectory(path + "\\FogMoe-Temp-YGO.zip", path);
            }
        }
        //copy folder to expansions and replace
        public static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
            Directory.Delete(sourcePath, true);
        }
        //Get new card data version
        public static string ReadTextFromUrl(string url)
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var textReader = new StreamReader(stream, Encoding.UTF8, true))
            {
                return textReader.ReadToEnd();
            }
        }
        //Get local card data version
        public static string ReadTextFromFile(string path)
        {
            return System.IO.File.ReadAllText(path);
        }


        //Delete file
        public static void DeleteFile(string path)
        {
            System.IO.File.Delete(path);
        }
    }
    //This program is made by Kc [https://github.com/scarletkc]
}
