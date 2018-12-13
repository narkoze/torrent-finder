using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TM2.Windows
{
    public partial class Login : Window
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        FanoCmd fanoCmd = new FanoCmd();
        KinozalCmd kinozalCmd = new KinozalCmd();
        FilebaseCmd filebaseCmd = new FilebaseCmd();
        Config config = new Config();
        TabControler tabControler = new TabControler();
        CoverCreator coverCreator = new CoverCreator();

        public Login()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            expFano.IsExpanded = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (config.Read("options", "autologin") == "True")
            {
                if (config.FanoAccountExist())
                {
                    main.groupBoxFano.Visibility = Visibility.Visible;
                    main.progressBarFano.Visibility = Visibility.Visible;

                    txtFano.Text = config.Read("account", "fanologin");
                    pwFano.Password = PasswordSecurity.Decrypt(config.Read("account", "fanopassword"), main.mySite);

                    LoginFano();
                }
                if (config.KinozalAccountExist())
                {
                    main.groupBoxKinozal.Visibility = Visibility.Visible;
                    main.progressBarKinozal.Visibility = Visibility.Visible;

                    txtKinozal.Text = config.Read("account", "kinozallogin");
                    pwKinozal.Password = PasswordSecurity.Decrypt(config.Read("account", "kinozalpassword"), main.mySite);

                    LoginKinozal();
                }
                if (config.FilebaseAccountExist())
                {
                    main.groupBoxFilebase.Visibility = Visibility.Visible;
                    main.progressBarFilebase.Visibility = Visibility.Visible;

                    txtFilebase.Text = config.Read("account", "filebaselogin");
                    pwFilebase.Password = PasswordSecurity.Decrypt(config.Read("account", "filebasepassword"), main.mySite);

                    LoginFilebase();
                }
            }
            if (config.Read("options", "autohide") == "True")
                Hide();
        }
        public void FanoLogin()
        {
            if (config.Read("options", "autologin") == "True")
            {
                if (config.FanoAccountExist())
                {
                    main.groupBoxFano.Visibility = Visibility.Visible;
                    main.progressBarFano.Visibility = Visibility.Visible;

                    txtFano.Text = config.Read("account", "fanologin");
                    pwFano.Password = PasswordSecurity.Decrypt(config.Read("account", "fanopassword"), main.mySite);

                    LoginFano();
                }
            }
        }
        private void Expanded(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;

            switch (exp.Name)
            {
                case "expFano":
                    expKinozal.IsExpanded = false;
                    expFilebase.IsExpanded = false;
                    break;
                case "expKinozal":
                    expFano.IsExpanded = false;
                    expFilebase.IsExpanded = false;
                    break;
                case "expFilebase":
                    expFano.IsExpanded = false;
                    expKinozal.IsExpanded = false;
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            main.CmbMeklet.Focus();
        }

        #region Fano
        private void btnFano_Click(object sender, RoutedEventArgs e)
        {
            CheckFanoFields();
        }
        private void pwFano_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && btnFano.IsEnabled)
                CheckFanoFields();
        }
        private void CheckFanoFields()
        {
            bool emptyTxt = string.IsNullOrEmpty(txtFano.Text);
            bool emptyPw = string.IsNullOrEmpty(pwFano.Password);

            if (!emptyTxt && !emptyPw)
            {
                LoginFano();
            }
            else
            {
                if (emptyTxt)
                    txtFano.Focus();
                else if (emptyPw)
                    pwFano.Focus();
            }
        }
        private async void LoginFano()
        {
            FanoControlsLoginStarted();

            await fanoCmd.Login(txtFano.Text, pwFano.Password);

            if (await fanoCmd.CheckLogin())
            {
                SaveAccount("Fano.in", txtFano.Text, pwFano.Password);
                FanoConnected(await fanoCmd.GetRatio());
            }
            else
            {
                FanoControlsLoginFailed();
                txtFanoError.Text = "Kļūda: Nepareizi ievadīts Lietotājvārds un/vai parole";

                int time = 15;
                DispatcherTimer timer = new DispatcherTimer();
                timer = new DispatcherTimer();
                timer.Tick += delegate
                {
                    btnFano.Content = --time;
                    if (time != 0) return;
                    btnFano.Content = "Pievienot";
                    btnFano.IsEnabled = true;
                    timer.Stop();
                };
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
            }
        }
        private void btnFanoDisconnect_Click(object sender, RoutedEventArgs e)
        {
            FanoDisconnect();
        }
        public void FanoDisconnect()
        {
            DeleteAccount("Fano.in");
            FanoDisconnected();
        }
        private void FanoConnected(double Ratio)
        {
            txtFanoError.Text = "";
            progressBarFano.Visibility = Visibility.Hidden;
            btnFano.Visibility = Visibility.Collapsed;
            btnFanoDisconnect.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_yes.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgFanoYesOrNo.Source = bi;

            main.txtFanoUser.Text = txtFano.Text;

            main.txtFanoRatio.Text = Math.Round(Ratio, 2).ToString();
            if(Ratio > 1)
                main.txtFanoRatio.Foreground = Brushes.Green;
            else
                main.txtFanoRatio.Foreground = Brushes.Red;

            main.progressBarFano.Visibility = Visibility.Hidden;
            EnableButtons();
        }
        private void FanoDisconnected()
        {
            txtFano.IsEnabled = true;
            pwFano.IsEnabled = true;
            btnFano.IsEnabled = true;
            btnFanoDisconnect.Visibility = Visibility.Collapsed;
            btnFano.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_no.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgFanoYesOrNo.Source = bi;

            main.groupBoxFano.Visibility = Visibility.Collapsed;
        }
        private void FanoControlsLoginStarted()
        {
            main.groupBoxFano.Visibility = Visibility.Visible;
            main.progressBarFano.Visibility = Visibility.Visible;

            progressBarFano.Visibility = Visibility.Visible;
            txtFano.IsEnabled = false;
            pwFano.IsEnabled = false;
            btnFano.IsEnabled = false;
        }
        private void FanoControlsLoginFailed()
        {
            progressBarFano.Visibility = Visibility.Hidden;
            txtFano.IsEnabled = true;
            pwFano.IsEnabled = true;
        }

        #endregion

        #region Kinozal
        private void btnKinozal_Click(object sender, RoutedEventArgs e)
        {
            CheckKinozalFields();
        }
        private void pwKinozal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && btnKinozal.IsEnabled)
                CheckKinozalFields();
        }
        private void CheckKinozalFields()
        {
            bool emptyTxt = string.IsNullOrEmpty(txtKinozal.Text);
            bool emptyPw = string.IsNullOrEmpty(pwKinozal.Password);

            if (!emptyTxt && !emptyPw)
            {
                LoginKinozal();
            }
            else
            {
                if (emptyTxt)
                    txtKinozal.Focus();
                else if (emptyPw)
                    pwKinozal.Focus();
            }
        }
        private async void LoginKinozal()
        {
            KinozalControlsLoginStarted();

            await kinozalCmd.Login(txtKinozal.Text, pwKinozal.Password);

            if (await kinozalCmd.CheckLogin())
            {
                SaveAccount("Kinozal.tv", txtKinozal.Text, pwKinozal.Password);
                KinozalConnected(await kinozalCmd.GetRatio(), await kinozalCmd.GetAvailable());
            }
            else
            {
                KinozalControlsLoginFailed();
                txtKinozalError.Text = "Kļūda: Nepareizi ievadīts Lietotājvārds un/vai parole";

                int time = 5;
                DispatcherTimer timer = new DispatcherTimer();
                timer = new DispatcherTimer();
                timer.Tick += delegate
                {
                    btnKinozal.Content = --time;
                    if (time != 0) return;
                    btnKinozal.Content = "Pievienot";
                    btnKinozal.IsEnabled = true;
                    timer.Stop();
                };
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
            }
        }
        private void btnKinozalDisconnect_Click(object sender, RoutedEventArgs e)
        {
            KinozalDisconnect();
        }
        public async void KinozalDisconnect()
        {
            await kinozalCmd.Disconnect();
            DeleteAccount("Kinozal.tv");
            KinozalDisconnected();
        }
        private void KinozalConnected(double Ratio, string Available)
        {
            txtKinozalError.Text = "";
            progressBarKinozal.Visibility = Visibility.Hidden;
            btnKinozal.Visibility = Visibility.Collapsed;
            btnKinozalDisconnect.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_yes.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgKinozalYesOrNo.Source = bi;

            main.txtKinozalUser.Text = txtKinozal.Text;

            main.txtKinozalRatio.Text = Math.Round(Ratio, 2).ToString();
            if (Ratio > 1)
                main.txtKinozalRatio.Foreground = Brushes.Green;
            else
                main.txtKinozalRatio.Foreground = Brushes.Red;

            main.txtAvailable.Text = Available;
            string[] available = Available.Split('/');
            if(int.Parse(available[0]) != 0)
            {
                if (int.Parse(available[1]) / int.Parse(available[0]) >= 1)
                    main.txtAvailable.Foreground = Brushes.Green;
                if (int.Parse(available[1]) / int.Parse(available[0]) >= 2)
                    main.txtAvailable.Foreground = Brushes.Orange;
            }
            else
            {
                main.txtAvailable.Foreground = Brushes.Red;
            }

            main.sp.Visibility = Visibility.Visible;
            main.progressBarKinozal.Visibility = Visibility.Hidden;
            EnableButtons();
        }
        public void KinozalDisconnected()
        {
            txtKinozal.IsEnabled = true;
            pwKinozal.IsEnabled = true;
            btnKinozal.IsEnabled = true;
            btnKinozalDisconnect.Visibility = Visibility.Collapsed;
            btnKinozal.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_no.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgKinozalYesOrNo.Source = bi;

            main.groupBoxKinozal.Visibility = Visibility.Collapsed;
        }
        private void KinozalControlsLoginStarted()
        {
            main.groupBoxKinozal.Visibility = Visibility.Visible;
            main.progressBarKinozal.Visibility = Visibility.Visible;

            progressBarKinozal.Visibility = Visibility.Visible;
            txtKinozal.IsEnabled = false;
            pwKinozal.IsEnabled = false;
            btnKinozal.IsEnabled = false;
        }
        public void KinozalControlsLoginFailed()
        {
            progressBarKinozal.Visibility = Visibility.Hidden;
            txtKinozal.IsEnabled = true;
            pwKinozal.IsEnabled = true;
        }
        private void txtRegKinozal_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
            main.tabControl.Items.Add(tabControler.AddTab("Kinozal.tv Reģistrēšanās", "/TM2;component/Images/Trackers/kinozal.tv.png", coverCreator.MyWebBrowser("http://kinozal.tv/signup.php", null)));
            main.tabControl.SelectedIndex = main.tabControl.Items.Count - 1;
        }
        #endregion

        #region Filebase

        private void btnFilebase_Click(object sender, RoutedEventArgs e)
        {
            CheckFilebaseFields();
        }
        private void pwFilebase_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && btnFilebase.IsEnabled)
                CheckFilebaseFields();
        }
        private void CheckFilebaseFields()
        {
            bool emptyTxt = string.IsNullOrEmpty(txtFilebase.Text);
            bool emptyPw = string.IsNullOrEmpty(pwFilebase.Password);

            if (!emptyTxt && !emptyPw)
            {
                LoginFilebase();
            }
            else
            {
                if (emptyTxt)
                    txtFilebase.Focus();
                else if (emptyPw)
                    pwFilebase.Focus();
            }
        }
        private async void LoginFilebase()
        {
            FilebaseControlsLoginStarted();

            await filebaseCmd.Login(txtFilebase.Text, pwFilebase.Password);

            if (await filebaseCmd.CheckLogin())
            {
                SaveAccount("Filebase.ws", txtFilebase.Text, pwFilebase.Password);
                FilebaseConnected(await filebaseCmd.GetRatio());
            }
            else
            {
                FilebaseControlsLoginFailed();
                txtFilebaseError.Text = "Kļūda: Nepareizi ievadīts Lietotājvārds un/vai parole";

                int time = 5;
                DispatcherTimer timer = new DispatcherTimer();
                timer = new DispatcherTimer();
                timer.Tick += delegate
                {
                    btnFilebase.Content = --time;
                    if (time != 0) return;
                    btnFilebase.Content = "Pievienot";
                    btnFilebase.IsEnabled = true;
                    timer.Stop();
                };
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
            }
        }
        private void btnFilebaseDisconnect_Click(object sender, RoutedEventArgs e)
        {
            FilebaseDisconnect();
        }
        public async void FilebaseDisconnect()
        {
            await filebaseCmd.Disconnect();
            DeleteAccount("Filebase.ws");
            FilebaseDisconnected();
        }
        private void FilebaseConnected(double Ratio)
        {
            txtFilebaseError.Text = "";
            progressBarFilebase.Visibility = Visibility.Hidden;
            btnFilebase.Visibility = Visibility.Collapsed;
            btnFilebaseDisconnect.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_yes.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgFilebaseYesOrNo.Source = bi;

            main.txtFilebaseUser.Text = txtFilebase.Text;

            main.txtFilebaseRatio.Text = Math.Round(Ratio, 2).ToString();
            if (Ratio >= 1)
                main.txtFilebaseRatio.Foreground = Brushes.Green;
            else
                main.txtFilebaseRatio.Foreground = Brushes.Red;

            if (Ratio == 1)
            {
                main.txtFilebaseRatio.Text = "1.00";
                main.txtFilebaseRatio.Foreground = Brushes.Green;
            }

            main.progressBarFilebase.Visibility = Visibility.Hidden;
            EnableButtons();
        }
        private void FilebaseDisconnected()
        {
            txtFilebase.IsEnabled = true;
            pwFilebase.IsEnabled = true;
            btnFilebase.IsEnabled = true;
            btnFilebaseDisconnect.Visibility = Visibility.Collapsed;
            btnFilebase.Visibility = Visibility.Visible;

            BitmapImage bi = new BitmapImage();
            Uri cover = new Uri("/TM2;component/Images/Login/hand_no.png", UriKind.RelativeOrAbsolute);
            bi.BeginInit();
            bi.UriSource = cover;
            bi.EndInit();
            imgFilebaseYesOrNo.Source = bi;

            main.groupBoxFilebase.Visibility = Visibility.Collapsed;
        }
        private void FilebaseControlsLoginStarted()
        {
            main.groupBoxFilebase.Visibility = Visibility.Visible;
            main.progressBarFilebase.Visibility = Visibility.Visible;

            progressBarFilebase.Visibility = Visibility.Visible;
            txtFilebase.IsEnabled = false;
            pwFilebase.IsEnabled = false;
            btnFilebase.IsEnabled = false;
        }
        private void FilebaseControlsLoginFailed()
        {
            progressBarFilebase.Visibility = Visibility.Hidden;
            txtFilebase.IsEnabled = true;
            pwFilebase.IsEnabled = true;
        }
        private void txtRegFilebase_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
            main.tabControl.Items.Add(tabControler.AddTab("Filebase.ws Reģistrēšanās", "/TM2;component/Images/Trackers/filebase.ws.png", coverCreator.MyWebBrowser("http://www.filebase.ws/registration/", null)));
            main.tabControl.SelectedIndex = main.tabControl.Items.Count - 1;
        }
        #endregion

        private void SaveAccount(string tracker, string user, string password)
        {
            switch (tracker)
            {
                case "Fano.in":
                    config.Write("fanologin", user);
                    config.Write("fanopassword", PasswordSecurity.Encrypt(password, main.mySite));
                    break;
                case "Kinozal.tv":
                    config.Write("kinozallogin", user);
                    config.Write("kinozalpassword", PasswordSecurity.Encrypt(password, main.mySite));
                    break;
                case "Filebase.ws":
                    config.Write("filebaselogin", user);
                    config.Write("filebasepassword", PasswordSecurity.Encrypt(password, main.mySite));
                    break;
            }
        }
        private void DeleteAccount(string tracker)
        {
            switch (tracker)
            {
                case "Fano.in":
                    config.Write("fanologin", string.Empty);
                    config.Write("fanopassword", string.Empty);
                    break;
                case "Kinozal.tv":
                    config.Write("kinozallogin", string.Empty);
                    config.Write("kinozalpassword", string.Empty);
                    break;
                case "Filebase.ws":
                    config.Write("filebaselogin", string.Empty);
                    config.Write("filebasepassword", string.Empty);
                    break;
            }
        }
        private void EnableButtons()
        {
            main.BtnMeklet.IsEnabled = true;
            main.CmbMeklet.IsEnabled = true;
            main.btnToday.IsEnabled = true;
        }
    }
}
