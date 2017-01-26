using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using System.Reflection;

/// <summary>
/// Drink Management
/// 
/// This class notifies the Main Page 
/// when a drink or any drinks are low or 
/// out of stock. The delegate will go to 
/// a method in the OrderSodaPage, which 
/// will then display a message to the user
/// asking if they would like to add more
/// drinks to the number in stock.
///
/// </summary>

namespace SemesterSoda101
{
    public class DrinkManagement
    {
        public string message;
        public Drink drink = new Drink();

        public List<Drink> lowDrinks = new List<Drink>();
        public List<Drink> outDrinks = new List<Drink>();

        private static SodaDatabase db;

        public delegate void ChangeHandler(DrinkManagement dm);
        public event ChangeHandler IsLowOrOut;

        public DrinkManagement(SodaDatabase database)
        {
            db = database;
            string message = string.Empty;
        }

        private List<Drink> CheckLowDrinks()
        {
            IEnumerable<Drink> allDrinks = new List<Drink>();

            allDrinks = db.GetDrinks();
            foreach (Drink d in allDrinks) {
                if (d.numDrinksInStock < 10) {
                    lowDrinks.Add(d);
                }
            }
            return lowDrinks;
        }

        private List<Drink> CheckOutDrinks()
        {
            IEnumerable<Drink> allDrinks = new List<Drink>();

            allDrinks = db.GetDrinks();
            foreach (Drink d in allDrinks) {
                if (d.numDrinksInStock == 0) {
                    outDrinks.Add(d);
                }
            }
            return outDrinks;
        }

        public bool NotifyIfLow()
        {
            //if (SodaDatabase.ColaDrink.numDrinksInStock < 10 || SodaDatabase.MountainDrink.numDrinksInStock < 10 || SodaDatabase.OrangeDrink.numDrinksInStock < 10 || SodaDatabase.WaterDrink.numDrinksInStock < 10) {
            //    // display an icon that when clicked single it will tell user that a drink is low, double click for details. 
            //    // If enter a password correctly on double click, then display full details of drinks in stock and drinks sold. Password is "soda"
            //}
            List<Drink> drinks = new List<Drink>();
            drinks = CheckLowDrinks();
            if (drinks.Count != 0) {
                foreach (Drink d in drinks) {
                    message = d.drinkName + " is low in stock! \nOnly " + d.numDrinksInStock + " left in stock.";
                }
                IsLowOrOut(this);
                return true;
            }
            return false;

        }

        public bool IsLow(Drink d)
        {
            if (d.numDrinksInStock < 10)  {
                return true;
            }
            return false;
        }
        
        public bool IsOut(Drink d)
        {
            if (d.numDrinksInStock == 0) {
                return true;
            }
            return false;
        }

        public bool NotifyOutOfStock()
        {
            List<Drink> drinks = new List<Drink>();
            drinks = CheckOutDrinks();
            if (drinks.Count != 0) {
                foreach (Drink d in drinks) {
                    message = d.drinkName + " is out of Stock! Please make another selection.";
                }
                IsLowOrOut(this);
                return true;
            }
            return false;
        }



    }
}
