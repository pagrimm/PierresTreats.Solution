using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, ToDoListContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(string searchQuery)
    {
      IQueryable<Treat> treatQuery = _db.Treats.Include(treat => treat.Flavors).ThenInclude(join => join.Flavor);
      ViewBag.SearchFlag = 0;
      if (!string.IsNullOrEmpty(searchQuery))
      {
        treatQuery = treatQuery.Where(treat => treat.Name.ToLower().Contains(searchQuery.ToLower()));
        ViewBag.SearchFlag = 1;
      }
      IEnumerable<Engineer> treatList = treatQuery.ToList().OrderBy(treat => treat.Name);
      return View(treatList);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.Treats)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor(int id)
    {
      Treat thisTreat = _db.Treat.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddFlavor (Treat treat, int FlavorId)
    {
      if (FlavorId != 0 && !(_db.TreatFlavor.Any(join => join.Treat.TreatId == treat.TreatId && join.Flavor.FlavorId == FlavorId)))
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult RemoveTreat (int FlavorId, int TreatId)
    {
      if (TreatId != 0 && FlavorId != 0)
      {
        TreatFlavor thisTreatFlavor = _db.TreatFlavor.FirstOrDefault(join => join.Flavor.FlavorId == FlavorId && join.Treat.TreatId == TreatId);
        _db.TreatFlavor.Remove(thisTreatFlavor);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}