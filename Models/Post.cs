using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class Post
    {
        //post properties

        /// <summary>
        /// unique ID of each post
        /// </summary>
        [Key]
        public int PostId { get; set; }

        /// <summary>
        /// time stamp of when the post was created
        /// </summary>       
        [Display(Name = "Date Posted:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostDate { get; set; }

        /// <summary>
        /// title of the post
        /// </summary>
        [Required]
        [Display(Name = "Post Title:")]
        public string PostTitle { get; set; }

        /// <summary>
        /// main body of text for the post
        /// </summary>
        [Required]
        [Display(Name = "Post Context:")]
        [DataType(DataType.MultilineText)]
        public string PostBody { get; set; }

        //navigational properties

        /// <summary>
        /// user ID that is attatched to the Post - one to many
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }
        /// <summary>
        /// user object that is attatched to the post - one to many
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// category id that is attatched to the post - one to one
        /// </summary>
        [ForeignKey("PostCategory")]
        public int CategoryId { get; set; }
        /// <summary>
        /// category object that is attatched to post - one to one
        /// </summary>
        public PostCategory PostCategory { get; set; }

        /// <summary>
        /// comments relating to that post
        /// </summary>
        public List<Comment> Comments { get; set; }

    }
}