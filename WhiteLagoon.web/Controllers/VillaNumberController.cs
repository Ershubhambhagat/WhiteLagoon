using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.web.Models.ViewModels;
namespace WhiteLagoon.web.Controllers
{
    public class VillaNumberController : Controller
    {
        #region Ctor
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region View Villa Number
        public IActionResult Index()
        {
            var VillaNumber = _db.VillaNumber.ToList();
            return View(VillaNumber);
        }
        #endregion

        #region Create Villa Number
        public IActionResult CreateVillaNumber()
        {
            //for Drop Down List  
            //Transfer temp data Controllert to View

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult CreateVillaNumber(VillaNumber obj)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumber.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa Number  has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Update Villa Number
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
        public IActionResult Update(VillaNumber obj)
        {
            if (ModelState.IsValid && obj.VillaId > 0)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been Update successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Delete Villa Number
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