using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(string searchQuery)
    {
      IQueryable<Flavor> flavorQuery = _db.Flavors.Include(flavor => flavor.Treats).ThenInclude(join => join.Treat);
      ViewBag.SearchFlag = 0;
      if (!string.IsNullOrEmpty(searchQuery))
      {
        flavorQuery = flavorQuery.Where(flavor => flavor.Name.ToLower().Contains(searchQuery.ToLower()));
        ViewBag.SearchFlag = 1;
      }
      IEnumerable<Flavor> flavorList = flavorQuery.ToList().OrderBy(flavor => flavor.Name);
      return View(flavorList);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost, Authorize]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
          .Include(flavor => flavor.Treats)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, Authorize]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, Authorize]
    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddTreat(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [HttpPost, Authorize]
    public ActionResult AddTreat (Flavor flavor, int TreatId)
    {
      if (TreatId != 0 && !(_db.TreatFlavor.Any(join => join.Flavor.FlavorId == flavor.FlavorId && join.Treat.TreatId == TreatId)))
      {
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, Authorize]
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