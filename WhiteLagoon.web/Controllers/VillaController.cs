using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
namespace WhiteLagoon.web.Controllers
{
    public class VillaController : Controller
    {
        #region Ctor
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region View Villa
        public IActionResult Index()
        {
            var villa = _db.Villas.ToList();
            return View(villa);
        }
        #endregion

        #region Create Villa
        public IActionResult CreateVilla(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Update Villa
        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj == null)
            {
                TempData["error"] = "Failed to Update the villa.";
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been Update successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Delete Villa
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj == null)
            {
                TempData["error"] = "Failed to delete the villa.";
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            if (obj == null)
            {
                TempData["error"] = "Failed to delete the villa.";
                return RedirectToAction("Error", "Home");
            }
            _db.Villas.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "The villa has been Deleted successfully.";
            return RedirectToAction("Index");
        }
        #endregion
    }
}