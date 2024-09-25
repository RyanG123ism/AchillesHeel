using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for creating a new member account
    /// </summary>
    public class EditAccountMemberViewModel
    {
        /// <summary>
        /// users unique id
        /// </summary>
        [Display(Name ="Account Number:")]
        public string Id { get; set; }
        /// <summary>
        /// display name for a user
        /// </summary>
        [Required]
        [Display(Name ="Display Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Display Name must be betwen 3-25 charecters long")]
        [RegularExpression("([A-Za-z0-9]+)", ErrorMessage = "Invalid Display Name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// first name of a user
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string FirstName { get; set; }
        /// <summary>
        /// last name of a user
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string LastName { get; set; }
        /// <summary>
        /// email address of a user
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

    }
}