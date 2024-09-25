using AchillesHeel_RG.Models;
using AchillesHeel_RG.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace AchillesHeel_RG.Controllers
{
    //this controller can only be accessed by the admin and manager role
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : AccountController
    {
        //adding an instance of the DB in the controller so the admin has access to the DB
        private AchillesHeelDbContext context = new AchillesHeelDbContext();
        public AdminController() : base()
        {

        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {

        }

        /// <summary>
        /// the index page for the admin dashboard - this is the first page the admin will be presented with when accessing the dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {           
            return View();
        }

        /// <summary>
        /// returns a new page with a list of all ACTIVE user accounts in the databse
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAllUsers()
        {          
            //get all the users and order them by the registration date
            var users = context.Users.OrderBy(u => u.Joined).ToList();

            foreach (var item in users)
            {
                context.Entry(item).Reload();
            }

            //passing all the roles to the view so that you can search users by role
            ViewBag.Roles = context.Roles.ToList();

            //send the list of users to the index view
            return View(users);
        }

        /// <summary>
        /// takes user input and searches for users first names / last names and fulls names
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>View, List<User></returns>
        public ActionResult SearchForUser(string fullName)
        {
            //passing all the roles to the view so that you can search users by role
            ViewBag.Roles = context.Roles.ToList();
            string fName;
            string lName;

            //if the search term contains a space - then we know its 2 names the user is searching for
            if(fullName.Contains(" "))
            {
                var names = fullName.Split(' ');
                fName = names[0];
                lName = names[1];

                var usersFirstNames = context.Users.Where(u => u.FirstName.Equals(fName)).ToList();               

                List<User> results= new List<User>();

                foreach (var item in usersFirstNames)
                {
                    if (item.LastName.Equals(lName))
                    {
                        results.Add(item);
                    }
                }
                return View("ViewAllUsers", results);
            }
            else//searching for first names AND last names seperately
            {
                fName = fullName;
                lName = null;

                var usersFirstNames = context.Users.Where(u => u.FirstName.Equals(fName)).ToList();
                var usersLastNames = context.Users.Where(u => u.LastName.Equals(fName)).ToList();

                //joins the list of first names and last name search results together
                var results = usersFirstNames.Concat(usersLastNames);

                return View("ViewAllUsers", results);
            }
            
        }

        /// <summary>
        /// DOESNT WORK Returns the "ViewAllUsers" view with all users relating to one role.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewAllUsersByRole(string id)
        {
            var role = context.Roles.Find(id);

            var userRoles = role.Users.AsEnumerable().ToList();
            var users = new List<User>();

            foreach(var item in userRoles)
            {
                User u = (User)context.Users.Find(item.UserId);
                users.Add(u);
            }
           
            //passing the list of product categories to the ViewAllUsers view
            ViewBag.Categories = context.ProductCategories.ToList();
            //passing all the roles to the view so that you can search users by role
            ViewBag.Roles = context.Roles.ToList();

            return View("ViewAllUsers", users);
        }

        /// <summary>
        /// returns an instance of the Create Member View Model ands sends it to the view
        /// </summary>
        /// <returns>View, CreateMemberViewModel</returns>
        [OverrideAuthorization]
        [Authorize(Roles = "Manager,Admin,SalesAssistant")]
        [HttpGet]
        public ActionResult CreateMember()
        {
            //create an instance of the create member view model
            CreateMemberViewModel member = new CreateMemberViewModel();

            //send the createmember model to the view
            return View(member);
        }

        /// <summary>
        /// Post Method for member creation - this will assign all the user properties to a new instance of member object, assign a role, and save into the context
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View, CreateMemberViewModel</returns>
        [HttpPost]
        [OverrideAuthorization]
        [Authorize(Roles = "Manager,Admin,SalesAssistant")]
        public ActionResult CreateMember(CreateMemberViewModel model)
        {
            //if model is not null
            if (ModelState.IsValid)
            {
                //build the staff
                Member member = new Member
                {
                    //assign the details entered in the form
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Joined = DateTime.Now,
                    DisplayName = model.Email.Split('@').ElementAtOrDefault(0),
                    Strikes = 0,
                };

                //creating the user, storing in the database and passing the password to be hashed
                var result = UserManager.Create(member, model.Password);

                //if user was stored in the database successfully
                if (result.Succeeded)
                {
                    //if IsSuspended status was checked
                    if (model.IsSuspended)
                    {
                        UserManager.AddToRole(member.Id, "Suspended");//adds to the suspended role
                    }
                    else
                    {                 
                        UserManager.AddToRole(member.Id, "Member");//adds user to the member role
                    }

                    TempData["AlertMessage"] = "User Account created!";

                    //if its not the admin or manager creating an account
                    if(User.IsInRole("SalesAssistant"))
                    {
                        return RedirectToAction("ViewAllMembers", "Staff");//redirects to the staff dashboard
                    }

                    return RedirectToAction("ViewAllUsers", "Admin");
                }
            }
            //if we get here then something is wrong - return the view
            return View(model);
        }

        /// <summary>
        /// returns an instance of the create staff view model and sends it to the view
        /// </summary>
        /// <returns>View, CreateStaffViewModel</returns>
        [HttpGet]
        public ActionResult CreateStaff()
        {
            //create an instance of the create staff view model
            CreateStaffViewModel staff = new CreateStaffViewModel();
            
            //gets all the roles from the context
            var roles = context.Roles.ToList();
            
            //loops through the list of roles and removes the roles that are alloacted to members
            foreach (var item in roles.ToList())
            {               
                if (item.Name.Equals("Member") || item.Name.Equals("Suspended"))
                {
                    roles.Remove(item);
                }
            }
            //passing the roles to the view
            ViewBag.StaffRoles = new SelectList(roles, "Id", "Name");

            //passing the viewmodel to the view           
            return View(staff);
        }
        /// <summary>
        /// Post method for staff creation - assings the users properties to a new instance of a STaff User, assings a role and saves to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateStaff(CreateStaffViewModel model)
        {
            //if model is not null
            if (ModelState.IsValid)
            {                                              
                Staff staff = new Staff
                {
                    //assign the details entered in the form to the new staff account
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    Joined = DateTime.Now,
                    DisplayName = model.Email.Split('@').ElementAtOrDefault(0),                   
                };
                 
                //create the user, and store in the database and pass the password to be hashed
                var result = UserManager.Create(staff, model.Password);
                //if user was stored in the database successfully
                if (result.Succeeded)
                {
                    var roleId = Request.Form["Id"].ToString();//gets the role ID from the drop down list

                    var role = context.Roles.Find(roleId);//finds the role in the DB using the ID

                    UserManager.AddToRole(staff.Id, role.Name);//adds the staff to the role                                      
                    //redirects to the admin index page
                    return RedirectToAction("ViewAllUsers", "Admin");
                }
            }

            //gets all the roles from the context and passes them back to the view
            var roles = context.Roles.ToList();
            ViewBag.StaffRoles = new SelectList(roles, "Id", "Name");

            //if we get here then something is wrong - direct to create staff view
            return View(model);
        }

        /// <summary>
        /// GET method - populates the view model with the choses staff details, to be edited later.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditStaff(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = context.Users.Find(id) as Staff;

            if(staff == null)
            {
                return HttpNotFound();
            }

            //gets all the roles from the context
            var roles = context.Roles.ToList();

            //loops through the list of roles and removes the roles that are alloacted to members AND the current staff members role
            foreach (var item in roles.ToList())
            {
                if (item.Name.Equals("Member") || item.Name.Equals("Suspended") || item.Name.Equals(staff.CurrentRole))
                {
                    roles.Remove(item);
                }
            }
            //passing the roles to the view
            ViewBag.StaffRoles = new SelectList(roles, "Id", "Name");

            return View(new EditStaffViewModel
            {
                Id = staff.Id,
                Email = staff.Email,
                Joined = staff.Joined,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                DisplayName = staff.DisplayName,
                Salary = staff.Salary,
                CurrentRole = staff.CurrentRole
            });

        }

        /// <summary>
        /// POST method - takes the users input and applies it to the viewmodel, then updates the choses staff members details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStaff(
            [Bind(Include = "Id,Email,Joined,FirstName,LastName,DisplayName,Salary,CurrentRole")] EditStaffViewModel model)
        {

            Staff staff = (Staff)context.Users.Find(TempData["StaffId"].ToString());

            if (ModelState.IsValid)
            {               
                var c = UserManager.GetRoles(staff.Id).Single();
                var currentRole = context.Roles.Where(r => r.Name.Equals(c)).SingleOrDefault();

                var roleId = Request.Form["Id"].ToString();//gets the role ID from the drop down list
                var role = context.Roles.Find(roleId);//finds the role in the DB using the ID

                //checks to see if a new role was selected
                if (role != null)
                {
                    //if a new role was selected then it checks that it does not equal the existing role of the staff member
                    if (!User.IsInRole(roleId))
                    {
                        UserManager.RemoveFromRole(staff.Id, currentRole.Id);
                        //if new role was selected then updates the role for the staff member
                        UserManager.AddToRoleAsync(staff.Id, role.Id);
                    }
                }

                UpdateModel(model);
                
                staff.Email = model.Email;
                staff.FirstName = model.FirstName;
                staff.LastName = model.LastName;
                staff.DisplayName = model.DisplayName;
                staff.Salary = model.Salary;

                context.Users.AddOrUpdate(staff);
                context.SaveChanges();

                return RedirectToAction("ViewAllUsers");
            }
            //gets all the roles from the context
            var roles = context.Roles.ToList();
            //loops through the list of roles and removes the roles that are alloacted to members AND the current staff members role
            foreach (var item in roles.ToList())
            {
                if (item.Name.Equals("Member") || item.Name.Equals("Suspended") || item.Name.Equals(staff.CurrentRole))
                {
                    roles.Remove(item);
                }
            }
            //passing the roles to the view
            ViewBag.StaffRoles = new SelectList(roles, "Id", "Name");

            return View(model);
        }

        /// <summary>
        /// GET method for Edit member - returns the EditMember View and populates thge fields with the members existing details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMember(string id)
        {
            //if the users id were trying to edit is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finds the member by id in the context
            Member member = context.Users.Find(id) as Member;

            //checks to see if the user's id is in the Suspended Role
            bool isSuspended = UserManager.IsInRole(id, "Suspended");

            //if the user cant be found
            if (member == null)
            {
                return HttpNotFound();
            }

            //returning the edit member view model
            return View(new EditMemberViewModel
            {
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Joined = member.Joined,
                DisplayName = member.DisplayName,
                IsSuspended = isSuspended
            });
        }

        /// <summary>
        /// post method for Editing Members - assinging new properties to the member and updating to the context 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditMember(string id,
            [Bind(Include = "Email,FirstName,LastName,DisplayName,IsSuspended")] EditMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                //finds user by ID and casts into member 
                Member member = (Member)await UserManager.FindByIdAsync(id);

                if (model.IsSuspended)
                {
                    //if the user's id is in the Suspended Role already 
                    if (!await UserManager.IsInRoleAsync(id, "Suspended"))
                    {
                        //removes from the member role
                        await UserManager.RemoveFromRoleAsync(id, "Member");
                        //adds the user to the Suspended role
                        await UserManager.AddToRoleAsync(id, "Suspended");
                    }
                }
                else
                {
                    //if the user's id is in the Suspended Role already 
                    if (await UserManager.IsInRoleAsync(id, "Suspended"))
                    {
                        //removes from the Suspended role
                        await UserManager.RemoveFromRoleAsync(id, "Suspended");
                        //adds the user to the member role
                        await UserManager.AddToRoleAsync(id, "Member");
                    }
                }

                UpdateModel(member);//updates the staff object details using the model

                IdentityResult result = await UserManager.UpdateAsync(member);//updates the new staff details to the database 

                if (result.Succeeded)
                {
                    return RedirectToAction("ViewAllUsers", "Admin");//redirect to index
                }
            }

            return View(model);
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Manager,Admin,SalesAssistant")]
        /// <summary>
        /// returns the approrpiate page that will display the employee/member details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = context.Users.Find(id);
            ViewBag.Orders = context.Orders.Where(o=>o.UserId == id);//sends the users orders to the list as well

            if(user == null)
            {
                return HttpNotFound();
            }
            if(user is Member)
            {
                return View("DetailsMember", (Member)user);
            }
            if (user is Staff)
            {
                return View("DetailsStaff", (Staff)user);
            }
            return HttpNotFound();
        }

        /// <summary>
        /// updates the users status to deactivated
        /// for Data protection and record keeping reasons we cannot delete members details straight away
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeactivateUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //returns to index if user tries to delete their own account
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ViewAllUsers", "Admin");
            }

            //get the user by id
            User user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            //stores the current role of the user
            string role = user.CurrentRole;

            //removes the user from their current role. 
            IdentityResult result = await UserManager.RemoveFromRoleAsync(id, role);

            if(result.Succeeded)
            {
                await UserManager.AddToRoleAsync(id, "Deactivated");//adds the user to the DeActivated role
                TempData["AlertMessage"] = "User Updated";
            }
           

            return RedirectToAction("ViewAllUsers", "Admin");
        }

        //******************************PRODUCT*************************************

        /// <summary>
        /// returns a new view with a list of all products listed on the context
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ViewAllProducts()
        {
            //get all the users and order them by the registration date
            var products = context.Products.OrderBy(p => p.ProductName).Include(p => p.ProductCategory).ToList();

            //passing the list of product categories to the index view - for the drop down list
            ViewBag.Categories = context.ProductCategories.ToList();

            //refreshes the posts each time the page is loaded - so that newly created/edited products can be seen at runtime
            foreach (var p in products)
            {
                context.Entry(p).Reload();//refreshes entities
            }
            //send the list of users to the index view
            return View(products);
        }

        /// <summary>
        /// returns a list of all products that generate a PDF report of all stock on the system
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductsPDF()
        {
            var products = context.Products.Include(p=>p.ProductCategory).ToList();
            return new ViewAsPdf(products);
        }
        /// <summary>
        /// Returns the "ViewAllProducts" view with all products relating to one category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewAllProductsByCategory(int? id)
        {
            //gets all the products that belong to the specific category ID
            var products = context.Products.Where(p => p.ProductCategoryId == id).ToList();

            //passing the list of product categories to the ViewAllProducts view
            ViewBag.Categories = context.ProductCategories.ToList();

            return View("ViewAllProducts", products);
        }

        //******************************ORDER***************************************

        [OverrideAuthorization]
        [Authorize(Roles = "Manager,Admin,SalesAssistant")]
        /// <summary>
        /// returns a view that displays all orders listed on the context
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ViewAllOrders()
        {
            //get all the users and order them by the registration date
            var orders = context.Orders.OrderBy(o => o.OrderDate).Include(o => o.User)/*.Include(o => o.Payment)*/.ToList();

            //send the list of users to the index view
            return View(orders);
        }



    }
}