using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TM2
{
    class FilebaseCmd
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        HtmlDocument doc = new HtmlDocument();

        public async Task Login(string user, string password)
        {
            main.FilebaseWeb = new MyWebClient("http://www.filebase.ws/login.php", user, password);
            await main.FilebaseWeb.LogInFilebase();
        }
        public async Task<bool> CheckLogin()
        {
            bool loginStatus = true;

            var checkLogin = await main.FilebaseWeb.DownloadDataTaskAsync("http://www.filebase.ws");
            var encoded = Encoding.UTF8.GetString(checkLogin);
            doc.LoadHtml(encoded);

            foreach (var nodes in doc.DocumentNode.SelectNodes("//form"))
            {
                loginStatus = nodes.GetAttributeValue("action", "") != "/login.php";
            }
            return loginStatus;
        }
        public async Task<double> GetRatio()
        {
            byte[] checkLogin = await main.FilebaseWeb.DownloadDataTaskAsync("http://www.filebase.ws");
            string encoded = Encoding.UTF8.GetString(checkLogin);
            doc.LoadHtml(encoded);

            var content =  doc.DocumentNode.SelectSingleNode("(.//td[@class='prof'])").SelectSingleNode(".//div").InnerText;
            string[] word = content.Split('|');
            string ratio = word[2].Substring(" Рейтинг: ".Length, word[2].Length - " Рейтинг: ".Length);
            if (ratio == "0.00")
                return 1.00;

            double r;
            double.TryParse(ratio, NumberStyles.Any, CultureInfo.InvariantCulture, out r);

            return r;
        }
        public async Task Disconnect()
        {
            await main.FilebaseWeb.DownloadStringTaskAsync("http://www.filebase.ws/logout.php");
        }
        public async Task DownloadTorrent(string uri, string Name)
        {
            var invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (var c in invalid)
            {
                Name = Name.Replace(c.ToString(), "");
            }

            var torrent = main.downloadsPath + @"\" + Name + ".torrent";

            try
            {
                await main.FilebaseWeb.DownloadFileTaskAsync(new Uri(uri), torrent);
                Process.Start(torrent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
