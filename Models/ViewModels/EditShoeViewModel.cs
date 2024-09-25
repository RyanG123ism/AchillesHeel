using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// view model for an admin or manager editing a shoe
    /// </summary>
    public class EditShoeViewModel
    {
        /// <summary>
        /// unique identitifier of the product
        /// </summary>
        [Display(Name = "Product Id")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Product Name")]
        [RegularExpression("([A-Za-z0-9]+( [A-Za-z0-9]+)+)", ErrorMessage = "Invalid product name")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Product Name must be between 4-100 charecters long")]
        public string ProductName { get; set; }

        /// <summary>
        /// price of the product 
        /// </summary>
        [Display(Name = "Product Price")]
        [DataType(DataType.Currency)]
        [Range(1, 10000, ErrorMessage = "Product Price cannot be below £1 or over £10000")]
        [Required]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// the amount the product is discounted by - in percent
        /// </summary>
        [Display(Name = "Sale Amount (Percent)")]
        [Required]
        [Range(0, 99)]
        public int SalePercentage { get; set; }

        /// <summary>
        /// the current amount of the stock held
        /// </summary>
        [Display(Name = "Stock Level")]
        [Required]
        [Range(0, 10000, ErrorMessage = "The Stock level must be between 0 - 10,000")]
        public int StockLevel { get; set; }

        /// <summary>
        /// product description
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(750, MinimumLength = 10, ErrorMessage = "Product Description must be between 1 - 750 charecters long")]
        public string Description { get; set; }

        /// <summary>
        /// colour of the product 
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Colour must be between 3-20 charecters")]
        public string Colour { get; set; }

        [Required]
        [Range(3, 14, ErrorMessage = "Size must be between 3-14")]
        /// <summary>
        /// size of the shoe
        /// </summary>        
        public double Size { get; set; }

        /// <summary>
        /// is activ status of the shoe
        /// </summary>
        public bool IsActive { get; set; }

    }
}