using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace myWebshop.Model
{
    internal class Customer
    {
        [PrimaryKey, AutoIncrement]
        private int Id {  get; set; }
        private int _UserId;
        private string _PhoneNumber;
        private string _Address;
        private string _City;
        private string _Country;
        private DateTime _BirthDate;
        private int _LoyaltyPoints;

        public Customer( int userId, string phoneNumber, string address, string city, string country, DateTime birthDate, int loyaltyPoints)
        {
            _UserId = userId;
            _PhoneNumber = phoneNumber;
            _Address = address;
            _City = city;
            _Country = country;
            _BirthDate = birthDate;
            _LoyaltyPoints = loyaltyPoints;
        }

        public int UserId { get => _UserId; set => _UserId = value; }
        public string PhoneNumber { get => _PhoneNumber; set => _PhoneNumber = value; }
        public string Address { get => _Address; set => _Address = value; }
        public string City { get => _City; set => _City = value; }
        public string Country { get => _Country; set => _Country = value; }
        public DateTime BirthDate { get => _BirthDate; set => _BirthDate = value; }
        public int LoyaltyPoints { get => _LoyaltyPoints; set => _LoyaltyPoints = value; }
    }
}
