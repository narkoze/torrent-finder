using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Web;

namespace TM2.Controls
{
    public partial class TorrentList : UserControl
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        TabControler tabControler = new TabControler();
        CoverCreator coverCreator = new CoverCreator();
        FanoCmd fanoCmd = new FanoCmd();
        KinozalCmd kinozalCmd = new KinozalCmd();
        FilebaseCmd filebaseCmd = new FilebaseCmd();
        Converter converter = new Converter();
        HtmlDocument doc = new HtmlDocument();
        Config config = new Config();
        public ListCollectionView MyCollectionView { get; set; }
        public ObservableCollection<Torrents> torrents = new ObservableCollection<Torrents>();
        public HashSet<string> categorySet = new HashSet<string>();
        public bool fanoConnected, fanoCompleted;
        public bool kinozalConnected, kinozalCompleted;
        public bool filebaseConnected, filebaseCompleted;      
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private int multiSearchKinozal = 0;
        private int multiSearchFilebase= 0;
        public TorrentList()
        {
            fanoConnected = (main.groupBoxFano.IsVisible && !main.progressBarFano.IsVisible) ? true : false;
            kinozalConnected = (main.groupBoxKinozal.IsVisible && !main.progressBarKinozal.IsVisible) ? true : false;
            filebaseConnected = (main.groupBoxFilebase.IsVisible && !main.progressBarFilebase.IsVisible) ? true : false;

            InitializeComponent();
            MyCollectionView = new ListCollectionView(torrents);
            tList.ItemsSource = MyCollectionView;
        }
        private void ThreadsCompleted()
        {
            if(fanoCompleted)
            {
                main.progressBarFano.Visibility = Visibility.Hidden;
            }
            if (kinozalCompleted)
            {
                main.progressBarKinozal.Visibility = Visibility.Hidden;
            }
            if (filebaseCompleted)
            {
                main.progressBarFilebase.Visibility = Visibility.Hidden;
            }
            if (fanoCompleted && kinozalCompleted && filebaseCompleted)
            {
                main.progressBar.Visibility = Visibility.Collapsed;
                main.containerCovers.IsEnabled = true;
                main.containerCovers.Cursor = Cursors.Arrow;
            }
        }

        public async Task Search(string Text, string Category, string Genre, string Year, bool SearchByDate)
        {
            string What = Text.Replace("'", "")
                              .Replace(":", "");
            if (SearchByDate)
                SortById();
            else
                SortByDate();

            switch (Category)
            {
                case "Movie":
                    Category = "Filmas";
                    break;
                case "TV":
                    Category = "TV";
                    break;
                case "Game":
                    Category = "Spēles";
                    break;
            }
#region Fano
            if (fanoConnected)
            {
                if(Genre == "Visi" && Year == "Visi")
                {
                    main.progressBarFano.Visibility = Visibility.Visible;
                    if (await fanoCmd.CheckLogin())
                    {
                        SearchFano("https://www.fano.in/browse_old.php?search=" + What + converter.CategoryToParametersFano(Category), SearchByDate);
                    }
                    else
                    {
                        if (config.FanoAccountExist())
                        {
                            await fanoCmd.Login(config.Read("account", "fanologin"), PasswordSecurity.Decrypt(config.Read("account", "fanopassword"), main.mySite));
                            SearchFano("https://www.fano.in/browse_old.php?search=" + What + converter.CategoryToParametersFano(Category), SearchByDate);
                        }
                    }
                }
                else
                {
                    fanoCompleted = true;
                    ThreadsCompleted();
                }
            }
            else
            {
                fanoCompleted = true;
                ThreadsCompleted();
            }
#endregion
#region Kinozal
            if (kinozalConnected)
            {
                main.progressBarKinozal.Visibility = Visibility.Visible;
                if(await kinozalCmd.CheckLogin() && Category != "Telefonam" && Category != "Mācības")
                {
                    string uri = "http://kinozal.tv/browse.php?s=" + What;

                    if (Category == "Visas")
                    {
                        await SearchKinozal(uri, SearchByDate, 1);
                    }
                    else if (Category == "Filmas" && Genre == "Visi")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string uris = "http://kinozal.tv/browse.php?s=" + What + converter.CategoryToParametersKinozal(Category + i.ToString());
                            if (Year != "Visi")
                                uris += "&d=" + Year;
                            await SearchKinozal(uris, false, 2);
                        }
                    }
                    else if (Category == "Filmas")
                    {
                        uri += converter.GenreToParametersKinozal(Genre);
                        if (Year != "Visi")
                            uri += "&d=" + Year;
                        await SearchKinozal(uri, SearchByDate, 1);
                    }
                    else if (Category == "Programmas" || Category == "Spēles" || Category == "Mūzika" || Category == "Videoklipi" || Category == "Erotika" || Category == "Multfilmas")
                    {
                        uri += converter.CategoryToParametersKinozal(Category);
                        if (Year != "Visi")
                            uri += "&d=" + Year;
                        await SearchKinozal(uri, SearchByDate, 1);
                    }
                    else if (Category == "TV")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string uris = "http://kinozal.tv/browse.php?s=" + What + converter.CategoryToParametersKinozal(Category + i.ToString());
                            if (Year != "Visi")
                                uris += "&d=" + Year;

                            await SearchKinozal(uris, false, 3);
                        }
                    }
                    else if (Category == "Grāmatas")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string uris = "http://kinozal.tv/browse.php?s=" + What + converter.CategoryToParametersKinozal(Category + i.ToString());
                            if (Year != "Visi")
                                uris += "&d=" + Year;
                            await SearchKinozal(uris, false, 2);
                        }
                    }
                    else
                    {
                        kinozalCompleted = true;
                        ThreadsCompleted();
                    }
                }
                else
                {
                    kinozalCompleted = true;
                    ThreadsCompleted();
                }
            }
            else
            {
                kinozalCompleted = true;
                ThreadsCompleted();
            }
#endregion
#region Filebase
            if (filebaseConnected)
            {
                main.progressBarFilebase.Visibility = Visibility.Visible;
                if (await filebaseCmd.CheckLogin() && Category != "Erotika" && Category != "Mācības" && Year == "Visi")
                {
                    string uri = "http://www.filebase.ws/torrents/search/?search=" + What + "&t=liveonly&c=0";
                    if (Category == "Visas")
                    {
                        await SearchFilebase(uri, SearchByDate, 1, false, true);
                    }
                    else if (Category == "Filmas" && Year == "Visi")
                    {
                        if(Genre == "Bojeviks/Kara" || Genre == "Trilleris/Detektīvs" || Genre == "Šausmu/Mistērija")
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                string uris = "http://www.filebase.ws/torrents/search/?search=" + What + converter.GenreToParametersFilebase(Genre + i.ToString()) + "&t=liveonly";
                                await SearchFilebase(uris, false, 2, true, false);
                            }
                        }
                        else
                        {
                            uri += converter.GenreToParametersFilebase(Genre);
                            await SearchFilebase(uri, false, 1, true, false);
                        }
                    }
                    else if (Category == "Multfilmas")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string uris = "http://www.filebase.ws/torrents/search/?search=" + What + converter.CategoryToParametersFilebase(Category + i.ToString()) + "&t=liveonly";
                            await SearchFilebase(uris, false, 2, true, false);
                        }
                    }
                    else if (Category == "TV" || Category == "Mūzika")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string uris = "http://www.filebase.ws/torrents/search/?search=" + What + converter.CategoryToParametersFilebase(Category + i.ToString()) + "&t=liveonly";
                            await SearchFilebase(uris, false, 2, false, true);
                        }
                    }
                    else if (Category == "Programmas" || Category == "Spēles" || Category == "Videoklipi" || Category == "Telefonam" || Category == "Grāmatas")
                    {
                        uri += converter.CategoryToParametersFilebase(Category);
                        await SearchFilebase(uri, false, 1, false, true);
                    }
                    else
                    {
                        filebaseCompleted = true;
                        ThreadsCompleted();
                    }
                }
                else
                {
                    filebaseCompleted = true;
                    ThreadsCompleted();
                }
            }
            else
            {
                filebaseCompleted = true;
                ThreadsCompleted();
            }
#endregion
        }
        private async void SearchFano(string Uri, bool SearchByDate)
        {
            int id = 0;
            for (var p = 0; ; p++)
            {
                byte[] content = await main.fanoWeb.DownloadDataTaskAsync(Uri + "&page=" + p);
                string encoded = Encoding.UTF8.GetString(content);
                doc.LoadHtml(encoded);

                HtmlNode table = doc.GetElementbyId("line");
                if (table != null)
                {                   
                    for (var i = 1; i < table.SelectNodes("tr").Count; i++)
                    {
                        DateTime date = Convert.ToDateTime(converter.DateFano(table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectNodes(".//font")[0].InnerText.Replace("&nbsp;", " ")));
                        if (SearchByDate)
                        {
                            DateTime selectedDate = (DateTime)main.txtToday.Tag;

                            string piesprausts = table.SelectNodes("tr")[i].SelectNodes(".//td")[1].FirstChild.InnerText.Trim();
                            if (date == selectedDate.AddDays(-1) && piesprausts != "Piesprausts")
                                goto End;

                            if (date != selectedDate)
                                continue;
                        }
                        string type = converter.TypeFano(table.SelectNodes("tr")[i].SelectNodes(".//td")[0].SelectSingleNode(".//img[@alt]").GetAttributeValue("alt", ""));
                        string name = HttpUtility.HtmlDecode(table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectSingleNode(".//b").InnerText);
                        string size = converter.SizeRussianToEng(table.SelectNodes("tr")[i].SelectNodes(".//td")[4].InnerHtml.Replace("<br>", " "));
                        double fileSize = converter.ConvertToKiloBytes(size);
                        int seed = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[6].InnerText);
                        int leech = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[7].InnerText);
                        int comments = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[2].SelectSingleNode(".//a[@class='loadcomments']").InnerText);
                        string downloadUri = HttpUtility.UrlDecode("https://www.fano.in/" + table.SelectNodes("tr")[i].SelectNodes(".//td")[0].SelectSingleNode(".//a[@id='a_down']").GetAttributeValue("href", ""));
                        string detailsUri = HttpUtility.UrlDecode("https://www.fano.in/details.php?id=" + table.SelectNodes("tr")[i].GetAttributeValue("id", "").Substring(4));

                        categorySet.Add(type);

                        torrents.Add(new Torrents()
                        {
                            Id = ++id,
                            Tracker = "/TM2;component/Images/Trackers/fano.in.png",
                            Type = type,
                            Name = name,
                            Size = size,
                            FileSize = fileSize,
                            Seed = seed,
                            Leech = leech,
                            Date = date,
                            Comments = comments,
                            DownloadUri = downloadUri,
                            DetailsUri = detailsUri,
                        });
                    }
                    if (!SearchByDate)
                        break;
                }
                if (!SearchByDate)
                    break;
            }
            End:;
            fanoCompleted = true;
            ThreadsCompleted();
        }
        private async Task SearchKinozal(string Uri, bool SearchByDate, int MultiSearchCount)
        {
            int id = 0;
            for (var p = 0; ; p++)
            {
                byte[] content = await main.kinozalWeb.DownloadDataTaskAsync(Uri + "&page=" + p);
                string encoded = Encoding.GetEncoding(1251).GetString(content);
                doc.LoadHtml(encoded);

                HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[@class='t_peer w100p']");
                if (table != null)
                {
                    for (var i = 1; i < table.SelectNodes("tr").Count; i++)
                    {
                        DateTime date = Convert.ToDateTime(converter.DateKinozal(table.SelectNodes("tr")[i].SelectNodes(".//td")[6].InnerText));
                        if (SearchByDate)
                        {
                            DateTime selectedDate = (DateTime)main.txtToday.Tag;

                            if (date == selectedDate.AddDays(-1))
                                goto End;
                            if (date != selectedDate)
                                continue;
                        }
                        string type = converter.TypeKinozal(table.SelectNodes("tr")[i].SelectNodes(".//td")[0].SelectSingleNode(".//img[@src]").GetAttributeValue("src", ""));
                        string name = RemoveEmailCode(HttpUtility.HtmlDecode(table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectSingleNode(".//a").InnerText));
                        string size = converter.SizeRussianToEng(table.SelectNodes("tr")[i].SelectNodes(".//td")[3].InnerText);
                        double fileSize = converter.ConvertToKiloBytes(size);
                        int seed = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[4].InnerText);
                        int leech = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[5].InnerText);
                        int comments = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[2].InnerText);
                        string section = table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectSingleNode(".//a").GetAttributeValue("href", "");
                        string torrentId = section.Substring(16, section.Length - 16);
                        string downloadUri = HttpUtility.UrlDecode("http://dl.kinozal.tv/download.php?id=" + torrentId);
                        string detailsUri = HttpUtility.UrlDecode("http://kinozal.tv" + section);

                        // Specific /////////////////////////////////////////////
                        string newType = converter.NameToTypeKinozal(name, type);
                        if (newType != "")
                            type = newType;
                        /////////////////////////////////////////////////////////

                        categorySet.Add(type);

                        torrents.Add(new Torrents()
                        {
                            Id = ++id,
                            Tracker = "/TM2;component/Images/Trackers/kinozal.tv.png",
                            Type = type,
                            Name = name,
                            Size = size,
                            FileSize = fileSize,
                            Seed = seed,
                            Leech = leech,
                            Date = date,
                            Comments = comments,
                            DownloadUri = downloadUri,
                            DetailsUri = detailsUri,
                        });
                    }
                    if (!SearchByDate)
                        break;
                }
                if (!SearchByDate)
                    break;
            }
            End:;

            if(MultiSearchCount == ++multiSearchKinozal)
            {
                kinozalCompleted = true;
                ThreadsCompleted();
            }
        }
        private async Task SearchFilebase(string Uri, bool SearchByDate, int MultiSearchCount, bool Movie, bool Other)
        {
            int id = 0;
            for (var p = 0; ; p++)
            {
                byte[] content = await main.FilebaseWeb.DownloadDataTaskAsync(Uri + "&p=" + p);
                string encoded = Encoding.UTF8.GetString(content);
                doc.LoadHtml(encoded);

                HtmlNode table = doc.GetElementbyId("torrents_list");
                if (table != null)
                {
                    for (var i = 1; i < table.SelectNodes("tr").Count; i++)
                    {
                        if (table.SelectNodes("tr").Count < 2 || table.SelectNodes("tr")[1].InnerText.Trim() == "---")
                            goto End;

                        string name = HttpUtility.HtmlDecode(table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectSingleNode(".//a").InnerText.Trim());
                        DateTime date = Convert.ToDateTime(converter.DateFilebase(table.SelectNodes("tr")[i].SelectNodes(".//td")[3].InnerText));
                        if (SearchByDate)
                        {
                            DateTime selectedDate = (DateTime)main.txtToday.Tag;

                            if (date == selectedDate.AddDays(-1) && !name.Contains("Bless MuOnline Episode 8"))
                                goto End;
                            if (date != selectedDate)
                                continue;
                        }
                        string type = converter.TypeFilebase(table.SelectNodes("tr")[i].SelectNodes(".//td")[0].SelectSingleNode(".//img[@alt]").GetAttributeValue("alt", ""));
                        string size = converter.SizeRussianToEng(table.SelectNodes("tr")[i].SelectNodes(".//td")[4].InnerText);
                        double fileSize = converter.ConvertToKiloBytes(size);
                        int seed = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[6].InnerText);
                        int leech = int.Parse(table.SelectNodes("tr")[i].SelectNodes(".//td")[7].InnerText);

                        string commentSource = table.SelectNodes("tr")[i].SelectNodes(".//td")[2].InnerText;
                        int comments;
                        if (commentSource == "--")
                            comments = 0;
                        else
                            comments = int.Parse(commentSource);

                        string torrentId = table.SelectNodes("tr")[i].GetAttributeValue("data-fid", "");
                        string section = table.SelectNodes("tr")[i].SelectNodes(".//td")[1].SelectSingleNode(".//a").GetAttributeValue("href", "");

                        string downloadUri = HttpUtility.UrlDecode("http://www.filebase.ws/download.php?id=" + torrentId);
                        string detailsUri = HttpUtility.UrlDecode("http://www.filebase.ws" + section);

                        // Specific /////////////////////////////////////////////
                        string newType = converter.NameToTypeFilebase(name, type);
                        if (newType != "")
                            type = newType;
                        /////////////////////////////////////////////////////////
                        if (!name.Contains("Bless MuOnline Episode 8")) //Remove advertisment
                        {
                            if (Movie && type.Contains("Filma") || Movie && type.Contains("Multfilma"))
                            {
                                categorySet.Add(type);

                                torrents.Add(new Torrents()
                                {
                                    Id = ++id,
                                    Tracker = "/TM2;component/Images/Trackers/filebase.ws.png",
                                    Type = type,
                                    Name = name,
                                    Size = size,
                                    FileSize = fileSize,
                                    Seed = seed,
                                    Leech = leech,
                                    Date = date,
                                    Comments = comments,
                                    DownloadUri = downloadUri,
                                    DetailsUri = detailsUri,
                                });
                            }
                            if (Other)
                            {
                                categorySet.Add(type);

                                torrents.Add(new Torrents()
                                {
                                    Id = ++id,
                                    Tracker = "/TM2;component/Images/Trackers/filebase.ws.png",
                                    Type = type,
                                    Name = name,
                                    Size = size,
                                    FileSize = fileSize,
                                    Seed = seed,
                                    Leech = leech,
                                    Date = date,
                                    Comments = comments,
                                    DownloadUri = downloadUri,
                                    DetailsUri = detailsUri,
                                });
                            }
                        }
                    }
                    if (!SearchByDate)
                        break;
                }
                if (!SearchByDate)
                    break;
            }
            End:;

            if (MultiSearchCount == ++multiSearchFilebase)
            {
                filebaseCompleted = true;
                ThreadsCompleted();
            }
        }

        private string RemoveEmailCode(string name)
        {
            string code = "/* <![CDATA[ */!function(t,e,r,n,c,a,p){try{t=document.currentScript||function(){for(t=document.getElementsByTagName('script'),e=t.length;e--;)if(t[e].getAttribute('data-cfhash'))return t[e]}();if(t&&(c=t.previousSibling)){p=t.parentNode;if(a=c.getAttribute('data-cfemail')){for(e='',r='0x'+a.substr(0,2)|0,n=2;a.length-n;n+=2)e+='%'+('0'+('0x'+a.substr(n,2)^r).toString(16)).slice(-2);p.replaceChild(document.createTextNode(decodeURIComponent(e)),c)}p.removeChild(t)}}catch(u){}}()/* ]]> */";
            return name.Replace(code, "");
        }
        private void SortByDate()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tList.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        }
        private void SortById()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tList.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
        }
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                tList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);

            if (column.Tag.ToString() != "Tracker" && column.Tag.ToString() != "Leech" && column.Tag.ToString() != "Seed" && column.Tag.ToString() != "Comments")
                AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);

            tList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Image img = btn.Content as Image;
            DownloadTorrent(img.Tag.ToString(), btn.Tag.ToString());
        }
        private void txtComments_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txt = sender as TextBox;

            Windows.Comments comments = new Windows.Comments(int.Parse(txt.Text), txt.Tag.ToString(), txt.ToolTip.ToString());
            comments.Owner = Application.Current.MainWindow;
            comments.Show();
        }

        private async void DownloadTorrent(string Uri, string Name)
        {
            tList.IsEnabled = false;

            if (Uri.StartsWith("https://www.fano.in"))
                await fanoCmd.DownloadTorrent(Uri, Name);
            else if (Uri.StartsWith("http://dl.kinozal.tv"))
                await kinozalCmd.DownloadTorrent(Uri, Name);
            else if (Uri.StartsWith("http://www.filebase.ws"))
                await filebaseCmd.DownloadTorrent(Uri, Name);

            tList.IsEnabled = true;
        }
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Image img = btn.Content as Image;
            Details(img.Tag.ToString(), btn.Tag.ToString());
        }
        private void Details(string Uri, string Name)
        {
            if (Uri.StartsWith("https://www.fano.in"))
                main.tabControl.Items.Add(tabControler.AddTab(Name, "/TM2;component/Images/Trackers/fano.in.png", new Tab(coverCreator.MyWebBrowser(Uri, "Fano"))));
            else if (Uri.StartsWith("http://kinozal.tv"))
                main.tabControl.Items.Add(tabControler.AddTab(Name, "/TM2;component/Images/Trackers/kinozal.tv.png", new Tab(coverCreator.MyWebBrowser(Uri, "Kinozal"))));
            else if (Uri.StartsWith("http://www.filebase.ws"))
                main.tabControl.Items.Add(tabControler.AddTab(Name, "/TM2;component/Images/Trackers/filebase.ws.png", new Tab(coverCreator.MyWebBrowser(Uri, "Filebase"))));
        }
    }
    public class Torrents : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    //NotifyPropertyChanged("Id");
                }
            }
        }
        private string tracker;
        public string Tracker
        {
            get { return tracker; }
            set
            {
                if (tracker != value)
                {
                    tracker = value;
                    //NotifyPropertyChanged("Tracker");
                }
            }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    //NotifyPropertyChanged("Type");
                }
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string size;
        public string Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    //NotifyPropertyChanged("Size");
                }
            }
        }
        private double fileSize;
        public double FileSize
        {
            get { return fileSize; }
            set
            {
                if (fileSize != value)
                {
                    fileSize = value;
                    //NotifyPropertyChanged("FileSize");
                }
            }
        }
        private int seed;
        public int Seed
        {
            get { return seed; }
            set
            {
                if (seed != value)
                {
                    seed = value;
                    //NotifyPropertyChanged("Seed");
                }
            }
        }
        private int leech;
        public int Leech
        {
            get { return leech; }
            set
            {
                if (leech != value)
                {
                    leech = value;
                    //NotifyPropertyChanged("Leech");
                }
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    //NotifyPropertyChanged("Date");
                }
            }
        }
        private int comments;
        public int Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    //NotifyPropertyChanged("Comments");
                }
            }
        }
        private string downloadUri;
        public string DownloadUri
        {
            get { return downloadUri; }
            set
            {
                if (downloadUri != value)
                {
                    downloadUri = value;
                    //NotifyPropertyChanged("DownloadUri");
                }
            }
        }
        private string detailsUri;
        public string DetailsUri
        {
            get { return detailsUri; }
            set
            {
                if (detailsUri != value)
                {
                    detailsUri = value;
                    //NotifyPropertyChanged("DetailsUri");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                    (
                            AdornedElement.RenderSize.Width - 15,
                            (AdornedElement.RenderSize.Height - 5) / 2
                    );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
