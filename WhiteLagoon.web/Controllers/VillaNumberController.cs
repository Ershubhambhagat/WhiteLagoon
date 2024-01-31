using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var VillaNumber = _db.VillaNumber.Include(u => u.villa).ToList();
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
        public IActionResult CreateVillaNumber(VillaNumberVM obj)
        {
            bool roomNumberExist = _db.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number); ;
            if (ModelState.IsValid && !roomNumberExist)
            {
                _db.VillaNumber.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number  has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (roomNumberExist)
            {
                TempData["error"] = "The villa Number  has been already exists.";

            }
            obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }
        #endregion

        #region Update Villa Number
        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _db.VillaNumber.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("error","Home"); 
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {

            if (ModelState.IsValid )
            {
                _db.VillaNumber.Update(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number  has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
          
            obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);

        }
        #endregion

        #region Delete Villa Number
        public IActionResult DeleteVillaNumber(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _db.VillaNumber.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult DeleteVillaNumber(VillaNumberVM villaNumberVM)
        {
            var villaNumber = _db.VillaNumber.
                FirstOrDefault(x => x.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (villaNumberVM == null)
            {
                TempData["error"] = "Failed to delete the villa.";
                return RedirectToAction("Error", "Home");
            }
            _db.VillaNumber.Remove(villaNumber);
            _db.SaveChanges();
            TempData["success"] = "The villa has been Deleted successfully.";
            return RedirectToAction("Index");
        }
        #endregion
    }
}