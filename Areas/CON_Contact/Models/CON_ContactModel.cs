using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AddressBookDemo.Areas.CON_Contact.Models
{
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public int CountryID { get; set; }

        [Required]
        [DisplayName("State Name")]
        public int StateID { get; set; }

        [Required]
        [DisplayName("City Name")]
        public int CityID { get; set; }

        [Required]
        [DisplayName("Contact Category Name")]
        public int ContactCategoryID { get; set; }

        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        public string ContactCategory { get; set; }

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Pin Code")]
        public string PinCode { get; set; }

        [Required]
        [DisplayName("Mobile Number")]
        public string Mobile { get; set; }


        public string AlternateContact { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("BirthDate")]
        public DateTime BirthDate { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
}
