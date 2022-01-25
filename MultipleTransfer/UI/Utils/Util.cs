using System;
using System.Collections.Generic;
using MultipleTransfer.UI.Models;

namespace MultipleTransfer.UI.Utils
{
    public class Util
    {
        public Util()
        {
        }

        public static double TotalAmount(List<Beneficiary> spacecrafts)
        {
            double total = 0;
            foreach (Beneficiary Spacecraft in spacecrafts)
            {
                total += Convert.ToDouble(Spacecraft.Amount);
            }
            return total;
        }
    }
}
