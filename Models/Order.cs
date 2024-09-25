using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// Order object - holds the relevant information regarding an order placed by the user
    /// </summary>
    public class Order 
    {
        /// <summary>
        /// constructor - creates an empty instance of ordelrines
        /// </summary>
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        /// <summary>
        /// unique identifier for the Order
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// the date the order was placed
        /// </summary>
        [Display(Name ="Order Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// the total monetary value of the order
        /// </summary>
        [Display(Name = "Order Total")]
        [DataType(DataType.Currency)]
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// the current status of the order (completed/incomplete etc)
        /// </summary>
        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// true or false - if order has been paid for
        /// </summary>
        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        //navigational propreties

        /// <summary>
        /// list of orderlines that belong to an order
        /// </summary>
        public List<OrderLine> OrderLines { get; set; }

        /// <summary>
        /// User id attatched to every order
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }

        /// <summary>
        /// User Object that belongs to the order
        /// </summary>
        public User User { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        /// <summary>
        /// the Payment Object that the payment is for
        /// </summary>
        //public Payment Payment { get; set; }
    }
    /// <summary>
    /// enum that holds all the typical order statuses 
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        AwaitingPayment,
        AwaitingShipping,
        Completed,
        Shipped,
        Cancelled,
        Declined,
        Refunded,
        Disputed
    }
}