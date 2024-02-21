using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Application.Common.Utility;
using WhiteLagoon.web.Models.ViewModels;
namespace WhiteLagoon.web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        #region Ctor UnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region View Amenity Number
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "villa");
            return View(amenities);
        }
        #endregion

        #region Create Amenity Number
        public IActionResult CreateAmenity()
        {
            //for Drop Down List  
            //Transfer temp data Controllert to View

            AmenityVM AmenityNumberVM = new()
            {
                VillaList = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(AmenityNumberVM);
        }
        [HttpPost]
        public IActionResult CreateAmenity(AmenityVM obj)
        {
            if (obj is not null)
            {
                _unitOfWork.Amenity.Create(obj.Amenity);
                TempData["success"] = "The Amenity Number  has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
           
                obj.VillaList = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }
        #endregion

        #region Update Amenity Number
        public IActionResult Update(int amenityId)
        {
            AmenityVM AmenityNumberVM = new()
            {
                VillaList =_unitOfWork.Amenity.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };
            if (AmenityNumberVM.Amenity == null)
            {
                return RedirectToAction("error","Home"); 
            }
            return View(AmenityNumberVM);
        }
        [HttpPost]
        public IActionResult Update(AmenityVM obj)
        {

            if (obj is not null)
            {
                _unitOfWork.Amenity.Update(obj.Amenity);
                TempData["success"] = "The Amenity Number  has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
          
            obj.VillaList = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);

        }
        #endregion

        #region Delete Amenity Number
        public IActionResult DeleteAmenity(int amenityId)
        {
            AmenityVM AmenityNumberVM = new()
            {
                VillaList = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };
            if (AmenityNumberVM.Amenity == null)
            {
                return RedirectToAction("error", "Home");
            }
            return View(AmenityNumberVM);
        }
        [HttpPost]
        public IActionResult DeleteAmenity(AmenityVM AmenityNumberVM)
        {
            var AmenityNumber = _unitOfWork.Amenity.Get(x => x.Id == AmenityNumberVM.Amenity.Id);
            if (AmenityNumberVM == null)
            {
                TempData["error"] = "Failed to delete the Amenity.";
                return RedirectToAction("Error", "Home");
            }
            _unitOfWork.Amenity.Remove(AmenityNumber);
            TempData["success"] = "The Amenity has been Deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}