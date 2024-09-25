using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for a staff member to edit their own account
    /// </summary>
    public class EditAccountStaffViewModel
    {
        /// <summary>
        /// unique id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// dislplay name of the staff account
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Display Name must be betwen 3-25 charecters long")]
        [RegularExpression("([A-Za-z0-9]+)", ErrorMessage = "Invalid Display Name")]
        public string DisplayName { get; set; }
    }
}