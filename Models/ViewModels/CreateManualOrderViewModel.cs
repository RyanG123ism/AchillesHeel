using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// place holder variables for all the data required to create a manual order by an admin
    /// </summary>
    public class CreateManualOrderViewModel
    {
        /// <summary>
        /// holds the user that the order is under
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// holds the address of the user
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// holds the current order being placed for the user
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// holds the current product being added to the order 
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// holds a list of all user for the admin to select from
        /// </summary>
        public IEnumerable<SelectListItem> Users { get; set; }
        /// <summary>
        /// holds a list of addresses that the user has
        /// </summary>
        public IEnumerable<SelectListItem> Addresses { get; set; }
        /// <summary>
        /// holds a list of all products to select from
        /// </summary>
        public IEnumerable<SelectListItem> Products { get; set; }


    }
}