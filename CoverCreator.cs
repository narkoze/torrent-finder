using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;

namespace TM2
{
    class CoverCreator
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        TabControler tabControler = new TabControler();
        Converter converter = new Converter();
        FanoCmd fanoCmd = new FanoCmd();
        Config config = new Config();

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetSetCookieEx(string url, string cookieName, StringBuilder cookieData, Int32 dwFlags, IntPtr lpReserved);

        public Border NewMovie(string year, string name, string genre, bool genreRussian, string sourceUri, string coverUri, string Type, double ratio, int votes, string pievienoja)
        {
            return MyBorder(false, MyGrid(
                   Year(year),
                   Name(name),
                   Genre(genre, genreRussian),
                   SourceUri(sourceUri),
                   CoverUri(coverUri, Type),
                   YoutubeUri("movie"),
                   Ratio(ratio, votes),
                   Thanks(pievienoja)));
        }
        public Border NewTv(string year, string name, string genre, bool genreRussian, string sourceUri, string coverUri, string Type, double ratio, int votes, string pievienoja)
        {
            return MyBorder(false, MyGrid(
                   Year(year),
                   Name(name),
                   Genre(genre, genreRussian),
                   SourceUri(sourceUri),
                   CoverUri(coverUri, Type),
                   YoutubeUri("movie"),
                   Ratio(ratio, votes),
                   Thanks(pievienoja)));
        }
        public Border NewGame(string year, string name, string genre, string descriptionUri, string descriptionType, string coverUri, string platform, string Type, string pievienoja)
        {
            return MyBorder(true, MyGameGrid(
                   Year(year),
                   Name(name),
                   Genre(converter.GameGenreEngToLv(genre), null),
                   Description(descriptionUri, descriptionType),
                   CoverUri(coverUri, Type),
                   YoutubeUri("game"),
                   Platform(converter.PlatformsToUri(platform)),
                   Thanks(pievienoja)));
        }

        private Border MyBorder(bool game, Grid grid)
        {
            LinearGradientBrush lgb = new LinearGradientBrush
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };

            GradientStop gs1 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFD, 0xCF, 0xBE),
                Offset = 1
            };

            GradientStop gs2 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF),
                Offset = 0.95
            };

            GradientStopCollection gscoll = lgb.GradientStops;
            gscoll.Add(gs1);
            gscoll.Add(gs2);

            Border border = new Border
            {
                BorderThickness = new Thickness(1),
                Height = 254,
                Margin = new Thickness(2),
                BorderBrush = lgb,

                Child = grid
            };

            if(game)
            {
                border.MouseEnter += MyGameBorder_MouseEnter;
                border.MouseLeave += MyGameBorder_MouseLeave;
            }
            else
            {
                border.MouseEnter += MyBorder_MouseEnter;
                border.MouseLeave += MyBorder_MouseLeave;
            }

            return border;
        }
        private Grid MyGrid(TextBox Year, TextBox Name, TextBox Genre, Image ImdbUri, Image CoverUri, Image YoutubeUri, DockPanel Ratio, Image Pievienoja)
        {
            Grid grid = new Grid { Background = Brushes.White };

            RowDefinition rd1 = new RowDefinition { Height = new GridLength(26) };
            RowDefinition rd2 = new RowDefinition { Height = GridLength.Auto };
            RowDefinition rd3 = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
            RowDefinition rd4 = new RowDefinition { Height = GridLength.Auto };

            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);
            grid.RowDefinitions.Add(rd3);
            grid.RowDefinitions.Add(rd4);

            ColumnDefinition cd1 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinition cd2 = new ColumnDefinition { Width = GridLength.Auto };

            grid.ColumnDefinitions.Add(cd1);
            grid.ColumnDefinitions.Add(cd2);

            Grid.SetRow(Year, 3);
            Grid.SetColumn(Year, 1);
            grid.Children.Add(Year);

            Grid.SetRow(Name, 0);
            Grid.SetColumnSpan(Name, 2);
            grid.Children.Add(Name);

            Grid.SetRow(Genre, 1);
            Grid.SetColumnSpan(Genre, 2);
            grid.Children.Add(Genre);

            Grid.SetRow(CoverUri, 2);
            Grid.SetColumnSpan(CoverUri, 2);
            grid.Children.Add(CoverUri);

            Grid.SetRow(ImdbUri, 2);
            Grid.SetColumnSpan(ImdbUri, 2);
            grid.Children.Add(ImdbUri);

            Grid.SetRow(YoutubeUri, 2);
            Grid.SetColumnSpan(YoutubeUri, 2);
            grid.Children.Add(YoutubeUri);

            Grid.SetRow(Ratio, 3);
            grid.Children.Add(Ratio);

            Grid.SetRow(Pievienoja, 2);
            Grid.SetColumnSpan(Pievienoja, 2);
            grid.Children.Add(Pievienoja);

            return grid;
        }
        private Grid MyGameGrid(TextBox Year, TextBox Name, TextBox Genre, Image DescriptionUri, Image CoverUri, Image YoutubeUri, StackPanel Platform, Image Pievienoja)
        {
            Grid grid = new Grid { Background = Brushes.White };

            RowDefinition rd1 = new RowDefinition { Height = new GridLength(26) };
            RowDefinition rd2 = new RowDefinition { Height = GridLength.Auto };
            RowDefinition rd3 = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
            RowDefinition rd4 = new RowDefinition { Height = GridLength.Auto };

            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);
            grid.RowDefinitions.Add(rd3);
            grid.RowDefinitions.Add(rd4);

            ColumnDefinition cd1 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinition cd2 = new ColumnDefinition { Width = GridLength.Auto };

            grid.ColumnDefinitions.Add(cd1);
            grid.ColumnDefinitions.Add(cd2);

            Grid.SetRow(Year, 3);
            Grid.SetColumn(Year, 1);
            grid.Children.Add(Year);

            Grid.SetRow(Name, 0);
            Grid.SetColumnSpan(Name, 2);
            grid.Children.Add(Name);

            Grid.SetRow(Genre, 1);
            Grid.SetColumnSpan(Genre, 2);
            grid.Children.Add(Genre);

            Grid.SetRow(CoverUri, 2);
            Grid.SetColumnSpan(CoverUri, 2);
            grid.Children.Add(CoverUri);

            if (DescriptionUri != null)
            {
                Grid.SetRow(DescriptionUri, 2);
                Grid.SetColumnSpan(DescriptionUri, 2);
                grid.Children.Add(DescriptionUri);
            }
            else
            {
                DescriptionUri = new Image();
                Grid.SetRow(DescriptionUri, 2);
                Grid.SetColumnSpan(DescriptionUri, 2);
                grid.Children.Add(DescriptionUri);
            }

            Grid.SetRow(YoutubeUri, 2);
            Grid.SetColumnSpan(YoutubeUri, 2);
            grid.Children.Add(YoutubeUri);

            Grid.SetRow(Platform, 3);
            grid.Children.Add(Platform);

            Grid.SetRow(Pievienoja, 2);
            Grid.SetColumnSpan(Pievienoja, 2);
            grid.Children.Add(Pievienoja);

            return grid;
        }

        private TextBox Year(string Year)
        {
            TextBox tb = new TextBox
            {
                Text = Year,
                VerticalAlignment = VerticalAlignment.Center,
                Background = null,
                BorderBrush = null,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsReadOnly = true,
                Focusable = false,
                FontSize = 9,
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Gray,
                Cursor = Cursors.Arrow,
                Margin = new Thickness(0, 2, 0, 0)
            };
            return tb;
        }
        private TextBox Name(string Name)
        {
            TextBox tb = new TextBox
            {
                Text = Name,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = null,
                BorderBrush = null,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsReadOnly = true,
                BorderThickness = new Thickness(0),
                SelectionBrush = Brushes.Gray
            };
            return tb;
        }
        private TextBox Genre(string Genre, bool? Russian)
        {
            string genre = string.Empty;
            if (Russian == false)
                genre = converter.MovieGenreEngToLv(Genre);
            else if (Russian == true)
                genre = converter.MovieGenreRusToLv(Genre);
            else if (Russian == null)
                genre = Genre;

            TextBox tb = new TextBox
            {
                Text = genre,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = null,
                BorderBrush = null,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsReadOnly = true,
                Focusable = false,
                FontSize = 9,
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Black,
                Cursor = Cursors.Arrow,
                TextWrapping = TextWrapping.Wrap
            };
            return tb;
        }
        private Image CoverUri(string CoverUri, string Type)
        {
            Image img = new Image { Tag = Type };
            img.PreviewMouseUp += CoverUri_PreviewMouseUp;
            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri(CoverUri, UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            img.Source = bi;
            img.Stretch = Stretch.Fill;
            img.IsEnabledChanged += delegate
            {
                if (img.IsEnabled)
                    img.Opacity = 1;
                else
                {
                    img.Opacity = 0.9;
                }
            };
            return img;
        }
        private Image SourceUri(string sourceUri)
        {
            Image img = new Image { Tag = sourceUri };
            if (!string.IsNullOrEmpty(sourceUri))
            {
                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
                img.PreviewMouseUp += sourceUri_PreviewMouseUp;
                img.MouseEnter += sourceUri_MouseEnter;
                img.MouseLeave += sourceUri_MouseLeave;
                img.Visibility = Visibility.Hidden;
                if(sourceUri.StartsWith("http://www.imdb"))
                    img.Source = new BitmapImage(new Uri("/TM2;component/Images/Covers/imdb.png", UriKind.RelativeOrAbsolute));
                else
                    img.Source = new BitmapImage(new Uri("/TM2;component/Images/Covers/kinopoisk.png", UriKind.RelativeOrAbsolute));
                img.Width = 48;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Bottom;
                img.Cursor = Cursors.Hand;
                img.Margin = new Thickness(0, 0, 48, 5);
            }
            return img;
        }
        private Image YoutubeUri(string type)
        {
            Image img = new Image();
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
            img.MouseEnter += Youtube_MouseEnter;
            img.MouseLeave += Youtube_MouseLeave;
            img.PreviewMouseUp += delegate (object sender, MouseButtonEventArgs e)
            {
                if (e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Middle)
                {
                    Image send = sender as Image;
                    Grid prnt = send.Parent as Grid;
                    if (prnt != null)
                    {
                        switch (type)
                        {
                            case "movie":
                                type = "trailer";
                                break;
                            case "game":
                                type = "gameplay";
                                break;
                        }
                        string movieName = prnt.Children.OfType<TextBox>().ElementAt(1).Text;

                        main.tabControl.Items.Add(tabControler.AddTab(movieName, "/TM2;component/Images/TabControl/youtube.png", new Controls.Tab(MyWebBrowser("https://www.youtube.com/results?search_query=" + movieName + " " + type, null))));

                        if (e.ChangedButton == MouseButton.Left)
                            main.tabControl.SelectedIndex = main.tabControl.Items.Count - 1;
                    }
                }
            };
            img.Visibility = Visibility.Hidden;
            img.Source = new BitmapImage(new Uri("/TM2;component/Images/Covers/youtube.png", UriKind.RelativeOrAbsolute));
            img.Width = 48;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.Cursor = Cursors.Hand;
            img.Margin = new Thickness(48, 0, 0, 5);
            return img;
        }
        private DockPanel Ratio(double Ratio, int Votes)
        {
            Image img = new Image();
            img.Width = 80;
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
            string star = "/TM2;component/Images/Stars/";
            if (Ratio >= 0 && Ratio < 1)
                img.Source = new BitmapImage(new Uri(star + "0.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 1 && Ratio < 2)
                img.Source = new BitmapImage(new Uri(star + "1.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 2 && Ratio < 3)
                img.Source = new BitmapImage(new Uri(star + "2.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 3 && Ratio < 4)
                img.Source = new BitmapImage(new Uri(star + "3.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 4 && Ratio < 5)
                img.Source = new BitmapImage(new Uri(star + "4.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 5 && Ratio < 6)
                img.Source = new BitmapImage(new Uri(star + "5.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 6 && Ratio < 7)
                img.Source = new BitmapImage(new Uri(star + "6.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 7 && Ratio < 8)
                img.Source = new BitmapImage(new Uri(star + "7.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 8 && Ratio < 9)
                img.Source = new BitmapImage(new Uri(star + "8.png", UriKind.RelativeOrAbsolute));
            if (Ratio >= 9 && Ratio < 10)
                img.Source = new BitmapImage(new Uri(star + "9.png", UriKind.RelativeOrAbsolute));
            if (Ratio == 10)
                img.Source = new BitmapImage(new Uri(star + "10.png", UriKind.RelativeOrAbsolute));

            DockPanel dp = new DockPanel();
            dp.Cursor = Cursors.Help;
            dp.MouseEnter += Dp_MouseEnter;
            dp.MouseLeave += Dp_MouseLeave;
            dp.ToolTip = RatioTooltip(Ratio, Votes);
            dp.HorizontalAlignment = HorizontalAlignment.Left;
            dp.Children.Add(img);

            return dp;
        }
        private ToolTip RatioTooltip(double Ratio, int Votes)
        {
            ToolTip tt = new ToolTip();

            Grid grid = new Grid();

            RowDefinition rd1 = new RowDefinition { Height = GridLength.Auto };
            RowDefinition rd2 = new RowDefinition { Height = GridLength.Auto };
            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);

            ColumnDefinition cd1 = new ColumnDefinition { Width = GridLength.Auto };
            ColumnDefinition cd2 = new ColumnDefinition { Width = GridLength.Auto };
            grid.ColumnDefinitions.Add(cd1);
            grid.ColumnDefinitions.Add(cd2);

            TextBlock tbRatio = new TextBlock
            {
                Text = Math.Round(Ratio,1).ToString(),
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)new BrushConverter().ConvertFromString("#FFE4C32E"),
                Margin = new Thickness(2, -2, 2, 4),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Grid.SetRow(tbRatio, 0);
            Grid.SetColumn(tbRatio, 0);
            grid.Children.Add(tbRatio);
            TextBlock tb10 = new TextBlock
            {
                Text = "/10",
                FontSize = 12,
                Foreground = Brushes.Gray,
                Margin = new Thickness(2),
                VerticalAlignment = VerticalAlignment.Bottom
            };
            Grid.SetRow(tb10, 0);
            Grid.SetColumn(tb10, 1);
            grid.Children.Add(tb10);
            Border border = new Border
            {
                BorderThickness = new Thickness(0,0,0,1),
                BorderBrush = Brushes.Silver,
                Margin = new Thickness(4,0,4,0)
            };
            Grid.SetRow(border, 0);
            Grid.SetColumnSpan(border, 2);
            grid.Children.Add(border);
            TextBlock tbVotes = new TextBlock
            {
                Text = Votes.ToString("#,0") + " balsis",
                FontSize = 12,
                Margin = new Thickness(4,0,4,0),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetRow(tbVotes, 1);
            Grid.SetColumnSpan(tbVotes, 2);
            grid.Children.Add(tbVotes);

            tt.Content = grid;
            return tt;
        }
        public WebBrowser MyWebBrowser(string Uri, string WebControl)
        {
            WebBrowser webBrowser = new WebBrowser();

            if (!InternetExplorerBrowserEmulation.IsBrowserEmulationSet())
            {
                InternetExplorerBrowserEmulation.SetBrowserEmulationVersion();
            }

            if (WebControl != null)
            {
                webBrowser.Visibility = Visibility.Hidden;
                switch (WebControl)
                {
                    case "Fano":
                        SetCookieForBrowserControl(main.fanoWeb._cookieContainer, new Uri(Uri));
                        webBrowser.LoadCompleted += delegate (object sender, NavigationEventArgs e)
                        {
                            webBrowser.InvokeScript("eval", "document.body.innerHTML=$('.mainouter')[0].outerHTML");
                            webBrowser.Visibility = Visibility.Visible;
                        };
                        break;
                    case "Kinozal":
                        SetCookieForBrowserControl(main.kinozalWeb._cookieContainer, new Uri(Uri));
                        webBrowser.LoadCompleted += delegate (object sender, NavigationEventArgs e)
                        {
                            webBrowser.InvokeScript("eval", "document.body.innerHTML=$('.mn_wrap')[0].outerHTML");
                            webBrowser.Visibility = Visibility.Visible;
                        };
                        break;
                    case "Filebase":
                        SetCookieForBrowserControl(main.FilebaseWeb._cookieContainer, new Uri(Uri));
                        webBrowser.LoadCompleted += delegate (object sender, NavigationEventArgs e)
                        {
                            webBrowser.InvokeScript("eval", "document.body.innerHTML=$('.def_block')[0].outerHTML");
                            webBrowser.Visibility = Visibility.Visible;
                        };
                        break;
                }                
            }

            webBrowser.Navigate(Uri);

            if (WebControl != null)
            {
                webBrowser.Navigating += delegate (object sender, NavigatingCancelEventArgs e)
                {
                    e.Cancel = true;
                };
            }

            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            object objComWebBrowser = null;

            if (fiComWebBrowser != null)
                objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);

            if (objComWebBrowser != null)
                objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { true });

            return webBrowser;
        }
        private Image Description(string Uri, string Type)
        {
            if (Uri != null)
            {
                Image img = new Image();
                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);

                string imgSrc = null;
                switch (Type)
                {
                    case "steam":
                        imgSrc = "/TM2;component/Images/Covers/steam.png";
                        break;
                    case "origin":
                        imgSrc = "/TM2;component/Images/Covers/origin.png";
                        break;
                }

                img.Visibility = Visibility.Hidden;
                img.Source = new BitmapImage(new Uri(imgSrc, UriKind.RelativeOrAbsolute));
                img.Width = 48;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Bottom;
                img.Cursor = Cursors.Hand;
                img.Margin = new Thickness(0, 0, 48, 5);

                img.PreviewMouseUp += delegate (object sender, MouseButtonEventArgs e)
                {
                    if (e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Middle)
                    {
                        Image send = sender as Image;
                        Grid prnt = send.Parent as Grid;
                        if (prnt != null)
                        {
                            string gameName = prnt.Children.OfType<TextBox>().ElementAt(1).Text;
                            main.tabControl.Items.Add(tabControler.AddTab(gameName, imgSrc.Replace("Covers","TabControl"), MyWebBrowser(Uri, null)));

                            if (e.ChangedButton == MouseButton.Left)
                                main.tabControl.SelectedIndex = main.tabControl.Items.Count - 1;
                        }
                    }
                };
                img.MouseEnter += Description_MouseEnter;
                img.MouseLeave += Description_MouseLeave;

                return img;
            }
            return null;
        }
        private StackPanel Platform(Dictionary<string, string> platforms)
        {
            StackPanel st = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            HashSet<string> list = new HashSet<string>();
            foreach (KeyValuePair<string,string> i in platforms)
            {
                list.Add(i.Value);
            }
            foreach (string src in list)
            {
                var img = new Image
                {
                    Source = new BitmapImage(new Uri(src, UriKind.RelativeOrAbsolute)),
                    Width = 16
                };
                img.MouseEnter += delegate (object sender, MouseEventArgs args)
                {
                    img = sender as Image;
                    img.Opacity = 0.7;
                };
                img.MouseLeave += delegate (object sender, MouseEventArgs args)
                {
                    img = sender as Image;
                    img.Opacity = 1;
                };
                foreach (var i in platforms)
                {
                    list.Add(i.Value);
                }
                img.ToolTip = Tt(platforms, src);
                st.Children.Add(img);
            }
            return st;
        }
        private ToolTip Tt(Dictionary<string, string> platforms, string src)
        {
            var b = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFFDCFBE")
            };
            var underline = new Border
            {
                BorderThickness = new Thickness(0, 1, 0, 0),
                BorderBrush = Brushes.Silver,
                Margin = new Thickness(3, 3, 3, 3)
            };

            var tt = new ToolTip
            {
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0)
            };

            var label = new Label();

            var g = new Grid
            {
                Background = Brushes.White
            };
            var rd = new RowDefinition { Height = GridLength.Auto };
            var rd2 = new RowDefinition { Height = GridLength.Auto };
            g.RowDefinitions.Add(rd);
            g.RowDefinitions.Add(rd2);

            var c = "";
            foreach (var i in platforms)
            {
                if (i.Value.Contains("windows") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("xbox") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("android") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("apple") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("linux") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("playstation") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("wii") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("chrome") && i.Value == src)
                    c += i.Key + "\n";
                if (i.Value.Contains("steam") && i.Value == src)
                    c += i.Key + "\n";
            }
            var img = new Image
            {
                Source = new BitmapImage(new Uri(src, UriKind.RelativeOrAbsolute)),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 36,
                Margin = new Thickness(3, 3, 3, 0)
            };

            Grid.SetRow(img, 0);
            g.Children.Add(img);

            label.Content = c.Trim();

            Grid.SetRow(label, 1);
            g.Children.Add(label);
            b.Child = g;

            Grid.SetRow(underline, 1);
            g.Children.Add(underline);
            tt.Content = b;
            return tt;
        }
        private Image Thanks(string user)
        {
            Image img = new Image();
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
            img.ToolTip = "Paldies " + user + "!";
            img.MouseEnter += Youtube_MouseEnter;
            img.MouseLeave += Youtube_MouseLeave;
            img.Visibility = Visibility.Hidden;
            img.Source = new BitmapImage(new Uri("/TM2;component/Images/Other/thanks.png", UriKind.RelativeOrAbsolute));
            img.Width = 48;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Top;
            img.Cursor = Cursors.Help;
            img.Margin = new Thickness(2);
            return img;
        }

        private void MyBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;

            LinearGradientBrush lgb = new LinearGradientBrush
            {
                EndPoint = new Point(0, 20),
                StartPoint = new Point(0, 0),
                MappingMode = BrushMappingMode.Absolute
            };

            GradientStop gs1 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xAB, 0xAD, 0xB3),
                Offset = 0.05
            };

            GradientStop gs2 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xE2, 0xE3, 0xEA),
                Offset = 0.07
            };

            GradientStop gs3 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFB, 0xB2, 0x96),
                Offset = 1
            };

            GradientStopCollection gscollection = lgb.GradientStops;
            gscollection.Add(gs1);
            gscollection.Add(gs2);
            gscollection.Add(gs3);

            b.BorderBrush = lgb;

            Grid g = b.Child as Grid;

            TextBox year = g.Children.OfType<TextBox>().FirstOrDefault();
            year.FontWeight = FontWeights.Bold;
            year.Foreground = Brushes.Black;

            TextBox name = g.Children.OfType<TextBox>().ElementAt(1);
            name.Foreground = Brushes.Maroon;

            Image imdb = g.Children.OfType<Image>().ElementAt(1);
            imdb.Visibility = Visibility.Visible;

            Image i = g.Children.OfType<Image>().FirstOrDefault();
            i.Opacity = 0.7;

            Image youtube = g.Children.OfType<Image>().ElementAt(2);
            youtube.Visibility = Visibility.Visible;

            Image pievienoja = g.Children.OfType<Image>().ElementAt(3);
            pievienoja.Visibility = Visibility.Visible;
        }
        private void MyBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;

            LinearGradientBrush lgb = new LinearGradientBrush
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };

            GradientStop gs1 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFD, 0xCF, 0xBE),
                Offset = 1
            };

            GradientStop gs2 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF),
                Offset = 0.95
            };

            GradientStopCollection gscollection = lgb.GradientStops;
            gscollection.Add(gs1);
            gscollection.Add(gs2);

            b.BorderBrush = lgb;

            Grid g = b.Child as Grid;

            TextBox year = g.Children.OfType<TextBox>().FirstOrDefault();
            year.FontWeight = FontWeights.Normal;
            year.Foreground = Brushes.Gray;

            TextBox name = g.Children.OfType<TextBox>().ElementAt(1);
            name.Foreground = Brushes.Black;

            Image imdb = g.Children.OfType<Image>().ElementAt(1);
            imdb.Visibility = Visibility.Hidden;

            Image i = g.Children.OfType<Image>().FirstOrDefault();
            i.Opacity = 1;

            Image youtube = g.Children.OfType<Image>().ElementAt(2);
            youtube.Visibility = Visibility.Hidden;

            Image pievienoja = g.Children.OfType<Image>().ElementAt(3);
            pievienoja.Visibility = Visibility.Hidden;

        }
        private void MyGameBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;

            LinearGradientBrush lgb = new LinearGradientBrush
            {
                EndPoint = new Point(0, 20),
                StartPoint = new Point(0, 0),
                MappingMode = BrushMappingMode.Absolute
            };

            GradientStop gs1 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xAB, 0xAD, 0xB3),
                Offset = 0.05
            };

            GradientStop gs2 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xE2, 0xE3, 0xEA),
                Offset = 0.07
            };

            GradientStop gs3 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFB, 0xB2, 0x96),
                Offset = 1
            };

            GradientStopCollection gscollection = lgb.GradientStops;
            gscollection.Add(gs1);
            gscollection.Add(gs2);
            gscollection.Add(gs3);

            b.BorderBrush = lgb;

            Grid g = b.Child as Grid;

            TextBox year = g.Children.OfType<TextBox>().FirstOrDefault();
            year.FontWeight = FontWeights.Bold;
            year.Foreground = Brushes.Black;

            TextBox name = g.Children.OfType<TextBox>().ElementAt(1);
            name.Foreground = Brushes.Maroon;

            TextBox genre = g.Children.OfType<TextBox>().ElementAt(2);
            genre.Foreground = Brushes.Black;

            Image description = g.Children.OfType<Image>().ElementAt(1);
            description.Visibility = Visibility.Visible;

            Image i = g.Children.OfType<Image>().FirstOrDefault();
            i.Opacity = 0.7;

            Image youtube = g.Children.OfType<Image>().ElementAt(2);
            youtube.Visibility = Visibility.Visible;

            Image pievienoja = g.Children.OfType<Image>().ElementAt(3);
            pievienoja.Visibility = Visibility.Visible;

        }
        private void MyGameBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;

            LinearGradientBrush lgb = new LinearGradientBrush
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };

            GradientStop gs1 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFD, 0xCF, 0xBE),
                Offset = 1
            };

            GradientStop gs2 = new GradientStop
            {
                Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF),
                Offset = 0.95
            };

            GradientStopCollection gscollection = lgb.GradientStops;
            gscollection.Add(gs1);
            gscollection.Add(gs2);

            b.BorderBrush = lgb;

            Grid g = b.Child as Grid;

            TextBox year = g.Children.OfType<TextBox>().FirstOrDefault();
            year.FontWeight = FontWeights.Normal;
            year.Foreground = Brushes.Gray;

            TextBox name = g.Children.OfType<TextBox>().ElementAt(1);
            name.Foreground = Brushes.Black;

            TextBox genre = g.Children.OfType<TextBox>().ElementAt(2);
            genre.Foreground = Brushes.Gray;

            Image imdb = g.Children.OfType<Image>().ElementAt(1);
            imdb.Visibility = Visibility.Hidden;

            Image i = g.Children.OfType<Image>().FirstOrDefault();
            i.Opacity = 1;

            Image youtube = g.Children.OfType<Image>().ElementAt(2);
            youtube.Visibility = Visibility.Hidden;

            Image pievienoja = g.Children.OfType<Image>().ElementAt(3);
            pievienoja.Visibility = Visibility.Hidden;
        }
        private async void CoverUri_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (main.groupBoxFano.IsVisible || main.groupBoxKinozal.IsVisible || main.groupBoxFilebase.IsVisible)
            {
                Image img = sender as Image;
                Grid g = img.Parent as Grid;
                TextBox name = g.Children.OfType<TextBox>().ElementAt(1);

                Controls.TorrentList tList = new Controls.TorrentList();

                if (e.ChangedButton == MouseButton.Left)
                {
                    main.Title = name.Text;
                    main.progressBar.Visibility = Visibility.Visible;
                    main.containerCovers.Visibility = Visibility.Hidden;
                    main.filterPanel.Visibility = Visibility.Collapsed;
                    main.filterPanelCategorys.Visibility = Visibility.Collapsed;
                    main.containerList.Children.Clear();
                    main.containerList.Children.Add(tList);

                    await tList.Search(name.Text, img.Tag.ToString(), "Visi", "Visi", false);

                    while (!tList.fanoCompleted || !tList.kinozalCompleted || !tList.filebaseCompleted)
                    {
                        await Task.Delay(300);
                    }

                    Controls.CategoryFilter catFilter = new Controls.CategoryFilter(tList, tList.categorySet);
                    main.filterPanelCategorys.Content = catFilter;
                    main.filterPanelCategorys.Visibility = Visibility.Visible;
                    main.borderBack.Visibility = Visibility.Visible;
                    main.btnBack.Visibility = Visibility.Visible;
                }
                if (e.ChangedButton == MouseButton.Middle)
                {
                    main.tabControl.Items.Add(tabControler.AddTab(name.Text, "/TM2;component/Images/TabControl/list.png",
                        new Controls.Tab(tList)));

                    main.containerCovers.Cursor = Cursors.Wait;
                    main.containerCovers.IsEnabled = false;

                    await tList.Search(name.Text, img.Tag.ToString(), "Visi", "Visi", false);

                }
            }
            else
            {
                MessageBox.Show("Jāpievieno vismaz viens trakeris, lai meklētu!", "TM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void sourceUri_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 0.8;
        }
        private void sourceUri_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 1;
        }
        private void sourceUri_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Middle)
            {
                Image img = sender as Image;
                Grid prnt = img.Parent as Grid;
                if (prnt != null)
                {
                    string movieName = prnt.Children.OfType<TextBox>().ElementAt(1).Text;

                    string sourceImg = string.Empty;
                    if (img.Tag.ToString().StartsWith("http://www.imdb"))
                        sourceImg = "/TM2;component/Images/TabControl/imdb.png";
                    else
                        sourceImg = "/TM2;component/Images/TabControl/kinopoisk.png";


                    main.tabControl.Items.Add(tabControler.AddTab(movieName, sourceImg, new Controls.Tab(MyWebBrowser(img.Tag.ToString(), null))));

                    if (e.ChangedButton == MouseButton.Left)
                        main.tabControl.SelectedIndex = main.tabControl.Items.Count - 1;
                }
            }
        }
        private void Youtube_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 0.8;
        }
        private void Youtube_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 1;
        }
        private void Dp_MouseLeave(object sender, MouseEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            dp.Opacity = 1;
        }
        private void Dp_MouseEnter(object sender, MouseEventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            dp.Opacity = 0.8;
        }
        private void Ratio_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.FontWeight = FontWeights.Normal;
        }
        private void Ratio_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.FontWeight = FontWeights.Bold;
        }
        private async void Ratio_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.Text = "Ielādē...";
            t.FontWeight = FontWeights.Normal;
            t.Cursor = Cursors.Arrow;
            t.PreviewMouseLeftButtonUp -= Ratio_PreviewMouseLeftButtonUp;
            t.MouseEnter -= Ratio_MouseEnter;
            t.MouseLeave -= Ratio_MouseLeave;

            Grid g = t.Parent as Grid;
            Image sourceImg = g.Children.OfType<Image>().ElementAt(1);
            string sourceUri = sourceImg.Tag.ToString();
            if (!string.IsNullOrEmpty(sourceUri))
            {
                if (sourceUri.StartsWith("http://www.imdb"))
                {
                    WebClient web = new WebClient();                   
                    byte[] ratingContent = await web.DownloadDataTaskAsync(sourceUri);
                    string ratingEncoded = Encoding.UTF8.GetString(ratingContent);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(ratingEncoded);

                    HtmlNode ratingClass = doc.DocumentNode.SelectSingleNode("//div[@class='imdbRating']");
                    if (ratingClass != null)
                    {
                        HtmlNode rValue = ratingClass.SelectSingleNode(".//span[@itemprop='ratingValue']");
                        HtmlNode rCount = ratingClass.SelectSingleNode(".//span[@itemprop='ratingCount']");

                        if (rValue != null)
                        {
                            t.Text =
                              rValue.InnerText
                            + "/10"
                            + " ("
                            + rCount.InnerText
                            + ")";
                        }
                        else
                        {
                            t.Text = "Nav!";
                        }
                    }
                    else
                    {
                        t.Text = "Nav!";
                    }
                }
                else
                {
                    string id = sourceUri.Substring("http://www.kinopoisk.ru/film/".Length).TrimEnd('/');

                    XmlDocument doc = new XmlDocument();
                    doc.Load("http://rating.kinopoisk.ru/"+id+".xml");

                    double rating = Math.Round(double.Parse(doc.SelectSingleNode("rating/kp_rating").InnerText), 2);
                    string votes = doc.SelectSingleNode("rating/kp_rating/@num_vote").Value;

                    t.Text = rating + "/10" + " (" + votes + ")";
                }
            }
            else
            {
                t.Text = "Nav!";
            }

        }
        private void Description_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 0.8;
        }
        private void Description_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Opacity = 1;
        }

        public void SetCookieForBrowserControl(CookieContainer cc, Uri uri)
        {
            string hostName = uri.Scheme + Uri.SchemeDelimiter + uri.Host;

            Uri hostUri = new Uri(hostName);
            foreach (Cookie cookie in cc.GetCookies(hostUri))
            {
                InternetSetCookieEx(hostName, cookie.Name, new StringBuilder(cookie.Value), 0, IntPtr.Zero);
            }
        }
        public enum BrowserEmulationVersion
        {
            /// <summary>
            /// Default
            /// </summary>
            Default = 0,

            /// <summary>
            /// Interner Explorer 7 Standards Mode
            /// </summary>
            Version7 = 7000,

            /// <summary>
            /// Interner Explorer 8
            /// </summary>
            Version8 = 8000,

            /// <summary>
            /// Interner Explorer 8 Standards Mode
            /// </summary>
            Version8Standards = 8888,

            /// <summary>
            /// Interner Explorer 9
            /// </summary>
            Version9 = 9000,

            /// <summary>
            /// Interner Explorer 9 Standards Mode
            /// </summary>
            Version9Standards = 9999,

            /// <summary>
            /// Interner Explorer 10
            /// </summary>
            Version10 = 10000,

            /// <summary>
            /// Interner Explorer 10 Standards Mode
            /// </summary>
            Version10Standards = 10001,

            /// <summary>
            /// Interner Explorer 11
            /// </summary>
            Version11 = 11000,

            /// <summary>
            /// Interner Explorer 11 Edge Mode
            /// </summary>
            Version11Edge = 11001
        }
        internal static class InternetExplorerBrowserEmulation
        {
            #region Constants

            private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";

            private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

            #endregion

            #region Public Class Members

            /// <summary>
            /// Gets the browser emulation version for the application.
            /// </summary>
            /// <returns>The browser emulation version for the application.</returns>
            public static BrowserEmulationVersion GetBrowserEmulationVersion()
            {
                BrowserEmulationVersion result;

                result = BrowserEmulationVersion.Default;

                try
                {
                    RegistryKey key;

                    key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                    if (key != null)
                    {
                        string programName;
                        object value;

                        programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                        value = key.GetValue(programName, null);

                        if (value != null)
                        {
                            result = (BrowserEmulationVersion)Convert.ToInt32(value);
                        }
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            /// <summary>
            /// Gets the major Internet Explorer version
            /// </summary>
            /// <returns>The major digit of the Internet Explorer version</returns>
            public static int GetInternetExplorerMajorVersion()
            {
                int result;

                result = 0;

                try
                {
                    RegistryKey key;

                    key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                    if (key != null)
                    {
                        object value;

                        value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                        if (value != null)
                        {
                            string version;
                            int separator;

                            version = value.ToString();
                            separator = version.IndexOf('.');
                            if (separator != -1)
                            {
                                int.TryParse(version.Substring(0, separator), out result);
                            }
                        }
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            /// <summary>
            /// Determines whether a browser emulation version is set for the application.
            /// </summary>
            /// <returns><c>true</c> if a specific browser emulation version has been set for the application; otherwise, <c>false</c>.</returns>
            public static bool IsBrowserEmulationSet()
            {
                return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
            }

            /// <summary>
            /// Sets the browser emulation version for the application.
            /// </summary>
            /// <param name="browserEmulationVersion">The browser emulation version.</param>
            /// <returns><c>true</c> the browser emulation version was updated, <c>false</c> otherwise.</returns>
            public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
            {
                bool result;

                result = false;

                try
                {
                    RegistryKey key;

                    key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                    if (key != null)
                    {
                        string programName;

                        programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                        if (browserEmulationVersion != BrowserEmulationVersion.Default)
                        {
                            // if it's a valid value, update or create the value
                            key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                        }
                        else
                        {
                            // otherwise, remove the existing value
                            key.DeleteValue(programName, false);
                        }

                        result = true;
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            /// <summary>
            /// Sets the browser emulation version for the application to the highest default mode for the version of Internet Explorer installed on the system
            /// </summary>
            /// <returns><c>true</c> the browser emulation version was updated, <c>false</c> otherwise.</returns>
            public static bool SetBrowserEmulationVersion()
            {
                int ieVersion;
                BrowserEmulationVersion emulationCode;

                ieVersion = GetInternetExplorerMajorVersion();

                if (ieVersion >= 11)
                {
                    emulationCode = BrowserEmulationVersion.Version11;
                }
                else
                {
                    switch (ieVersion)
                    {
                        case 10:
                            emulationCode = BrowserEmulationVersion.Version10;
                            break;
                        case 9:
                            emulationCode = BrowserEmulationVersion.Version9;
                            break;
                        case 8:
                            emulationCode = BrowserEmulationVersion.Version8;
                            break;
                        default:
                            emulationCode = BrowserEmulationVersion.Version7;
                            break;
                    }
                }

                return SetBrowserEmulationVersion(emulationCode);
            }

            #endregion
        }
    }
}
