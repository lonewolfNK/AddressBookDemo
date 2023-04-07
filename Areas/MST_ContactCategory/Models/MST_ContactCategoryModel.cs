using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AddressBookDemo.Areas.MST_ContactCategory.Models
{
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }

        [Required]
        [DisplayName("Contact Category Name")]
        public string ContactCategoryName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class MST_ContactCategoryDropDownModel
    {
        public int? ContactCategoryID { get; set; }
        public string? ContactCategoryName { get; set; }
    }
}
