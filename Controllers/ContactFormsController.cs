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

namespace AchillesHeel_RG.Controllers
{
    public class ContactFormsController : Controller
    {
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        /// <summary>
        /// index page - returns a view that displays all contact forms
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {           
            return View(context.ContactForms.OrderBy(c=>c.SubmitDate).ToList());
        }

        /// <summary>
        /// returns a list of all open cases
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexOpenCases()
        {
            return View(context.ContactForms.Where(c=>c.OpenCase == true).OrderBy(c => c.SubmitDate).ToList());
        }

        /// <summary>
        /// returns a list of all closed cases
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexClosedCases()
        {
            return View(context.ContactForms.Where(c => c.OpenCase == false).OrderBy(c => c.SubmitDate).ToList());
        }
        
        /// <summary>
        /// returns the details of a specific contact form in a new view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = context.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        /// <summary>
        /// this action will trigger once an open case has been responded to (via email - not through the application) - and the case is now closed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CloseCase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = context.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            else
            {
                contactForm.OpenCase = false;
                context.ContactForms.AddOrUpdate(contactForm);
                context.SaveChanges();
            }

            return RedirectToAction("IndexOpenCases");

        }

        /// <summary>
        /// this action will trigger once a closed case gets reopened
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OpenCase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = context.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            else
            {
                contactForm.OpenCase = true;
                context.ContactForms.AddOrUpdate(contactForm);
                context.SaveChanges();
            }

            return RedirectToAction("IndexOpenCases");

        }



        // GET: ContactForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = context.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactForm contactForm = context.ContactForms.Find(id);
            context.ContactForms.Remove(contactForm);
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
