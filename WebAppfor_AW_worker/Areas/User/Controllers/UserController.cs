using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Areas.User.Controllers
{
    [Area("User")]
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
    }
}