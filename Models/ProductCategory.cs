using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class ProductCategory
    {
        //ProductCategory Properties

        /// <summary>
        /// unique Id for the product category
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// The name of the categrory
        /// </summary>
        [Display(Name ="Category Name")]
        public string CategoryType { get; set; }

        /// <summary>
        /// the sub category of the product
        /// </summary>
        [Display(Name = "Sub Category")]
        public string SubCategory { get; set; }


        //navigational properties

        /// <summary>
        /// List of all products associated with the category
        /// </summary>
        public List<Product> Products { get; set; }

    }
}