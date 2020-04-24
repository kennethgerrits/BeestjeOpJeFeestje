using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Models;
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
    public partial class ContactpersonVM
    {

        private ContactPerson _person;
        public ContactpersonVM()
        {
            _person = new ContactPerson();
            //Booking = new List<BookingVM>();
        }

        public ContactpersonVM(ContactPerson contactPerson)
        {
            _person = contactPerson;
            //Booking = new List<BookingVM>(contactPerson.Booking.Select(b => new BookingVM(b)));
        }

        [Key]
        public int ID { get => _person.ID ; set {_person.ID = value; } }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Voornaam")]
        public string FirstName { get => _person.FirstName; set {_person.FirstName = value; } }

        [MaxLength(50)]
        [DisplayName("Tussenvoegsel")]
        public string InBetween { get => _person.InBetween; set { _person.InBetween = value; } }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Achternaam")]
        public string LastName { get => _person.LastName; set { _person.LastName = value; } }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Adres")]
        public string Adress { get => _person.Adress; set { _person.Adress = value; } }

        [MinLength(2)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail adres")]
        public string Email { get => _person.Email; set {_person.Email = value; } }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Telefoonnummer")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefoonnummer voldoet niet aan het format. Klopt hij wel?")]
        public string PhoneNumber { get => _person.PhoneNumber; set { _person.PhoneNumber = value; } }

        [DisplayName("Volledige naam")]
        public string Fullname
        {
            get
            {
                if (InBetween != null)
                {
                    return FirstName + " " + InBetween + " " + LastName;
                }
                return FirstName + " " + LastName;

            }
        }

        public ContactPerson ContactPerson { get => _person; }
        public virtual List<BookingVM> Booking { get => _person.Booking.Select(b => new BookingVM(b)).ToList(); set { _person.Booking = value.Select(b => b.Booking).ToList(); } }
    }
}