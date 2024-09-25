using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// holds the atributes needed for storing a clothing product 
    /// </summary>
    public class Clothing : Product
    {
        /// <summary>
        /// Size of the product 
        /// </summary>
        [DataType(DataType.Text)]
        public string Size { get; set; }
        /// <summary>
        /// horizontal dimensions of the product
        /// </summary>
        [Display(Name = "Pit to Pit")]
        [DataType(DataType.Text)]
        public string PitToPit { get; set; }
        /// <summary>
        /// vertical dimensions of the product
        /// </summary>
        [DataType(DataType.Text)]
        public string Height { get; set; }

    }
}