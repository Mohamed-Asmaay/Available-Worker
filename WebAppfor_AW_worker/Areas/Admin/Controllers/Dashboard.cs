using Microsoft.AspNetCore.Mvc;
using WebAppfor_AW_worker.Data;

namespace WebAppfor_AW_worker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly WebAppfor_AW_workerContext _context;

        public Dashboard(WebAppfor_AW_workerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
