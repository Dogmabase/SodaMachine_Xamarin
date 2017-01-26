using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

/// <summary>
/// Manage Soda Page
/// 
/// This page is displayed when the Manage Soda
/// button is clicked on the OrderSodaPage,
/// or if the stock is low for any given soda, a
/// message is displayed and the user may select
/// OrderMore. The user can only order more
/// soda if they know the password, which is displayed
/// for debugging purposes.
/// 
/// 
/// </summary>

namespace SemesterSoda101
{
    public partial class ManageSodaPage : ContentPage
    {
        private static SodaDatabase db;
        public ManageSodaPage(SodaDatabase database)
        {
            db = database;
            InitializeComponent();
        }


        private async void BackButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void BtnSubmitClicked(object sender, EventArgs e)
        {
            int qtyCola = 0, qtyMtnSoda = 0, qtyOrgSoda = 0, qtyWater = 0;
            if (etyCellPassword.Text == "Soda") {
                try { 
                    if (etyCellCola.Text != string.Empty) {
                        qtyCola = Convert.ToInt32(etyCellCola.Text);
                        db.UpdateDrinkOrderedMore(1, qtyCola);
                    }
                    if (etyCellMtn.Text != string.Empty) {
                        qtyMtnSoda = Convert.ToInt32(etyCellMtn.Text);
                        db.UpdateDrinkOrderedMore(2, qtyMtnSoda);

                    }
                    if (etyCellOrange.Text != string.Empty) {
                        qtyOrgSoda = Convert.ToInt32(etyCellOrange.Text);
                        db.UpdateDrinkOrderedMore(3, qtyOrgSoda);

                    }
                    if (etyCellWater.Text != string.Empty) {
                        qtyWater = Convert.ToInt32(etyCellWater.Text);
                        db.UpdateDrinkOrderedMore(4, qtyWater);
                    }

                    lblMessage.Text = "Success! Sodas have been ordered.\nPress back to return to main page.";
                    IEnumerable<Drink> drinks = db.GetDrinks();
                    foreach(Drink d in drinks) {
                        Debug.WriteLine(d.drinkName + " inStock:" + d.numDrinksInStock + ", sold:" + d.numDrinksSold);
                    }

                }
                catch {
                    lblMessage.Text = "Error in number conversion! Please ensure you enter only numbers.";
                }
            }
            else {
                lblMessage.Text = "Error, Wrong Password!";
            }
        }













    }
}
