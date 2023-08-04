using Microsoft.AspNetCore.Mvc;

namespace WebAppfor_AW_worker.Areas.User.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
