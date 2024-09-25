using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class Payment
    {
        //payment properties

        /// <summary>
        /// unique identifier for a payment
        /// </summary>
        [Key, ForeignKey("Order")]
        public int PaymentId { get; set; }
        /// <summary>
        /// the date a payment was attempted
        /// </summary>
        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// the status of the payment 
        /// </summary>
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }
        /// <summary>
        /// The card number associated with the paymenty
        /// </summary>
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        /// <summary>
        /// expiration date of the card
        /// </summary> 
        [Display(Name = "Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public string ExpiryDate { get; set; }
        /// <summary>
        /// 3 digit security number on the back of the card
        /// </summary>
        [Display(Name = "Security Number")]
        public string SecuirtyNumber { get; set; }
        /// <summary>
        /// name on the card
        /// </summary>
        [Display(Name = "Cardholder Name")]
        public string CardHolderName { get; set; }

        //navigational properties

        /// <summary>
        /// the order Object that the payment is for
        /// </summary>
        public Order Order { get; set; }


    }
}