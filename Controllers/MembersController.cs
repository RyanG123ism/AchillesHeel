using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace AchillesHeel_RG.Controllers
{
    public class MembersController : Controller
    {
        private AchillesHeelDbContext context = new AchillesHeelDbContext();
        private ApplicationUserManager UserManager;

        /// <summary>
        /// returns the members index page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            //getting the member account
            Member member = (Member)context.Users.Find(id);

            ViewBag.Orders = context.Orders.Where(o => o.UserId == id).ToList();

            return View(member);
        }

        /// <summary>
        /// returns the members account details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GET method - returns the edit member4 view model
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

            Member member = (Member)context.Users.Find(id);
            ViewBag.OriginalEmail = member.Email;

            if (member == null)
            {
                return HttpNotFound();
            }

            return View(new EditAccountMemberViewModel
            {
                Id = member.Id,
                DisplayName = member.DisplayName,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email
            });
        }

        /// <summary>
        /// POST Method - takes the details enetered in the view model and applies them to an existing member account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisplayName,FirstName,LastName, Email")] EditAccountMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = (Member)context.Users.Find(TempData["Id"]);
                //checks if the email was modified using the temp data
                //then searches through the database to make sure that the new email isnt currently in use.
                if (!model.Email.Equals(TempData["OriginalEmail"].ToString()))
                {
                    foreach (var user in context.Users)
                    {
                        if (user.Email == model.Email)
                        {
                            return View(member);//error message here
                        }
                    }
                    UpdateModel(model);

                    //assinging model variables to member - and setting email auth to false
                    member.EmailConfirmed = false;
                    member.DisplayName = model.DisplayName;
                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.Email = model.Email;

                    //update and save context
                    context.Users.AddOrUpdate(member);
                    context.SaveChanges();

                    //THIS SHOULD SEND A NEW CONFORMATION EMAIL BUT ATM IT DOESNT WORK
                    //string code = UserManager.GenerateEmailConfirmationToken(member.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = member.Id, code = code }, protocol: Request.Url.Scheme);
                    //UserManager.SendEmail(member.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    //return View("AuthenticationEmailSent");
                }

                //context.Entry(model).State = EntityState.Modified;
                UpdateModel(model);

                //assinging model variables to member
                member.DisplayName = model.DisplayName;
                member.FirstName = model.FirstName;
                member.LastName = model.LastName;
                member.Email = model.Email;

                //update and save context
                context.Users.AddOrUpdate(member);
                context.SaveChangesAsync();

               return View("Index", member);
            }
            return View(model);
        }

        public ActionResult ViewAddresses(string id)
        {
            Member member = (Member)context.Users.Find(id);
            ViewBag.Addresses = context.Addresses.Where(a => a.UserId == member.Id).ToList();
            

            return View(member);
        }
        /// <summary>
        /// returns the Add address view model for a member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddAddress(string id)
        {
            Member member = (Member)context.Users.Find(id);
            ViewBag.Member = member;

            if (member == null)
            {
                return View();
            }

            AddAddressViewModel model = new AddAddressViewModel();

            return View(model);
        }

        /// <summary>
        /// POST method - takes the view model and uses the data input to create a new address for a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAddress([Bind(Include = "Line1,Line2,City,PostCode")] AddAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = (Member)TempData["Member"];

                UpdateModel(model);

                Address address = new Address
                {
                    Line1 = model.Line1,
                    Line2 = model.Line2,
                    City = model.City,
                    PostCode = model.PostCode,
                    UserId = member.Id
                };

                context.Addresses.AddOrUpdate(address);
                context.SaveChanges();

                ViewBag.Addresses = context.Addresses.Where(a => a.UserId == member.Id).ToList();

                return View("ViewAddresses", member);
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
            Member member = (Member)context.Users.Find(address.UserId);

            context.Addresses.Remove(address);
            context.SaveChanges();

            //passing the list of addresses back to the view
            ViewBag.Addresses = context.Addresses.Where(a => a.UserId == member.Id).ToList();

            return View("ViewAddresses", member);
        }

        /// <summary>
        /// GET method - returns the edit address view model empty
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
        /// POST method - applies the data from the edit address view model to an exisiting member address
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

                Member member = (Member)context.Users.Find(address.UserId);
                ViewBag.Addresses = context.Addresses.Where(a => a.UserId == member.Id).ToList();

                return View("ViewAddresses", member);
            }
            return View(model);
        }

        /// <summary>
        /// returns a view with a list of all ONE users orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewAllOrders(string id)
        {
            Member member = (Member)context.Users.Find(id);

            //get all the users orders to list 
            var orders = context.Orders.OrderBy(o => o.OrderDate).Include(o => o.User).Where(o => o.UserId == member.Id).ToList();


            return View(orders);
        }


        // GET: Members/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Members/Delete/5
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
