using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for a user to edit their own address
    /// </summary>
    public class EditAddressViewModel
    {
        [Required]
        [Display(Name = "Address Line 1:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Address line must be betwen 3-25 charecters long")]
        [RegularExpression("([A-Za-z0-9]+( [A-Za-z0-9]+)+)", ErrorMessage = "Invalid address line")]
        public string Line1 { get; set; }
        [Display(Name = "Address Line 2:")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "Address line must not exceed 25 charecters long")]
        public string Line2 { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "City must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid City")]
        public string City { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Post Code:")]
        [StringLength(7, MinimumLength = 6, ErrorMessage = "Address line must be between 6-7 charecters long and include ZERO spaces")]
        [RegularExpression("([A-Za-z0-9]+( [A-Za-z0-9]+)+)", ErrorMessage = "Invalid post code")]
        public string PostCode { get; set; }
    }
}