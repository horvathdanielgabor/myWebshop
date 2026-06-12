using myWebshop.Models;
using myWebshop.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myWebshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List <Product> products;
        private UserControls.UserControlProduct productsUserControl;
        public MainWindow()
        {
            InitializeComponent();

            var customerSetup = (new GenericRepository<Customer>(App.databasePath)).GetAll();
            loadIn();

            if ((new GenericRepository<User>(App.databasePath)).GetAll().Find(user => user.Id == App.userId).RoleNumber > 1)
            {
                createProduct_BTN.Visibility = Visibility.Collapsed;
            }

            if ((new GenericRepository<User>(App.databasePath)).GetAll().Find(user => user.Id == App.userId).RoleNumber > 2)
            {
                MessageBox.Show("Sajnálom, de ön el van tiltva az alkalmazás használatától!","System");
                Application.Current.Shutdown();
            }
        }

        public void loadIn()
        {
            products = (new GenericRepository<Product>(App.databasePath)).GetAll();
            productsView_STP.Children.Clear();
            productsUserControl = new UserControlProduct(this);
            productsView_STP.Children.Add(productsUserControl);
            List<string> categories = [];

            products.ForEach(product =>
            {
                foreach (string category in product.Category.Split(';'))
                {
                    if (!categories.Contains(category))
                    {
                        categories.Add(category);
                    }
                }
            });

            filters_STP.Children.Clear();
            categories.ToList().ForEach(category =>
            {
                CheckBox newFilter = new CheckBox();
                newFilter.Content = category;
                newFilter.Checked += Filter_Changed;
                newFilter.Unchecked += Filter_Changed;
                filters_STP.Children.Add(newFilter);
            });

            productsUserControl.SetProducts(products);
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            List<string> selectedCategories = filters_STP.Children
                .OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => cb.Content.ToString())
                .ToList();

            List<Product> filtered;

            if (selectedCategories.Count == 0)
            {
                filtered = products; // show all
            }
            else
            {
                filtered = products.Where(product =>
                    selectedCategories.Any(cat =>
                        product.Category.Split(';').Contains(cat))
                ).ToList();
            }

            productsUserControl.SetProducts(filtered);
        }

        private void createProduct_BTN_Click(object sender, RoutedEventArgs e)
        {
            AddNewProductWindow addNewProduct = new AddNewProductWindow();
            this.Hide();
            addNewProduct.ShowDialog();
            loadIn();
            this.Show();
        }

        private void Account_BTN_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow editAccount = new AccountWindow();
            this.Hide();
            editAccount.ShowDialog();
            loadIn();
            this.Show();
        }
    }
}