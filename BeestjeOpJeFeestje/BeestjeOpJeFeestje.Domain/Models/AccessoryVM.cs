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
    public partial class AccessoryVM
    {
        public AccessoryVM()
        {
            Accessory = new Accessory();
            Beast = new BeastVM();
            Booking = new List<BookingVM>();
        }

        public AccessoryVM(Accessory accessory)
        {
            Accessory = accessory;
            Beast = new BeastVM(accessory.Beast);
        }
        [Key]
        public int ID
        {
            get => Accessory.ID; set => Accessory.ID = value;
        }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [DisplayName("Naam accessoire")]
        public string Name
        {
            get => Accessory.Name; set => Accessory.Name = value;
        }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Prijs accessoire")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "De geldprijs moet positief zijn.")]
        public decimal Price
        {
            get => Accessory.Price; set => Accessory.Price = value;
        }

        [Required]
        [DisplayName("Beest")]
        public int BeastID
        {
            get => Accessory.BeastID; set => Accessory.BeastID = value;
        }

        public bool IsSelected { get; set; }

        public string Selected { get; set; } = "Selecteren";

        public string ImagePath => Name + ".png";

        public Accessory Accessory { get; }

        public BeastVM Beast { get; set; }

        public virtual List<BookingVM> Booking { get => Accessory.Booking.Select(b => new BookingVM(b)).ToList(); set { Accessory.Booking = value.Select(b => b.Booking).ToList(); } }
    }
}