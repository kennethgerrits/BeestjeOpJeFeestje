using BeestjeOpJeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain
{
    public static class Validator
    {

        public static bool ExcludeDesert(BookingVM temp)
        {
            if (temp.Date.Month > 9 || temp.Date.Month < 3)
                return true;
            else
                return false;
        }

        public static bool ExcludeSnow(BookingVM temp)
        {
            if (temp.Date.Month > 5 && temp.Date.Month < 9)
                return true;
            else
                return false;
        }

        public static bool IsWeekend(BookingVM temp)
        {
            if (temp.Date.DayOfWeek == DayOfWeek.Saturday || temp.Date.DayOfWeek == DayOfWeek.Sunday)
                return true;
            else
                return false;
        }


    }
}
