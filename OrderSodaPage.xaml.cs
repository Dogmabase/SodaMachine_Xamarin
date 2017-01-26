using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using System.Reflection;
using System.Diagnostics;
//using Java.IO;

/// <summary>
/// Order Soda Page
/// 
/// This is the Main Page of this application.
/// The "graphics" were created by me, and are 
/// clickable. The page will navigate to the 
/// ManageSodaPage on a button click, or 
/// a message is automatically displayed if
/// any soda has fewer than 10 drinks in stock.
/// The user may choose to order more drinks, or 
/// to cancel and enter the main screen.
/// 
/// Please deploy this app only with Droid.
/// The SQLite getConnection extension for the 
/// other projects is missing.
/// 
/// Caitlin Boake
///
/// </summary>

namespace SemesterSoda101
{
    public partial class OrderSodaPage : ContentPage
    {
        private int addCash = 0;
        private decimal _coh = 0;
        private bool waitingOnBoxTouch = false;
        private bool vend = false;
        private bool watchForLow = false;
        private bool outOfStock = false;

        private Cashier cashier;
        private static SodaDatabase db;

        private DrinkManagement drinkManager;
        private ManageSodaPage manageSodas;


        public OrderSodaPage(SodaDatabase database)
        {
            db = database;
            InitializeComponent();
            DrinkMachine drinkMachine = new DrinkMachine(db);
            drinkManager = new DrinkManagement(db);
            drinkManager.IsLowOrOut += DrinkManager_IsLowOrOut;
            watchForLow = drinkManager.NotifyIfLow();
            outOfStock = drinkManager.NotifyOutOfStock();
            cashier = new Cashier();
            IEnumerable<Drink> drinks = db.GetDrinks();
            foreach (Drink dr in drinks) {
                Debug.WriteLine(dr.ID + " " + dr.drinkName + " " + dr.drinkPrice + " " + dr.numDrinksInStock + " " + dr.numDrinksSold);

            }
            //if (watchForLow) {
            //    DrinkManager_IsLowOrOut(drinkManager);
            //}
        }

        private async void DrinkManager_IsLowOrOut(DrinkManagement dm)
        {
            var response = await DisplayMessage(dm.drink.ID, dm.message);
            manageSodas = new ManageSodaPage(db);
            if (response) {
                await Navigation.PushModalAsync(manageSodas);
            }
        }

        public Task<bool> DisplayMessage(int id, string message)
        {
            //bool bResponse = false;
            //Device.BeginInvokeOnMainThread(async () => {
            //    bResponse = await WaitAndExecute(10000, message);
            //});
            //return bResponse;
            return WaitAndExecute(10000, message);
        }

        protected async Task<bool> WaitAndExecute(int milisec, string message)
        {
            await Task.Delay(milisec);
            var response = await DisplayAlert("Alert!", message, "OrderMore", "Cancel");
            return response;
        }

        private void Dispense(Drink d)
        {
            if (_coh >= d.drinkPrice && !drinkManager.IsOut(d)) {
                vend = true;
                _coh -= d.drinkPrice;
                DisplayCashOnHand();
                db.UpdateDrinkSold(d.ID);
                lblMessage.Text = "Tap the dispenser to get your drink!!";
                IEnumerable<Drink> drinks = db.GetDrinks();
                foreach (Drink dr in drinks) {
                    Debug.WriteLine(dr.ID + " " + dr.drinkName + " " + dr.drinkPrice + " " + dr.numDrinksInStock + " " + dr.numDrinksSold);
                }
            }
            else {
                lblMessage.Text = "Insufficient funds!";
            }
        }

        public void DisplayCashOnHand()
        {
            decimal minCost = 1.75m;
            if (_coh >= minCost) {
                lblCashOnHand.TextColor = Color.Green;
            }
            else {
                lblCashOnHand.TextColor = Color.Red;
            }
            lblCashOnHand.Text = _coh.ToString("c");
        }

        private void SodaSingle_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                lblMessage.Text = "Cash lost! Try again.";
                waitingOnBoxTouch = false;
            }
            else {
                lblMessage.Text = "Double Tap to Order!";
            }
        }


        private void ColaDouble_Tapped(object sender, EventArgs e)
        {
            Drink d = db.GetDrink(1);
            Dispense(d);
        }

        private void MountainSodaDouble_Tapped(object sender, EventArgs e)
        {
            Drink d = db.GetDrink(2);
            Dispense(d);
        }

        private void OrangeSodaDouble_Tapped(object sender, EventArgs e)
        {
            Drink d = db.GetDrink(3);
            Dispense(d);
        }

        private void WaterDouble_Tapped(object sender, EventArgs e)
        {
            Drink d = db.GetDrink(4);
            Dispense(d);
        }


        private void AddFiveDouble_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                addCash -= 5;
                lblMessage.Text = "Add cash canceled!";
            }
            else {
                lblMessage.Text = "Tap to add cash.";
            }

        }

        private void AddFiveSingle_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                lblMessage.Text = "Tap the box to insert your cash!";
            }
            else {
                waitingOnBoxTouch = true;
                cashier.AddCoin(500);
                addCash += 5;
                lblMessage.Text = "Now tap the box to insert your cash! \nDouble tap to cancel";
            }

        }
        private void AddOneDouble_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                addCash -= 1;
                lblMessage.Text = "Add cash canceled!";
            }
            else {
                lblMessage.Text = "Tap to add cash.";
            }
        }

        private void AddOneSingle_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                lblMessage.Text = "Tap the box to insert your cash!";
            }
            else {
                waitingOnBoxTouch = true;
                cashier.AddCoin(100);
                addCash += 1;
                lblMessage.Text = "Now tap the box to insert your cash!\n\nDouble tap to cancel";
            }
        }

        private void AddCash_Tapped(object sender, EventArgs e)
        {
            if (waitingOnBoxTouch) {
                // add to cash on hand
                _coh += addCash;
                // display cash on hand and message
                lblMessage.Text = "Added Cash!";
                DisplayCashOnHand();
                // clear vars
                waitingOnBoxTouch = false;
                addCash = 0;
            }
            else {
                lblMessage.Text = "Tap the cash to add first.";
            }

        }

        private async void CoinSlotDouble_Tapped(object sender, EventArgs e)
        {
            var again = true;
            do {
                var coin = await DisplayActionSheet("Add Your Coin!", "Cancel", null, "25 c", "10 c", "5 c", "1 c");
                if (coin != "Cancel") {
                    string scoin = coin.ToString().Substring(0, 2).Trim();
                    cashier.AddCoin(Convert.ToInt32(scoin));

                    again = await DisplayAlert("Add Another?", "Add another coin?", "Yes", "No");
                }
                else {
                    break;
                }


            } while (again);

            _coh += cashier.CalculateTotal();
            DisplayCashOnHand(); // display cash on hand

        }

        private void CoinSlotSingle_Tapped(object sender, EventArgs e)
        {
            lblMessage.Text = "Double tap to Add Coin!";
        }

        private void GetSodaDouble_Tapped(object sender, EventArgs e)
        {
            if (vend) {
                lblMessage.Text = "Thanks! Enjoy!!";
                vend = false;
            }
            else {
                lblMessage.Text = "Oops!";
            }
        }

        private async void GetSodaSingle_Tapped(object sender, EventArgs e)
        {
            if (vend) {
                lblMessage.Text = "Thanks! Enjoy!!";
                vend = false;
                var response = await DisplayAlert("Continue Purchase?", "Would you like to buy another drink?", "Yes", "No thanks");
                if (!response) {
                    cashier.ChangeAmounts(_coh);
                    lblMessage.Text = "Your change is " + _coh.ToString("c") + "\n" + cashier.returnChange.ToString() + "\nThank you!!";
                    _coh = 0;
                    DisplayCashOnHand();
                }
            }
            else {
                lblMessage.Text = "Oops!";
            }
        }

        private async void CancelButton_Tapped(object sender, EventArgs e)
        {
            var cancel = await DisplayAlert("Cancel?", "Cancel order and get change?", "Yes, Cancel", "No, Continue");
            if (cancel) {
                cashier.ChangeAmounts(_coh);
                _coh = 0;
                DisplayCashOnHand();
                lblMessage.Text = "Your change is: \n" + cashier.returnChange.ToString() + "Thank you!";
                // play bucket animation?
            }
        }

        private async void btnManageSoda_Clicked(object sender, EventArgs e)
        {
            manageSodas = new ManageSodaPage(db);
            await Navigation.PushModalAsync(manageSodas);
        }
    }
}
