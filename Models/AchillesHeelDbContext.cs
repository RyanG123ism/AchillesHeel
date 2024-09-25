using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    /// <summary>
    /// context context for the application - this will information neccesary to contruct the tables in the database
    /// </summary>
    public class AchillesHeelDbContext : IdentityDbContext<User>
    {
        //CREATING TABLES FOR THE DATABASE
        /// <summary>
        /// ContactForms Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<ContactForm> ContactForms { get; set; }

        /// <summary>
        /// Products Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Product Categories Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Orders Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Orderlines Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<OrderLine> OrderLines { get; set; }

        /// <summary>
        /// Payments Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Posts Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// COmments Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<Comment> Comment { get; set; }

        /// <summary>
        /// Post Categories Table inside AchillesHeelDbContext
        /// </summary>
        public DbSet<PostCategory> PostCategories { get; set; }

        /// <summary>
        /// Addresses Table inside achillesHeelDbContext 
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// constructor for AchillesHeelDbContext
        /// </summary>
        public AchillesHeelDbContext()
            //connection string 
            : base("AchillesHeelConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<AchillesHeelDbContext>(new DatabaseInitialiser());
        }

        public static AchillesHeelDbContext Create()
        {
            return new AchillesHeelDbContext();
        }


    }
}