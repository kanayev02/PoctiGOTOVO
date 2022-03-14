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
    /// Логика взаимодействия для EditCreateWindow.xaml
    /// </summary>
    public partial class EditCreateWindow : Window
    {
        string path;
        bool IsCreate = false;
        List<MaterialSupplier> MS = DataBaseClass.DB.MaterialSupplier.ToList();
        public EditCreateWindow()
        {
            InitializeComponent();
            CbMaterialType.ItemsSource = DataBaseClass.DB.MaterialType.ToList();
            CbMaterialType.SelectedValuePath = "ID";
            CbMaterialType.DisplayMemberPath = "Title";

            CbSupplier.ItemsSource = DataBaseClass.DB.Supplier.ToList();
            CbSupplier.SelectedValuePath = "ID";
            CbSupplier.DisplayMemberPath = "Title";
            IsCreate = true;
            
        }
        Material MaterialEdit = new Material();
        public EditCreateWindow(Material editImport)
        {
            InitializeComponent();
            MaterialEdit = editImport;

            CbMaterialType.ItemsSource = DataBaseClass.DB.MaterialType.ToList();
            CbMaterialType.SelectedValuePath = "ID";
            CbMaterialType.DisplayMemberPath = "Title";

            TbTitle.Text = MaterialEdit.Title;
            TbCountInStock.Text = MaterialEdit.CountInStock.ToString();
            TbCountInPack.Text = MaterialEdit.CountInPack.ToString();
            TbUnit.Text = MaterialEdit.Unit.ToString();
            TbCost.Text = MaterialEdit.Cost.ToString();
            TbMinCount.Text = MaterialEdit.MinCount.ToString();

            if (MaterialEdit.Image != null)
            {
                BitmapImage BI = new BitmapImage(new Uri(MaterialEdit.Image, UriKind.RelativeOrAbsolute));
                MaterialImage.Source = BI;
            }

            CbSupplier.ItemsSource = DataBaseClass.DB.Supplier.ToList();
            CbSupplier.SelectedValuePath = "ID";
            CbSupplier.DisplayMemberPath = "Title";
            
        }

        private void ButtSupplierAdd_Click(object sender, RoutedEventArgs e)
        {
            List<Supplier> sup = DataBaseClass.DB.Supplier.ToList();
            
        }

        private void ButtSupplierRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Увы не работает ");
        }

        

        private void ButtUpdate_Click(object sender, RoutedEventArgs e)
        {

            MaterialEdit.Title = TbTitle.Text;
            MaterialEdit.MaterialTypeID = CbMaterialType.SelectedIndex + 1;
            MaterialEdit.CountInStock = Convert.ToSingle(TbCountInStock.Text);
            MaterialEdit.Unit = TbUnit.Text;
            MaterialEdit.CountInPack = Convert.ToInt32(TbCountInPack.Text);
            MaterialEdit.MinCount = Convert.ToInt32(TbMinCount.Text);
            MaterialEdit.Cost = Convert.ToInt32(TbCost.Text);
            MaterialEdit.Description = TbDescription.Text;
            MaterialEdit.Image = path;
            if (IsCreate == true)
            {
                DataBaseClass.DB.Material.Add(MaterialEdit);
            }
            DataBaseClass.DB.SaveChanges();
            MessageBox.Show(MaterialEdit.ID.ToString());
            List<MaterialSupplier> materialSuppliersOld = MS.Where(x => x.MaterialID == MaterialEdit.ID).ToList();
            if (materialSuppliersOld.Count != 0)
            {
                foreach (MaterialSupplier ms in materialSuppliersOld)
                {
                    DataBaseClass.DB.MaterialSupplier.Remove(ms);
                }
            }
           
            DataBaseClass.DB.SaveChanges();
            MessageBox.Show("Данные записаны");
            
        }

        private void ButtDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtEditImage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
