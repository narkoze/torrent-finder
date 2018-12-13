using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TM2.Windows
{
    public partial class Comments : Window
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;

        private string uri { get; }
        private string title { get; }
        private int commentsCount { get; }

        private List<string> lstUsers = new List<string>();
        private List<string> lstComments = new List<string>();
        private List<string> lstImages = new List<string>();

        public Comments(int _commentsCount, string _uri, string _title)
        {
            commentsCount = _commentsCount;
            uri = _uri;
            title = _title;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            PrgSearch.Visibility = Visibility.Visible;
            Title = title;

            if (commentsCount % 10 == 1 && commentsCount != 11)
                CommentsCount.Content = commentsCount.ToString() + " Komentārs";
            else
                CommentsCount.Content = commentsCount.ToString() + " Komentāri";


            if (uri.Contains("fano"))
            {
                GetCommentsFromFano();
            }
            if (uri.Contains("kinozal"))
            {
                GetCommentsFromKinozal();
            }
            if (uri.Contains("filebase"))
            {
                GetCommentsFromFilebase();
            }
        }

        private async void GetCommentsFromFano()
        {
            for (var p = 0; ; p++)
            {
                var content = await main.fanoWeb.DownloadDataTaskAsync(uri + "&page=" + p);
                var doc = new HtmlDocument();
                var encoded = Encoding.UTF8.GetString(content);
                doc.LoadHtml(encoded);

                var sub = doc.DocumentNode.SelectNodes(".//p[@class='sub']");
                var mainline1 = doc.DocumentNode.SelectNodes(".//table[@class='mainline1']");

                if (sub != null)
                {
                    foreach (var users in sub)
                    {
                        if (users.InnerText.Contains("Autors"))
                            lstUsers.Add(users.SelectSingleNode("a").InnerText);
                    }

                    foreach (var comments in mainline1)
                    {
                        lstComments.Add(comments.SelectSingleNode(".//td[@class='text']").SelectSingleNode(".//p").InnerText);
                        lstImages.Add(comments.SelectSingleNode(".//img[@alt='avatar']").GetAttributeValue("src", ""));
                    }

                    var singlePage = doc.DocumentNode.SelectSingleNode(".//a[@name='page']");
                    if (singlePage != null)
                    {
                        var howManyPages = singlePage.ParentNode.NextSibling.NextSibling.NextSibling.InnerText.Replace("&nbsp;", string.Empty);
                        if (string.IsNullOrEmpty(howManyPages))
                        {
                            break;
                        }
                    }

                }
                else
                {
                    break;
                }
            }
            CreateCommentBox();
        }
        private void CreateCommentBox()
        {
            lstUsers.Reverse();
            lstComments.Reverse();
            lstImages.Reverse();

            using (var e1 = lstUsers.GetEnumerator())
            using (var e2 = lstComments.GetEnumerator())
            using (var e3 = lstImages.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                {
                    var user = e1.Current;
                    var comment = System.Web.HttpUtility.HtmlDecode(e2.Current);
                    var image = e3.Current;

                    Grid grid = new Grid
                    {
                        Margin = new Thickness(4)
                    };

                    var rd = new RowDefinition { Height = GridLength.Auto };
                    var rd2 = new RowDefinition { Height = GridLength.Auto };
                    grid.RowDefinitions.Add(rd);
                    grid.RowDefinitions.Add(rd2);

                    var cd = new ColumnDefinition { Width = GridLength.Auto };
                    var cd2 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd);
                    grid.ColumnDefinitions.Add(cd2);

                    TextBlock txtUser = new TextBlock
                    {
                        Text = user,
                        Margin = new Thickness(4),
                        FontWeight = FontWeights.Bold,
                        Foreground = (Brush)new BrushConverter().ConvertFromString("#FFFF946B"),
                        FontSize = 16.667,
                    };
                    Grid.SetRow(txtUser, 0);
                    Grid.SetColumn(txtUser, 0);
                    Grid.SetColumnSpan(txtUser, 2);
                    grid.Children.Add(txtUser);

                    //string ext = System.IO.Path.GetExtension(image);
                    //if (ext == ".gif")
                    {
                        //WebBrowser wb = new WebBrowser();
                        //wb.NavigateToString("<img src='" + image + "' style='width:150px;height:auto;max-width:100px; max-height:150px'/>");
                        //wb.LoadCompleted += Wb_LoadCompleted;

                        //Grid.SetRow(wb, 1);
                        //Grid.SetColumn(wb, 0);
                        //grid.Children.Add(wb);
                    }
                    //else

                    Image img = new Image();

                    if (!string.IsNullOrEmpty(image))
                        img.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));

                    img.Width = 100;

                    Grid.SetRow(img, 1);
                    Grid.SetColumn(img, 0);
                    grid.Children.Add(img);

                    TextBox txtComment = new TextBox
                    {
                        Text = comment,
                        Margin = new Thickness(4),
                        TextWrapping = TextWrapping.Wrap,
                        IsReadOnly = true,
                        BorderThickness = new Thickness(0),
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = Brushes.Transparent
                    };
                    Grid.SetRow(txtComment, 1);
                    Grid.SetColumn(txtComment, 1);
                    //Grid.SetColumnSpan(txtComment, 2);
                    grid.Children.Add(txtComment);

                    //if(!string.IsNullOrEmpty(txtComment.Text.Trim()))
                    Container.Children.Add(grid);
                }
            }
            PrgSearch.Visibility = Visibility.Hidden;

        }

        private async void GetCommentsFromKinozal()
        {
            for (var p = 0; ; p++)
            {
                var content = await main.kinozalWeb.DownloadDataTaskAsync(uri.Replace("details", "comment") + "&page=" + p);
                var encoded = Encoding.GetEncoding(1251).GetString(content);
                var doc = new HtmlDocument();
                doc.LoadHtml(encoded);

                //document.getElementsByClassName('mn2 cmet_bx')[0].innerText
                //"Пока нет комментариев к данной раздаче."
                var cmet_bx = doc.DocumentNode.SelectNodes(".//div[@class='mn2 cmet_bx']");
                if (cmet_bx != null)
                {
                    var isOrNot = cmet_bx[0].InnerText;
                    if (isOrNot != "Пока нет комментариев к данной раздаче.")
                    {
                        foreach (var c in cmet_bx)
                        {
                            var user = c.SelectSingleNode(".//dt").SelectSingleNode(".//a");
                            if (user != null)
                            {
                                lstUsers.Add(user.InnerText);
                            }

                            lstComments.Add(c.SelectSingleNode(".//div[@class='tx']").InnerText);

                            var imageSource = c.FirstChild.NextSibling;
                            string image = string.Empty;
                            if (imageSource != null && imageSource.Name == "img")
                            {
                                image = imageSource.GetAttributeValue("src", "");
                            }
                            lstImages.Add(image);

                        }

                        var pages = doc.DocumentNode.SelectSingleNode(".//div[@class='paginator']");
                        if (pages == null)
                            break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            CreateCommentBox();
        }

        private async void GetCommentsFromFilebase()
        {
            var content = await main.FilebaseWeb.DownloadDataTaskAsync(uri);
            var encoded = Encoding.UTF8.GetString(content);
            var doc = new HtmlDocument();
            doc.LoadHtml(encoded);

            int commentsCount = 0;
            var bline = doc.DocumentNode.SelectSingleNode(".//div[@class='b-line']");
            if (bline != null)
            {
                string regCount = Regex.Match(bline.InnerText, @"\d+").Value;
                int.TryParse(regCount, out commentsCount);
            }

            string fid = Regex.Match(uri, @"\d+").Value;

            int counter = 0;
            for (var p = 1; ; p++)
            {
                var data = new NameValueCollection
                {
                    {"p", p.ToString() },
                    {"fid", fid }
                };
                var loadData = await main.FilebaseWeb.UploadValuesTaskAsync(new Uri("http://www.filebase.ws/ajax_comments.php?do=loadPage&ajax=1"), data);
                var jsonReader = JsonReaderWriterFactory.CreateJsonReader(loadData, new System.Xml.XmlDictionaryReaderQuotas());
                var root = XElement.Load(jsonReader);
                var c = root.XPathSelectElement("//c").Value;
                doc.LoadHtml(c);

                var unique = doc.DocumentNode.SelectNodes(".//div[contains(@class,'comment')]");
                if (unique != null)
                {
                    foreach (var i in unique)
                    {
                        counter++;
                        lstUsers.Add(i.SelectSingleNode(".//div[@class='panel']").FirstChild.NextSibling.InnerText);
                        lstComments.Add(i.SelectSingleNode(".//div[@class='entry']").InnerText);
                        lstImages.Add("http://www.filebase.ws" + i.SelectSingleNode(".//img[@class='avatar']").GetAttributeValue("src", ""));
                    }
                }

                if (counter == commentsCount)
                {
                    break;
                }
            }

            CreateCommentBox();
        }
    }
}
