using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MaterialListPage.xaml
    /// </summary>
    public partial class MaterialListPage : Page
    {
        List<Material> MatStart = DataBaseClass.DB.Material.ToList();
        public MaterialListPage()
        {
            InitializeComponent();
            LVMaterial.ItemsSource = MatStart;
            CbFilt.Items.Add("Все типы");
            List<MaterialType> mt = DataBaseClass.DB.MaterialType.ToList();
            for (int i = 0; i < mt.Count; i++)
            {
                CbFilt.Items.Add(mt[i].Title);
            }
            CbFilt.SelectedIndex = 0;
            TbCount.Text = "Записей: " + MatStart.Count().ToString() + " из " + MatStart.Count().ToString();
        }
        private void TbSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<MaterialSupplier> mtList = DataBaseClass.DB.MaterialSupplier.Where(x => x.MaterialID == index).ToList();
            string str = "";
            foreach (MaterialSupplier item in mtList)
            {
                str += item.Supplier.Title + ", ";
            }
            if (mtList.Count == 0)
            {
                tb.Text = "Поставщики: отсутствуют";
            }
            else
            {
                tb.Text = "Поставщики: " + str.Substring(0, str.Length - 2);
            }
        }
        List<Material> MatFilter = new List<Material>();

        List<Material> MatSearch = new List<Material>();

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbSearch.Text != String.Empty)
            {
                MatSearch = MatStart.Where(x => x.Title.Contains(TbSearch.Text) || x.Description.Contains(TbSearch.Text)).ToList();
                FliterSort();
            }
            else
            {
                FliterSort();
            }
        }
        private void CbFilt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FliterSort();
        }
        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FliterSort();
        }
        private void FliterSort()
        {
            int filterIndex = CbFilt.SelectedIndex;

            if (TbSearch.Text != String.Empty)
            {
                if (filterIndex != 0)
                {
                    MatFilter = MatSearch.Where(x => x.MaterialTypeID == filterIndex).ToList();
                }
                else
                {
                    MatFilter = MatSearch;
                }
            }
            else
            {
                if (filterIndex != 0)
                {
                    MatFilter = MatStart.Where(x => x.MaterialTypeID == filterIndex).ToList();
                }
                else
                {
                    MatFilter = MatStart;
                }
            }

            switch (CbSort.SelectedIndex)
            {
                case 0:
                    MatFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    break;
                case 1:
                    MatFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    MatFilter.Reverse();
                    break;
                case 2:
                    MatFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    break;
                case 3:
                    MatFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    MatFilter.Reverse();
                    break;
                case 4:
                    //MatFilter.Sort((x, y) => x.CountInStock.CompareTo(y.CountInStock));
                    break;
                case 5:
                    //MatFilter.Sort((x, y) => x.CountInStock.CompareTo(y.CountInStock));
                    //MatFilter.Reverse();
                    break;
            }

            LVMaterial.ItemsSource = MatFilter;
            LVMaterial.Items.Refresh();
            TbCount.Text = "Записей: " + MatFilter.Count().ToString() + " из " + MatStart.Count().ToString();
        }

        private void LVMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVMaterial.SelectedIndex != -1)
            {
                ButtEditMin.Visibility = Visibility.Visible;
            }
            else
            {
                ButtEditMin.Visibility = Visibility.Hidden;
            }
        }

        private void ButtEditMin_Click(object sender, RoutedEventArgs e)
        {
            var selectedList = LVMaterial.SelectedItems;
            FrameClass.frm.Navigate(new MinCount());
        }
            private void ButtEdit_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Material MaterialEdit = DataBaseClass.DB.Material.FirstOrDefault(y => y.ID == id);
            EditCreateWindow editCreateWindow = new EditCreateWindow(MaterialEdit);
            editCreateWindow.ShowDialog();
        }

        

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            EditCreateWindow editWindow = new EditCreateWindow();
            editWindow.ShowDialog();
        }
    }
}
