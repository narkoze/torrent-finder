using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TM2.Controls
{
    public partial class MovieFilter : UserControl
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        WebClient webClient;
        HtmlDocument doc = new HtmlDocument();
        SortedSet<string> sortedSet = new SortedSet<string>();
        List<string> list;
        Converter converter = new Converter();

        private string movieFilterUri = "http://www.piemeram.lv/JjxJNdbf/MoviesFilter.php";

        public MovieFilter()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            UpdateYears();
            UpdateGenres();
        }
        private async void UpdateYears()
        {
            webClient = new WebClient();
            byte[] content = await webClient.DownloadDataTaskAsync(movieFilterUri + "?req=year&genre=");
            string encodedYears = Encoding.Default.GetString(content);
            doc.LoadHtml(encodedYears);

            IEnumerable<string[]> table = doc.DocumentNode.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            sortedSet.Clear();

            foreach (var td in table)
            {
                sortedSet.Add(td[1]);
            }

            list = new List<string> { "Visi" };
            list.AddRange(sortedSet.Reverse());

            foreach (string i in list)
            {
                cmbYear.Items.Add(i);
            }
        }
        private async void UpdateGenres()
        {
            webClient = new WebClient();
            byte[] content = await webClient.DownloadDataTaskAsync(movieFilterUri + "?req=genre&year=");
            string encodedGenres = Encoding.Default.GetString(content);
            doc.LoadHtml(encodedGenres);

            IEnumerable<string[]> table = doc.DocumentNode.Descendants("tr").Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            sortedSet.Clear();

            foreach (var td in table)
            {
                string[] split = td[0].Split(',').Select(p => p.Trim()).ToArray();
                foreach (var i in split)
                {
                    string convert = converter.MovieGenreEngToLv(i);
                    if(!string.IsNullOrEmpty(convert))
                        sortedSet.Add(convert);
                }
            }

            list = new List<string> { "Visi" };
            list.AddRange(sortedSet);

            foreach (string i in list)
            {
                cmbGenre.Items.Add(i);
            }
        }

        private void Search()
        {
            main.filterPanel.Visibility = Visibility.Hidden;
            main.containerCovers.Children.Clear();

            string genre = cmbGenre.Text == "Visi" ? "" : converter.MovieGenreLvToEng(cmbGenre.Text) + "|" + converter.MovieGenreLvToRus(cmbGenre.Text);

            int? yearParse = cmbYear.Text == "Visi" ? 0 : int.Parse(cmbYear.Text);
            int? year = null;
            if (yearParse == 0)
                year = null;
            else
                year = yearParse;

            main.containerCovers.Children.Add(new Covers("Movies", cmbOrder.Text, genre, year, txtName.Text, null));

            imgReset.Visibility = Visibility.Visible;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search();
        }

        private void imgReset_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cmbOrder.SelectedIndex = 0;
            cmbGenre.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
            txtName.Text = "";
            Search();
            imgReset.Visibility = Visibility.Collapsed;
        }
    }
}
