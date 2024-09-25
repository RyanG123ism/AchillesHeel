using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// One line on an order that belongs to the Order object
    /// </summary>
    public class OrderLine
    {
        /// <summary>
        /// unique identifier for OrderLine
        /// </summary>
        [Key]
        public int OrderLineId { get; set; }
        /// <summary>
        /// the quantity of the product ordered
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// the total value of the order line
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal LineTotal { get; set; }

        //navigational properties

        /// <summary>
        /// Order id that the orderline belongs to
        /// </summary>
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        /// <summary>
        /// Order object that the orerline belongs to
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Product Id relating to the orderline
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Product Object that belong to the orderline
        /// </summary>
        public Product Product { get; set; }


    }
}