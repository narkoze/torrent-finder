using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TM2.Controls
{
    public partial class Covers : UserControl
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        WebClient webClient = new WebClient();
        HtmlDocument doc = new HtmlDocument();

        private string movieUri = "http://www.piemeram.lv/JjxJNdbf/Movies.php";
        private string tvUri = "http://www.piemeram.lv/JjxJNdbf/Tv.php";
        private string gameUri = "http://www.piemeram.lv/JjxJNdbf/Games.php";
        private string gameCoverUri = "http://piemeram.lv/GameCovers/";
        private string category, genre, name, order, platform;
        private int? year;
        private int from = 0;
        private int count = 48;
        private bool coversLoaded;

        public Covers(string Category, string Order, string Genre, int? Year, string Name, string Platform)
        {
            order = Order;
            genre = Genre;
            name = Name;
            year = Year;
            category = Category;
            platform = Platform;

            InitializeComponent();
            Init();
        }
        private void Init()
        {
            switch (category)
            {
                case "Movies":
                    LoadMovies(movieUri, from, count, year, genre, order, name);
                    break;
                case "TV":
                    LoadTv(tvUri, from, count, year, genre, order, name);
                    break;
                case "Games":
                    LoadGames(gameUri, from, count, year, genre, order, name, platform);
                    break;
            }
        }
        private async void LoadMovies(string Uri, int From, int To, int? Year, string Genre, string Order, string Name)
        {
            coversLoaded = false;

            byte[] content = await webClient.DownloadDataTaskAsync(Uri
                + "?no=" + From 
                + "&count=" + To 
                + "&year=" + Year 
                + "&genre=" + Genre 
                + "&order=" + Order
                + "&name=" + Name);

            string encoded = Encoding.UTF8.GetString(content);
            doc.LoadHtml(encoded);

            IEnumerable<string[]> table = doc.DocumentNode.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            foreach (string[] td in table)
            {
                string year = td[2],
                       name = td[3],
                       genre = td[4],
                       imdbUri = td[5],
                       coverUri = td[6],
                       kinopoiskUri = td[7],
                       pievienoja = td[10];

                double ratio = double.Parse(td[8]);
                int votes = int.Parse(td[9]);

                string sourceUri = string.Empty;
                bool genreRussian = false;

                if (string.IsNullOrEmpty(imdbUri))
                {
                    sourceUri = kinopoiskUri;
                    genreRussian = true;
                }
                else
                    sourceUri = imdbUri;

                CoverList.Children.Add(new CoverCreator().NewMovie(year, name, genre, genreRussian, sourceUri, coverUri, "Movie", ratio, votes, pievienoja));
            }

            coversLoaded = true;

            main.filterPanel.Visibility = Visibility.Visible;
            main.progressBar.Visibility = Visibility.Collapsed;
            
        }
        private async void LoadTv(string Uri, int From, int To, int? Year, string Genre, string Order, string Name)
        {
            coversLoaded = false;

            byte[] content = await webClient.DownloadDataTaskAsync(Uri 
                + "?no=" + From 
                + "&count=" + To 
                + "&year=" + Year 
                + "&genre=" + Genre
                + "&order=" + Order
                + "&name=" + Name);
            string encoded = Encoding.UTF8.GetString(content);
            doc.LoadHtml(encoded);

            IEnumerable<string[]> table = doc.DocumentNode.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            foreach (string[] td in table)
            {
                string year = td[2],
                       name = td[3],
                       genre = td[4],
                       imdbUri = td[5],
                       coverUri = td[6],
                       kinopoiskUri = td[7],
                       pievienoja = td[10];

                double ratio = double.Parse(td[8]);
                int votes = int.Parse(td[9]);

                string sourceUri = string.Empty;
                bool genreRussian = false;

                if (string.IsNullOrEmpty(imdbUri))
                {
                    sourceUri = kinopoiskUri;
                    genreRussian = true;
                }            
                else
                    sourceUri = imdbUri;

                CoverList.Children.Add(new CoverCreator().NewTv(year, name, genre, genreRussian, sourceUri, coverUri, "TV", ratio, votes, pievienoja));
            }

            coversLoaded = true;

            main.filterPanel.Visibility = Visibility.Visible;
            main.progressBar.Visibility = Visibility.Collapsed;
        }
        private async void LoadGames(string Uri, int From, int To, int? Year, string Genre, string Order, string Name, string Platform)
        {
            coversLoaded = false;

            byte[] content = await webClient.DownloadDataTaskAsync(Uri
                + "?no=" + From
                + "&count=" + To
                + "&year=" + Year
                + "&genre=" + Genre
                + "&order=" + Order
                + "&name=" + Name
                + "&platform=" + Platform);

            string encoded = Encoding.UTF8.GetString(content);
            doc.LoadHtml(encoded);

            IEnumerable<string[]> table = doc.DocumentNode.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            foreach (string[] td in table)
            {
                string name = td[3],
                       genre = td[4],
                       coverUri = gameCoverUri + td[0] + ".jpg",
                       year = td[2],
                       steamUri = td[5],
                       originUri = td[6],
                       platform = td[7],
                       pievienoja = td[8];

                string descriptionUri = null,
                       descriptionType = null;

                if (!string.IsNullOrEmpty(steamUri))
                {
                    descriptionUri = steamUri;
                    descriptionType = "steam";
                }
                if (!string.IsNullOrEmpty(originUri))
                {
                    descriptionUri = originUri;
                    descriptionType = "origin";
                }

                CoverList.Children.Add(new CoverCreator().NewGame(year, name, genre, descriptionUri, descriptionType, coverUri, platform, "Game", pievienoja));
            }

            coversLoaded = true;

            main.filterPanel.Visibility = Visibility.Visible;
            main.progressBar.Visibility = Visibility.Collapsed;
        }
        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;

            double value = scroll.VerticalOffset;

            if (value >= scroll.ScrollableHeight && value != 0)
            {
                if(coversLoaded)
                {
                    switch(category)
                    {
                        case "Movies":
                            LoadMovies(movieUri, from += count, count, year, genre, order, name);
                            break;
                        case "TV":
                            LoadTv(tvUri, from += count, count, year, genre, order, name);
                            break;
                        case "Games":
                            LoadGames(gameUri, from += count, count, year, genre, order, name, platform);
                            break;
                    }
                }
                    
            }
        }
    }
}
