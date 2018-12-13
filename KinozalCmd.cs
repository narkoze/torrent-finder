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
    class KinozalCmd
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        HtmlDocument doc = new HtmlDocument();

        public async Task Login(string user, string password)
        {
            main.kinozalWeb = new MyWebClient("http://kinozal.tv/takelogin.php", user, password);
            await main.kinozalWeb.LogIn();
        }
        public async Task<bool> CheckLogin()
        {
            bool loginStatus = true;

            string checkLogin = await main.kinozalWeb.DownloadStringTaskAsync("http://kinozal.tv");
            doc.LoadHtml(checkLogin);

            var info = doc.GetElementbyId("main").SelectSingleNode(".//form");
            if (info != null)
                loginStatus = false;

            return loginStatus;
        }
        public async Task<double> GetRatio()
        {
            string getProfileUri = await main.kinozalWeb.DownloadStringTaskAsync("http://kinozal.tv");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(getProfileUri);

            string profile = doc.GetElementbyId("main").SelectSingleNode(".//a[1]").GetAttributeValue("href", "");

            byte[] getProfile = await main.kinozalWeb.DownloadDataTaskAsync("http://kinozal.tv" + profile);
            string encoded = Encoding.GetEncoding(1251).GetString(getProfile);
            doc.LoadHtml(encoded);

            string ratio = doc.DocumentNode.SelectSingleNode("(//td[@class='w135'])[4]").NextSibling.InnerText.Trim();

            if (ratio == "Беск.")
                return 1.00;

            double r;
            double.TryParse(ratio, NumberStyles.Any, CultureInfo.InvariantCulture, out r);

            return r;
        }
        public async Task<string> GetAvailable()
        {
            string getProfileUri = await main.kinozalWeb.DownloadStringTaskAsync("http://kinozal.tv");
            doc.LoadHtml(getProfileUri);

            string profile = doc.GetElementbyId("main").SelectSingleNode(".//a[1]").GetAttributeValue("href", "");

            byte[] getProfile = await main.kinozalWeb.DownloadDataTaskAsync("http://kinozal.tv" + profile);
            string encoded = Encoding.GetEncoding(1251).GetString(getProfile);
            doc.LoadHtml(encoded);

            var allowedInfo = doc.DocumentNode.
                SelectSingleNode("//div[@class='mn1_content']").
                SelectSingleNode("(.//div)[3]").
                SelectSingleNode(".//table").
                SelectSingleNode("(.//tr)[6]").
                SelectSingleNode("(.//td)[2]").InnerText;
            //Доступно в сутки ( 3 ) Скачано ( 3 ) Последний ( здесь )

            var totaly = GetBetween(allowedInfo, "Доступно в сутки ( ", " ) Скачано"); //Cik dienaktī var kačāt
            int total;
            int.TryParse(totaly, out total);

            var allowedToday = GetBetween(allowedInfo, "Скачано ( ", " ) Последний"); //cik jau nokachats
            int already;
            int.TryParse(allowedToday, out already);

            var today = total - already;

            return today + "/" + totaly;
        }
        public async Task Disconnect()
        {
            string getProfileUri = await main.kinozalWeb.DownloadStringTaskAsync("http://kinozal.tv");
            doc.LoadHtml(getProfileUri);
            await main.kinozalWeb.DownloadStringTaskAsync("http://kinozal.tv" + doc.GetElementbyId("main").SelectSingleNode(".//a[2]").GetAttributeValue("href", ""));
        }
        private string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (!strSource.Contains(strStart) || !strSource.Contains(strEnd)) return "";
            var start = strSource.IndexOf(strStart, 0, StringComparison.Ordinal) + strStart.Length;
            var end = strSource.IndexOf(strEnd, start, StringComparison.Ordinal);
            return strSource.Substring(start, end - start);
        }
        public async Task DownloadTorrent(string uri, string Name)
        {
            if(int.Parse(main.txtAvailable.Text.Substring(0,1)) != 0)
            {
                string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                foreach (var c in invalid)
                {
                    Name = Name.Replace(c.ToString(), "");
                }

                string torrent = main.downloadsPath + @"\" + Name + ".torrent";

                try
                {
                    await main.kinozalWeb.DownloadFileTaskAsync(new Uri(uri), torrent);
                    Process.Start(torrent);
                    main.txtAvailable.Text = await GetAvailable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Kinozal.tv, atļauto torrentu skaits ir beidzies!", "TM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
