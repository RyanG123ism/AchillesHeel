using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for an admin or manager to edit a member
    /// </summary>
    public class EditMemberViewModel
    {
        /// <summary>
        /// email address of a member
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        /// <summary>
        /// the date the member joined
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name ="Date Registered")]
        public DateTime Joined { get; set; }
        /// <summary>
        /// first name of the member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="First Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be betwen 3-25 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string FirstName { get; set; }
        /// <summary>
        /// last name of the member
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="Last Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last Name must be betwen 3-30 charecters long")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Invalid charecters")]
        public string LastName { get; set; }
        /// <summary>
        /// display name of the member
        /// </summary>
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Display Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Display Name must be betwen 3-25 charecters long")]
        public string DisplayName { get; set; }

        [Range(0,3, ErrorMessage = "A member can only have upto 3 stirkes against their acocunt")]
        /// <summary>
        /// the amount of strikes the member has accrewed
        /// </summary>
        public int Strikes { get; set; }
        /// <summary>
        /// the suspended status of the member
        /// </summary>
        [Display(Name = "Suspended Status")]
        public bool IsSuspended { get; set; }

    }
}