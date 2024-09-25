using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.IO;
using System.Configuration;
using Microsoft.Owin.Security;
using System.Web.UI.HtmlControls;
using EllipticCurve.Utils;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;

namespace AchillesHeel_RG.Controllers
{
    public class ProductsController : Controller
    {

        public ApplicationUserManager UserManager;

        public ProductsController()
        {

        }
        //adding an instance of the DB in the controller so the admin has access to the DB
        private AchillesHeelDbContext context = new AchillesHeelDbContext();

        public ActionResult AllProducts()
        {
            //gets all products that have an image and are active on the site
            var products = context.Products.DistinctBy(p => p.ProductName).Where(p => p.ImageUrl != null && p.NotActive == false).ToList();

            //refrreshes the entities
            foreach (var item in products.ToList())
            {
                context.Entry(item).Reload();
            }

            //sends a list of categories to the view
            ViewBag.Categories = context.ProductCategories.ToList();

            return View(products);
        }

        public ActionResult AllProductsByCategory(int? id)
        {
            //gets all the products that belong to the specific category ID
            var products = context.Products.Where(p => p.ProductCategoryId == id).DistinctBy(p => p.ProductName).ToList();

            //passing the list of product categories to the ViewAllProducts view
            ViewBag.Categories = context.ProductCategories.ToList();

            return View("AllProducts", products);
        }

        /// <summary>
        /// returns the approrpiate view that will display the product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finds the product by ID
            Product product = context.Products.Find(id);

            //gets all the products that have the same name and ARE active
            var products = context.Products.Where(p => p.ProductName.Equals(product.ProductName) && p.NotActive == false).ToList();

            //passing the product image to the view to display
            ViewBag.Image = product.ImageUrl;

            if (product == null)
            {
                return HttpNotFound();
            }
            if (product is Shoe)
            {
                //delcaring a new list of shoes
                List<Shoe> shoes = new List<Shoe>();

                //loops through the list of products and casts each item into SHOE object 
                //so that we can make use of the 'Size' property. 
                foreach (var item in products)
                {
                    shoes.Add((Shoe)item);
                }

                //passing the list of shoes into a the viewbag and displaying by the SIZE
                ViewBag.AvailableSizes = new SelectList(shoes, "ProductId", "Size");            

                return View("DetailsShoe", (Shoe)product);
            }
            if (product is Clothing)
            {

                //delcaring a new list of shoes
                List<Clothing> clothing = new List<Clothing>();

                //loops through the list of products and casts each item into SHOE object 
                //so that we can make use of the 'Size' property. 
                foreach (var item in products)
                {
                    clothing.Add((Clothing)item);
                }

                //passing the list of shoes into a the viewbag and displaying by the SIZE
                ViewBag.AvailableSizes = new SelectList(clothing, "ProductId", "Size");

                return View("DetailsClothing", (Clothing)product);
            }
            return HttpNotFound();
        }

        /// <summary>
        /// returns the view model empty for a shoe to be added to the context
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateShoe()
        {
            //create an instance of the create shoe view model
            CreateShoeViewModel shoe = new CreateShoeViewModel();

            var shoeCategories = context.ProductCategories.Where(c => c.SubCategory.Equals("Shoes")).ToList();

            //passing the categories to the view
            ViewBag.ShoeCategories = new SelectList(shoeCategories, "CategoryId", "CategoryType");

            //send the createshoe model to the view
            return View(shoe);
        }
        /// <summary>
        /// post method - applies the view model data to a new instance of the shoe object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateShoe([Bind(Include = "ProductName,ProductPrice,SalePercentage,StockLevel,Description,Colour,Size,NotActive")] 
        CreateShoeViewModel model, HttpPostedFileBase file)       
        {
            //if model is not null
            if (ModelState.IsValid)
            {
                //build the staff
                Shoe shoe = new Shoe
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    SalePercentage = model.SalePercentage,
                    StockLevel = model.StockLevel,
                    Description = model.Description,
                    Colour = model.Colour,
                    Size = model.Size,
                    NotActive = model.NotActive,
                };
                //gets the category id from the drop down list and applies it to the new shoe object
                var categoryId = Request.Form["CategoryId"];
                shoe.ProductCategoryId = (Int32.Parse(categoryId));

                context.Products.Add(shoe);
                context.SaveChanges();

                if(file != null)
                {
                    int dotPosition = Path.GetFileName(file.FileName).IndexOf('.');

                    string fileExtention = Path.GetFileName(file.FileName).Substring(dotPosition);

                    var fileName = shoe.ProductId + fileExtention;

                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    file.SaveAs(path);

                    shoe.ImageUrl = "Images/" + fileName;
                    context.Products.AddOrUpdate(shoe);
                    context.SaveChanges();
                    //C:\Users\grant\OneDrive\Desktop\College\Graded Unit\Development\AchillesHeel_RG\AchillesHeel_RG\Images\UploadedImages\
                }

                return RedirectToAction("ViewAllProducts", "Admin");
            }


            var shoeCategories = context.ProductCategories.Where(c => c.SubCategory.Equals("Shoes")).ToList();
            //passing the categories to the view
            ViewBag.ShoeCategories = new SelectList(shoeCategories, "CategoryId", "CategoryType");

            //if we get here then something is wrong - return the view
            return View(model);
        }

        /// <summary>
        /// get method - returns the view model for creating clothing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateClothing()
        {
            //create an instance of the create member view model
            CreateClothingViewModel clothing = new CreateClothingViewModel();

            //getting a list of all categories with the sub category of clothing
            var clothingCategories = context.ProductCategories.Where(c => c.SubCategory.Equals("Clothing")).ToList();

            //passing the roles to the view
            ViewBag.ClothingCategories = new SelectList(clothingCategories, "CategoryId", "CategoryType");

            //send the createclothing model to the view
            return View(clothing);
        }
        /// <summary>
        /// post method - takes the model data and applies it to a new instance of clothing object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateClothing(
            [Bind(Include = "ProductName,ProductPrice,SalePercentage,StockLevel,Description,Colour,Size,PitToPit,Height,NotActive")] 
        CreateClothingViewModel model, HttpPostedFileBase file)
        {
            //if model is not null
            if (ModelState.IsValid)
            {
                //build the clothing item through the model
                Clothing clothing = new Clothing
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    SalePercentage = model.SalePercentage,
                    StockLevel = model.StockLevel,
                    Description = model.Description,
                    Colour = model.Colour,
                    Size = model.Size,
                    PitToPit = model.PitToPit,
                    Height = model.Height,
                    NotActive = model.NotActive
                };

                //gets the ID from the drop down list 
                var categoryId = Request.Form["CategoryId"];
                //asigns the ID to the clothing object
                clothing.ProductCategoryId = (Int32.Parse(categoryId));

                //adds to the context
                context.Products.Add(clothing);
                context.SaveChanges();

                if (file != null)
                {
                    int dotPosition = Path.GetFileName(file.FileName).IndexOf('.');

                    string fileExtention = Path.GetFileName(file.FileName).Substring(dotPosition);

                    var fileName = clothing.ProductId + fileExtention;

                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    file.SaveAs(path);

                    clothing.ImageUrl = "Images/" + fileName;
                    context.Products.AddOrUpdate(clothing);
                    context.SaveChanges();
                    //C:\Users\grant\OneDrive\Desktop\College\Graded Unit\Development\AchillesHeel_RG\AchillesHeel_RG\Images\UploadedImages\
                }

                return RedirectToAction("ViewAllProducts", "Admin");
            }

            //getting a list of all categories with the sub category of clothing
            var clothingCategories = context.ProductCategories.Where(c => c.SubCategory.Equals("Clothing")).ToList();
            //passing the roles to the view
            ViewBag.ClothingCategories = new SelectList(clothingCategories, "CategoryId", "CategoryType");
            //if we get here then something is wrong - return the view
            return View(model);
        }
        /// <summary>
        /// gets the sleected product and populates the view model with data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult EditClothing(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clothing product = (Clothing)context.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            //gets all the categories from the context
            var categories = context.ProductCategories.ToList();

            //loops through the list of categories and removes any that arent related to shoes
            foreach (var item in categories.ToList())
            {
                if (item.SubCategory.Equals("Shoes") || item.SubCategory.Equals("Accessories"))
                {
                    categories.Remove(item);
                }
            }

            //passing the categories to the view
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryType");

            return View(new EditClothingViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                SalePercentage = product.SalePercentage,
                StockLevel = product.StockLevel,
                Description = product.Description,
                Colour = product.Colour,
                Size = product.Size,
                PitToPit = product.PitToPit,
                Height = product.Height,
                IsActive = product.NotActive,
            }); ;

        }
        /// <summary>
        /// Applies the model values to the product and updates the context
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> EditClothing(
            [Bind(Include = "Id,ProductName,ProductPrice,SalePercentage,StockLevel,Gender,Description,Colour,Size,PitToPit,Height,NotActive")] EditClothingViewModel model)
        {
            //finds clothing by Id
            Clothing product = (Clothing)context.Products.Find(model.Id);

            //gets the original sale percentage from temp data - so we can always calucate the original product price 
            string originalSalePercentage_String = TempData["SalePercentage"].ToString();
            int originalSalePercentage = int.Parse(originalSalePercentage_String);

            //gets the original sale percentage from temp data - so we can always calucate the original product price 
            string originalPrice_String = TempData["OriginalPrice"].ToString();
            decimal price = decimal.Parse(originalPrice_String);

            //gets all the products with the sae name to list
            var products = context.Products.Where(p => p.ProductName == product.ProductName).ToList();

            if (ModelState.IsValid)
            {
                UpdateModel(model);//updates the post object details using the model

                product.ProductId = model.Id;
                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                product.StockLevel = model.StockLevel;
                product.Description = model.Description;
                product.Colour = model.Colour;
                product.Size = model.Size;
                product.PitToPit = model.PitToPit;
                product.Height = model.Height;
                product.NotActive = model.IsActive;

                //if sale percentage was updated
                if (model.SalePercentage != product.SalePercentage)
                {
                    //if users enters a negative sale percentage
                    if (model.SalePercentage < 0)
                    {
                        return View(product.ProductId);
                        //throw error 
                    }
                    else
                    {
                        //Calculates the original price everytime before we do anything else
                        //otherwise we'd lose the original price of the product and the sale percentage wouldnt be acurate
                        //and stores it in a variable to be used later
                        decimal discountAmount = 100;
                        discountAmount = discountAmount - originalSalePercentage;

                        decimal originalPrice = price / discountAmount;
                        originalPrice = originalPrice * 100; //originalprice times 100

                        if (model.SalePercentage > 0)
                        {
                            //calculates the amount to discount and subtracts from the product price
                            discountAmount = (decimal)model.SalePercentage / 100;
                            discountAmount = originalPrice * discountAmount;

                            //subtracts the discount amount from the price to get the new product pirce
                            product.ProductPrice = originalPrice - discountAmount;

                        }
                        else if (model.SalePercentage == 0)
                        {
                            //resets the price and sale percentage back to normal
                            product.ProductPrice = originalPrice;
                            product.SalePercentage = model.SalePercentage;
                        }
                    }
                }

                //loops through all products that relate to the sepeific shoe and change their price and sale percentage accordingly
                foreach (var item in products)
                {
                    item.SalePercentage = model.SalePercentage;
                    item.ProductPrice = product.ProductPrice;
                    item.Colour = product.Colour;
                    item.Description = product.Description;

                    context.Products.AddOrUpdate(product);
                }

                //saves the context 
                context.SaveChanges();

                return RedirectToAction("ViewAllProducts", "Admin");

            }

            return View("Index");
        }
        /// <summary>
        /// gets the selected product and populates the view model with its data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult EditShoe(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Shoe product = (Shoe)context.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            //gets all the categories from the context
            var categories = context.ProductCategories.ToList();

            //loops through the list of categories and removes any that arent related to shoes
            foreach (var item in categories.ToList())
            {
                if (item.SubCategory.Equals("Clothing") || item.SubCategory.Equals("Accessories"))
                {
                    categories.Remove(item);
                }
            }

            //passing the categories to the view
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryType");

            return View(new EditShoeViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                SalePercentage = product.SalePercentage,
                StockLevel = product.StockLevel,
                Description = product.Description,
                Colour = product.Colour,
                Size = product.Size,
                IsActive = product.NotActive,
            }); ;

        }
        /// <summary>
        /// Applies the model values to the product and updates the context
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> EditShoe(
            [Bind(Include = "Id,ProductName,ProductPrice,SalePercentage,StockLevel,Gender,Description,Colour,Size,NotActive")] EditShoeViewModel model)
        {
            //fuinds the shoe by ID
            Shoe product = (Shoe)context.Products.Find(model.Id);
            //gets the original sale percentage from temp data - so we can always calucate the original product price 
            string originalSalePercentage_String = TempData["SalePercentage"].ToString();
            int originalSalePercentage = int.Parse(originalSalePercentage_String);

            //gets the original sale percentage from temp data - so we can always calucate the original product price 
            string originalPrice_String = TempData["OriginalPrice"].ToString();
            decimal price = decimal.Parse(originalPrice_String);

            //gets all the products with the sae name to list
            var products = context.Products.Where(p => p.ProductName == product.ProductName).ToList();


            if (ModelState.IsValid)
            {
                UpdateModel(model);//updates the post object details using the model

                product.ProductId = model.Id;
                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                product.StockLevel = model.StockLevel;
                product.Description = model.Description;
                product.Colour = model.Colour;
                product.Size = model.Size;
                product.NotActive = model.IsActive;

                //if sale percentage was updated
                if (model.SalePercentage != product.SalePercentage)
                {
                    //if users enters a negative sale percentage
                    if (model.SalePercentage < 0)
                    {
                        return View(product.ProductId);
                        //throw error 
                    }
                    else
                    {
                        //Calculates the original price everytime before we do anything else
                        //otherwise we'd lose the original price of the product and the sale percentage wouldnt be acurate
                        //and stores it in a variable to be used later
                        decimal discountAmount = 100;
                        discountAmount = discountAmount - originalSalePercentage;

                        decimal originalPrice = price / discountAmount;
                        originalPrice = originalPrice * 100; //originalprice times 100

                        if (model.SalePercentage > 0)
                        {
                            //calculates the amount to discount and subtracts from the product price
                            discountAmount = (decimal)model.SalePercentage / 100;
                            discountAmount = originalPrice * discountAmount;

                            //subtracts the discount amount from the price to get the new product pirce
                            product.ProductPrice = originalPrice - discountAmount;

                        }
                        else if (model.SalePercentage == 0)
                        {
                            //resets the price and sale percentage back to normal
                            product.ProductPrice = originalPrice;
                            product.SalePercentage = model.SalePercentage;
                        }
                    }
                }

                //loops through all products that relate to the sepeific shoe and change their price and sale percentage accordingly
                foreach (var item in products)
                {
                    item.ProductName = model.ProductName;
                    item.SalePercentage = model.SalePercentage;
                    item.ProductPrice = product.ProductPrice;
                    item.Colour = product.Colour;
                    item.Description = product.Description;

                    context.Products.AddOrUpdate(product);
                }

                //saves the context 
                context.SaveChanges();

                return RedirectToAction("ViewAllProducts", "Admin");
            }

            return View();
        }
        /// <summary>
        /// Changes the NotActive variable to false - so that the product is no longer listed on the website to customers
        /// you can NEVER fully remove a product - need a record of everything for order history
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveProduct(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finds the product by ID
            Product p = context.Products.Find(id);

            if (p == null)
            {
                return HttpNotFound();
            }

            //changes the isActive status of the product 
            p.NotActive = false;

            UpdateModel(p);

            //updates the DB records
            context.Products.AddOrUpdate(p);
            context.SaveChanges();

            return RedirectToAction("ViewAllProducts", "Admin");
        }

    }
}
