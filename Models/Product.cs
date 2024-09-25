using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public abstract class Product
    {
        //product properties

        /// <summary>
        /// unique identifier for a product 
        /// </summary>
        [Key]
        public int ProductId { get; set; }
        /// <summary>
        /// name of the product
        /// </summary>
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        /// <summary>
        /// price of the product 
        /// </summary>
        [Display(Name = "Product Price")]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }
        /// <summary>
        /// the amount the product is discounted by - in percent
        /// </summary>
        [Display(Name = "Sale Amount (Percent)")]
        public int SalePercentage { get; set; }
        /// <summary>
        /// the current amount of the stock held
        /// </summary>
        [Display(Name = "Stock Level")]
        public int StockLevel { get; set; }
        /// <summary>
        /// product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// colour of the product 
        /// </summary>
        public string Colour { get; set; }
        public bool NotActive { get; set; }
        /// <summary>
        /// the path to the image of the product
        /// </summary>
        public string ImageUrl { get; set; }

        //navigational properties

        /// <summary>
        /// Id of the productcategory object associated with the product 
        /// </summary>
        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        /// <summary>
        /// productCategory object associated with the product
        /// </summary>
        public ProductCategory ProductCategory { get; set; }


    }
}