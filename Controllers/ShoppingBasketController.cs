using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
//using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace AchillesHeel_RG.Controllers
{
    /// <summary>
    /// Holds all the relevant functions for inteacting with the shopping basket
    /// Only Accessible for authorized users - logged in users.
    /// </summary>
    [Authorize]
    public class ShoppingBasketController : Controller
    {
        //adding an instance of the DB in the controller so the admin has access to the DB
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        /// <summary>
        /// index page for the basket - returns the live Sesison of a users basket
        /// </summary>
        /// <returns></returns>
        public ActionResult BasketIndex()
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //gets the current user and their addresses
            User user = (User)context.Users.Find(User.Identity.GetUserId());

            //if the shopping cart is null - create a new instance of an Order for the session
            if (shoppingBasket == null)
            {
                shoppingBasket = new Order();
            }          

            //assings user to the basket session - to be used further down the line
            shoppingBasket.UserId = user.Id;           
          
            return View(shoppingBasket);
        }

        /// <summary>
        /// this aciton is called when the 'checkout' button is pressed
        /// the shopping basket session will saved to the database as a new order with the status of - awaiting payment
        /// once the payment goes through the status will be changed
        /// </summary>
        /// <returns></returns>
        public ActionResult GoToCheckOut()
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //if the shopping cart is null then return error
            if (shoppingBasket == null)
            {
                //PLACE ERROR HERE
                return HttpNotFound();
            }


            return View();
        }

        /// <summary>
        /// Adds a product to the users basket session
        /// </summary>
        /// <param name="qty"></param>
        /// <returns></returns>
        public ActionResult AddToCart(int qty)
        {
            //gets the ID of the product from the size of product that the user picks.
            //if we just went off the model Id it could be wrong - multiple sizes = many unique product ID's 
            int id = int.Parse(Request.Form["Size"]);

            //finds the product in the context
            Product p = context.Products.Find(id);

            //if the user is trying to purchase more stock than what is capable
            if(p.StockLevel < qty)
            {
                TempData["AlertMessage"] = "We could not add that many products to your basket. Sorry :(";
                return RedirectToAction("Details", id);
            }
            else
            {
                //declares a new variable for the users ShoppingCart session
                var shoppingBasket = Session["ShoppingBasket"] as Order;

                //if the shopping cart is null - create a new instance of an Order for the session
                if (shoppingBasket == null)
                {
                    shoppingBasket = new Order();
                }

                //creating a new instance of an orderline
                //populating the orderline with the product details
                OrderLine ol = new OrderLine();
                ol.ProductId = p.ProductId;
                ol.Product = p;
                ol.Quantity = qty;
                ol.LineTotal = ol.Quantity * p.ProductPrice;

                //adds the ordelrine to the ordder
                shoppingBasket.OrderLines.Add(ol);
                shoppingBasket.OrderTotal = shoppingBasket.OrderTotal + ol.LineTotal;//updates the order total

                //saves the shopping basket back to the session
                Session["ShoppingBasket"] = shoppingBasket;
                TempData["AlertMessage"] = "Product Added to your Basket.";

                return RedirectToAction("AllProducts", "Products");
            }
            

            
        }
        /// <summary>
        /// Adds 1 to the Quantity of a product in the users basket
        /// </summary>
        /// <param name="qty"></param>
        /// <returns></returns>
        public ActionResult AddQty(int id)
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //if the shopping cart is null - create a new instance of an Order for the session
            if (shoppingBasket == null)
            {
                return RedirectToAction("BasketIndex");
            }

            //finds the product in the context
            Product p = context.Products.Find(id);

            //searches for the product in the orderlines and then checks that the 
            //quantity doesnt exceed the stock count
            //updates thes quantity
            foreach(var item in shoppingBasket.OrderLines)
            {
                if(item.ProductId == id)
                {
                    if(p.StockLevel >= item.Quantity + 1)
                    {
                        item.Quantity = item.Quantity + 1;
                        item.LineTotal = p.ProductPrice * item.Quantity;//updates the line total
                        shoppingBasket.OrderTotal = shoppingBasket.OrderTotal + p.ProductPrice;//updates order total
                        TempData["AlertMessage"] = "Your Basket Has Been Updated";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Unabale to increase the quanity of that product";
                    }
                }
            }

            //saves the updates shopping basket back to the session
            Session["ShoppingBasket"] = shoppingBasket;

            return RedirectToAction("BasketIndex");
        }
        /// <summary>
        /// Removes 1 from the quantity of a product in the users basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveQty(int id)
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //if the shopping cart is null - create a new instance of an Order for the session
            if (shoppingBasket == null)
            {
                return RedirectToAction("BasketIndex");
            }

            //finds the product in the context
            Product p = context.Products.Find(id);

            foreach (var item in shoppingBasket.OrderLines.ToList())//to list to avoid run time error - "collection has been modified"
            {       
                if (item.ProductId == id)
                {
                    //removes product if the qty is 1
                    if(item.Quantity == 1)
                    {
                        shoppingBasket.OrderLines.Remove(item);
                        shoppingBasket.OrderTotal = shoppingBasket.OrderTotal - item.LineTotal;
                        TempData["AlertMessage"] = "Item Removed";
                    }

                    item.Quantity = item.Quantity - 1;
                    item.LineTotal = p.ProductPrice * item.Quantity;//updates the line total
                    shoppingBasket.OrderTotal = shoppingBasket.OrderTotal - p.ProductPrice;//updates order total
                    TempData["AlertMessage"] = "Your Basket Has Been Updated";
                }
                
            }

            //saves the updates shopping basket back to the session
            Session["ShoppingBasket"] = shoppingBasket;

            return RedirectToAction("BasketIndex");
        }
        /// <summary>
        /// removes a product from the users shopping basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveProduct(int id)
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //if the shopping cart is null - redirect to the basket index
            if (shoppingBasket == null)
            {
                return RedirectToAction("BasketIndex");
            }

            //finds the product in the context
            Product p = context.Products.Find(id);

            foreach(var item in shoppingBasket.OrderLines.ToList())//to list to avoid run time error - "collection has been modified"
            {
                if (item.ProductId == p.ProductId)
                {
                    shoppingBasket.OrderLines.Remove(item);
                    shoppingBasket.OrderTotal = shoppingBasket.OrderTotal - item.LineTotal;
                    TempData["AlertMessage"] = "Item Removed";
                }
            }

            //saves the updates shopping basket back to the session
            Session["ShoppingBasket"] = shoppingBasket;

            return RedirectToAction("BasketIndex");
        }
        /// <summary>
        /// rerturns the users ssleetced address
        /// </summary>
        /// <param name="value_ddl"></param>
        /// <returns></returns>
        public ActionResult getAddress(string value_ddl)
        {
            int id = int.Parse(Request.Form["Addresses"]);
            Address address = context.Addresses.Find(id);
            return RedirectToAction("SelectDeliveryAddress", address);
        }

        /// <summary>
        /// returns the slected dlivery address view model empty
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectDeliveryAddress(Address address)
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;
            
            //if the shopping cart is null - redirect to the basket index
            if (shoppingBasket == null)
            {
                return RedirectToAction("BasketIndex");
            }
            
            //finds the user and sends all their saved address to a drop down list via the viewbag.
            User user = (User)context.Users.Find(shoppingBasket.UserId);
            ViewBag.Addresses = new SelectList(context.Addresses.Where(a => a.UserId == user.Id), "AddressId", "Line1");

            //if the address ID is null
            if (address == null)
            {
                //creates a new instance of the selectDliveryAddress view model and passes the exisiting order in
                SelectDeliveryAddressViewModel model = new SelectDeliveryAddressViewModel
                {
                    Order = shoppingBasket
                };

                return View(model);
            }
            else
            {               
                //populates the view model with the addresses selected by the user.
                SelectDeliveryAddressViewModel model = new SelectDeliveryAddressViewModel
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    PostCode = address.PostCode,
                    Order = shoppingBasket
                };

                return View(model);
            }
        }

        /// <summary>
        /// applies the data from the view model an address and searches for a match in the DB, and applies that to the shopping basket
        /// if no address is found then it creates a new address in the DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectDeliveryAddress(
            [Bind(Include = "Line1,Line2,City,PostCode")] SelectDeliveryAddressViewModel model)
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            //finds the user and all their saved addresses
            User user = (User)context.Users.Find(shoppingBasket.UserId);
            var addresses = context.Addresses.Where(a => a.UserId == user.Id);

            //loops through the users addresses to see if any match with whats been input in the form
            //if none match then 
            foreach (var item in addresses)
            {
                if(item.Line1.Equals(model.Line1) &&                      
                    item.City.Equals(model.City) && 
                    item.PostCode.Equals(model.PostCode))
                {
                    shoppingBasket.AddressId = item.AddressId;
                    shoppingBasket.Address = item;
                }
            }

            //if the address in the order is still null - i.e no address was found saved by the user
            if(shoppingBasket.Address == null )
            {
                //creates a new address for the user and saves it to the context
                Address newAddress = new Address
                {
                    Line1 = model.Line1,
                    Line2 = model.Line2,
                    City = model.City,
                    PostCode = model.PostCode,
                    UserId = user.Id
                };

                context.Addresses.Add(newAddress);
                context.SaveChanges();

                //adds the address to the shopping basket order
                shoppingBasket.AddressId = newAddress.AddressId;
            }
            
            //updating the session to include the new address selected/added by the user
            Session["ShoppingBasket"] = shoppingBasket;

            //running the create order method
            //this will add the order to the database and update the product stock, and return an order object
            //once the order is returned the session is updated to reflect this order.
            Session["ShoppingBasket"] = CreateOrder();

            return RedirectToAction("Index", "Checkout");

        }

        /// <summary>
        /// takes the users current shopping basket session and creates a new instance of an order that is saved to the DB with the stataus of 'awaiting payment'
        /// </summary>
        /// <returns></returns>
        public Order CreateOrder()
        {
            //declares a new variable for the users ShoppingCart session
            var shoppingBasket = Session["ShoppingBasket"] as Order;

            Order order = new Order
            {
                OrderDate = DateTime.Now,
                OrderTotal = shoppingBasket.OrderTotal,
                OrderStatus = OrderStatus.AwaitingPayment,
                IsPaid = false,
                UserId = shoppingBasket.UserId,
                AddressId = shoppingBasket.AddressId,
                OrderLines = new List<OrderLine>()
            };

            //adds the order to the context
            context.Orders.Add(order);
            context.SaveChanges();

            //loops through the orderlines in the original order from the view
            //checks that the system has adequette stock 
            foreach (var item in shoppingBasket.OrderLines)
            {
                Product p = context.Products.Find(item.ProductId);
                p.StockLevel = p.StockLevel - item.Quantity;

                item.OrderId = order.OrderId;
                item.ProductId = p.ProductId;

                if (p.StockLevel == 0)
                {
                    p.NotActive = true; //sets the product to inactive/OOS if the stock count goes to 0
                }

                //adds the orderline to the context
                context.OrderLines.Add(item);
                //updates the product in the context
                context.Products.AddOrUpdate(p);
            }

            context.SaveChanges();

            return order;
        }

    }

    
}