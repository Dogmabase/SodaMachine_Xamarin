using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net;

/// <summary>
/// Database
/// 
/// 
/// This is the database object for the application.
/// It's main instance is created in the 
/// App.cs class, and is then passed down to the 
/// OrderSodaPage and to the ManageSodaPage 
/// respectively.
/// 
/// This uses SQLite
/// 
/// If the database already contains data,
/// or to check for proper function, please 
/// uncomment the DebugDropTable() call in 
/// the constructor.
/// 
/// </summary>

namespace SemesterSoda101
{

    public class SodaDatabase
    {
        private SQLiteConnection _connection;

        public Drink ColaDrink;
        public Drink MountainDrink;
        public Drink OrangeDrink;
        public Drink WaterDrink;

        public SodaDatabase()
        {
            CreateSodaObjects();
            _connection = DependencyService.Get<ISQLite>().getConnection();
            //DebugDropTable();
            _connection.CreateTable<Drink>();
            AddDrinks();
        }

        public void CreateSodaObjects()
        {
            ColaDrink = new Drink(1, "Cola Soda", 1.85m, 25, 0);
            MountainDrink = new Drink(2, "Mountain Soda", 2.00m, 25, 0);
            OrangeDrink = new Drink(3, "Orange Soda", 1.85m, 27, 0);
            WaterDrink = new Drink(4, "Water", 1.75m, 3, 0);
        }

        public IEnumerable<Drink> GetDrinks()
        {
            var drinks = (from d in _connection.Table<Drink>()
                          select d).ToList();
            return drinks;
        }

        public Drink GetDrink(int id)
        {
            var drink = _connection.Table<Drink>().FirstOrDefault(d => d.ID == id);
            return drink;
        }

        public void AddDrinks()
        {
            _connection.InsertOrIgnore(ColaDrink);
            _connection.InsertOrIgnore(MountainDrink);
            _connection.InsertOrIgnore(OrangeDrink);
            _connection.InsertOrIgnore(WaterDrink);
            _connection.Commit();
        }

        public void UpdateDrinkSold(int id)
        {

            Drink d = GetDrink(id);
            int numStock = d.numDrinksInStock--;
            int numSold = d.numDrinksSold++;

            _connection.Update(d);
            _connection.Commit();

        }

        public void UpdateDrinkOrderedMore(int id, int qtyOrdered)
        {

            Drink d = GetDrink(id);

            d.numDrinksInStock += qtyOrdered;


            _connection.Update(d);
            _connection.Commit();

        }

        public void DebugDropTable()
        {
            _connection.DropTable<Drink>();
        }


        //public List<Drink> DrinkLowOrOut()
        //{
        //    List<Drink> lowDrinks = new List<Drink>();
        //    List<Drink> lowDrinks = new List<Drink>();
        //    foreach (Drink d in GetDrinks()) {
        //        if (d.numDrinksInStock < 10) {

        //        }
        //        if (d.numDrinksInStock == 0) {
        //            dm.OutOfStock(d);
        //        }
        //    }
        //}


    }
}
