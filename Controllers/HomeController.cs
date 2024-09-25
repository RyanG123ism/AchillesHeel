using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchillesHeel_RG.Controllers
{
    public class HomeController : Controller
    {
        //adding instance of the context so we can access/add data in this controller
        AchillesHeelDbContext context = new AchillesHeelDbContext();

        /// <summary>
        /// returns the home page view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //hard coded in random products to display - mostly just for asthetic purposes
            ViewBag.p1 = context.Products.Find(56);
            ViewBag.p2 = context.Products.Find(41);
            ViewBag.p3 = context.Products.Find(35);
            ViewBag.p4 = context.Products.Find(64);

            return View();
        }

        /// <summary>
        /// returns the about page view
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Returns the ContactForm ViewModel for users to send a message to the company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contact()
        {
            ContactFormViewModel model = new ContactFormViewModel();

            return View(model);
        }

        /// <summary>
        /// creates a new contact form via the details input in the view model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Contact(
            [Bind(Include = "Name,Email,PhoNumber,Message")] ContactFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                ContactForm c = new ContactForm
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    SubmitDate = DateTime.Now,
                    OpenCase = true
                };

                context.ContactForms.Add(c);
                context.SaveChanges();

                TempData["AlertMessage"] = "Your contact form has been submitted to us.";
            }
            else
            {
                return View(model);
            }
            

            return RedirectToAction("Index");
        }
    }
}