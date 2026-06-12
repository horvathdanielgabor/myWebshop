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
using System.Windows.Shapes;

namespace myWebshop
{
    /// <summary>
    /// Interaction logic for BuyingWindow.xaml
    /// </summary>
    public partial class BuyingWindow : Window
    {
        static Product chosenProduct;
        public BuyingWindow(int Id)
        {
            InitializeComponent();
            loadProduct(Id);
        }

        private void loadProduct(int id)
        {
            chosenProduct = (new GenericRepository<Product>(App.databasePath)).GetAll().Find(product => product.Id == id);

            name_TBC.Text = chosenProduct.Name;
            price_TBC.Text = chosenProduct.Price.ToString();
            stockQuantity_TBC.Text = chosenProduct.StockQuantity.ToString();
            brand_TBC.Text = chosenProduct.Brand;
            weight_TBC.Text = chosenProduct.Weight.ToString();

            for (int i = 0; i < chosenProduct.Category.Split(';').Length; i++)
            {
                category_TBC.Text += chosenProduct.Category.Split(';')[i];
                if (i < (chosenProduct.Category.Split(';').Length - 1))
                {
                    category_TBC.Text = ", ";
                }
            }

            description_TBC.Text = chosenProduct.Description;
        }

        private void cancel_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buyProduct_BTN_Click(object sender, RoutedEventArgs e)
        {
            (new GenericRepository<Product>(App.databasePath)).Update(chosenProduct.removeOne());
            this.Close();
        }

        private void buyBrand_BTN_Click(object sender, RoutedEventArgs e)
        {
            (new GenericRepository<Product>(App.databasePath)).Delete(chosenProduct);
            this.Close();
        }
    }
}
