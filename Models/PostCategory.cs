using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// Holds the attrbutes that define a POSTCATEGORY
    /// </summary>
    public class PostCategory
    {
        //PostCategory Properties

        /// <summary>
        /// unique identifier for a category
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// enum for the type of category
        /// </summary>
        public string CategoryType { get; set; }

        //navigational properties

        /// <summary>
        /// list of posts associated with the PostCategory
        /// </summary>
        public List<Post> Posts { get; set; }

    }
}