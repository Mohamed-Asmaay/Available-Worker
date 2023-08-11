using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Controllers
{
    public class UserController : Controller
    {


        private readonly WebAppfor_AW_workerContext _context;

        public UserController(WebAppfor_AW_workerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int UsId = (int)HttpContext.Session.GetInt32("UserId");
                dynamic mymodel = new ExpandoObject();
                mymodel.Jops = _context.JobTbl.ToList();
                mymodel.Requests = _context.RequestTbl.Where(Model => Model.UsId == UsId);
                return View(mymodel);
            }
            else
            {
                return RedirectToAction("u_login", "Home");
            }
        }

        [HttpGet]
        [ActionName("SetJobId")]

        public IActionResult SetJobId(int id)
        {
            HttpContext.Session.SetInt32("Jobid", id);
            HttpContext.Session.SetInt32("RegionId", 0);



            return RedirectToAction("srv_req");

        }
        public IActionResult SetRegionId(int id)
        {
            HttpContext.Session.SetInt32("RegionId", id);


            return RedirectToAction("srv_req");

        }
        public IActionResult SetWorkerId(int id)
        {
            HttpContext.Session.SetInt32("wrID", id);


            return RedirectToAction("finish_req");

        }
        public IActionResult SetWorkerProfileId(int id)
        {
            HttpContext.Session.SetInt32("WorkerProfileId", id);


            return RedirectToAction("Profile");

        }





        [HttpGet]
        [ActionName("srv_req")]

        public IActionResult srv_req()
        {
            if (HttpContext.Session.GetInt32("Jobid") != null)
            {
                if ((int)HttpContext.Session.GetInt32("RegionId") == 0)
                {
                    int JobID = (int)HttpContext.Session.GetInt32("Jobid");

                    var job = _context.JobTbl.Where(Model => Model.JobId == JobID).FirstOrDefault();
                    string jobname = job.JobName;
                    var user = _context.UserTbl.Find((int)HttpContext.Session.GetInt32("UserId"));
                    dynamic mymodel = new ExpandoObject();
                    mymodel.Regions = _context.RegionTbl.ToList();
                    mymodel.Workers = _context.WorkerTbl.Where(Model => Model.RegionName == user.RegionName && Model.JobName == jobname);


                    return View(mymodel);
                }
                else
                {
                    int JobID = (int)HttpContext.Session.GetInt32("Jobid");

                    var job = _context.JobTbl.Where(Model => Model.JobId == JobID).FirstOrDefault();
                    string jobname = job.JobName;
                    var region = _context.RegionTbl.Find((int)HttpContext.Session.GetInt32("RegionId"));
                    dynamic mymodel = new ExpandoObject();
                    mymodel.Regions = _context.RegionTbl.ToList();
                    mymodel.Workers = _context.WorkerTbl.Where(Model => Model.RegionName == region.RegionName && Model.JobName == jobname);



                    return View(mymodel);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        [HttpGet]
        [ActionName("finish_req")]

        public IActionResult finish_req()
        {
            int wrid = (int)HttpContext.Session.GetInt32("wrID");
            dynamic mymodel = new ExpandoObject();
            mymodel.worker = _context.WorkerTbl.Where(Model => Model.WrId == wrid).ToList();
            return View(mymodel);
        }

        [HttpPost]
        [ActionName("finish_req")]

        public IActionResult finish_req(IFormCollection s)
        {
            if (HttpContext.Session.GetInt32("wrID") != null)
            {
                int wrID = (int)HttpContext.Session.GetInt32("wrID");
                if (wrID > 0 && wrID != null)
                {
                    if ((string)s["Title"] != "" && (string)s["Description"] != "")
                    {
                        string ReqProblem = s["Title"];
                        string ReqDescription = s["Description"];

                        RequestTbl request = new RequestTbl()
                        {
                            ReqTime = DateTime.Now,
                            ReqProblem = ReqProblem,
                            ReqDescription = ReqDescription,
                            UsId = (int)HttpContext.Session.GetInt32("UserId"),
                            WrId = wrID

                        };

                        _context.RequestTbl.Add(request);
                        _context.SaveChanges();
                        TempData["successMessage"] = "Your Request has been successfully created!";
                        return RedirectToAction("finish_req");
                    }
                    else
                    {

                        TempData["errorMessage"] = "Invalid Information!";
                        return RedirectToAction("finish_req");


                    }
                }
                else
                {

                    TempData["errorMessage"] = "please select your worker";
                    return View(s);

                }
            }
            else
            {
                return RedirectToAction("srv_req");

            }


        }


        [HttpGet]
        [ActionName("Update")]

        public IActionResult Update()
        {
            ViewBag.Region = _context.RegionTbl.ToList();
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int userId = (int)HttpContext.Session.GetInt32("UserId");

                var user = _context.UserTbl.Where(e => e.UsId == userId).FirstOrDefault();
                return View(user);
            }
            else {
                return RedirectToAction("u_login","Home");
            }
        }

        [HttpPost]
        [ActionName("Update")]

        public async Task<IActionResult> UpdateAsync([Bind("UsName,UsEmail,UsPassword,UsAddress,UsPhone,UsGender,RegionName")] UserTbl1 user, int id)
        {
            if (ModelState.IsValid)
            {
                UserTbl1 User=await _context.UserTbl.FindAsync(id);
                User.UsEmail=user.UsEmail;
                User.UsName=user.UsName;
                User.UsPhone=user.UsPhone;
                User.UsPassword=user.UsPassword;
                User.UsAddress=user.UsAddress;
                User.UsGender=user.UsGender;
                User.RegionName=user.RegionName;
                _context.Update(User);
                _context.SaveChanges();

                TempData["successMessage"] = "Your account has been successfully Updated!";
                return RedirectToAction("Update");
            }
            else
            {
                TempData["errorMessage"] = "Invalid Information!";
                return View();
            }
        }




        [ActionName("Profile")]

        public async Task<IActionResult> ProfileAsync()
        {
            if (HttpContext.Session.GetInt32("WorkerProfileId") != null)
            {
                int wrid = (int)HttpContext.Session.GetInt32("WorkerProfileId");
                var status = await _context.WorkerTbl.Where(Model => Model.WrId == wrid).ToListAsync();
                HttpContext.Session.Remove("WorkerProfileId");
                return View(status);
            }
            else
            {
                TempData["errorMessage"] = "please select your worker";
                return View();
            }
        }




        [HttpGet]
        [ActionName("C_complain")]

        public IActionResult C_complain()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("u_login", "Home");
            }
        }

        [HttpPost]
        [ActionName("C_complain")]

        public IActionResult C_complain([Bind("ReqId,ComDescription")] ComplaintTbl com)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = (int)HttpContext.Session.GetInt32("UserId");
                    var item = _context.RequestTbl.Where(e => e.UsId == userId && e.ReqId == com.ReqId).FirstOrDefault();

                    if (item != null)
                    {
                        ComplaintTbl complaint = new ComplaintTbl()
                        {
                            ComDate = DateTime.Now,
                            ComDescription = com.ComDescription,
                            UsId = userId,
                            ReqId = com.ReqId
                        };

                        _context.ComplaintTbl.Add(complaint);
                        _context.SaveChangesAsync();
                        TempData["successMessage"] = "Your Complain has been successfully created!";
                        return View();

                    }
                    else
                    {
                        TempData["errorMessage"] = "Invalid Request Id !";
                        return View();
                    }

                }
                else
                {
                    TempData["errorMessage"] = "Invalid Information!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }










        [HttpPost]
        [ActionName("confirm")]
        public async Task<IActionResult> confirmAsync(IFormCollection form)
        {
            var x = float.Parse(form["email"]);
            var id = int.Parse(form["id"]);

            RequestTbl request = await _context.RequestTbl.FindAsync(id);
            request.ReqConfirmation = true;
            request.ReqCost = x;

            _context.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ActionName("Cancel")]
        public async Task<IActionResult> CancelAsync(int id)
        {

            RequestTbl request = await _context.RequestTbl.FindAsync(id);


            _context.RequestTbl.Remove(request);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }



        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                HttpContext.Session.Remove("UserId");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("u_login", "Home");
            }
            
        }

    }
}
