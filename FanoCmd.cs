using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace TM2
{
    class FanoCmd
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        HtmlDocument doc = new HtmlDocument();
        Config config = new Config();

        public async Task Login(string user, string password)
        {
            main.fanoWeb = new MyWebClient("https://www.fano.in/takelogin.php", user, password);
            await main.fanoWeb.LogIn();
        }
        public async Task<bool> CheckLogin()
        {
            bool loginStatus = false;

            string checkLogin = await main.fanoWeb.DownloadStringTaskAsync("https://www.fano.in/index.php");

            doc.LoadHtml(checkLogin);

            if (doc != null)
            {
                string info = doc.GetElementbyId("menu").SelectSingleNode(".//a").GetAttributeValue("href", "");

                if (info == "/login.php")
                {
                    loginStatus = false;
                }
                else
                {
                    loginStatus = true;
                }
            }
            return loginStatus;
        }
        public async Task<double> GetRatio()
        {
            string ratioContent = await main.fanoWeb.DownloadStringTaskAsync("https://www.fano.in/index.php");
            doc.LoadHtml(ratioContent);

            double r;
            double.TryParse(doc.DocumentNode.SelectSingleNode("(//p[@class='t_mt'])").ChildNodes[6].InnerText, NumberStyles.Any, CultureInfo.InvariantCulture, out r);

            return r;
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
                if (await CheckLogin())
                {
                    await main.fanoWeb.DownloadFileTaskAsync(new Uri(uri), torrent);
                    Process.Start(torrent);
                }
                else
                {
                    if (config.FanoAccountExist())
                    {
                        await Login(config.Read("account", "fanologin"), PasswordSecurity.Decrypt(config.Read("account", "fanopassword"), main.mySite));
                        await main.fanoWeb.DownloadFileTaskAsync(new Uri(uri), torrent);
                        Process.Start(torrent);
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
