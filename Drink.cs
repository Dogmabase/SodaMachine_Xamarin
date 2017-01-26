using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace SemesterSoda101
{
    public class Drink
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string drinkName { get; set; }
        public decimal drinkPrice { get; set; }
        public int numDrinksInStock { get; set; }
        public int numDrinksSold { get; set; }

        //private int _id;
        //private string _drinkName;
        //private decimal _drinkPrice;
        //private int _numDrinksInStock;
        //private int _numDrinksSold;

        public Drink() { }

        public Drink(int id, string name, decimal price, int inStock, int sold)
        {
            ID = id;
            drinkName = name;
            drinkPrice = price;
            numDrinksInStock = inStock;
            numDrinksSold = sold;
        }

        //[PrimaryKey,AutoIncrement]
        //public int ID {
        //    get { return _id; }
        //    set {
        //        if (_id != value)
        //            _id = value;
        //    }
        //}

        //public string drinkName {
        //    get { return _drinkName; }
        //    set {
        //        if (_drinkName != value)
        //            _drinkName = value;
        //    }
        //}

        //public decimal drinkPrice {
        //    get { return _drinkPrice; }
        //    set {
        //        if (_drinkPrice != value)
        //            _drinkPrice = value;
        //    }
        //}

        //public int numDrinksInStock {
        //    get { return _numDrinksInStock; }
        //    set {
        //        if (_numDrinksInStock != value)
        //            _numDrinksInStock = value;
        //    }
        //}

        //public int numDrinksSold {
        //    get { return _numDrinksSold; }
        //    set {
        //        if (_numDrinksSold != value)
        //            _numDrinksSold = value;
        //    }
        //}

    }
}
