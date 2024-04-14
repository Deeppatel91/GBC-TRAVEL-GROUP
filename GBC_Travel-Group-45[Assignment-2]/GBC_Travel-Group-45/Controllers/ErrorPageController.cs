using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group_45.Controllers
{
    public class ErrorPageController : Controller
    {
        [Route("ErrorPage/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["Error"] = "Page Not Found";
                    break;
                case 500:
                    ViewData["Error"] = "Internal Server Error";
                    break;
                default:
                    ViewData["Error"] = "An error occurred";
                    break;
            }
            return View("ErrorPage");
        }

        [Route("ErrorPage/500")]
        public IActionResult InternalServerError()
        {
            ViewData["Error"] = "Internal Server Error";
            return View("ErrorPage");
        }
    }
}
