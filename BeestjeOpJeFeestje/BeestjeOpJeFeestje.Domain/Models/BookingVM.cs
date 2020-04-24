using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public partial class BookingVM
    {
        private Booking _booking;
        public BookingVM()
        {
            _booking = new Booking();
            Discounts = new List<Discount>();
            Accessory = new List<AccessoryVM>();
            Beast = new List<BeastVM>();
        }

        public BookingVM(Booking booking)
        {
            _booking = booking;
            Discounts = new List<Discount>();
        }

        [Key]
        public int ID { get => _booking.ID; set { _booking.ID = value; } }
        public int ContactpersonID { get => _booking.ContactpersonID; set { _booking.ContactpersonID = value; } }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Datum booking")]
        public System.DateTime Date { get => _booking.Date; set { _booking.Date = value; } }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Prijs boeking")]
        public decimal Price { get => _booking.Price; set { _booking.Price = value; } }

        public ICollection<Discount> Discounts { get; set; }

        public ContactpersonVM ContactPerson { get => new ContactpersonVM(_booking.ContactPerson); set {_booking.ContactPerson = value.ContactPerson; } }

        public Booking Booking { get => _booking; }
        public virtual List<BeastVM> Beast { get => _booking.Beast.Select(b => new BeastVM(b)).ToList(); set { _booking.Beast = value.Select(b => b.Beast).ToList(); } }
        public virtual List<AccessoryVM> Accessory { get => _booking.Accessory.Select(b => new AccessoryVM(b)).ToList(); set { _booking.Accessory = value.Select(b => b.Accessory).ToList(); } }
    }
}