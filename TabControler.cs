using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TM2
{
    partial class TabControler : ResourceDictionary
    {

        public TabItem AddTab(string Name, string Type, UIElement Content)
        {
            DockPanel dockPanel = new DockPanel();

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(dockPanel);

            ProgressBar progressBar = new ProgressBar
            {
                Height = 4,
                IsIndeterminate = true,
                Margin = new Thickness(0, 4, 0, 0)
            };
            stackPanel.Children.Add(progressBar);

            TabItem tabItem = new TabItem
            {
                Style = (Style)Application.Current.MainWindow.FindResource("Tab"),
                Header = stackPanel,
                Tag = Name,
                Content = Content
            };

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(Type, UriKind.RelativeOrAbsolute);
            bi.EndInit();

            Image typeImage = new Image
            {
                Width = 16,
                Margin = new Thickness(0, 5, 2, 0),
                Source = bi
            };
            dockPanel.Children.Add(typeImage);

            TextBlock header = new TextBlock
            {
                Margin = new Thickness(2, 4, 0, 0),
                Text = Name,
                Width = 154,
                ToolTip = Name,
                TextTrimming = TextTrimming.CharacterEllipsis
            };
            dockPanel.Children.Add(header);

            Button close = new Button
            {
                Margin = new Thickness(0, 4, 0, 0),
                Style = (Style)Application.Current.MainWindow.FindResource("btnTab"),
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = new Image
                {
                    Source = new BitmapImage
                    {
                        UriSource = new Uri("Images/TabControl/closeGray.png", UriKind.RelativeOrAbsolute)
                    }
                }
            };
            close.Click += DeleteTab;
            DockPanel.SetDock(close, Dock.Right);
            dockPanel.Children.Add(close);

            return tabItem;
        }
        public void DeleteTab(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dockPanel = button.Parent as DockPanel;
            var stackPanel = dockPanel.Parent as StackPanel;
            var tab = stackPanel.Parent as TabItem;

            var tabControl = tab.Content as Controls.Tab;
            if(tabControl != null)
            {
                var container = tabControl.containerList.Children;
                if(container.Count > 0)
                {
                    var web = container[0] as WebBrowser;
                    if (web != null)
                    {
                        web.Navigate(@"about:blank");
                    }
                }
            }

            var web2 = tab.Content as WebBrowser;
            if(web2 != null)
            {
                web2.Navigate(@"about:blank");
            }


            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.tabControl.Items.Remove(tab);
        }
        private void ScrollTabs(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
            e.Handled = true;
        }

    }
}
