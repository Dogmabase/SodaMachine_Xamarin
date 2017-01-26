using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class was replaced by the Drink Management class
/// 
/// </summary>

namespace SemesterSoda101
{
    public class DrinkMachine
    {

        public Drink ColaDrink;
        public Drink MountainDrink;
        public Drink OrangeDrink;
        public Drink WaterDrink;

        private SodaDatabase db;

        List<Drink> drinks;

        public DrinkMachine(SodaDatabase database)
        {
            db = database;
            ColaDrink = db.ColaDrink;
            MountainDrink = db.MountainDrink;
            OrangeDrink = db.OrangeDrink;
            WaterDrink = db.WaterDrink;
            drinks = new List<Drink> { ColaDrink, MountainDrink, OrangeDrink, WaterDrink };
        }

        private void DrinkSold(Drink drink)
        {
            drink.numDrinksInStock--;
            drink.numDrinksSold++;
        }

        private void UpdateDrinks()
        {
            // push to database
        }



    }
}
