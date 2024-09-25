using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models.ViewModels
{
    /// <summary>
    /// View model for creating a new clothing item on the application
    /// </summary>
    public class CreateClothingViewModel
    {
        /// <summary>
        /// name of the product
        /// </summary>
        [Required]
        [Display(Name = "Product Name")]
        [RegularExpression("([A-Za-z0-9]+( [A-Za-z0-9]+)+)", ErrorMessage = "Invalid product name")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Product Name must be between 4-100 charecters long")]
        public string ProductName { get; set; }
        /// <summary>
        /// price of the product 
        /// </summary>
        [Display(Name = "Product Price")]
        [Required]
        [Range(1, 10000, ErrorMessage = "Product Price cannot be below £1 or over £10000")]
        [DataType(DataType.Currency)]
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
        [Range(0, 10000, ErrorMessage = "The Stock level must be between 0 - 10,000"),]
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

        /// <summary>
        /// Size of the product 
        /// </summary>
        /// <summary>
        /// the target gender of the product
        /// </summary>
        [DataType(DataType.Text)]
        [Required]
        [RegularExpression("^(?!^(?:XS|S|M|L|XL)$).*$", ErrorMessage = "Size must be 'XS' 'S' 'M' 'L' 'XL'")]
        [StringLength(1, ErrorMessage = "Gender must Fall Under 'M' or 'F'")]
        public string Size { get; set; }

        /// <summary>
        /// horizontal dimensions of the product
        /// </summary>
        [Required]
        [Display(Name = "Pit to Pit")]
        [StringLength(5, ErrorMessage = "Pit to Pit must be 5 charecters long and in the format of '11x11'")]
        public string PitToPit { get; set; }
        /// <summary>
        /// vertical dimensions of the product
        /// </summary>
        [Required]
        [StringLength(5, ErrorMessage = "Height must be 5 charecters long and in the format of '11x11'")]
        public string Height { get; set; }

        public bool NotActive { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Uplaod Image")]
        public string File { get; set; }

    }
}