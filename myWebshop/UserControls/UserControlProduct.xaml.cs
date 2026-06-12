using myWebshop.Models;
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

namespace myWebshop.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlProduct.xaml
    /// </summary>
    public partial class UserControlProduct : UserControl
    {
        public List<Product> products;
        public Product chosenProduct;
        public MainWindow main;
        public UserControlProduct(MainWindow Main)
        {
            InitializeComponent();
            products = new List<Product>();
            main = Main;
        }

        public void SetProducts(List<Product> products)
        {
            dataGridProducts.ItemsSource = products;
        }

        private void dataGridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!(new GenericRepository<Customer>(App.databasePath)).GetAll().Exists(customer =>
                {
                    return customer.UserId == App.userId;
                }))
                {
                    throw new Exception("Hiba\nFiók nincs befejezve!");
                }

                if (dataGridProducts.SelectedItem != null)
                {
                    chosenProduct = (Product)dataGridProducts.SelectedItem;

                    if (!chosenProduct.IsAvailable)
                    {
                        throw new Exception("A termék jelenleg kivan fogyva!");
                    }

                    BuyingWindow newPurchase = new BuyingWindow((new GenericRepository<Product>(App.databasePath)).GetAll().Find(product => product.Name == chosenProduct.Name).Id);
                    newPurchase.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System");
            }
            finally
            {
                main.loadIn();
            }
        }
    }
}
