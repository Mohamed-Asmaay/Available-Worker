using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Controllers
{

    public class HomeController : Controller
    {


        private readonly WebAppfor_AW_workerContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(WebAppfor_AW_workerContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
     
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("w_sign")]

        public IActionResult W_sign()
        {
            ViewBag.Regions = _context.RegionTbl.ToList();
            ViewBag.jobs = _context.JobTbl.ToList();
            return View();

        }


        [HttpPost]
        [ActionName("w_sign")]
        public async Task<IActionResult> w_sign([Bind("WrName,WrEmail,WrPassword,WrGender,WrPhone,WrAddress,JobName,RegionName,ImageFile,NationalId")] WorkerTbl workertbl)
        {
            try
            {
                var stat1 = _context.WorkerTbl.Where(Model => Model.WrEmail == workertbl.WrEmail).FirstOrDefault();
                if (stat1 == null)
                {


                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    string fileName = Path.GetFileNameWithoutExtension(workertbl.ImageFile.FileName);
                    string extension = Path.GetExtension(workertbl.ImageFile.FileName);

                    workertbl.WrPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/Workers_photos/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await workertbl.ImageFile.CopyToAsync(fileStream);
                    }


                    _context.Add(workertbl);
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Your account has been successfully created!";
                    return RedirectToAction("w_login");





                }



                else
                {
                    ViewBag.LoginStatus = 0;
                    TempData["errorMessage"] = "pleaze enter correct email address!";
                    return RedirectToAction("w_sign");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                ModelState.Clear();
                return RedirectToAction("w_sign");
            }


        }


        [HttpGet]
        [ActionName("w_login")]

        public IActionResult w_login()
        {

            return View();
        }


        [HttpPost]
        [ActionName("w_login")]
        public IActionResult w_login([Bind("WrEmail,WrPassword")] WorkerTbl worker)
        {
            var status = _context.WorkerTbl.Where(Model => Model.WrEmail == worker.WrEmail && Model.WrPassword == worker.WrPassword).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                int wrId = status.WrId;



                HttpContext.Session.SetInt32("wrid", wrId);

                return Redirect("~/Worker/Home/Index");

            }

            return View("w_login");
            
        }



        [HttpGet]
        [ActionName("u_sign")]

        public IActionResult u_sign()
        {
            ViewBag.Regions = _context.RegionTbl.ToList();


            return View();
        }


        [HttpPost]
        [ActionName("u_sign")]
        public async Task<IActionResult> u_signAsync([Bind("UsName,UsEmail,UsPassword,UsAddress,UsPhone,UsGender,RegionName")] UserTbl1 userTbl)
        {
            try
            {
                if (ModelState.IsValid)//get and compare the Bind
                {
                    _context.Add(userTbl);
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Your account has been successfully created!";
                    return RedirectToAction("u_login");
                }
                else
                {
                    TempData["errorMessage"] = "Invalid Information!";
                    return View("u_sign");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("u_sign");
            }

        }




        [HttpGet]
        [ActionName("u_login")]

        public IActionResult u_login()
        {

            return View();
        }

        

        [HttpPost]
        [ActionName("u_login")]
        public  IActionResult u_login([Bind("UsEmail,UsPassword")] UserTbl1 user)
        {

            var status = _context.UserTbl.Where(Model => Model.UsEmail == user.UsEmail && Model.UsPassword == user.UsPassword).FirstOrDefault();
            if(status == null)
            {
                ViewBag.LoginStatus = 0;
            }else
            {
                int usId=status.UsId;


                
                HttpContext.Session.SetInt32("UserId", usId);

                return RedirectToAction("Index", "User");

            }

            return View("u_login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



//for (int i = 101; i <= 200; i++)
//{
//    if (i <= 110)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region1,
//            JobName = "Air condition",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 120 && i > 110)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region2,
//            JobName = "Air condition",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 130 && i > 120)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region3,
//            JobName = "Air condition",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 140 && i > 130)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region1,
//            JobName = "Auto mechanic",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 150 && i >140)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region2,
//            JobName = "Auto mechanic",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 160 && i > 150)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region3,
//            JobName = "Auto mechanic",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 170 && i > 160)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region1,
//            JobName = "electrician",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 170 && i > 180)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region2,
//            JobName = "Carpenter",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 180 && i > 170)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region3,
//            JobName = "Carpenter",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 190 && i > 180)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region1,
//            JobName = "Carpenter",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }
//    else if (i <= 200 && i > 190)
//    {
//        WorkerTbl workerTbl = new WorkerTbl()
//        {
//            WrName = name + i,
//            WrEmail = email + i + "@gmail.com",
//            WrPassword = "123123",
//            WrGender = "Male",
//            WrPhone = "01027019322",
//            WrAddress = "Assuit_firyal",
//            WrPhoto = "avatar-05.png",
//            RegionName = region2,
//            JobName = "electrician",
//            NationalId = "30107282502414"
//        };
//        _context.WorkerTbl.Add(workerTbl);
//        _context.SaveChanges();

//    }

//}
//for (int i = 1; i <= 16; i++)
//{
//    if (i <= 4)
//    {
//        RequestTbl request = new RequestTbl()
//        {
//            ReqTime = DateTime.Now,
//            ReqProblem = "need a AC repair",
//            ReqDescription = "The air conditioning is not working well",
//            UsId = 1,
//            WrId = 122,
//            ReqAccept = true,
//            ReqConfirmation = true,
//            ReqDecline = false,

//        };
//        _context.RequestTbl.Add(request);
//        _context.SaveChanges();

//    }
//    else if (i > 4 && i <= 8)
//    {
//        RequestTbl request = new RequestTbl()
//        {
//            ReqTime = DateTime.Now,
//            ReqProblem = "need a AC repair",
//            ReqDescription = "The air conditioning is not working well",
//            UsId = 1,
//            WrId = 122,
//            ReqAccept = false,
//            ReqConfirmation = false,
//            ReqDecline = true,

//        };
//        _context.RequestTbl.Add(request);
//        _context.SaveChanges();

//    }
//    else if (i > 8 && i <= 12)
//    {
//        RequestTbl request = new RequestTbl()
//        {
//            ReqTime = DateTime.Now,
//            ReqProblem = "need a AC repair",
//            ReqDescription = "The air conditioning is not working well",
//            UsId = 1,
//            WrId = 122,
//            ReqAccept = null,
//            ReqConfirmation = null,
//            ReqDecline = null,

//        };
//        _context.RequestTbl.Add(request);
//        _context.SaveChanges();

//    }
//    else if (i > 12 && i <= 16)
//    {
//        RequestTbl request = new RequestTbl()
//        {
//            ReqTime = DateTime.Now,
//            ReqProblem = "need a AC repair",
//            ReqDescription = "The air conditioning is not working well",
//            UsId = 1,
//            WrId = 122,
//            ReqAccept = true,
//            ReqConfirmation = null,
//            ReqDecline = null,

//        };
//        _context.RequestTbl.Add(request);
//        _context.SaveChanges();

//    }





//}