using myWebshop.Models;
using myWebshop.Services;
using SQLite;
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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        List<User> users = new List<User>();
        public RegistrationWindow()
        {
            InitializeComponent();
            Role_CBX.ItemsSource = Enum.GetNames(typeof(Role));
            var userRepo = new GenericRepository<User>(App.databasePath);
            users = userRepo.GetAll();
        }

        private void login_BTN_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow openLogin = new LoginWindow();
            openLogin.Show();
            this.Close();
        }

        private void signUp_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(registrateUser_TBX.Text) || string.IsNullOrEmpty(registrateEmail_TBX.Text) || string.IsNullOrEmpty(registrateFirstName_TBX.Text) || string.IsNullOrEmpty(registrateLastName_TBX.Text) || string.IsNullOrEmpty(registratePassword_PBX.Password) || string.IsNullOrEmpty(registratePasswordAgain_PBX.Password))
                {
                    throw new Exception("Hiba!\nAz egyik mező hiányos!");
                }

                users.ForEach(user =>
                {
                    if (user.Username == registrateUser_TBX.Text)
                    {
                        throw new Exception("Hiba!\nIlyen felhasználó létezik!");
                    }
                });

                if (PasswordHelper.HashPassword(registratePassword_PBX.Password) != PasswordHelper.HashPassword(registratePasswordAgain_PBX.Password))
                {
                    throw new Exception("Hiba!\nJelszó nem egyezik!");
                }

                string chosenRoleName = (string)Role_CBX.SelectedItem;
                Role chosenRole = (Role)Enum.Parse(typeof(Role), chosenRoleName);
                int chosenRoleId = (int)chosenRole;

                User newUser = new User(registrateUser_TBX.Text, registrateEmail_TBX.Text, PasswordHelper.HashPassword(registratePassword_PBX.Password), registrateFirstName_TBX.Text, registrateLastName_TBX.Text, chosenRoleId);

                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.Insert(newUser);

                    var insertedUser = connection.Table<User>().FirstOrDefault(u => u.Username == newUser.Username);

                    App.userId = insertedUser?.Id ?? 0;
                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System");
            }
        }
    }
}
