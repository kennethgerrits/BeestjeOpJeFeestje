using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain
{
    public class Discount
    {
        public Discount(String name, int discount)
        {
            PercentageDiscount = discount;
            DiscountName = name;
        }
        public int PercentageDiscount { get; set; }
        public string DiscountName { get; set; }
    }
}
