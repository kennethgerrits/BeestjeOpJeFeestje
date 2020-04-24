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
    public partial class BeastVM : IValidatableObject
    {
        public BeastVM()
        {
            Beast = new Beast();
            Selected = "Selecteren";
        }

        public BeastVM(Beast beast)
        {
            Beast = beast;
            Selected = "Selecteren";
        }
        [Key]
        public int ID
        {
            get => Beast.ID;
            set => Beast.ID = value;
        }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [DisplayName("Naam beest")]
        public string Name
        {
            get => Beast.Name;

            set => Beast.Name = value;
        }

        [Required]
        [DisplayName("Type")]
        public string Type
        {
            get => Beast.Type; set => Beast.Type = value;
        }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "De geldprijs moet positief zijn.")]
        [DisplayName("Prijs beest")]
        public decimal Price
        {
            get => Beast.Price; set => Beast.Price = value;
        }

        public string Selected { get; set; } = "Selected";
        public string ImagePath => Name + ".png";

        public Beast Beast { get; }

        public virtual List<AccessoryVM> Accessory { get => Beast.Accessory.Select(a => new AccessoryVM(a)).ToList(); set { Beast.Accessory = value.Select(a => a.Accessory).ToList(); } }
        public virtual List<BookingVM> Booking { get => Beast.Booking.Select(a => new BookingVM(a)).ToList(); set { Beast.Booking = value.Select(a => a.Booking).ToList(); } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}