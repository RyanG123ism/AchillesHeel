using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// holds the attrabutes that define a COMMENT
    /// </summary>
    public class Comment
    {

        /// <summary>
        /// unique identifier for a comment
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// Date the comment was published
        /// </summary>
        [Display(Name = "Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CommentDate { get; set; }

        /// <summary>
        /// main text for the comment
        /// </summary>
        [Required]
        [Display(Name = "Comment:")]
        [DataType(DataType.MultilineText)]
        public string CommentBody { get; set; }
        /// <summary>
        /// Boolean status for if the comment has been removed by an admin/moderator
        /// </summary>
        public bool IsRemoved { get; set; }

    }
}