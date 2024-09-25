using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// holds the neccesary attributes for storing an address
    /// </summary>
    public class Address
    {
        //address properties

        /// <summary>
        /// unique identifier for an address
        /// </summary>
        [Key]
        public int AddressId { get; set; }
        /// <summary>
        /// first line of the address
        /// </summary>
        [Display(Name ="Address Line 1")]
        [DataType(DataType.Text)]
        public string Line1 { get; set; }
        /// <summary>
        /// second line of the address
        /// </summary>
        [Display(Name = "Address Line 2")]
        [DataType(DataType.Text)]
        public string Line2 { get; set; }
        /// <summary>
        /// The city where the address is in
        /// </summary>
        [DataType(DataType.Text)]
        public string City { get; set; }
        /// <summary>
        /// the post code associated with the address
        /// </summary>
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        //navigational properties

        /// <summary>
        /// userId that is attatched to the address
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }
        /// <summary>
        /// user object attatched to the address
        /// </summary>
        public User User { get; set; }


    }
}