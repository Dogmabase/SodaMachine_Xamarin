using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterSoda101
{
    class Cashier : ChangeDenominations
    {
        private decimal purchaseAmount;
        private decimal cashTendered;
        private decimal currentCash;

        public ChangeDenominations changeAdded = new ChangeDenominations();
        public ChangeDenominations returnChange = new ChangeDenominations();

        public Cashier()
        {
            purchaseAmount = changeAdded.CalculateTotal();
            cashTendered = returnChange.CalculateTotal();
        }

        public void AddCoin(int coin)
        {
            changeAdded.SortCoinTally(coin);
        }

        public ChangeDenominations ChangeAmounts(decimal changeAmt)
        {
            changeAmt = changeAmt * 100;
            decimal remainder = 0;
            if (changeAmt % 25 >= 0) {
                for(int i = 1; i <= Convert.ToInt32(Decimal.Truncate(changeAmt / 25)); i++) {
                    returnChange.SortCoinTally(quarter);
                }
                remainder = changeAmt % 25;
            }
            if (remainder != 0  && (remainder % 10 >= 0) && (remainder % 10 != remainder)) {
                for (int i = 1; i <= Convert.ToInt32(Decimal.Truncate(remainder / 10)); i++) {
                    returnChange.SortCoinTally(dime);
                }
                remainder = remainder % 10;
            }
            if (remainder != 0 && (remainder % 5 >= 0) && (remainder % 5 != remainder)) {
                for (int i = 1; i <= Convert.ToInt32(Decimal.Truncate(remainder / 5)); i++) {
                    returnChange.SortCoinTally(nickel);
                }
                remainder = remainder % 5;
            }
            if (remainder != 0 && (remainder % 1 >= 0) && (remainder % 1 != remainder)) {
                for (int i = 1; i <= Convert.ToInt32(Decimal.Truncate(remainder / 1)); i++) {
                    returnChange.SortCoinTally(penny);
                }
                remainder = remainder % 1;
            }
            if (remainder == 0) {
                return returnChange;
            }

            return returnChange;
        }

        public decimal returnPurchaseAmount()
        {
            return purchaseAmount;
        }

        public decimal returnCashTendered()
        {
            return cashTendered;
        }


        public override string ToString()
        {
            return "Change added: " + changeAdded.CalculateTotal().ToString() + " and change returned: " + returnChange.CalculateTotal().ToString();
        }





    }
}
