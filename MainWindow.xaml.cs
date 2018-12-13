using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using TM2.Controls;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Deployment.Application;
using System.Runtime.InteropServices;

namespace TM2
{
    public partial class MainWindow
    {
        private string title = "Torrentu meklētājs";
        Config config = new Config();
        Lists lists = new Lists();
        Windows.Login login;

        public string downloadsPath = string.Empty;
        public string mySite = "http://www.piemeram.lv";

        internal MyWebClient fanoWeb;
        internal MyWebClient kinozalWeb;
        internal MyWebClient FilebaseWeb;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            PublishVersion();
            DateFormat();
            ReadDownloadsPath();
            LoadGenres();
            LoadYears();
            LoadCategorys();
            LoadHistory();

            cmbCategory.SelectionChanged += cmbCategory_SelectionChanged;

            calendar.DisplayDateStart = DateTime.Today.AddDays(-6);
            calendar.DisplayDateEnd = DateTime.Today;
            txtToday.Tag = DateTime.Today;
        }
        private void PublishVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd =
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                Version pubVersion = cd.CurrentVersion;
                publishVersion.Text = string.Format("Versija: {0}.{1}.{2}.{3}", pubVersion.Major, pubVersion.Minor, pubVersion.Build, pubVersion.Revision);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            login = new Windows.Login()
            {
                Owner = Application.Current.MainWindow
            };
            try {
                login.ShowDialog();
            }
            catch (System.Net.WebException ex) {
                if (ex.Response.ResponseUri.Host == "kinozal.tv")
                {
                    login.KinozalControlsLoginFailed();
                    login.KinozalDisconnected();
                }
            }
        }
        private void LoadHistory()
        {
            if(config.CheckIfHistoryExist())
            {
                foreach(string i in config.GetHistory())
                {
                    CmbMeklet.Items.Add(i);
                }
            }
        }
        private void DateFormat()
        {
            var newCulture = new CultureInfo("");

            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        private void ReadDownloadsPath()
        {
            downloadsPath = config.Read("options", "downloadspath");
            if (downloadsPath == string.Empty)
            {
                downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                config.Write("downloadspath", downloadsPath);
            }
        }
        private void imgWorld_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(mySite);
        }
        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            if (groupBoxFano.IsVisible || groupBoxKinozal.IsVisible || groupBoxFilebase.IsVisible)
            {
                SearchToday();
            }
            else
            {
                MessageBox.Show("Jāpievieno vismaz viens trakeris, lai meklētu!", "TM", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        private async void SearchToday()
        {
            if (groupBoxFano.IsVisible || groupBoxKinozal.IsVisible || groupBoxFilebase.IsVisible)
            {
                UIDisable();

                borderBack.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;

                Title = txtToday.Text + " torrenti";
                progressBar.Visibility = Visibility.Visible;
                containerCovers.Children.Clear();
                containerList.Children.Clear();
                filterPanel.Content = null;
                filterPanelCategorys.Content = null;

                TorrentList tList = new TorrentList();
                containerList.Children.Add(tList);
                await tList.Search("", "Visas", "Visi", "Visi", true);

                while (!tList.fanoCompleted || !tList.kinozalCompleted || !tList.filebaseCompleted)
                {
                    await Task.Delay(300);
                }

                UIEnable();
                CategoryFilter catFilter = new CategoryFilter(tList, tList.categorySet);
                filterPanelCategorys.Content = catFilter;
                filterPanelCategorys.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Jāpievieno vismaz viens trakeris, lai meklētu!", "TM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnToday_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            calendar.SelectedDate = DateTime.Today;
            datePicker.IsOpen = true;           
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            datePicker.HorizontalOffset = point.X;
            datePicker.VerticalOffset = point.Y;
        }
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = calendar.SelectedDate.Value;
            if (date == DateTime.Today.AddDays(-1))
            {
                txtToday.Text = "Vakardienas";
                txtToday.Tag = DateTime.Today.AddDays(-1);
                txtToday.FontSize = 12;
            }
            else if (date == DateTime.Today)
            {
                txtToday.Text = "Šodienas";
                txtToday.Tag = DateTime.Today;
                txtToday.FontSize = 14.667;
            }
            else
            {
                txtToday.Text = date.ToShortDateString();
                txtToday.Tag = date;
                txtToday.FontSize = 12;
            }
            datePicker.IsOpen = false;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Title = title;
            containerCovers.Visibility = Visibility.Visible;
            filterPanel.Visibility = Visibility.Visible;
            containerList.Children.Clear();
            filterPanelCategorys.Content = null;
            filterPanelCategorys.Visibility = Visibility.Hidden;
            btnBack.Visibility = Visibility.Collapsed;
            borderBack.Visibility = Visibility.Collapsed;
        }
        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {
            Title = title;

            borderBack.Visibility = Visibility.Collapsed;
            btnBack.Visibility = Visibility.Collapsed;

            progressBar.Visibility = Visibility.Visible;

            filterPanelCategorys.Content = null;
            filterPanel.Content = null;
            filterPanel.Visibility = Visibility.Hidden;
            filterPanelCategorys.Visibility = Visibility.Hidden;
            filterPanel.Content = new MovieFilter();

            containerList.Children.Clear();
            containerCovers.Children.Clear();
            containerCovers.Visibility = Visibility.Visible;
            containerCovers.Children.Add(new Covers("Movies", null, null, null, null, null));
        }
        private void btnTV_Click(object sender, RoutedEventArgs e)
        {
            Title = title;

            borderBack.Visibility = Visibility.Collapsed;
            btnBack.Visibility = Visibility.Collapsed;

            progressBar.Visibility = Visibility.Visible;

            filterPanelCategorys.Content = null;
            filterPanel.Content = null;
            filterPanel.Visibility = Visibility.Hidden;
            filterPanelCategorys.Visibility = Visibility.Hidden;
            filterPanel.Content = new TVFilter();

            containerList.Children.Clear();

            containerCovers.Children.Clear();
            containerCovers.Visibility = Visibility.Visible;
            containerCovers.Children.Add(new Covers("TV", null, null, null, null, null));
        }
        private void btnGames_Click(object sender, RoutedEventArgs e)
        {
            Title = title;

            borderBack.Visibility = Visibility.Collapsed;
            btnBack.Visibility = Visibility.Collapsed;

            progressBar.Visibility = Visibility.Visible;

            filterPanelCategorys.Content = null;
            filterPanel.Content = null;
            filterPanel.Visibility = Visibility.Hidden;
            filterPanelCategorys.Visibility = Visibility.Hidden;
            filterPanel.Content = new GameFilter();

            containerList.Children.Clear();

            containerCovers.Children.Clear();
            containerCovers.Visibility = Visibility.Visible;
            containerCovers.Children.Add(new Covers("Games", null, null, null, null, null));
        }

        private void LoadGenres()
        {
            cmbGenre.Items.Add("Visi");
            var list = new List<string>
            {
                "Komēdija",
                "Bojeviks/Kara",
                "Trilleris/Detektīvs",
                "Drāma",
                "Romantika",
                "Fantastika",
                "Fantāzijas",
                "Šausmu/Mistērija",
                "Piedzīvojuma",
                "Dokumentāla",
                "Sports",
                "Bērnu/Ģimenes",
                "Klasika",
                "Vēstures"
            };
            foreach (var i in list)
                cmbGenre.Items.Add(i);
        }
        private void LoadYears()
        {
            cmbYear.Items.Add("Visi");
            var year = DateTime.Now.Year + 1;
            for (var i = year; i-- > 1900;)
            {
                cmbYear.Items.Add(i.ToString());
            }
        }
        private void LoadCategorys()
        {
            foreach (string cat in lists.Categorys)
            {
                cmbCategory.Items.Add(cat);
            }
        }
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string category = (e.AddedItems[0] as ComboBoxItem).Content as string;
            ComboBox combo = sender as ComboBox;
            string category = combo.SelectedItem as string;
            EnableComboBoxes(category);
        }
        public void EnableComboBoxes(string Category)
        {
            if (Category == "Visas")
            {
                cmbGenre.IsEnabled = false;
                cmbGenre.SelectedIndex = 0;
                cmbYear.IsEnabled = false;
                cmbYear.SelectedIndex = 0;
            }
            else if (Category == "Filmas")
            {
                cmbGenre.IsEnabled = true;
                cmbYear.IsEnabled = true;
            }
            else if (Category == "TV" || Category == "Programmas" || Category == "Spēles" || Category == "Mūzika" || Category == "Videoklipi" || Category == "Grāmatas" || Category == "Erotika")
            {
                cmbGenre.IsEnabled = false;
                cmbGenre.SelectedIndex = 0;
                cmbYear.IsEnabled = true;
            }
            else if (Category == "Telefonam" || Category == "Mācības")
            {
                cmbGenre.IsEnabled = false;
                cmbGenre.SelectedIndex = 0;
                cmbYear.IsEnabled = false;
                cmbYear.SelectedIndex = 0;
            }
        }
        private void BtnMeklet_Click(object sender, RoutedEventArgs e)
        {
            MekletStart();
        }
        private void CmbMeklet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MekletStart();
        }
        private async void MekletStart()
        {
            if (groupBoxFano.IsVisible || groupBoxKinozal.IsVisible || groupBoxFilebase.IsVisible)
            {
                UIDisable();

                borderBack.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;

                progressBar.Visibility = Visibility.Visible;
                containerCovers.Children.Clear();
                containerList.Children.Clear();
                filterPanel.Content = null;
                filterPanelCategorys.Content = null;

                TorrentList tList = new TorrentList();
                containerList.Children.Add(tList);
                await tList.Search(CmbMeklet.Text, cmbCategory.Text, cmbGenre.Text, cmbYear.Text, false);

                while (!tList.fanoCompleted || !tList.kinozalCompleted || !tList.filebaseCompleted)
                {
                    await Task.Delay(300);
                }

                CategoryFilter catFilter = new CategoryFilter(tList, tList.categorySet);
                filterPanelCategorys.Content = catFilter;
                filterPanelCategorys.Visibility = Visibility.Visible;

                if(!string.IsNullOrEmpty(CmbMeklet.Text.Trim()))
                {
                    Title = CmbMeklet.Text.Trim();
                    config.DeleteHistoryElementIfExist(CmbMeklet.Text.Trim());
                    config.AddHistory(CmbMeklet.Text.Trim());
                    CmbMeklet.Items.Clear();
                    LoadHistory();
                    CmbMeklet.Text = "";
                }

                UIEnable();
                cmbCategory.IsEnabled = true;
                EnableComboBoxes(cmbCategory.Text);

            }
            else
            {
                MessageBox.Show("Jāpievieno vismaz viens trakeris, lai meklētu!", "TM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void groupBoxFano_MouseEnter(object sender, MouseEventArgs e)
        {
            btnFanoClose.Visibility = Visibility.Visible;
        }
        private void groupBoxFano_MouseLeave(object sender, MouseEventArgs e)
        {
            btnFanoClose.Visibility = Visibility.Hidden;
        }
        private void btnFanoClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            login.FanoDisconnect();
        }
        private void groupBoxFano_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            object source = e.Source;

            if (source != source as Button)
            {
                login.expFano.IsExpanded = true;
                login.ShowDialog();
            }
        }

        private void groupBoxKinozal_MouseEnter(object sender, MouseEventArgs e)
        {
            btnKinozalClose.Visibility = Visibility.Visible;
        }
        private void groupBoxKinozal_MouseLeave(object sender, MouseEventArgs e)
        {
            btnKinozalClose.Visibility = Visibility.Hidden;
        }
        private void btnKinozalClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           login.KinozalDisconnect();
        }
        private void groupBoxKinozal_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            object source = e.Source;

            if (source != source as Button)
            {
                login.expKinozal.IsExpanded = true;
                login.ShowDialog();
            }
        }

        private void groupBoxFilebase_MouseEnter(object sender, MouseEventArgs e)
        {
            btnFilebaseClose.Visibility = Visibility.Visible;
        }
        private void groupBoxFilebase_MouseLeave(object sender, MouseEventArgs e)
        {
            btnFilebaseClose.Visibility = Visibility.Hidden;
        }
        private void btnFilebaseClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            login.FilebaseDisconnect();
        }
        private void groupBoxFilebase_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            object source = e.Source;

            if (source != source as Button)
            {
                login.expFilebase.IsExpanded = true;
                login.ShowDialog();
            }
        }

        private void btnKonti_Click(object sender, RoutedEventArgs e)
        {
            login.ShowDialog();
        }
        private void SplitterCheck(object sender, SizeChangedEventArgs e)
        {
            if ((groupBoxFano.Visibility == Visibility.Collapsed) && (groupBoxKinozal.Visibility == Visibility.Collapsed) && (groupBoxFilebase.Visibility == Visibility.Collapsed))
                spliter.Visibility = Visibility.Collapsed;
            else
                spliter.Visibility = Visibility.Visible;
        }

        private void btnIespejas_Click(object sender, RoutedEventArgs e)
        {
            Windows.Options options = new Windows.Options
            {
                Owner = Application.Current.MainWindow
            };
            options.ShowDialog();
        }

        private void UIDisable()
        {
            btnToday.IsEnabled = false;
            btnMovies.IsEnabled = false;
            btnTV.IsEnabled = false;
            btnGames.IsEnabled = false;
            BtnMeklet.IsEnabled = false;
            CmbMeklet.IsEnabled = false;
        }
        private void UIEnable()
        {
            btnToday.IsEnabled = true;
            btnMovies.IsEnabled = true;
            btnTV.IsEnabled = true;
            btnGames.IsEnabled = true;
            BtnMeklet.IsEnabled = true;
            CmbMeklet.IsEnabled = true;
        }

        private void CmbMeklet_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var cmb = sender as ComboBox;
            var cMenu = new ContextMenu();
            var mItem = new MenuItem
            {
                Command = ApplicationCommands.Paste,
                Header = "Paste"
            };
            cMenu.Items.Add(mItem);
            cmb.ContextMenu = cMenu;
        }
    }
    public static class KnownFolders
    {
        private static string[] _knownFolderGuids = new string[]
        {
        "{56784854-C6CB-462B-8169-88E350ACB882}", // Contacts
        "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", // Desktop
        "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", // Documents
        "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads
        "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", // Favorites
        "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}", // Links
        "{4BD8D571-6D19-48D3-BE97-422220080E43}", // Music
        "{33E28130-4E1E-4676-835A-98395C3BC3BB}", // Pictures
        "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}", // SavedGames
        "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}", // SavedSearches
        "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}", // Videos
        };

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string GetPath(KnownFolder knownFolder)
        {
            return GetPath(knownFolder, false);
        }

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <param name="defaultUser">Specifies if the paths of the default user (user profile
        ///     template) will be used. This requires administrative rights.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string GetPath(KnownFolder knownFolder, bool defaultUser)
        {
            return GetPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
        }

        private static string GetPath(KnownFolder knownFolder, KnownFolderFlags flags,
            bool defaultUser)
        {
            IntPtr outPath;
            int result = SHGetKnownFolderPath(new Guid(_knownFolderGuids[(int)knownFolder]),
                (uint)flags, new IntPtr(defaultUser ? -1 : 0), out outPath);
            if (result >= 0)
            {
                return Marshal.PtrToStringUni(outPath);
            }
            else
            {
                throw new ExternalException("Unable to retrieve the known folder path. It may not "
                    + "be available on this system.", result);
            }
        }

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken,
            out IntPtr ppszPath);

        [Flags]
        private enum KnownFolderFlags : uint
        {
            SimpleIDList = 0x00000100,
            NotParentRelative = 0x00000200,
            DefaultPath = 0x00000400,
            Init = 0x00000800,
            NoAlias = 0x00001000,
            DontUnexpand = 0x00002000,
            DontVerify = 0x00004000,
            Create = 0x00008000,
            NoAppcontainerRedirection = 0x00010000,
            AliasOnly = 0x80000000
        }
    }
    public enum KnownFolder
    {
        Contacts,
        Desktop,
        Documents,
        Downloads,
        Favorites,
        Links,
        Music,
        Pictures,
        SavedGames,
        SavedSearches,
        Videos
    }
}
