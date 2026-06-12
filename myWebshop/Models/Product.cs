using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWebshop.Models
{
    public class Product
    {
        public Product()
        {
            _CreatedAt = DateTime.Now;
        }

        public Product(string name, string description, decimal price, int stockQuantity, string category, string brand, double weight)
        {
            _Name = name;
            _Description = description;
            _Price = price;
            _StockQuantity = stockQuantity;
            _Category = category;
            _Brand = brand;
            _Weight = weight;
            _CreatedAt = DateTime.Now;

            _IsAvailable = _StockQuantity > 0? true: false;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string _Name;
        private string _Description;
        private decimal _Price;
        private int _StockQuantity;
        private string _Category;
        private string _Brand;
        private double _Weight;
        private bool _IsAvailable;
        private DateTime _CreatedAt;

        public string Name { get => _Name; set => _Name = value; }
        public string Description { get => _Description; set => _Description = value; }
        public decimal Price { get => _Price; set => _Price = value; }
        public int StockQuantity { get => _StockQuantity; set => _StockQuantity = value; }
        public string Category { get => _Category; set => _Category = value; }
        public string Brand { get => _Brand; set => _Brand = value; }
        public double Weight { get => _Weight; set => _Weight = value; }
        public bool IsAvailable { get => _IsAvailable; set => _IsAvailable = value; }
        public DateTime CreatedAt { get => _CreatedAt; }

        public Product addMore(int amount)
        {
            _StockQuantity+= amount;

            _IsAvailable = _StockQuantity > 0 ? true : false;

            return this;
        }

        public Product removeOne()
        {
            _StockQuantity--;

            _IsAvailable = _StockQuantity > 0 ? true : false;

            return this;
        }
    }
}
