using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for admin/manager editing a staff account 
    /// </summary>
    public class EditStaffViewModel
    {
        /// <summary>
        /// unqiue identifier of the staff account
        /// </summary>
        public string Id { get; set; }  
        
        /// <summary>
        /// email address of the staff account
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        /// <summary>
        /// the date the account was created
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name ="Registered on")]
        public DateTime Joined { get; set; }

        /// <summary>
        /// first Name of the staff account
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string FirstName { get; set; }

        /// <summary>
        /// last name of the staff member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-30 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string LastName { get; set; }

        /// <summary>
        /// display name of the staff member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Display Name must be betwen 3-25 charecters long")]
        public string DisplayName { get; set; }

        /// <summary>
        /// salary of the staff member
        /// </summary>
        [Required]
        [Display(Name = "Salary:")]
        [DataType(DataType.Currency)]
        [Range(1000, 150000, ErrorMessage = "Anual Salary Must be between £1,000-£150,000")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "invalid salary")]
        public double Salary { get; set; }

        /// <summary>
        /// current role of the staff member
        /// </summary>
        [Display(Name = "Current Role")]
        public string CurrentRole { get; set; }


    }
}