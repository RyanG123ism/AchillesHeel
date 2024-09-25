using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class Shoe : Product
    {

        /// <summary>
        /// Size of the product - letter for clothing - numerical for shoes (use isDigit method)
        /// </summary>
        public double Size { get; set; }

    }
}