using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eKreta.Models;
using SQLite;

namespace myWebshop.Model
{
    internal class User
    {

        public User(string username, string email, string passwordHash, string firstName, string lastName)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
        }

        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }
        public string Username;
        public string Email;
        public string PasswordHash;
        public string FirstName;
        public string LastName;
        public DateTime CreatedAt;
        public bool IsActive;
        public string RoleNumber;

        public string Role => Enum.GetName(typeof(Role), RoleNumber) ?? "Ismeretlen";

        
    }
}
