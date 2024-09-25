using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AchillesHeel_RG.Models
{
    public abstract class User : IdentityUser
    {
        //user additional properties

        /// <summary>
        /// The date the user account was created
        /// </summary>
        [Display(Name ="Registered On")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Joined { get; set; }

        /// <summary>
        /// Display name for user that other user will be able to see
        /// </summary>
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// first name of the user
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// last name for the user
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        //instance of the usermanager to get the users current role
        private ApplicationUserManager userManager;

        /// <summary>
        /// using the instance of ApplicationUserManager in User class to get the users current role - to single
        /// </summary>
        [Display(Name ="Current Role")]
        public string CurrentRole
        {
            get
            {
                if (userManager == null)
                {
                    userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }

                return userManager.GetRoles(Id).Single();
            }
        }


        //NAVIGATIONAL PROPERTIES


        /// <summary>
        /// List of all comments attatched to the User - one to many
        /// </summary>
        public List<Comment> Comments { get; set; }
        /// <summary>
        /// list of all orders placed by the user
        /// </summary>
        public List<Order> Orders { get; set; }
        /// <summary>
        /// list of addresses assosiated with the user
        /// </summary>
        public List<Address> Addresses { get; set; }





        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}