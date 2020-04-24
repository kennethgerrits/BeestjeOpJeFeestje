using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System.Data.Entity.Migrations;
using BeestjeOpJeFeestje.Domain.Models;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class BoekingRepository : Repository<Booking>, IBoekingRepository 
    {
        public BoekingRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {
            TempBooking = new BookingVM();
        }

        public BookingVM TempBooking { get; set; }
        public IEnumerable<BeastVM> AnimalsBooked()
        {
            return TempBooking.Beast;
        }

        public IEnumerable<AccessoryVM> AccessoriesBooked()
        {
            return TempBooking.Accessory;
        }

        public IEnumerable<AccessoryVM> AvailableAccessories()
        {
            List<AccessoryVM> list = new List<AccessoryVM>();
            foreach (var item in AnimalsBooked())
            {
                foreach (var acc in item.Accessory)
                {
                    list.Add(acc);
                }
            }
            return list;
        }

        public void AddBookedAccessory(AccessoryVM acc)
        {
           var b = AnimalsBooked().SingleOrDefault(beast => beast.Accessory.Contains(acc));
           var a = b.Accessory.First(accs => accs.ID == acc.ID);
            a.IsSelected = true;
            a.Selected = "Deselecteren";
        }

        public void RemoveBookAccessory(AccessoryVM acc)
        {
            var b = AnimalsBooked().SingleOrDefault(beast => beast.Accessory.Contains(acc));
            var a = b.Accessory.First(accs => accs.ID == acc.ID);
            a.IsSelected = false;
            a.Selected = "Selecteren";
        }

        public void RecalculateTotalPrice(IEnumerable<BookingVM> bookings)
        {
            var context = Context.Set<Booking>();
            var list = bookings.ToList();
            for (int i = list.Count()-1; i >= 0; i--)
            {
                var temp = list[i];
                var discountcalc = new DiscountCalculator();
                discountcalc.CalculateTotalDiscount(temp.Booking);
                temp.Price = discountcalc.CalculateTotalPrice(temp.Booking);
                context.AddOrUpdate(temp.Booking);
            }
            Complete();
        }

        public bool SnowExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Sneeuw")
                {
                    return true;
                }
            }
            return false;
        }

        public bool FarmExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Boerderij")
                {
                    return true;
                }
            }
            return false;
        }

        public bool DesertExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Woestijn")
                {
                    return true;
                }
            }
            return false;
        }

        public bool PolarLionExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == "Leeuw" || list[i].Name == "Ijsbeer")
                {
                    return true;
                }
            }
            return false;
        }

    }
}
