using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// viewmodel for creating a new staff member
    /// </summary>
    public class CreateStaffViewModel
    {

        /// <summary>
        /// First name of the staff member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string FirstName { get; set; }
        /// <summary>
        /// last name of the staff member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string LastName { get; set; }
        /// <summary>
        /// email address for staff member
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessage ="Invalid Email address")]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Salary of the new staff account
        /// </summary>
        [Required]
        [Display(Name = "Salary:")]
        [DataType(DataType.Currency)]
        [Range(1000,150000, ErrorMessage ="Anual Salary Must be between £1,000-£150,000")]
        [RegularExpression("^[0-9]*$", ErrorMessage ="invalid salary")]
        public double Salary { get; set; }

        /// <summary>
        /// password for the new account
        /// </summary>
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }       
       

    }
}