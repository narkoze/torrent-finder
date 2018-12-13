using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TM2.Controls
{
    public partial class Tab : UserControl
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        UIElement el = new UIElement();

        public Tab(UIElement Element)
        {
            el = Element;
            InitializeComponent();
            Init();
        }
        private async void Init()
        {
            if (el.GetType() == typeof(WebBrowser))
            {
                WebBrowser web = el as WebBrowser;
                web.LoadCompleted += Web_LoadCompleted;
            }
            if (el.GetType() == typeof(TorrentList))
            {
                TorrentList tList = el as TorrentList;
                while (!tList.fanoCompleted || !tList.kinozalCompleted || !tList.filebaseCompleted)
                {
                    await Task.Delay(1000);
                }

                progressBar.Visibility = Visibility.Collapsed;

                CategoryFilter catFilter = new CategoryFilter(tList, tList.categorySet);
                filterPanel.Content = catFilter;
            }
            containerList.Children.Add(el);
        }

        private void Web_LoadCompleted(object sender, NavigationEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;
        }
    }
}
