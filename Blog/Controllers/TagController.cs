using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class TagController : Controller
    {

        [HttpGet ]
        public IActionResult Add()
        {
            return View();
        }
    }
}
