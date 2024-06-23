using Kiwi.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<ProductModel> res = [];
            return View(res);
        }
    }
}
