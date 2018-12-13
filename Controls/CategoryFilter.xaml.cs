using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TM2.Controls
{
    public partial class CategoryFilter : UserControl
    {
        TorrentList torrentList;
        Converter converter = new Converter();
        private string Category = "";
        public CategoryFilter(TorrentList TorrentList, HashSet<string> CategorySet)
        {
            torrentList = TorrentList;

            InitializeComponent();

            cmbCategory.Items.Add("Visas");
            bool filmasExist = CategorySet.Any(x => x.Contains("Filma"));

            if(filmasExist)
                CategorySet.Add("Filma");

            IEnumerable<string> chkListSorted = CategorySet.OrderBy(s => s);
            foreach (string cat in chkListSorted)
            {
                cmbCategory.Items.Add(cat);
            }
            Init();
        }

        private void Init()
        {
            cmbCategory.SelectionChanged += cmbCategory_SelectionChanged;
            txtName.KeyUp += TxtName_KeyUp;
        }

        private void TxtName_KeyUp(object sender, KeyEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(torrentList.tList.ItemsSource);
            view.Filter = DoFilter;
            CollectionViewSource.GetDefaultView(torrentList.tList.Items).Refresh();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            Category = combo.SelectedItem as string;

            if (Category == "Visas")
                Category = "";

            var view = (CollectionView)CollectionViewSource.GetDefaultView(torrentList.tList.ItemsSource);
            view.Filter = DoFilter;
            CollectionViewSource.GetDefaultView(torrentList.tList.Items).Refresh();
        }

        private bool DoFilter(object item)
        {
            return ((item as Torrents).Type.IndexOf(Category, StringComparison.OrdinalIgnoreCase) == 0)
                && ((item as Torrents).Name.IndexOf(txtName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
