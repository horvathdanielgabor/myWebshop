using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using myWebshop.Services;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SQLite;
using myWebshop.Models;

namespace myWebshop
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            var setup = new GenericRepository<User>(App.databasePath);
            setup.GetAll();
        }

        private void signUp_BTN_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow openRegistration = new RegistrationWindow();
            openRegistration.Show();
            this.Close();
        }

        private void login_BTN_Click(object sender, RoutedEventArgs e)
        {
            string userNameInp = loginUser_TBX.Text;
            string passwordInp = PasswordHelper.HashPassword(loginPassword_PBX.Password);

            try
            {
                if (string.IsNullOrEmpty(loginUser_TBX.Text) || string.IsNullOrEmpty(loginPassword_PBX.Password))
                {
                    throw new Exception("Hiba!\nFelhasználónév és jelszó hiányzik!");
                }

                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    var user = connection.Table<User>().FirstOrDefault(u => u.Username == userNameInp);

                    if (user == null || user.Password != passwordInp)
                    {
                        throw new Exception("Hiba!\nBelépés megtagadva!\nBot");
                    }

                    App.userId = user.Id;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System");
            }
        }
    }
}
