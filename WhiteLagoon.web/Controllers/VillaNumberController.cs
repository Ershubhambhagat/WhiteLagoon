using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.web.Models.ViewModels;

namespace WhiteLagoon.web.Controllers
{
    [Authorize]
    public class VillaNumberController : Controller
    {
        #region Ctor UnitOfWork

        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Ctor UnitOfWork

        #region View Villa Number

        public IActionResult Index()
        {
            // var VillaNumber = _unitOfWork.VillaNumber.Get(includeProperties: "villa");
            var VillaNumber = _unitOfWork.VillaNumber.GetAll(includeProperties: "villa");
            return View(VillaNumber);
        }

        #endregion View Villa Number

        #region Create Villa Number

        public IActionResult CreateVillaNumber()
        {
            //for Drop Down List
            //Transfer temp data Controllert to View
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
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
            bool roomNumberExist = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number); ;
            if (ModelState.IsValid && !roomNumberExist)
            {
                _unitOfWork.VillaNumber.Create(obj.VillaNumber);
                TempData["success"] = "The villa Number  has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (roomNumberExist)
            {
                TempData["error"] = "The villa Number  has been already exists.";
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        #endregion Create Villa Number

        #region Update Villa Number

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(obj.VillaNumber);
                TempData["success"] = "The villa Number  has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        #endregion Update Villa Number

        #region Delete Villa Number

        public IActionResult DeleteVillaNumber(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
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
            var villaNumber = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (villaNumberVM == null)
            {
                TempData["error"] = "Failed to delete the villa.";
                return RedirectToAction("Error", "Home");
            }
            _unitOfWork.VillaNumber.Remove(villaNumber);
            TempData["success"] = "The villa has been Deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        #endregion Delete Villa Number
    }
}