using BeestjeOpJeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IBoekingRepository : IRepository<Booking>
    {
        BookingVM TempBooking { get; set; }
        IEnumerable<BeastVM> AnimalsBooked();

        IEnumerable<AccessoryVM> AvailableAccessories();
        IEnumerable<AccessoryVM> AccessoriesBooked();
        void AddBookedAccessory(AccessoryVM acc);
        void RemoveBookAccessory(AccessoryVM acc);

        void RecalculateTotalPrice(IEnumerable<BookingVM> bookings);

        bool SnowExists();
        bool FarmExists();
        bool DesertExists();

        bool PolarLionExists();
    }

    
}
