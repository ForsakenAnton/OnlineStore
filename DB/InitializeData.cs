using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineStore.DB
{
    public static class InitializeData
    {
        public static async Task InitializeAsync(OnlineStoreContext context, IWebHostEnvironment environment/*, UserManager<User> userManager, RoleManager<IdentityRole> roleManager*/)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            if (context.Products.Any() == false)
            {
                IWebHostEnvironment _environment = environment;

                
                byte[] mainPhonesCategoryImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\main-phones.jpg");
                byte[] mainTelevisionsCategoryImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\televisions-category.jpg");
                byte[] phonesCategoryImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\phones-category.jpg");
                byte[] samsungCategoryImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\samsung-category.jpg");
                byte[] xiaomiCategoryImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\sub-category-xaomi.jpg");
                byte[] phonesPhonesAccessoriesImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\phones-accessories.jpg");
                byte[] phonesPhonesAccessoriesPowerbanksImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\powerbanks.jpg");
                byte[] phonesPhonesAccessoriesMemoryCardsImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\memory-cards.jpg");

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                byte[] manufacturerHuaweiImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\manufacturerHuawei.jpg");
                byte[] manufacturerSamsungImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\manufacturerSamsung.jpg");
                byte[] manufacturerXiaomiImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\manufacturerXiaomi.jpg");
                byte[] manufacturerKingstonImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\manufacturerKingston.png");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                byte[] huaweiP40 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\huawei-p40.jpg");
                byte[] huaweiPSmart2021 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\huawei-psmart-2021.jpg");
                byte[] huaweiMateXs = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\huawei-mate-xs.jpg");

                byte[] galaxyA32 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\galaxy-a32.jpg");
                byte[] galaxyA52 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\galaxy-a52.jpg");

                byte[] xiaomiRedmiNote9ProImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XiaomiRedmiNote9ProImg.jpg");
                byte[] xiaomiRedmiNote9Img = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmiNote9Img.jpg");
                byte[] xiaomiRedmiNote10sImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmiNote10sImg.jpg");
                byte[] xiaomiRedmiNote10ProImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmiNote10ProImg.jpg");
                byte[] xiaomiRedmiNote10Img = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmiNote10Img.jpg");
                byte[] xiaomiRedmi9Img = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmi9Img.jpg");
                byte[] xiaomiRedmi9CImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XiaomiRedmi9CImg.jpg");
                byte[] xiaomiRedmi9AImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIRedmi9AImg.jpg");
                byte[] xiaomiPOCOM3Img = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIPOCOM3Img.jpg");
                byte[] xiaomiMi11LiteImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIMi11LiteImg.jpg");
                byte[] xiaomiMi11Img = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIMi11Img.jpg");
                byte[] xiaomiMi11iImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIMi11iImg.jpg");
                byte[] xiaomiMi10TImg = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\XIAOMIMi10TImg.jpg");



                byte[] microSD1 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\micro-sd1.jpg");
                byte[] microSD2 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\micro-sd2.jpg");

                byte[] pb1 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\pb1.jpg");
                byte[] pb2 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\pb2.jpg");
                byte[] pb3 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\pb3.jpg");
                byte[] pb4 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\pb4.jpg");
                byte[] pb5 = System.IO.File.ReadAllBytes($"{_environment.WebRootPath}\\images\\pb5.jpg");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Category phonesCategory = new Category // Phones (main)
                {
                    Title = "Phones",
                    Image = mainPhonesCategoryImg,
                    CategoryLevel = 1,
                    Order = 3,
                    IsTop = false
                };


                Category televisionsCategory = new Category // Televisions (main)
                {
                    Title = "Televisions",
                    Image = mainTelevisionsCategoryImg,
                    CategoryLevel = 1,
                    Order = 2,
                    IsTop = false
                };

                Category phonesAllPhonesCategory = new Category // Phones => All Phones
                {
                    Title = "All Phones",
                    Image = phonesCategoryImg,
                    CategoryLevel = 2,
                    Order = 2,
                    IsTop = true,
                    ParentCategory = phonesCategory
                };

                Category phonesSamsungCategory = new Category // Phones => Samsung
                {
                    Title = "Samsung",
                    Image = samsungCategoryImg,
                    CategoryLevel = 2,
                    Order = 3,
                    IsTop = false,
                    ParentCategory = phonesCategory
                };

                Category accesoriesCategory = new Category  // Phones => Accesories
                {
                    Title = "Accesories",
                    Image = phonesPhonesAccessoriesImg,
                    CategoryLevel = 2,
                    Order = 1,
                    IsTop = false,
                    ParentCategory = phonesCategory
                };

                Category powerBanksCategory = new Category  // Phones => Accesories => Power banks
                {
                    Title = "Power banks",
                    Image = phonesPhonesAccessoriesPowerbanksImg,
                    CategoryLevel = 3,
                    Order = 2,
                    IsTop = true,
                    ParentCategory = accesoriesCategory
                };

                Category memoryCardsCategory = new Category // Phones => Accesories => Memory cards
                {
                    Title = "Memory cards",
                    Image = phonesPhonesAccessoriesMemoryCardsImg,
                    CategoryLevel = 3,
                    Order = 1,
                    IsTop = false,
                    ParentCategory = accesoriesCategory
                };

                Category TopCategory = new Category // Phones => Top
                {
                    Title = "Top",
                    Image = xiaomiCategoryImg,
                    CategoryLevel = 1,
                    Order = 0,
                    IsTop = true
                };


                Category xiaomiCategory = new Category // Phones => Xiaomi
                {
                    Title = "Xiaomi",
                    Image = xiaomiCategoryImg,
                    CategoryLevel = 2,
                    Order = 1,
                    IsTop = false,
                    ParentCategory = phonesCategory
                };

                ////////////////////////////////////////////////////////////////

                Manufacturer manufacturerHuawei = new Manufacturer { Title = "Huawei", Image = manufacturerHuaweiImg };
                Manufacturer manufacturerSamsung = new Manufacturer { Title = "Samsung", Image = manufacturerSamsungImg };
                Manufacturer manufacturerXiaomi = new Manufacturer { Title = "Xiaomi", Image = manufacturerXiaomiImg };
                Manufacturer manufacturerKingston = new Manufacturer { Title = "Kingston", Image = manufacturerKingstonImg };


                Product productHuaweiPhone1 = new Product
                {
                    Title = "Huawei PSmart 2021",
                    Feature = "4/128GB Crush Green",
                    Price = 150,
                    IsTop = true,
                    Discount = 20,
                    Count = 50,
                    CommodityCode = "6060100",
                    Image = huaweiPSmart2021,
                    Manufacturer = manufacturerHuawei,
                    //Categories = { phonesAllPhonesCategory }
                };

                Product productHuaweiPhone2 = new Product
                {
                    Title = "Huawei P40",
                    Feature = "6/128GB Blue",
                    Price = 400,
                    IsTop = true,
                    Discount = 10,
                    Count = 20,
                    CommodityCode = "6060101",
                    Image = huaweiP40,
                    Manufacturer = manufacturerHuawei
                };

                Product productSamsungPhone1 = new Product
                {
                    Title = "Samsung A32",
                    Feature = "3/64GB white",
                    Price = 160,
                    IsTop = false,
                    Discount = 5,
                    Count = 30,
                    CommodityCode = "6060150",
                    Image = galaxyA32,
                    Manufacturer = manufacturerSamsung
                };

                Product productSamsungPhone2 = new Product
                {
                    Title = "Samsung A52",
                    Feature = "4/64GB Black",
                    Price = 180,
                    IsTop = true,
                    Discount = 0,
                    Count = 40,
                    CommodityCode = "6060151",
                    Image = galaxyA52,
                    Manufacturer = manufacturerSamsung
                };

                ////////////////////////////////////////////////////// Second adding

                Product productHuaweiPhone3 = new Product
                {
                    Title = "Huawei mate Xs",
                    Feature = "8/512GB Blue-green",
                    Price = 1500,
                    IsTop = true,
                    Discount = 0,
                    Count = 10,
                    CommodityCode = "6060601",
                    Image = huaweiMateXs,
                    Manufacturer = manufacturerHuawei
                };

                Product productKingstonMicroSD1 = new Product
                {
                    Title = "KINGSTON microSDHC",
                    Feature = "32Gb Canvas Select+ A1 UHS-I (U1) (SDCS2/32GB)",
                    Price = 50,
                    IsTop = true,
                    Discount = 5,
                    Count = 10,
                    CommodityCode = "6060456",
                    Image = microSD1,
                    Manufacturer = manufacturerKingston
                };

                Product productKingstonMicroSD2 = new Product
                {
                    Title = "KINGSTON microSDHC",
                    Feature = "16Gb Canvas Select+ A1 (R100/W10) +ad",
                    Price = 35,
                    IsTop = true,
                    Discount = 2,
                    Count = 20,
                    CommodityCode = "6060457",
                    Image = microSD2,
                    Manufacturer = manufacturerKingston
                };

                Product productXiaomiPB1 = new Product
                {
                    Title = "Powerbank XIAOMI Mi 2S",
                    Feature = "10000 mAh (VXN4228CN)",
                    Price = 10,
                    IsTop = true,
                    Discount = 0,
                    Count = 150,
                    CommodityCode = "25428901",
                    Image = pb1,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiPB2 = new Product
                {
                    Title = "Powerbank XIAOMI Redmi",
                    Feature = "20000mAh VXN4285 White",
                    Price = 15,
                    IsTop = true,
                    Discount = 0,
                    Count = 120,
                    CommodityCode = "25428902",
                    Image = pb2,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiPB3 = new Product
                {
                    Title = "Powerbank XIAOMI Xiaomi Mi3",
                    Feature = "10000mAh Silver",
                    Price = 17,
                    IsTop = false,
                    Discount = 0,
                    Count = 35,
                    CommodityCode = "25428903",
                    Image = pb3,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiPB4 = new Product
                {
                    Title = "Powerbank XIAOMI Mi3",
                    Feature = "NEW 10000mAh Black (73/74GL)",
                    Price = 17,
                    IsTop = false,
                    Discount = 5,
                    Count = 10,
                    CommodityCode = "25428904",
                    Image = pb4,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiPB5 = new Product
                {
                    Title = "Powerbank XIAOMI Redmi",
                    Feature = "10000mAh VXN4286 White",
                    Price = 10,
                    IsTop = true,
                    Discount = 0,
                    Count = 250,
                    CommodityCode = "25428905",
                    Image = pb5,
                    Manufacturer = manufacturerXiaomi
                };



                Product productXiaomiRedmiNote9pro = new Product
                {
                    Title = "XIAOMI Redmi Note 9 Pro",
                    Feature = "6/128 Dual Sim + Memory 512",
                    Price = 220,
                    IsTop = true,
                    Discount = 20,
                    Count = 10,
                    CommodityCode = "123120",
                    Image = xiaomiRedmiNote9ProImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmiNote9 = new Product
                {
                    Title = "XIAOMI Redmi Note 9",
                    Feature = "4/128 Dual Sim + Memory 256",
                    Price = 180,
                    IsTop = true,
                    Discount = 10,
                    Count = 50,
                    CommodityCode = "123121",
                    Image = xiaomiRedmiNote9Img,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmiNote10s = new Product
                {
                    Title = "XIAOMI Redmi Note 10s",
                    Feature = "6/64",
                    Price = 120,
                    IsTop = false,
                    Discount = 0,
                    Count = 150,
                    CommodityCode = "123122",
                    Image = xiaomiRedmiNote10sImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmiNote10Pro = new Product
                {
                    Title = "XIAOMI Redmi Note 10 pro",
                    Feature = "6/256 Dual Sim + Memory 256 Stereo",
                    Price = 240,
                    IsTop = true,
                    Discount = 10,
                    Count = 400,
                    CommodityCode = "123123",
                    Image = xiaomiRedmiNote10ProImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmiNote10 = new Product
                {
                    Title = "XIAOMI Redmi Note 10",
                    Feature = "4/128 Stereo system",
                    Price = 220,
                    IsTop = false,
                    Discount = 0,
                    Count = 50,
                    CommodityCode = "123124",
                    Image = xiaomiRedmiNote10Img,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmi9 = new Product
                {
                    Title = "XIAOMI Redmi Note 9",
                    Feature = "3/128",
                    Price = 140,
                    IsTop = false,
                    Discount = 5,
                    Count = 150,
                    CommodityCode = "123125",
                    Image = xiaomiRedmi9Img,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmi9C = new Product
                {
                    Title = "XIAOMI Redmi Note 9C",
                    Feature = "4/128",
                    Price = 140,
                    IsTop = true,
                    Discount = 10,
                    Count = 150,
                    CommodityCode = "123126",
                    Image = xiaomiRedmi9CImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiRedmi9A = new Product
                {
                    Title = "XIAOMI Redmi Note 9A",
                    Feature = "4/64",
                    Price = 120,
                    IsTop = true,
                    Discount = 10,
                    Count = 50,
                    CommodityCode = "123127",
                    Image = xiaomiRedmi9AImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiPOCOM3 = new Product
                {
                    Title = "XIAOMI POCO M3",
                    Feature = "6/256",
                    Price = 260,
                    IsTop = true,
                    Discount = 100,
                    Count = 340,
                    CommodityCode = "123128",
                    Image = xiaomiPOCOM3Img,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiMi11Lite = new Product
                {
                    Title = "XIAOMI Mi 11 Lite",
                    Feature = "6/128",
                    Price = 320,
                    IsTop = true,
                    Discount = 30,
                    Count = 100,
                    CommodityCode = "123129",
                    Image = xiaomiMi11LiteImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiMi11 = new Product
                {
                    Title = "XIAOMI Mi 11",
                    Feature = "8/512",
                    Price = 260,
                    IsTop = true,
                    Discount = 50,
                    Count = 15,
                    CommodityCode = "123130",
                    Image = xiaomiMi11Img,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiMi11i = new Product
                {
                    Title = "XIAOMI Mi 11i",
                    Feature = "6/128",
                    Price = 300,
                    IsTop = true,
                    Discount = 25,
                    Count = 100,
                    CommodityCode = "123131",
                    Image = xiaomiMi11iImg,
                    Manufacturer = manufacturerXiaomi
                };

                Product productXiaomiMi10T = new Product
                {
                    Title = "XIAOMI Mi 10T",
                    Feature = "6/256",
                    Price = 270,
                    IsTop = true,
                    Discount = 0,
                    Count = 50,
                    CommodityCode = "123132",
                    Image = xiaomiMi10TImg,
                    Manufacturer = manufacturerXiaomi
                };
                /////////////////////////////////////////////////////////////////////////////////////////////


                //phonesAllPhonesCategory.Products.Add(productHuawei1);

                context.Manufacturers.AddRange(manufacturerHuawei, manufacturerSamsung, manufacturerXiaomi);

                context.CategoryProducts.AddRange(
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productHuaweiPhone1
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productHuaweiPhone2
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productSamsungPhone1
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productSamsungPhone2
                    },
                    new CategoryProduct
                    {
                        Category = phonesSamsungCategory,
                        Product = productSamsungPhone1
                    },
                    new CategoryProduct
                    {
                        Category = phonesSamsungCategory,
                        Product = productSamsungPhone2
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productHuaweiPhone3
                    },

                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmiNote9pro
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmiNote9
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmiNote10
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmiNote10s
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmiNote10Pro
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmi9
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmi9C
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiRedmi9A
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiPOCOM3
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiMi11Lite
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiMi11
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiMi11i
                    },
                    new CategoryProduct
                    {
                        Category = phonesAllPhonesCategory,
                        Product = productXiaomiMi10T
                    },

                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmiNote9pro
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmiNote9
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmiNote10
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmiNote10s
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmiNote10Pro
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmi9
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmi9C
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiRedmi9A
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiPOCOM3
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiMi11Lite
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiMi11
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiMi11i
                    },
                    new CategoryProduct
                    {
                        Category = xiaomiCategory,
                        Product = productXiaomiMi10T
                    },

                    new CategoryProduct
                    {
                        Category = TopCategory,
                        Product = productXiaomiMi10T
                    },
                    new CategoryProduct
                    {
                        Category = TopCategory,
                        Product = productXiaomiRedmiNote10Pro
                    },
                    new CategoryProduct
                    {
                        Category = TopCategory,
                        Product = productHuaweiPhone3
                    },
                    new CategoryProduct
                    {
                        Category = TopCategory,
                        Product = productSamsungPhone1
                    }
                    );

                /////////////////////////////////////////////////////

                context.Products.AddRange(
                    productKingstonMicroSD1, 
                    productKingstonMicroSD2,
                    productXiaomiPB1,
                    productXiaomiPB2, 
                    productXiaomiPB3, 
                    productXiaomiPB4,
                    productXiaomiPB5);
                
                context.Categories.AddRange(
                    phonesCategory, 
                    televisionsCategory, 
                    phonesAllPhonesCategory, 
                    phonesSamsungCategory, accesoriesCategory,
                    powerBanksCategory, memoryCardsCategory,
                    TopCategory,
                    xiaomiCategory);



                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////// USERS & ROLES ////////////////////////////////////////////////////////////////////////////////////////////////////////


                await context.SaveChangesAsync();



                //string adminEmail = "admin@gmail.com";
                //string password = "a";

                //string[] usersEmails =
                //{
                //    "u1@gmail.com", "u2@gmail.com", "u3@gmail.com", "u4@gmail.com", "u5@gmail.com", "u6@gmail.com"
                //};

                //string[] usersPasswords =
                //{
                //    "1",  "2", "3", "4", "5", "6",
                //};


                //if (await roleManager.FindByNameAsync("admin") == null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("admin"));
                //}
                //if (await roleManager.FindByNameAsync("user") == null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("user"));
                //}
                //if (await userManager.FindByNameAsync(adminEmail) == null)
                //{
                //    User admin = new User { Email = adminEmail, UserName = adminEmail };

                //    IdentityResult result = await userManager.CreateAsync(admin, password);
                //    if (result.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(admin, "admin");
                //    }


                //    for (int i = 0; i < 6; i++)
                //    {
                //        User user = new User { Email = usersEmails[i], UserName = usersEmails[i] };

                //        IdentityResult res = await userManager.CreateAsync(user, usersPasswords[i]);
                //        if (res.Succeeded)
                //        {
                //            await userManager.AddToRoleAsync(user, "user");
                //        }


                //        //Comment comment = new Comment
                //        //{
                //        //    Message = (i + 1) + " Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum",
                //        //    Virtues = "Lorem Ipsum",
                //        //    Shortcomings = "Lorem Ipsum",
                //        //    Rating = i,
                //        //    IsModerated = true,
                //        //    Date = new DateTime(2021, 7, 20, 18, 30, 25),
                //        //    User = user,
                //        //    Product = productXiaomiRedmiNote10Pro,
                //        //};

                //        //Like like = new Like
                //        //{
                //        //    Liking = i % 2 == 0,
                //        //    Unliking = i % 1 == 0,
                //        //    Comment = comment,
                //        //    User = user
                //        //};

                //        //context.Likes.Add(like);
                //        //context.Comments.Add(comment);
                //    }
                //}

                //await context.SaveChangesAsync();
            }
        }
    }
}
