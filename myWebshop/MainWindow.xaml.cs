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
        public MainWindow()
        {
            InitializeComponent();

            loadProducts();
        }

        private void loadProducts()
        {
            productsView_STP.Children.Clear();
            productsView_STP.Children.Add(new UserControls)
        }

        private void createProduct_BTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Account_BTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}