using Microsoft.AspNetCore.Mvc;

namespace WebAppfor_AW_worker.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult loginpage()
        {
            return View();
        }
    }
}
