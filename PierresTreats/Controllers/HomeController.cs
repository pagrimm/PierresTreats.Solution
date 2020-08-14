using Microsoft.AspNetCore.Mvc;

namespace PierresTreats.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(string searchOption, string searchString)
    {
      if (searchOption == "treats")
      {
        return RedirectToAction ("Index", "Treats", new {searchQuery = searchString});
      }
      else
      {
        return RedirectToAction ("Index", "Flavors", new {searchQuery = searchString});
      }
    }
  }
}