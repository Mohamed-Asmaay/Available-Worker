using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Controllers
{
    public class WorkerController : Controller
    {
        dynamic mymodel = new ExpandoObject();



        private readonly WebAppfor_AW_workerContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WorkerController(WebAppfor_AW_workerContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("wrid") == null)
            {
                return RedirectToAction("u_login", "Home");
            }
            var wrId = HttpContext.Session.GetInt32("wrid");

            mymodel.Current = _context.RequestTbl.Where(Model => Model.WrId == wrId && Model.ReqAccept == null && Model.ReqDecline == null && Model.ReqConfirmation == null);
            mymodel.Inprogress = _context.RequestTbl.Where(Model => Model.WrId == wrId && Model.ReqAccept == true && Model.ReqConfirmation == null);
            mymodel.Finished = _context.RequestTbl.Where(Model => Model.WrId == wrId && Model.ReqConfirmation == true);
            mymodel.UserInfo = _context.UserTbl;


            return View(mymodel);
        }


        [ActionName("Accept")]
        public async Task<IActionResult> AcceptAsync(int id )
        {

            RequestTbl request = await _context.RequestTbl.FindAsync(id);
            request.ReqAccept = true;
            request.ReqDecline = false;

            _context.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ActionName("Reject")]
        [HttpPost]
        public async Task<IActionResult> RejectAsync(int id)
        {
            RequestTbl request = await _context.RequestTbl.FindAsync(id);
            request.ReqAccept = false;
            request.ReqDecline = true;
            request.ReqConfirmation = false;

            _context.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        [ActionName("GetInfoUser")]
        public IActionResult GetInfoUser(int id)
        {

            UserTbl userInfo = _context.UserTbl.Find(id);

            return PartialView(userInfo);
        }



        [HttpGet]
        [ActionName("Update")]
        public IActionResult Update()
        {
            ViewBag.Regions = _context.RegionTbl.ToList();
            ViewBag.jobs = _context.JobTbl.ToList();
            int workerId = (int)HttpContext.Session.GetInt32("wrid");

            var worker = _context.WorkerTbl.Where(e => e.WrId == workerId).FirstOrDefault();
            return View(worker);
        }
        [HttpGet]
        [ActionName("NewViewPhoto")]
        public IActionResult NewViewPhoto()
        {
            ViewBag.Regions = _context.RegionTbl.ToList();
            ViewBag.jobs = _context.JobTbl.ToList();
            int workerId = (int)HttpContext.Session.GetInt32("wrid");

            var worker = _context.WorkerTbl.Where(e => e.WrId == workerId).FirstOrDefault();
            return View(worker);
        }


        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateAsync([Bind("WrName,WrEmail,WrPassword,WrGender,WrPhone,WrAddress,JobName,RegionName,ImageFile,NationalId")] WorkerTbl worker,int id)
        {
            if (worker!=null)
            {
                WorkerTbl worker1 = await _context.WorkerTbl.FindAsync(id);
                worker1.WrEmail = worker.WrEmail;
                worker1.WrName= worker.WrName;
                worker1.WrPhone= worker.WrPhone;
                worker1.WrPassword= worker.WrPassword;
                worker1.WrAddress= worker.WrAddress;
                worker1.WrGender= worker.WrGender;
                worker1.RegionName = worker.RegionName;
                worker1.JobName= worker.JobName;
                worker1.ImageFile= worker.ImageFile;
                worker1.NationalId= worker.NationalId;



                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(worker1.ImageFile.FileName);
                string extension = Path.GetExtension(worker1.ImageFile.FileName);

                worker1.WrPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/Workers_photos/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await worker1.ImageFile.CopyToAsync(fileStream);
                }

                _context.Update(worker1);
                _context.SaveChanges();

                TempData["successMessage"] = "Your account has been successfully Updated!";
                return RedirectToAction("Update");
            }
            else
            {
                TempData["errorMessage"] = "Invalid Information!";
                return RedirectToAction("Update");
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("wrid");
            return RedirectToAction("Index","Home");
        }
    }
}
