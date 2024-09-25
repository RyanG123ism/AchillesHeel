using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.Ajax.Utilities;

namespace AchillesHeel_RG.Controllers
{
    [Authorize(Roles = "Admin,AssistantManager,Manager,SalesAssistant")]
    public class StaffController : Controller
    {
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        
        public ActionResult Index(string id)
        {
            Staff staff = (Staff)context.Users.Find(id);

            return View(staff);
            
        }
        /// <summary>
        /// returns the staff index page (Dashboard)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StaffDashboard()
        {
            return View();
        }

        /// <summary>
        /// returns a a list of all members - no staff members
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAllMembers()
        {
            var users = context.Users.ToList();
            List<User> members = new List<User>();
            foreach(var item in users)
            {
                if(item is Models.Member) //only adds members - not staff
                {
                    members.Add(item);
                }
            }

            return View(members);
        }

        /// <summary>
        /// takes user input and searches for users first names / last names and fulls names
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>View, List<User></returns>
        public ActionResult SearchForUser(string fullName)
        {
            
            string fName;
            string lName;

            //if the search term contains a space - then we know its 2 names the user is searching for
            if (fullName.Contains(" "))
            {
                var names = fullName.Split(' ');
                fName = names[0];
                lName = names[1];

                var usersFirstNames = context.Users.Where(u => u.FirstName.Equals(fName)).ToList();

                List<User> results = new List<User>();

                foreach (var item in usersFirstNames)
                {
                    if (item.LastName.Equals(lName))
                    {
                        results.Add(item);
                    }
                }
                return View("ViewAllMembers", results);
            }
            else//searching for first names AND last names seperately
            {
                fName = fullName;
                lName = null;

                var usersFirstNames = context.Users.Where(u => u.FirstName.Equals(fName)).ToList();
                var usersLastNames = context.Users.Where(u => u.LastName.Equals(fName)).ToList();

                //joins the list of first names and last name search results together
                var results = usersFirstNames.Concat(usersLastNames);

                return View("ViewAllMembers", results);
            }

        }

        /// <summary>
        /// returns the edit account staff view model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = (Staff)context.Users.Find(id);
            ViewBag.OriginalEmail = staff.Email;

            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(new EditAccountStaffViewModel
            {
                Id = staff.Id,
                DisplayName = staff.DisplayName               
            });
        }

        /// <summary>
        /// takes thre data from the view model and applies it to the staff memner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisplayName")] EditAccountStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                Staff staff = (Staff)context.Users.Find(TempData["Id"]);

                //context.Entry(model).State = EntityState.Modified;
                UpdateModel(model);

                //assinging model variables to member
                staff.DisplayName = model.DisplayName;

                //update and save context
                context.Users.AddOrUpdate(staff);
                context.SaveChangesAsync();

                return View("Index", staff);
            }
            return View(model);
        }

        /// <summary>
        /// returns a list of address that apply to a single user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewAddresses(string id)
        {
            Staff staff = (Staff)context.Users.Find(id);
            ViewBag.Addresses = context.Addresses.Where(a => a.UserId == staff.Id).ToList();


            return View(staff);
        }
        /// <summary>
        /// returns the add address view model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddAddress(string id)
        {
            Staff staff = (Staff)context.Users.Find(id);
            ViewBag.Staff = staff;

            if (staff == null)
            {
                return View();
            }

            AddAddressViewModel model = new AddAddressViewModel();

            return View(model);
        }

        /// <summary>
        /// Applies the view model data to create a new address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAddress([Bind(Include = "Line1,Line2,City,PostCode")] AddAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                Staff staff = (Staff)TempData["Staff"];
                //getting the current users addresses from the database - so that the list isnt null before we add the new address nd get s runn time error
                //var addresses = context.Addresses.Where(a => a.UserId == staff.Id).ToList();
                //staff.Addresses = addresses;

                UpdateModel(model);

                Address address = new Address
                {
                    Line1 = model.Line1,
                    Line2 = model.Line2,
                    City = model.City,
                    PostCode = model.PostCode,
                    //User = staff,
                    UserId = staff.Id
                };

                //staff.Addresses.Add(address);
                context.Addresses.AddOrUpdate(address);
                //context.Users.AddOrUpdate(staff);
                context.SaveChanges();

                ViewBag.Addresses = context.Addresses.Where(a => a.UserId == staff.Id).ToList();

                return View("ViewAddresses", staff);
            }
            return View(model);
        }

        /// <summary>
        /// deletes a users selected address from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveAddress(int id)
        {
            Address address = context.Addresses.Find(id);
            Staff staff = (Staff)context.Users.Find(address.UserId);

            context.Addresses.Remove(address);
            context.SaveChanges();

            //passing the list of addresses back to the view
            ViewBag.Addresses = context.Addresses.Where(a => a.UserId == staff.Id).ToList();

            return View("ViewAddresses", staff);
        }

        /// <summary>
        /// returns the edit address view model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditAddress(int id)
        {
            Address address = (Address)context.Addresses.Find(id);
            ViewBag.AddressId = address.AddressId;


            if (address == null)
            {
                return View();
            }

            EditAddressViewModel model = new EditAddressViewModel
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                PostCode = address.PostCode
            };

            return View(model);
        }
        /// <summary>
        /// applies the view model data to an existing address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAddress([Bind(Include = "Line1,Line2,City,PostCode")] EditAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                Address address = (Address)context.Addresses.Find(int.Parse(TempData["AddressId"].ToString()));

                UpdateModel(model);

                address.Line1 = model.Line1;
                address.Line2 = model.Line2;
                address.City = model.City;
                address.PostCode = model.PostCode;

                context.Addresses.AddOrUpdate(address);
                context.SaveChanges();

                Staff staff = (Staff)context.Users.Find(address.UserId);
                ViewBag.Addresses = context.Addresses.Where(a => a.UserId == staff.Id).ToList();

                return View("ViewAddresses", staff);
            }
            return View(model);
        }

        /// <summary>
        /// returns a list of all order made by one user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewAllOrders(string id)
        {
            Staff staff = (Staff)context.Users.Find(id);

            //get all the users orders to list 
            var orders = context.Orders.OrderBy(o => o.OrderDate).Include(o => o.User).Where(o => o.UserId == staff.Id).ToList();


            return View(orders);
        }

        // GET: Staff/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
