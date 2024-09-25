using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AchillesHeel_RG.Models
{
    public class DatabaseInitialiser : DropCreateDatabaseAlways<AchillesHeelDbContext>
    {

        protected override void Seed(AchillesHeelDbContext context)
        {
            base.Seed(context);

            //SEED THE DATABASE HERE

            if (!context.Users.Any())
            {
                //create the roleManager and roleStore to create roles
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

                //create the userManager and userStore to create users
                UserStore<User> userStore = new UserStore<User>(context);
                UserManager<User> userManager = new UserManager<User>(userStore);

                //***************SEEDING ROLES***********************

                //if the roles dont exist then create them and add to the context
                if (!roleManager.RoleExists("Admin")) { roleManager.Create(new IdentityRole("Admin")); }
                if (!roleManager.RoleExists("Manager")) { roleManager.Create(new IdentityRole("Manager")); }
                if (!roleManager.RoleExists("SalesAssistant")) { roleManager.Create(new IdentityRole("SalesAssistant")); }
                if (!roleManager.RoleExists("InvoiceClerk")) { roleManager.Create(new IdentityRole("InvoiceClerk")); }
                if (!roleManager.RoleExists("AssistantManager")) { roleManager.Create(new IdentityRole("AssistantManager")); }
                if (!roleManager.RoleExists("StockControlManager")) { roleManager.Create(new IdentityRole("StockControlManager")); }
                if (!roleManager.RoleExists("WharehouseAssistant")) { roleManager.Create(new IdentityRole("WharehouseAssistant")); }               
                if (!roleManager.RoleExists("Moderator")) { roleManager.Create(new IdentityRole("SocialMediaManager")); }
                if (!roleManager.RoleExists("Member")) { roleManager.Create(new IdentityRole("Member")); }
                if (!roleManager.RoleExists("Suspended")) { roleManager.Create(new IdentityRole("Suspended")); }
                if (!roleManager.RoleExists("Deactivated")) { roleManager.Create(new IdentityRole("Deactivated")); }
                context.SaveChanges();

                //***************SEEDING PRODUCT CATEGORIES***************

                ProductCategory cat1 = new ProductCategory { CategoryType = "Trail", SubCategory = "Shoes" };
                ProductCategory cat2= new ProductCategory { CategoryType = "Road", SubCategory = "Shoes" };
                ProductCategory cat3 = new ProductCategory { CategoryType = "Track&Feild", SubCategory = "Shoes" };
                ProductCategory cat4 = new ProductCategory { CategoryType = "Racing", SubCategory = "Shoes" };
                ProductCategory cat5 = new ProductCategory { CategoryType = "CarbonPlated", SubCategory = "Shoes" };
                ProductCategory cat6 = new ProductCategory { CategoryType = "Lifestyle", SubCategory = "Shoes" };
                ProductCategory cat7 = new ProductCategory { CategoryType = "Walking", SubCategory = "Shoes" };
                ProductCategory cat8 = new ProductCategory { CategoryType = "Sandals", SubCategory = "Shoes" };
                ProductCategory cat9 = new ProductCategory { CategoryType = "Pants", SubCategory = "Clothing" };
                ProductCategory cat10 = new ProductCategory { CategoryType = "Shorts", SubCategory = "Clothing" };
                ProductCategory cat11= new ProductCategory { CategoryType = "Tees", SubCategory = "Clothing" };
                ProductCategory cat12 = new ProductCategory { CategoryType = "Hats", SubCategory = "Accessories" };
                ProductCategory cat13 = new ProductCategory { CategoryType = "Socks", SubCategory = "Accessories" };
                ProductCategory cat14 = new ProductCategory { CategoryType = "Bags", SubCategory = "Accessories" };

                //***************SEEDING POST CATEGORIES***************

                PostCategory cat15 = new PostCategory { CategoryType = "Events"};
                PostCategory cat16 = new PostCategory { CategoryType = "StoreInfo" };
                PostCategory cat17 = new PostCategory { CategoryType = "Misc" };

                //adding product categories to database
                context.ProductCategories.Add(cat1);
                context.ProductCategories.Add(cat2);
                context.ProductCategories.Add(cat3);
                context.ProductCategories.Add(cat4);
                context.ProductCategories.Add(cat5);
                context.ProductCategories.Add(cat6);
                context.ProductCategories.Add(cat7);
                context.ProductCategories.Add(cat8);
                context.ProductCategories.Add(cat9);
                context.ProductCategories.Add(cat10);
                context.ProductCategories.Add(cat11);
                context.ProductCategories.Add(cat12);
                context.ProductCategories.Add(cat13);
                context.ProductCategories.Add(cat14);

                //adding the post categories to the databse
                context.PostCategories.Add(cat15);
                context.PostCategories.Add(cat16);
                context.PostCategories.Add(cat17);

                context.SaveChanges();//saving the context

                //***************************SEEDING CONTACT FORMS**********************
                ContactForm c1 = new ContactForm
                {
                    Name = "ryan",
                    Email = "grantryan182@gmail.com",
                    Message = "hello help me please",
                    SubmitDate = DateTime.Now.AddDays(-3),
                    OpenCase = true
                };
                ContactForm c2 = new ContactForm
                {
                    Name = "helen",
                    Email = "helen1213@gmail.com",
                    Message = "hello help me please i need assiatnce",
                    SubmitDate = DateTime.Now.AddDays(-2),
                    OpenCase = true
                };
                ContactForm c3 = new ContactForm
                {
                    Name = "jacobs",
                    Email = "jacobKing@gmail.com",
                    Message = "what are the opening hours of the store???",
                    SubmitDate = DateTime.Now.AddDays(1),
                    OpenCase = true
                };
                ContactForm c4 = new ContactForm
                {
                    Name = "shelly",
                    Email = "shellybaby@gmail.com",
                    Message = "can you email me regarding your refund policy please",
                    SubmitDate = DateTime.Now.AddDays(2),
                    OpenCase = false
                };

                context.ContactForms.Add(c1);
                context.ContactForms.Add(c2);
                context.ContactForms.Add(c3);
                context.ContactForms.Add(c4);
                context.SaveChanges();

                //***************************SEEDING PRODUCTS****************************

               
  
                var Product1_01 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 3,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_02 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 4,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_03 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 5,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_04 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 6,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_05 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 6.5,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_06 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 7,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_07 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 7.5,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_08 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 8,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_09 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 9,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };
                var Product1_10 = new Shoe
                {
                    ProductName = "Nike Air Zoom Pegasus 38",
                    ProductPrice = 119.99m,
                    SalePercentage = 0,
                    StockLevel = 50,
                    Size = 11,
                    Description = "The Nike Air Zoom Pegasus 38 continues to put a spring in your step, using the same responsive foam as its predecessor. The lightweight mesh upper adds breathability and comfort, while premium details combine with a dreamy color scheme for a fresh, energized look.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Air_Zoom_Pegasus_Black.png"
                };

                var Product2_01 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 4,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_02 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 5,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_03 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 6,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_04 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 7,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_05 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 8,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4   ,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_06 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 9,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };
                var Product2_07 = new Shoe
                {
                    ProductName = "Adidas Ultraboost 21",
                    ProductPrice = 179.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 10,
                    Description = "The Ultraboost 21 is built for comfort on long-distance runs. It has a breathable knit upper that's made with yarn spun from recycled plastic waste. Responsive cushioning returns energy to your stride, and the heel construction supports your foot for a locked-in feel.",
                    Colour = "Core Black/Grey Six/Solar Yellow",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Adidas_Ultra_Boost.png"
                };

                var Product3_01 = new Shoe
                {
                    ProductName = "ASICS GEL-Kayano 28",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 7,
                    Description = "The GEL-Kayano 28 running shoe creates a stable stride that moves you towards a balanced mindset. Featuring a lower-profile external heel counter, this piece cradles your foot with improved fit and reinforced stability.",
                    Colour = "Black/Graphite Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Asics_Gel.png"
                };
                var Product3_02 = new Shoe
                {
                    ProductName = "ASICS GEL-Kayano 28",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 7.5,
                    Description = "The GEL-Kayano 28 running shoe creates a stable stride that moves you towards a balanced mindset. Featuring a lower-profile external heel counter, this piece cradles your foot with improved fit and reinforced stability.",
                    Colour = "Black/Graphite Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Asics_Gel.png"
                };
                var Product3_03 = new Shoe
                {
                    ProductName = "ASICS GEL-Kayano 28",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 8,
                    Description = "The GEL-Kayano 28 running shoe creates a stable stride that moves you towards a balanced mindset. Featuring a lower-profile external heel counter, this piece cradles your foot with improved fit and reinforced stability.",
                    Colour = "Black/Graphite Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Asics_Gel.png"
                };
                var Product3_04 = new Shoe
                {
                    ProductName = "ASICS GEL-Kayano 28",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 8.5,
                    Description = "The GEL-Kayano 28 running shoe creates a stable stride that moves you towards a balanced mindset. Featuring a lower-profile external heel counter, this piece cradles your foot with improved fit and reinforced stability.",
                    Colour = "Black/Graphite Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Asics_Gel.png"
                };
                var Product3_05 = new Shoe
                {
                    ProductName = "ASICS GEL-Kayano 28",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 9,
                    Description = "The GEL-Kayano 28 running shoe creates a stable stride that moves you towards a balanced mindset. Featuring a lower-profile external heel counter, this piece cradles your foot with improved fit and reinforced stability.",
                    Colour = "Black/Graphite Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Asics_Gel.png"
                };

                var Product4_01 = new Shoe
                {
                    ProductName = "Brooks Adrenaline GTS 21",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 40,
                    Size = 8,
                    Description = "The Adrenaline GTS 21 is the go-to shoe for serious runners looking for a comfortable, reliable ride. It has a cushioned midsole and stable support that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Brooks_Ghost.png"         
                };
                var Product4_02 = new Shoe
                {
                    ProductName = "Brooks Adrenaline GTS 21",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 40,
                    Size = 9,
                    Description = "The Adrenaline GTS 21 is the go-to shoe for serious runners looking for a comfortable, reliable ride. It has a cushioned midsole and stable support that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Brooks_Ghost.png"
                };
                var Product4_03 = new Shoe
                {
                    ProductName = "Brooks Adrenaline GTS 21",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 40,
                    Size = 10,
                    Description = "The Adrenaline GTS 21 is the go-to shoe for serious runners looking for a comfortable, reliable ride. It has a cushioned midsole and stable support that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Brooks_Ghost.png"
                };

                var Product5_01 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 5,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_02 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 5.5,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_03 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 6,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_04 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 6.5,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_05 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 7,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_06 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 8,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };
                var Product5_07 = new Shoe
                {
                    ProductName = "New Balance Fresh Foam 1080v11",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 25,
                    Size = 9,
                    Description = "The Fresh Foam 1080v11 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/Silver",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_Fresh_Foam.png"
                };

                var Product6_01 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 4,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };
                var Product6_02 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 5,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };
                var Product6_03 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 5.5,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };
                var Product6_04 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 6,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };
                var Product6_05 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 7,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };
                var Product6_06 = new Shoe
                {
                    ProductName = "Hoka One One Bondi 7",
                    ProductPrice = 149.99m,
                    SalePercentage = 0,
                    StockLevel = 30,
                    Size = 7.5,
                    Description = "The Bondi 7 is a highly cushioned shoe that's perfect for runners who want maximum comfort on long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Hoka_Bondi.png"
                };

                var Product7_01 = new Shoe
                {
                    ProductName = "Saucony Guide 14",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 7,
                    Description = "The Guide 14 is a stability shoe that's perfect for runners who need extra support. It has a cushioned midsole and a sturdy outsole that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Navy/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Kinvara.png"
                };
                var Product7_02 = new Shoe
                {
                    ProductName = "Saucony Guide 14",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 8,
                    Description = "The Guide 14 is a stability shoe that's perfect for runners who need extra support. It has a cushioned midsole and a sturdy outsole that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Navy/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Kinvara.png"

                };
                var Product7_03 = new Shoe
                {
                    ProductName = "Saucony Guide 14",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 8.5,
                    Description = "The Guide 14 is a stability shoe that's perfect for runners who need extra support. It has a cushioned midsole and a sturdy outsole that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Navy/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Kinvara.png"
                };
                var Product7_04 = new Shoe
                {
                    ProductName = "Saucony Guide 14",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 9,
                    Description = "The Guide 14 is a stability shoe that's perfect for runners who need extra support. It has a cushioned midsole and a sturdy outsole that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Navy/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Kinvara.png"
                };
                var Product7_05 = new Shoe
                {
                    ProductName = "Saucony Guide 14",
                    ProductPrice = 129.99m,
                    SalePercentage = 0,
                    StockLevel = 20,
                    Size = 9.5,
                    Description = "The Guide 14 is a stability shoe that's perfect for runners who need extra support. It has a cushioned midsole and a sturdy outsole that keeps you feeling good for miles on end. The upper is made from breathable mesh, and the outsole is designed for durable traction.",
                    Colour = "Navy/Blue",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Kinvara.png"
                };
   
                var Product8_01 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 5,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };
                var Product8_02 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 6,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };
                var Product8_03 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 7,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };
                var Product8_04 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 7.5,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };
                var Product8_05 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 8,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };
                var Product8_06 = new Shoe
                {
                    ProductName = "Nike React Infinity Run Flyknit 2",
                    ProductPrice = 159.99m,
                    SalePercentage = 0,
                    StockLevel = 35,
                    Size = 8.5,
                    Description = "The React Infinity Run Flyknit 2 is a cushioned, neutral shoe that's perfect for long runs. It has a soft and springy midsole that absorbs shock and returns energy with every stride. The upper is made from breathable Flyknit material, and the outsole is designed for durable traction.",
                    Colour = "Black/White",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Nike_Tempo_Next.png"
                };

       
                var Product9_01 = new Shoe
                {
                    ProductName = "Nike Zoom Fly 3",
                    ProductPrice = 160.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 7,
                    Description = "The Nike Zoom Fly 3 is a lightweight and responsive shoe that's perfect for speedwork and racing.",
                    Colour = "Black",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Nike_Zoom_fly.png"
                };
                var Product9_02 = new Shoe
                {
                    ProductName = "Nike Zoom Fly 3",
                    ProductPrice = 160.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 8,
                    Description = "The Nike Zoom Fly 3 is a lightweight and responsive shoe that's perfect for speedwork and racing.",
                    Colour = "Black",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Nike_Zoom_fly.png"
                };
                var Product9_03 = new Shoe
                {
                    ProductName = "Nike Zoom Fly 3",
                    ProductPrice = 160.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 8.5,
                    Description = "The Nike Zoom Fly 3 is a lightweight and responsive shoe that's perfect for speedwork and racing.",
                    Colour = "Black",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Nike_Zoom_fly.png"
                };
                var Product9_04 = new Shoe
                {
                    ProductName = "Nike Zoom Fly 3",
                    ProductPrice = 160.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 9,
                    Description = "The Nike Zoom Fly 3 is a lightweight and responsive shoe that's perfect for speedwork and racing.",
                    Colour = "Black",
                    ProductCategory = cat4,
                    ImageUrl = "Images/Shoes/Nike_Zoom_fly.png"
                };

    
                var Product10_01 = new Shoe
                {
                    ProductName = "Saucony Kinvara 12",
                    ProductPrice = 110.00m,
                    SalePercentage = 0,
                    StockLevel = 90,
                    Size = 3,
                    Description = "The Saucony Kinvara 12 is a lightweight and comfortable shoe that's great for tempo runs and racing.",
                    Colour = "Purple",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Endorphin_Speed_3.png"
                };
                var Product10_02 = new Shoe
                {
                    ProductName = "Saucony Kinvara 12",
                    ProductPrice = 110.00m,
                    SalePercentage = 0,
                    StockLevel = 90,
                    Size = 4,
                    Description = "The Saucony Kinvara 12 is a lightweight and comfortable shoe that's great for tempo runs and racing.",
                    Colour = "Purple",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Endorphin_Speed_3.png"
                };
                var Product10_03 = new Shoe
                {
                    ProductName = "Saucony Kinvara 12",
                    ProductPrice = 110.00m,
                    SalePercentage = 0,
                    StockLevel = 90,
                    Size = 5,
                    Description = "The Saucony Kinvara 12 is a lightweight and comfortable shoe that's great for tempo runs and racing.",
                    Colour = "Purple",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Endorphin_Speed_3.png"
                };
                var Product10_04 = new Shoe
                {
                    ProductName = "Saucony Kinvara 12",
                    ProductPrice = 110.00m,
                    SalePercentage = 0,
                    StockLevel = 90,
                    Size = 6,
                    Description = "The Saucony Kinvara 12 is a lightweight and comfortable shoe that's great for tempo runs and racing.",
                    Colour = "Purple",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/Saucony_Endorphin_Speed_3.png"
                };       

                var Product11_01 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 4,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_02 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 5,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_03 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 6,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_04 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 7,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_05 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 8,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_06 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 9,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_07 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 10,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
                var Product11_08 = new Shoe
                {
                    ProductName = "Hoka One One Clifton 8",
                    ProductPrice = 140.00m,
                    SalePercentage = 0,
                    StockLevel = 60,
                    Size = 11,
                    Description = "The Hoka One One Clifton 8 is a highly cushioned shoe that's great for long runs and recovery days.",
                    Colour = "Yellow",
                    ProductCategory = cat1,
                    ImageUrl = "Images/Shoes/Hoka_One.png"
                };
 
                var Product12_01 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 3,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",                    
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_02 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 4,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_03 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 5,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_04 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 6,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_05 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 7,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_06 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 80,
                    Size = 8,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_07 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = 9,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };
                var Product12_08 = new Shoe
                {
                    ProductName = "New Balance RC Elite V2",
                    ProductPrice = 180.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = 10,
                    Description = "The New Balance Fresh Foam 1080v11 is a highly cushioned and comfortable shoe that's great for long runs.",
                    Colour = "Grey",
                    ProductCategory = cat2,
                    ImageUrl = "Images/Shoes/New_Balance_RC_Elite.png"
                };

                var Product13_01 = new Clothing
                {
                    ProductName = "District Vision Singlet",
                    ProductPrice = 150.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = "M",
                    PitToPit = "10",
                    Height = "13",
                    Description = "a newly designed fabric to keep you cool in warm conditions",
                    Colour = "Grey",
                    ProductCategory = cat11,
                    ImageUrl = "Images/Clothes/District_Vision_Singlet.png"
            };
                var Product13_02 = new Clothing
                {
                    ProductName = "District Vision Singlet",
                    ProductPrice = 150.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = "S",
                    PitToPit = "10",
                    Height = "13",
                    Description = "a newly designed fabric to keep you cool in warm conditions",
                    Colour = "Grey",
                    ProductCategory = cat11,
                    ImageUrl = "Images/Clothes/District_Vision_Singlet.png"
            };
                var Product13_03 = new Clothing
                {
                    ProductName = "District Vision Singlet",
                    ProductPrice = 150.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = "L",
                    PitToPit = "10",
                    Height = "13",
                    Description = "a newly designed fabric to keep you cool in warm conditions",
                    Colour = "Grey",
                    ProductCategory = cat11,
                    ImageUrl = "Images/Clothes/District_Vision_Singlet.png"
                };

                var Product14 = new Clothing
                {
                    ProductName = "Nike Race Shorts",
                    ProductPrice = 150.00m,
                    SalePercentage = 0,
                    StockLevel = 10,
                    Size = "M",
                    PitToPit = "10",
                    Height = "13",
                    Description = "Help you run quicker for longer",
                    Colour = "Grey",
                    ProductCategory = cat10,
                    ImageUrl = "Images/Clothes/Nike_Race_Shorts.png"
                    

                };

                //adding products to the context

                context.Products.Add(Product1_01);
                context.Products.Add(Product1_02);
                context.Products.Add(Product1_03);
                context.Products.Add(Product1_04);
                context.Products.Add(Product1_05);
                context.Products.Add(Product1_06);
                context.Products.Add(Product1_07);
                context.Products.Add(Product1_08);
                context.Products.Add(Product1_09);
                context.Products.Add(Product1_10);

                context.Products.Add(Product2_01);
                context.Products.Add(Product2_02);
                context.Products.Add(Product2_03);
                context.Products.Add(Product2_04);
                context.Products.Add(Product2_05);
                context.Products.Add(Product2_06);
                context.Products.Add(Product2_07);

                context.Products.Add(Product3_01);
                context.Products.Add(Product3_02);
                context.Products.Add(Product3_03);
                context.Products.Add(Product3_04);
                context.Products.Add(Product3_05);

                context.Products.Add(Product4_01);
                context.Products.Add(Product4_02);
                context.Products.Add(Product4_03);

                context.Products.Add(Product5_01);
                context.Products.Add(Product5_02);
                context.Products.Add(Product5_03);
                context.Products.Add(Product5_04);
                context.Products.Add(Product5_05);
                context.Products.Add(Product5_06);
                context.Products.Add(Product5_07);

                context.Products.Add(Product6_01);
                context.Products.Add(Product6_02);
                context.Products.Add(Product6_03);
                context.Products.Add(Product6_04);
                context.Products.Add(Product6_05);
                context.Products.Add(Product6_06);

                context.Products.Add(Product7_01);
                context.Products.Add(Product7_02);
                context.Products.Add(Product7_03);
                context.Products.Add(Product7_04);
                context.Products.Add(Product7_05);

                context.Products.Add(Product8_01);
                context.Products.Add(Product8_02);
                context.Products.Add(Product8_03);
                context.Products.Add(Product8_04);
                context.Products.Add(Product8_05);
                context.Products.Add(Product8_06);

                context.Products.Add(Product9_01);
                context.Products.Add(Product9_02);
                context.Products.Add(Product9_03);
                context.Products.Add(Product9_04);

                context.Products.Add(Product8_01);
                context.Products.Add(Product8_02);
                context.Products.Add(Product8_03);
                context.Products.Add(Product8_04);
                context.Products.Add(Product8_05);
                context.Products.Add(Product8_06);

                context.Products.Add(Product9_01);
                context.Products.Add(Product9_02);
                context.Products.Add(Product9_03);
                context.Products.Add(Product9_04);

                context.Products.Add(Product10_01);
                context.Products.Add(Product10_02);
                context.Products.Add(Product10_03);
                context.Products.Add(Product10_04);

                context.Products.Add(Product11_01);
                context.Products.Add(Product11_02);
                context.Products.Add(Product11_03);
                context.Products.Add(Product11_04);
                context.Products.Add(Product11_05);
                context.Products.Add(Product11_06);
                context.Products.Add(Product11_07);
                context.Products.Add(Product11_08);

                context.Products.Add(Product12_01);
                context.Products.Add(Product12_02);
                context.Products.Add(Product12_03);
                context.Products.Add(Product12_04);
                context.Products.Add(Product12_05);
                context.Products.Add(Product12_06);
                context.Products.Add(Product12_07);
                context.Products.Add(Product12_08);

                context.Products.Add(Product13_01);
                context.Products.Add(Product13_02);
                context.Products.Add(Product13_03);

                context.Products.Add(Product14);
                

                context.SaveChanges();

                //************************SEEDING USERS***********************

                //very loose password validation for seeding, testing, and marking purposes
                userManager.PasswordValidator = new PasswordValidator
                {
                    RequireDigit = false,
                    RequiredLength = 1,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };

                //creating one staff member for every role 

                var admin = new Staff
                {
                    //identity properties 
                    Email = "admin@AchillesHeel.com", //password admin123
                    UserName = "admin@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Admin",
                    FirstName = "Ryan",
                    LastName = "Grant",
                    Salary = 100000.00,
                    EmailConfirmed = true,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "Glasgow",
                            Line1 = "12 Waverley Street",
                            Line2 = null,
                            PostCode = "G41 2EA"
                        }
                    },
                    
                    Orders = new List<Order>()

                    
                };

                var manager = new Staff
                {
                    //identity properties 
                    Email = "Manager@AchillesHeel.com",
                    UserName = "Manager@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Manager",
                    FirstName = "Stuart",
                    LastName = "Donnachie",
                    Salary = 100000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "Stewarton",
                            Line1 = "13 lane avenue",
                            Line2 = "stewarton",
                            PostCode = "s54 5rh"
                        }
                    }
                   
                };


                var salesAsistant1 = new Staff
                {
                    //identity properties 
                    Email = "Finn@AchillesHeel.com",
                    UserName = "Finn@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Finn",
                    FirstName = "Finn",
                    LastName = "Taylor",
                    Salary = 20000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "Stewarton",
                            Line1 = "18 lane avenue",
                            Line2 = "stewarton",
                            PostCode = "s54 5bf"
                        }
                    }
                   
                };

                var salesAsistant2 = new Staff
                {
                    //identity properties 
                    Email = "Ronan@AchillesHeel.com",
                    UserName = "Ronan@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Ronan",
                    FirstName = "Ronan",
                    LastName = "Mallan",
                    Salary = 20000.00,
                    EmailConfirmed = true,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "200 barn street",
                            Line2 = "flat 3/1",
                            PostCode = "g67 7eg"
                        }
                    }
                   
                };

                var salesAsistant3 = new Staff
                {
                    //identity properties 
                    Email = "Joe@AchillesHeel.com",
                    UserName = "Joe@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Joe",
                    FirstName = "Joe",
                    LastName = "Budi",
                    Salary = 20000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "10 darnley street",
                            Line2 = "polloksheilds",
                            PostCode = "g41 4rg"
                        }
                    }
                    
                };

                var invoiceClerk = new Staff
                {
                    //identity properties 
                    Email = "invoices@AchillesHeel.com",
                    UserName = "invoices@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "david",
                    FirstName = "david",
                    LastName = "mcdonald",
                    Salary = 30000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "3 crab lane",
                            Line2 = "battlefield",
                            PostCode = "g45 2rf"
                        }
                    }
                };

                var assistantManager = new Staff
                {
                    //identity properties 
                    Email = "Dana@AchillesHeel.com",
                    UserName = "Dana@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Dana",
                    FirstName = "Dana",
                    LastName = "Carson",
                    Salary = 30000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "10 catherdral street",
                            Line2 = "COGC",
                            PostCode = "g3 4dt"
                        }
                    }                  
                };

                var stockControlManager = new Staff
                {
                    //identity properties 
                    Email = "Stephen@AchillesHeel.com",
                    UserName = "Stephen@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Stephen",
                    FirstName = "Stephen",
                    LastName = "alexander",
                    Salary = 35000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 great western road",
                            Line2 = "flat 3/2",
                            PostCode = "g4 3eb"
                        }
                    }                   
                };

                var wharehouseAssistant = new Staff
                {
                    //identity properties 
                    Email = "john@AchillesHeel.com",
                    UserName = "John@AchillesHeel.com",
                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "John",
                    FirstName = "John",
                    LastName = "Jacobs",
                    Salary = 35000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 great western road",
                            Line2 = "flat 3/2",
                            PostCode = "g4 3eb"
                        }
                    }                   
                };

                var socialMediaManager = new Staff
                {
                    //identity properties 
                    Email = "Morgan@AchillesHeel.com",
                    UserName = "Morgan@AchillesHeel.com",

                    Joined = new DateTime(2020, 12, 22),
                    DisplayName = "Morgan",
                    FirstName = "Morgan",
                    LastName = "alexander",
                    Salary = 35000.00,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "17 dale road",
                            Line2 = null,
                            PostCode = "g7 7hh"
                        }
                    }                   
                };

                //******************SEEDING MEMBERS**********************************

                var member1 = new Member
                {
                    //identity properties 
                    Email = "ross@gmail.com",
                    UserName = "ross@gmail.com",

                    Joined = new DateTime(2021, 12, 22),
                    DisplayName = "ross",
                    FirstName = "ross",
                    LastName = "clark",
                    IsSuspended = false,
                    Strikes = 0,
                    EmailConfirmed = true,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "1 ten street",
                            Line2 = "garnethill",
                            PostCode = "g4 7fg",
                        }
                    },

                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderDate = new DateTime(2022,11,20),
                            OrderTotal = 219.19m,
                            OrderStatus = OrderStatus.Completed,



                            OrderLines = new List<OrderLine>
                            {
                                new OrderLine
                                {
                                    Quantity = 1,
                                    LineTotal = 110.00m,
                                    Product = Product10_01
                                },

                                new OrderLine
                                {
                                    Quantity = 1,
                                    LineTotal = 119.99m,
                                    Product = Product1_01
                                }
                            }

                        }
                    }
                };

                var member2 = new Member
                {
                    //identity properties 
                    Email = "gary@gmail.com",
                    UserName = "gary@gmail.com",

                    Joined = new DateTime(2021, 06, 13),
                    DisplayName = "gary",
                    FirstName = "gary",
                    LastName = "kelly",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "one tree lane",
                            Line2 = "shawlands",
                            PostCode = "g4 75g"
                        }
                    }
                };

                var member3 = new Member
                {
                    //identity properties 
                    Email = "shelly@gmail.com",
                    UserName = "shelly@gmail.com",

                    Joined = new DateTime(2021, 04, 01),
                    DisplayName = "shelly",
                    FirstName = "shelly",
                    LastName = "jane",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 lacey street",
                            Line2 = "battlefield",
                            PostCode = "g50 8rg"
                        }
                    }
                };

                var member4 = new Member
                {
                    //identity properties 
                    Email = "jackson@gmail.com",
                    UserName = "jackson@gmail.com",

                    Joined = new DateTime(2021, 01, 22),
                    DisplayName = "shelly",
                    FirstName = "shelly",
                    LastName = "jane",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 lacey street",
                            Line2 = "battlefield",
                            PostCode = "g50 8rg"
                        }
                    }
                };

                var member5 = new Member
                {
                    //identity properties 
                    Email = "runner134@gmail.com",
                    UserName = "runner134@gmail.com",

                    Joined = new DateTime(2021, 11, 01),
                    DisplayName = "jake",
                    FirstName = "jake",
                    LastName = "Wright",
                    IsSuspended = false,
                    Strikes = 2,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 lacey street",
                            Line2 = "battlefield",
                            PostCode = "g50 8rg"
                        }
                    }
                };

                var member6 = new Member
                {
                    //identity properties 
                    Email = "sandyblue@gmail.com",
                    UserName = "sandyblue@gmail.com",

                    Joined = new DateTime(2021, 11, 01),
                    DisplayName = "sandy",
                    FirstName = "sandy",
                    LastName = "blue",
                    IsSuspended = false,
                    Strikes = 2,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "34 bikini bottom",
                            Line2 = null,
                            PostCode = "r45 7gs"
                        }
                    }
                };

                var member7 = new Member
                {
                    //identity properties 
                    Email = "maddy12@gmail.com",
                    UserName = "maddy12@gmail.com",

                    Joined = new DateTime(2021, 09, 30),
                    DisplayName = "madison",
                    FirstName = "madison",
                    LastName = "Wright",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "12",
                            Line2 = "lancefield quay",
                            PostCode = "g11 7yf"
                        }
                    }
                };

                var member8 = new Member
                {
                    //identity properties 
                    Email = "man_of_Steel@gmail.com",
                    UserName = "man_of_Steel@gmail.com",

                    Joined = new DateTime(2021, 12, 05),
                    DisplayName = "graham",
                    FirstName = "graham",
                    LastName = "munson",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "17",
                            Line2 = "montague street",
                            PostCode = "g3 87g"
                        }
                    }
                };

                var member9 = new Member
                {
                    //identity properties 
                    Email = "randomgirl12@gmail.com",
                    UserName = "randomgirl12@gmail.com",

                    Joined = new DateTime(2021, 7, 5),
                    DisplayName = "jen",
                    FirstName = "jen",
                    LastName = "flower",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "20/1",
                            Line2 = "hillhead road",
                            PostCode = "g4 6kj"
                        }
                    }
                };

                var member10 = new Member
                {
                    //identity properties 
                    Email = "celine@hotmail.com",
                    UserName = "celine@hotmail.com",

                    Joined = new DateTime(2021, 12, 05),
                    DisplayName = "celine",
                    FirstName = "celine",
                    LastName = "beem",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "flat 5/2",
                            Line2 = "victoria road",
                            PostCode = "g41 00g"
                        }
                    }
                };

                var member11 = new Member
                {
                    //identity properties 
                    Email = "sharon@hotmail.com",
                    UserName = "sharon@hotmail.com",

                    Joined = new DateTime(2021, 01, 26),
                    DisplayName = "sharon",
                    FirstName = "sharon",
                    LastName = "crispin",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "kilmarnock",
                            Line1 = "36 dundonald road",
                            Line2 = "kilmarnock",
                            PostCode = "ka1 1rz"
                        }
                    }
                };

                var member12 = new Member
                {
                    //identity properties 
                    Email = "liamg123@hotmail.com",
                    UserName = "liamg123@hotmail.com",

                    Joined = new DateTime(2021, 01, 26),
                    DisplayName = "liam",
                    FirstName = "liam",
                    LastName = "grant",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "kilmarnock",
                            Line1 = "36 dundonald road",
                            Line2 = "kilmarnock",
                            PostCode = "ka1 1rz"
                        }
                    }
                };

                var member13 = new Member
                {
                    //identity properties 
                    Email = "markcrispin@hotmail.com",
                    UserName = "markcrispin@hotmail.com",

                    Joined = new DateTime(2021, 03, 01),
                    DisplayName = "mark",
                    FirstName = "mark",
                    LastName = "crispin",
                    IsSuspended = false,
                    Strikes = 0,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "kilmarnock",
                            Line1 = "36 dundonald road",
                            Line2 = "kilmarnock",
                            PostCode = "ka1 1rz"
                        }
                    }
                };

                var member14 = new Member
                {
                    //identity properties 
                    Email = "greggor@hotmail.com",
                    UserName = "greggor@hotmail.com",

                    Joined = new DateTime(2021, 03, 01),
                    DisplayName = "greg",
                    FirstName = "greg",
                    LastName = "mance",
                    IsSuspended = false,
                    Strikes = 1,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "99 nine lane",
                            Line2 = "east kilbride",
                            PostCode = "ek11 3rg"
                        }
                    }
                };

                var member15 = new Member
                {
                    //identity properties 
                    Email = "declan145@hotmail.com",
                    UserName = "declan145@hotmail.com",

                    Joined = new DateTime(2021, 08, 16),
                    DisplayName = "declan",
                    FirstName = "declan",
                    LastName = "burns",
                    IsSuspended = false,
                    Strikes = 1,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "9 albert drive",
                            Line2 = "polloksheilds",
                            PostCode = "g40 4yj"
                        }
                    }

                };

                var memberDeactived = new Member
                {
                    //identity properties 
                    Email = "seanbean@hotmail.com",
                    UserName = "seanbean@hotmail.com",

                    Joined = new DateTime(2022, 01, 30),
                    DisplayName = "sean",
                    FirstName = "sean",
                    LastName = "bean",
                    IsSuspended = false,
                    Strikes = 0,
                    EmailConfirmed = true,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "14 sinlane road",
                            Line2 = "govan",
                            PostCode = "g56 2dd"
                        }
                    },

                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderDate = new DateTime(2022,11,20),
                            OrderTotal = 219.19m,
                            OrderStatus = OrderStatus.Completed,

                            

                            OrderLines = new List<OrderLine>
                            {
                                new OrderLine
                                {
                                    Quantity = 1,
                                    LineTotal = 110.00m,
                                    Product = Product10_01
                                },

                                new OrderLine
                                {
                                    Quantity = 1,
                                    LineTotal = 119.99m,
                                    Product = Product1_01
                                }
                            }

                        }
                    }
                };
                var memberSuspended = new Member
                {
                    //identity properties 
                    Email = "harry4578@hotmail.com",
                    UserName = "harry4578@hotmail.com",

                    Joined = new DateTime(2022, 02, 11),
                    DisplayName = "harry",
                    FirstName = "harry",
                    LastName = "dali",
                    IsSuspended = true,
                    Strikes = 3,

                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "glasgow",
                            Line1 = "89 walk lane",
                            Line2 = "merchant city",
                            PostCode = "g76 45f"
                        }
                    }
                };

                //checking the users dont exist in the context already - then adding them to the DB if they dont, and assigning a role. 
                //passwords assinged to members are all the same - for testing and marking purposes

                if (userManager.FindByName("admin@AchillesHeel.com") == null)
                {
                    userManager.Create(admin, "admin123");//adding user to the users table - password "admin123"
                    userManager.AddToRole(admin.Id, "Admin");//assign user to the appropriate role
                }

                if (userManager.FindByName("Manager@AchillesHeel.com") == null)
                {
                    userManager.Create(manager, "manager123");
                    userManager.AddToRole(manager.Id, "Manager");
                }

                if (userManager.FindByName("Finn@AchillesHeel.com") == null)
                {
                    userManager.Create(salesAsistant1, "finn123");
                    userManager.AddToRole(salesAsistant1.Id, "SalesAssistant");
                }

                if (userManager.FindByName("Ronan@AchillesHeel.com") == null)
                {
                    userManager.Create(salesAsistant2, "ronan123");
                    userManager.AddToRole(salesAsistant2.Id, "SalesAssistant");
                }

                if (userManager.FindByName("Joe@AchillesHeel.com") == null)
                {
                    userManager.Create(salesAsistant3, "joe123");
                    userManager.AddToRole(salesAsistant3.Id, "SalesAssistant");
                }

                if (userManager.FindByName("invoices@AchillesHeel.com") == null)
                {
                    userManager.Create(invoiceClerk, "invoices123");
                    userManager.AddToRole(invoiceClerk.Id, "InvoiceClerk");
                }

                if (userManager.FindByName("Manager@AchillesHeel.com") == null)
                {
                    userManager.Create(assistantManager, "assistantManager123");
                    userManager.AddToRole(assistantManager.Id, "AssistantManager");
                }

                if (userManager.FindByName("Stephen@AchillesHeel.com") == null)
                {
                    userManager.Create(stockControlManager, "Stephen123");
                    userManager.AddToRole(stockControlManager.Id, "StockControlManager ");
                }

                if (userManager.FindByName("john@AchillesHeel.com") == null)
                {
                    userManager.Create(wharehouseAssistant, "john123");
                    userManager.AddToRole(wharehouseAssistant.Id, "WharehouseAssistant");
                }

                if (userManager.FindByName("Morgan@AchillesHeel.com") == null)
                {
                    userManager.Create(socialMediaManager, "morgan123");
                    userManager.AddToRole(socialMediaManager.Id, "SocialMediaManager");
                }

                //******************* MEMBERS BELOW **********************

                if (userManager.FindByName("ross@gmail.com") == null)
                {
                    userManager.Create(member1, "password123");
                    userManager.AddToRole(member1.Id, "Member");
                }

                if (userManager.FindByName("gary@gmail.com") == null)
                {
                    userManager.Create(member2, "password123");
                    userManager.AddToRole(member2.Id, "Member");
                }

                if (userManager.FindByName("shelly@gmail.com") == null)
                {
                    userManager.Create(member3, "password123");
                    userManager.AddToRole(member3.Id, "Member");
                }

                if (userManager.FindByName("jackson@gmail.com") == null)
                {
                    userManager.Create(member4, "password123");
                    userManager.AddToRole(member4.Id, "Member");
                }

                if (userManager.FindByName("runner134@gmail.com") == null)
                {
                    userManager.Create(member5, "password123");
                    userManager.AddToRole(member5.Id, "Member");
                }

                if (userManager.FindByName("sandyblue@gmail.com") == null)
                {
                    userManager.Create(member6, "password123");
                    userManager.AddToRole(member6.Id, "Member");
                }

                if (userManager.FindByName("maddy12@gmail.com") == null)
                {
                    userManager.Create(member7, "password123");
                    userManager.AddToRole(member7.Id, "Member");
                }

                if (userManager.FindByName("man_of_Steel@gmail.com") == null)
                {
                    userManager.Create(member8, "password123");
                    userManager.AddToRole(member8.Id, "Member");
                }

                if (userManager.FindByName("randomgirl12@gmail.com") == null)
                {
                    userManager.Create(member9, "password123");
                    userManager.AddToRole(member9.Id, "Member");
                }

                if (userManager.FindByName("celine@hotmail.com") == null)
                {
                    userManager.Create(member10, "password123");
                    userManager.AddToRole(member10.Id, "Member");
                }

                if (userManager.FindByName("sharon@hotmail.com") == null)
                {
                    userManager.Create(member11, "password123");
                    userManager.AddToRole(member11.Id, "Member");
                }

                if (userManager.FindByName("markcrispin@hotmail.com") == null)
                {
                    userManager.Create(member13, "password123");
                    userManager.AddToRole(member13.Id, "Member");
                }

                if (userManager.FindByName("greggor@hotmail.com") == null)
                {
                    userManager.Create(member14, "password123");
                    userManager.AddToRole(member14.Id, "Member");
                }

                if (userManager.FindByName("declan145@hotmail.com") == null)
                {
                    userManager.Create(member15, "password123");
                    userManager.AddToRole(member15.Id, "Member");
                }

                if (userManager.FindByName("declan145@hotmail.com") == null)
                {
                    userManager.Create(member15, "password123");
                    userManager.AddToRole(member15.Id, "Member");
                }

                if (userManager.FindByName("seanbean@hotmail.com") == null)
                {
                    userManager.Create(memberDeactived, "password123");
                    userManager.AddToRole(memberDeactived.Id, "Member");
                }

                if (userManager.FindByName("harry4578@hotmail.com") == null)
                {
                    userManager.Create(memberSuspended, "password123");
                    userManager.AddToRole(memberSuspended.Id, "Member");
                }

                //saving the databse
                context.SaveChanges();




            }
        }

    }
}