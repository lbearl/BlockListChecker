using BlockListChecker.ViewModels;
using System;
using System.Web.Mvc;

namespace BlockListChecker.Controllers
{
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("single")]
        public ActionResult Single(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException(nameof(emailAddress));

            var vm = new SingleEmailViewModel
            {
                EmailAddress = emailAddress
            };

            return View(vm);

        }
    }
}