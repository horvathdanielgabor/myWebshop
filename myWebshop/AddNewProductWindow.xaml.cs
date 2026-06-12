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
    /// Interaction logic for AddNewProductWindow.xaml
    /// </summary>
    public partial class AddNewProductWindow : Window
    {
        GenericRepository<Product> productRepo = new GenericRepository<Product>(App.databasePath);
        public AddNewProductWindow()
        {
            InitializeComponent();

        }

        private void close_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text + e.Text;
            e.Handled = !IsValidDouble(newText);
        }

        private void NumberOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidDouble(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsValidDouble(string text)
        {
            return double.TryParse(text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out _);
        }

        private void newProduct_Click(object sender, RoutedEventArgs e)
        {
            var products = productRepo.GetAll();
            try
            {
                if (products.Exists(product => product.Name == name_TBX.Text))
                {
                    productRepo.Update(products.Find(product => product.Name == name_TBX.Text).addMore(int.Parse(stockQuantity_TBX.Text)));
                }
                else
                {
                    if (name_TBX.Text == "" || price_TBX.Text == "" || stockQuantity_TBX.Text == "" || brand_TBX.Text == "" || weight_TBX.Text == "" || category_TBX.Text == "" || description_TBX.Text == "")
                    {
                        throw new Exception("Valamelyik mező(k) hiányosak!");
                    }

                    Product newProduct = new Product(name_TBX.Text, description_TBX.Text, int.Parse(price_TBX.Text), int.Parse(stockQuantity_TBX.Text), category_TBX.Text, brand_TBX.Text, double.Parse(weight_TBX.Text));

                    productRepo.Insert(newProduct);
                }

                name_TBX.Text = string.Empty;
                price_TBX.Text= string.Empty;
                stockQuantity_TBX.Text = string.Empty;
                brand_TBX.Text = string.Empty;
                weight_TBX.Text = string.Empty;
                category_TBX.Text = string.Empty;
                description_TBX.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"System");
            }
        }
    }
}
