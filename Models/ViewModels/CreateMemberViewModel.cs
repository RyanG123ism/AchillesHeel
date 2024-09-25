using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// The View Model that will hold the attributes relevant for creating a member account manually
    /// this can only be access by the admin or the manager
    /// </summary>
    public class CreateMemberViewModel
    {
        /// <summary>
        /// email address of the new account
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }
        /// <summary>
        /// password of the new account
        /// </summary>
        [Required]
        [DataType(DataType.Password, ErrorMessage = "please enter a valid password")]
        public string Password { get; set; }
        /// <summary>
        /// first name of the new account
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength =3, ErrorMessage ="First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string FirstName { get; set; }
        /// <summary>
        /// last name of the new account
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-30 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        /// <summary>
        /// last name of the new account
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// suspsneded status of the new account
        /// </summary>
        public bool IsSuspended { get; set; }
    }
}