using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AchillesHeel_RG.Models;
using Stripe;
using Stripe.Checkout;

namespace AchillesHeel_RG.Controllers
{
    /// <summary>
    /// this method handles the STRIPE payment API
    /// index action will deal directly with the invoice generation and payment gateway
    /// success/cancelled actions are called based on the outcome of the payment.
    /// </summary>
    public class CheckoutController : Controller
    {       
        //adding an instance of the DB in the controller so the admin has access to the DB
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        /// <summary>
        /// This method will get the current Basket Order from the session and parse that object into an order format that stipe can proccess.
        /// each orderline is added into a list of 'lineItems' 
        /// an invoice is also formatted so that it can be sent to the user once they payment is made - THIS DOESNT WORK IN TEST ENVIRONMENT. THE PROJECT MUST GO LIVE FOR THIS TO WORK
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;
            var lineItems = new List<SessionLineItemOptions>();

            StripeConfiguration.ApiKey = "sk_test_51NAvgmA0PtJAlRu6iTKITFzjrgYCpeEK9LcbUR88N3SjTr1QSrPjhbQ17p7ymkZi2xcUSV08Gzfm0zDC0ZGugIi700XjjYEqS2";//FIND YOUR API KEY ON THE WEBSITE

            //loops through the orderlines and adds each one into a new list of SessionLineItemOptions
            foreach (var item in shoppingBasket.OrderLines)
            {
                Models.Product p = (Models.Product)context.Products.Find(item.ProductId);

                var sessionLineItemOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Convert.ToInt32(p.ProductPrice) * 100,
                        Currency = "gbp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = p.ProductName,
                            Description = p.Description,
                        },
                    },
                    Quantity = item.Quantity
                };

                lineItems.Add(sessionLineItemOptions);
            }

            var options = new Stripe.Checkout.SessionCreateOptions
            {          
                //line items equal to the list of orderlines
                LineItems = lineItems,
                //this will automatically generate an invoice for every order and send it to the customer
                //NOTE - whilst this is set up to work - Stripe test account done recieve automatic email conformations - they have to be sent manually on the website
                //this will only work we move from a test account to a LIVE account.
                InvoiceCreation = new SessionInvoiceCreationOptions
                {
                    Enabled = true,
                    InvoiceData = new SessionInvoiceCreationInvoiceDataOptions
                    {
                        Description = "Invoice for Product X",
                        CustomFields = new List<SessionInvoiceCreationInvoiceDataCustomFieldOptions>
                        {
                            new SessionInvoiceCreationInvoiceDataCustomFieldOptions
                            {
                                Name = "Purchase Order",
                                Value = shoppingBasket.OrderId.ToString(),
                            },
                        },
                        Footer = "Achilles Heel ltd.",
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:44358/thank-you-for-your-payment",
                CancelUrl = "https://localhost:44358/order-cancelled",
                
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }

        /// <summary>
        /// takes the order Post successful payment and updates the status to PIAD
        /// clears the shopping basket session, and returns an order success view
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {

            //gets the order from the session
            var shoppingBasket = Session["ShoppingBasket"] as Order;
            //updates the order status to awaiting shipping - meaning the payment was successful
            shoppingBasket.OrderStatus = OrderStatus.AwaitingShipping;
            shoppingBasket.IsPaid = true;
            Order updatedOrder = shoppingBasket;

            context.Orders.AddOrUpdate(updatedOrder);
            context.SaveChanges();

            //clearing the session
            Session["ShoppingBasket"] = null;

            return View();
        }
        /// <summary>
        /// takes the order post payment and updates accordingly - this method will only run if the payment is unsuccessful
        /// the basket session will remain untouched so the user can rety the payment if they chose
        /// the order status is changed to cancelled. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancelled()
        {
            //gets the order from the session
            var shoppingBasket = Session["ShoppingBasket"] as Order;
            //updates the order status to awaiting shipping - meaning the payment was successful
            shoppingBasket.OrderStatus = OrderStatus.Cancelled;
            context.Orders.AddOrUpdate(shoppingBasket);

            return View();
        }
    }
}