using myWebshop.Models;
using myWebshop.Services;
using SQLite;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        User selectedUser;
        Customer selectedCustomer;
        public AccountWindow()
        {
            InitializeComponent();

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                selectedUser = connection.Table<User>().FirstOrDefault(user => user.Id == App.userId);
                if (selectedUser != null)
                {
                    User_TBX.Text = selectedUser.Username;
                    Email_TBX.Text = selectedUser.Email;
                    FirstName_TBX.Text = selectedUser.FirstName;
                    LastName_TBX.Text = selectedUser.LastName;
                    Role_TBC.Text = selectedUser.Role;
                    CreatedAt_TBC.Text = selectedUser.CreatedAt.ToString();
                }

                selectedCustomer = connection.Table<Customer>().FirstOrDefault(customer => customer.UserId == App.userId);
                if (selectedCustomer != null)
                {
                    PhoneNumber_TBX.Text = selectedCustomer.PhoneNumber;
                    City_TBX.Text = selectedCustomer.City;
                    Country_TBX.Text = selectedCustomer.Country;
                    BirthDate_DPR.SelectedDate = selectedCustomer.BirthDate;
                    LoyaltyPoints_TBC.Text = selectedCustomer.LoyaltyPoints.ToString();
                }
                else
                {
                    selectedCustomer = new Customer();
                }
            }

        }

        private void close_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (User_TBX.Text != "") { selectedUser.Username = User_TBX.Text; }
            if (Email_TBX.Text != "") { selectedUser.Email = Email_TBX.Text; }
            if (FirstName_TBX.Text != "") { selectedUser.FirstName = FirstName_TBX.Text; }
            if (LastName_TBX.Text != "") { selectedUser.LastName = LastName_TBX.Text; }
            if (Password_PBX.Password != "") { selectedUser.Password = PasswordHelper.HashPassword(Password_PBX.Password); }

            selectedCustomer.UserId = App.userId;
            if (PhoneNumber_TBX.Text != "") { selectedCustomer.PhoneNumber = PhoneNumber_TBX.Text; }
            if (City_TBX.Text != "") { selectedCustomer.City = City_TBX.Text; }
            if (Country_TBX.Text != "") { selectedCustomer.Country = Country_TBX.Text; }
            if (BirthDate_DPR.Text != "") { selectedCustomer.BirthDate = BirthDate_DPR.SelectedDate.Value; }

            var userRepo = new GenericRepository<User>(App.databasePath);
            userRepo.Update(selectedUser);

            var customerRepo = new GenericRepository<Customer>(App.databasePath);
            if (customerRepo.GetAll().Exists(user => user.UserId == selectedCustomer.UserId))
            {
                customerRepo.Update(selectedCustomer);
            }
            else
            {
                customerRepo.Insert(selectedCustomer);
            }
        }

        private void logOut_BTN_Click(object sender, RoutedEventArgs e)
        {
            string exePath = Process.GetCurrentProcess().MainModule!.FileName;
            Process.Start(exePath);
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
