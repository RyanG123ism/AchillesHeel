using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    public class ContactFormViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30,MinimumLength =30, ErrorMessage= "Name must be between 3-30 charecters long")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(750, MinimumLength = 10, ErrorMessage = "Name must be between 10-750 charecters long")]
        public string Message { get; set; }
        
    }
}