using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myWebshop.Models;
using SQLite;

namespace myWebshop.Models
{
    public class User
    {
        public User()
        {

        }

        public User(string username, string email, string password, string firstName, string lastName, int roleNumber)
        {
            _username = username;
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _createdAt = DateTime.Now;
            _roleNumber = roleNumber;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string _username;
        private string _email;
        private string _password;
        private string _firstName;
        private string _lastName;
        private DateTime _createdAt;
        private bool _isActive;
        private int _roleNumber;

        public string Role => Enum.GetName(typeof(Role), RoleNumber) ?? "Ismeretlen";

        public string Username { get => _username; set => _username = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public int RoleNumber { get => _roleNumber; set => _roleNumber = value; }
    }
}
