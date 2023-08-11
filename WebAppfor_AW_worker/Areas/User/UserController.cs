using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Areas.User
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
            return View();
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
            else
            {
                return RedirectToAction("u_login", "Home");
            }
        }

        [HttpPost]
        [ActionName("Update")]

        public async Task<IActionResult> UpdateAsync([Bind("UsName,UsEmail,UsPassword,UsAddress,UsPhone,UsGender,RegionName")] UserTbl user, int id)
        {
            if (ModelState.IsValid)
            {
                UserTbl User = await _context.UserTbl.FindAsync(id);
                User.UsEmail = user.UsEmail;
                User.UsName = user.UsName;
                User.UsPhone = user.UsPhone;
                User.UsPassword = user.UsPassword;
                User.UsAddress = user.UsAddress;
                User.UsGender = user.UsGender;
                User.RegionName = user.RegionName;
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





    }
}
