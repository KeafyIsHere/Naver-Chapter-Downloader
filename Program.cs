using System;
using System.IO;
using System.Net;

namespace NaverDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Naver Comic Downloader By KeafyIsHere";
            Console.WriteLine("Enter Naver comic chapter Link for example\nhttps://comic.naver.com/webtoon/detail.nhn?titleId=730694&no=1");
            string url = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter Folder/Prefix Title for chapter.");
            string prefix = Console.ReadLine();
            if (!Directory.Exists(prefix))
            {
                Directory.CreateDirectory(prefix);
            }
            else
            {
                Console.WriteLine("Folder already exist are you sure you want to continue?");
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
            try
            {
                WebClient wc = new WebClient();
                
                string source = wc.DownloadString(url);
                string htmlimagebits = source.Split(new string[] { "<div class=\"wt_viewer\" style=\"" }, StringSplitOptions.None)[1].Trim().Split(new string[] { "</div>" }, StringSplitOptions.None)[0];
                int pagenum = 1;
                foreach (string line in htmlimagebits.Split(new string[] { "<img src=\"" }, StringSplitOptions.None))
                {
                    if (line.StartsWith("https://image-comic.pstatic.net"))
                    {
                        SetHeaders(wc);
                        Console.WriteLine($"Downloading Page {pagenum}!");
                        wc.DownloadFile(line.Split('"')[0], $"{prefix}\\{prefix} Page {pagenum}.png");
                        pagenum++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Job Done!");
            Console.ReadLine();
        }

        private static void SetHeaders(WebClient wc)
        {
            wc.Headers.Add("authority", "image-comic.pstatic.net");
            wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36");
        }
    }
}
