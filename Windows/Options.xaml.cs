using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TM2.Windows
{
    public partial class Options : Window
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        Config config = new Config();
        private int torrentCount;

        public Options()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            chkAutologin.IsChecked = bool.Parse(config.Read("options", "autologin"));
            chkAutohide.IsChecked = bool.Parse(config.Read("options", "autohide"));
            bool accounts = config.CheckIfAccountsExist();
            chkAutologin.IsEnabled = accounts;
            chkAutologin.IsChecked = accounts;
            btnDeleteAccounts.IsEnabled = accounts;
            btnDeleteHistory.IsEnabled = config.CheckIfHistoryExist();
            Download.IsExpanded = true;
            txtDownloadsPath.Text = config.Read("options", "downloadspath");
            txtDownloadsPath.Focus();
            txtDownloadsPath.Select(txtDownloadsPath.Text.Length, 0);
            FolderUpdate();
        }
        private void Expanded(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;

            switch (exp.Name)
            {
                case "Startup":
                    Download.IsExpanded = false;
                    History.IsExpanded = false;
                    break;
                case "Download":
                    Startup.IsExpanded = false;
                    History.IsExpanded = false;
                    break;
                case "History":
                    Startup.IsExpanded = false;
                    Download.IsExpanded = false;
                    break;
            }
        }
        private void Save()
        {
            string autologin = chkAutologin.IsChecked.ToString();
            string autohide = chkAutohide.IsChecked.ToString();
            string downloadsPath = txtDownloadsPath.Text;

            config.Write("autologin", autologin);
            config.Write("autohide", autohide);
            config.Write("downloadspath", downloadsPath);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnDownloadsPath_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "Izvēlies mapīti, kurā turpmāk saglabāsies lejuplādētie torrenti",
                IsFolderPicker = true,
                InitialDirectory = main.downloadsPath,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = main.downloadsPath,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                main.downloadsPath = dlg.FileName;
                FolderUpdate();
            }
            else
            {
                Activate();
            }
        }
        private void FolderUpdate()
        {
            torrentCount = 0;
            string path = main.downloadsPath;
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Name.EndsWith(".torrent"))
                        torrentCount++;
                }
                txtDeleteTorrents.Text = "Dzēst lejuplādētos torrentus " + "(" + torrentCount + ")";
                btnDeleteTorrents.IsEnabled = torrentCount != 0;
                txtDownloadsPath.Text = di.FullName;
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }
        private void btnDeleteTorrents_Click(object sender, RoutedEventArgs e)
        {
            var di = new DirectoryInfo(main.downloadsPath);
            foreach (var file in di.GetFiles())
            {
                if (file.Name.EndsWith(".torrent"))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                    
            }
            FolderUpdate();
        }
        private void btnDeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            if (config.CheckIfHistoryExist())
            {
                config.DeleteHistory();
                btnDeleteHistory.IsEnabled = false;
            }
            main.CmbMeklet.Items.Clear();
        }
        private void btnDeleteAccounts_Click(object sender, RoutedEventArgs e)
        {
            if(config.CheckIfAccountsExist())
            {
                config.DeleteAccounts();
                btnDeleteAccounts.IsEnabled = false;
            }
                
        }
    }
}
