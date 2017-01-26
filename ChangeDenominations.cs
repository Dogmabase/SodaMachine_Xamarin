using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterSoda101
{
    public class ChangeDenominations
    {
        public const int five = 500;
        public const int dollar = 100;
        public const int quarter = 25;
        public const int dime = 10;
        public const int nickel = 5;
        public const int penny = 1;

        private int fvt; // fives tally
        private int dlt; // dollars tally
        private int qt; // quarters tally
        private int dt; // dimes tally
        private int nt; // nickels tally
        private int pt; // pennies tally
        private int ft; // foreign tally

        public ChangeDenominations()        {        }

        public void SortCoinTally(int coin)
        {
            switch (coin) {
                case five:
                    fvt++;
                    break;
                case dollar:
                    dlt++;
                    break;
                case quarter:
                    qt++;
                    break;
                case dime:
                    dt++;
                    break;
                case nickel:
                    nt++;
                    break;
                case penny:
                    pt++;
                    break;
                default:
                    ft++;
                    break;
            }
        }

        public override string ToString()
        {
            string tallies = String.Format("\nFives: {0}\nDollars: {1}\nQuarters: {2}\nDimes: {3}\nNickels: {4}\nPennies: {5}\nForeign: {6}", fvt, dlt, qt, dt, nt, pt, ft);
            return tallies;
        }

        public decimal CalculateTotal()
        {
            decimal tv = 0;

            tv += five * fvt;
            tv += dollar * dlt;
            tv += quarter * qt;
            tv += dime * dt;
            tv += nickel * nt;
            tv += penny * pt;
            tv = tv / 100;

            return tv;
        }




    }
}
