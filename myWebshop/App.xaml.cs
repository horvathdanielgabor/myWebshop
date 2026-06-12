using myWebshop.Models;
using SQLite;
using System.Configuration;
using System.Data;
using System.Windows;

namespace myWebshop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string database = "myWebshop.db";
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(path, database);
        public static int userId;
    }

}
