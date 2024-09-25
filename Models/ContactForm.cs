using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class ContactForm
    {
        [Key]
        public int ContactFormId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool OpenCase { get; set; }
    }
}