using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class Staff : User
    {
        //staff properties

        /// <summary>
        /// the salary of a staff memebr
        /// </summary>
        public double Salary { get; set; }

        //navigational properties

        /// <summary>
        /// list of posts created by the staff member
        /// </summary>
        public List<Post> Posts { get; set; }




    }
}