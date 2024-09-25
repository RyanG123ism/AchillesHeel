using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// holds the needed attributes for storing a members info
    /// </summary>
    public class Member : User
    {
        //user properties

        /// <summary>
        /// toggle for suspension of the member
        /// </summary>
        [Display(Name = "Suspended Status")]
        public bool IsSuspended { get; set; }

        /// <summary>
        /// the number of stikes a member has against their account - 3 or more will result in suspension
        /// </summary>
        public int Strikes { get; set; }
        


    }
}