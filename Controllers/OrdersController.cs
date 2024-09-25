using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.BuilderProperties;

namespace AchillesHeel_RG.Controllers
{
    public class OrdersController : Controller
    {
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        public ApplicationUserManager UserManager;

        /// <summary>
        /// For testing purposes
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                OrderTotal = 100,
                OrderStatus = OrderStatus.AwaitingShipping,
                IsPaid = true,
                OrderLines = new List<OrderLine>(),
                UserId = User.Identity.GetUserId(),
                AddressId = 1
            };

            context.Orders.Add(order);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// loads a new page that displays existing information of a given order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = context.Orders.Find(id);

            var user = context.Users.Find(order.UserId);

            ViewBag.User = user;

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderLines = context.OrderLines.Where(o => o.OrderId == order.OrderId).Include(o => o.Product).ToList();

            return View(order);
        }

        /// <summary>
        /// GET method for an Admin/Manager/AssistantManager creating a manual order for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateManualOrder()
        {
            CreateManualOrderViewModel model = (CreateManualOrderViewModel)TempData["model"];

            if(model == null)
            {
                CreateManualOrderViewModel NewModel = new CreateManualOrderViewModel
                {
                    Users = new SelectList(context.Users, "Id", "Email"),
                };
                return View(NewModel);
            }
            //if the user has been selected
            if(model.User != null)
            {
                //if the address has been selected - populate the products select list 
                if(model.Address != null)
                {
                    ViewBag.User = model.User;
                    ViewBag.Address = model.Address;
                    model.Products = new SelectList(context.Products, "ProductId", "ProductName");
                }
                model.Addresses = new SelectList(context.Addresses.Where(a => a.UserId == model.User.Id), "AddressId", "Line1");
            }           
            return View(model);
        }

        /// <summary>
        /// POST method - takes the details from the view model and uses it to create a new order for a user that is already 'paid' for
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateManualOrder(CreateManualOrderViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //gets the user/address/order from the View
            User user = (User)TempData["User"];
            Models.Address address = (Models.Address)TempData["address"];
            Order o = (Order)TempData["Order"];

            //creates a new order and populates it with the appropriate data
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                OrderTotal = o.OrderTotal,
                OrderStatus = OrderStatus.AwaitingShipping,
                IsPaid = true,//order created by admin so no need for payment
                UserId = user.Id,
                AddressId = address.AddressId,
                OrderLines = new List<OrderLine>()                                               
            };

            //adds the order to the context
            context.Orders.Add(order); 
            context.SaveChanges();

            //loops through the orderlines in the original order from the view
            //checks that the system has adequette stock 
            foreach(var item in o.OrderLines)
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
            return RedirectToAction("ViewAllOrders", "Admin");

        }

        /// <summary>
        /// gets the user from the DDL in the CreateManualOrder view and passes it back to the CreateManualOrder action
        /// </summary>
        /// <param name="model"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_UserSelected(CreateManualOrderViewModel model, int? i )
        {
            if(model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //gets the user from the drop down list and casts into User Object.
            string usersValue = Request.Form["Users"].ToString();
            User user = context.Users.Find(usersValue);

            //applying the data to the model
            model.User = user;

            //passing the model to the next controller action
            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }

        /// <summary>
        /// gets the address selected in the DDL in the createManualOrder view and passes that back to the CreateManualOrder action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_AddressSelected(CreateManualOrderViewModel model)
        {

            //gets the address from the drop down list and casts into Address Object.
            int addressValue = Int32.Parse(Request.Form["Addresses"]);
            Models.Address address = context.Addresses.Find(addressValue);

            //gets the user that belongs to the address
            User user = context.Users.Find(address.UserId);

            //applies the data to the model
            model.User = user;
            model.Address = address;

            //passing the model to the next controller action
            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }

        /// <summary>
        /// gets the product selected in the DDL in the CreateManualOrder view and passes it back to the createManualOrder action as a new order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_AddProduct(CreateManualOrderViewModel model)
        {
            //applying the rest data to the model
            model.User = (User)TempData["User"];
            model.Address = (Models.Address)TempData["Address"];
            Order currentOrder = (Order)TempData["Order"];

            //gets the product from the drop down list and casts into Product Object.
            int productValue = Int32.Parse(Request.Form["Products"]);
            Product product = context.Products.Find(productValue);

            //adding a new orderline of the product selected - the quantity will ALWAYS be 1 - this can be changed after the product is added.
            OrderLine ol = new OrderLine
            {
                Product = product,
                ProductId = product.ProductId,
                Quantity = 1,
                LineTotal = product.ProductPrice
            };           

            //if order is null - i.e this is the first product to be added
            if (currentOrder == null)
            {
                //creating a new instance of an empty order and assigning it to the model
                Order order = new Order();
                //adding the user to the order
                order.User = model.User;
                //adding the order to the model
                model.Order = order;
                model.Order.OrderTotal = 0;
            }
            else
            {
                model.Order = currentOrder;
            }
            
            //adding the new orderline to the Order.
            model.Order.OrderLines.Add(ol);

            //updating the order total
            model.Order.OrderTotal = model.Order.OrderTotal + ol.LineTotal;

            //passing the model to the next controller action
            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }

        /// <summary>
        /// Adds 1 qty to the manual order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_AddSingle(int id)
        {
            //gets the current order from the temp data in the view
            Order currentOrder = (Order)TempData["Order"];

            //gets the product from the ID passed in by the view
            Product p = context.Products.Find(id);

            //loops through each order line 
            foreach(var item in currentOrder.OrderLines)
            {
                //if the product is found in the orderline
                if(item.ProductId == p.ProductId)
                {
                    //if there is enough stock to add more
                    if(p.StockLevel > item.Quantity + 1)
                    {
                        item.Quantity++;//adding 1 to the qty
                        item.LineTotal = item.LineTotal + p.ProductPrice;//adding the product price to the line total
                        currentOrder.OrderTotal = currentOrder.OrderTotal + p.ProductPrice;//adding the new product to the order total
                    }
                    else
                    {
                        //TODO - THROW ERROR MESSAGE
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }                   
                }
            }

            //creating a new instance of the viewmodel and passing it back to the Create manual order page WITH the updated order
            CreateManualOrderViewModel model = new CreateManualOrderViewModel();
            model.Order = currentOrder;

            //applying the rest data to the model - if we dont do this here the view won't load property due to to the IF statements 
            model.User = (User)TempData["User"];
            model.Address = (Models.Address)TempData["Address"];

            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }
        
        /// <summary>
        /// removes 1 qty from the manual order 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_RemoveSingle(int id)
        {
            //gets the current order from the temp data in the view
            Order currentOrder = (Order)TempData["Order"];

            //gets the product from the ID passed in by the view
            Product p = context.Products.Find(id);

            //loops through each order line -- toList() so that we avoid 'collection was modified' error
            foreach (var item in currentOrder.OrderLines.ToList())
            {
                //if the product is found in the orderline
                if (item.ProductId == p.ProductId)
                {
                    //checks to make sure that the item qty is greater than 0
                    if (item.Quantity > 0)
                    {
                        //if there is only 1 qty on the item - then the product will be removed from the order line completely
                        if (item.Quantity == 1)
                        {
                            //removing and updating price
                            currentOrder.OrderTotal = currentOrder.OrderTotal - p.ProductPrice;
                            currentOrder.OrderLines.Remove(item);
                        }
                        else
                        {
                            item.Quantity--;//minus 1 to the qty
                            item.LineTotal = item.LineTotal - p.ProductPrice;//minus the product price to the line total
                            currentOrder.OrderTotal = currentOrder.OrderTotal - p.ProductPrice;//minus the product to the order total
                        }
                    }
                    else
                    {
                        //TODO - THROW ERROR MESSAGE
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
            }

            //creating a new instance of the viewmodel and passing it back to the Create manual order page WITH the updated order
            CreateManualOrderViewModel model = new CreateManualOrderViewModel();
            model.Order = currentOrder;

            //applying the rest data to the model - if we dont do this here the view won't load property due to to the IF statements 
            model.User = (User)TempData["User"];
            model.Address = (Models.Address)TempData["Address"];

            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }

        /// <summary>
        /// removes a product from a manual order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateManualOrder_RemoveProduct(int id)
        {
            //gets the current order from the temp data in the view
            Order currentOrder = (Order)TempData["Order"];

            //gets the product from the ID passed in by the view
            Product p = context.Products.Find(id);

            //loops through each order line -- toList() so that we avoid 'collection was modified' error
            foreach (var item in currentOrder.OrderLines.ToList())
            {
                //if the product is found in the orderline
                if (item.ProductId == p.ProductId)
                {
                    currentOrder.OrderTotal = currentOrder.OrderTotal - item.LineTotal;
                    currentOrder.OrderLines.Remove(item);
                }
            }

            //creating a new instance of the viewmodel and passing it back to the Create manual order page WITH the updated order
            CreateManualOrderViewModel model = new CreateManualOrderViewModel();
            model.Order = currentOrder;

            //applying the rest data to the model - if we dont do this here the view won't load property due to to the IF statements 
            model.User = (User)TempData["User"];
            model.Address = (Models.Address)TempData["Address"];

            TempData["model"] = model;

            return RedirectToAction("CreateManualOrder");
        }

        /// <summary>
        /// loads a view of the existing order status of an order, and allows the user to edit this
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdateOrderStatus(int? id)
        {
            //finds the order by ID
            Order order = context.Orders.Find(id);

            return View(order);

        }

        /// <summary>
        /// Takes the value from the selectListItem of Order status' and updates the Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            //finds the order and the user attatched to it by ID
            Order order = context.Orders.Find(id);
            User user = context.Users.Find(order.UserId);
          
            //changes the order status to the select list item value
            order.OrderStatus = orderStatus;

            //updates and saves the DB
            context.Orders.AddOrUpdate(order);
            context.SaveChanges();

            //passess the user and orderlines of the Order back to the Details page
            ViewBag.User = user;
            ViewBag.OrderLines = context.OrderLines.Where(o => o.OrderId == order.OrderId).Include(o => o.Product).ToList();

            return View("Details", order);
        }

        /// <summary>
        /// searches for an order in the context by ID
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public ActionResult SearchForOrder(string searchTerm)
        {
            //gets all the Orders in the context where the ID equals the search term
            var results = context.Orders.Where(o => o.OrderId.ToString().Equals(searchTerm)).ToList();
          
            return View("ViewAllOrders", results);
        }       

        /// <summary>
        /// changes the order status to cancelled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CancelOrder(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = context.Orders.Find(id);

            order.OrderStatus = OrderStatus.Cancelled;
            context.Orders.AddOrUpdate(order);
            context.SaveChanges();

            return RedirectToAction("ViewAllOrders", "Admin");

        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = context.Orders.Find(id);
            context.Orders.Remove(order);
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
