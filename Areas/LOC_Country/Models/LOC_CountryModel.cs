using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AddressBookDemo.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [Required]
        [DisplayName("Country Code")]
        public string CountryCode { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }

    }
    public class LOC_CountryDropDownModel
    {
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
