using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Domain.Entities;
namespace WhiteLagoon.web.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {
        #region Ctor UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;//for Image Uplode 
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Get All Villa
        public IActionResult Index()
        {
            var villa = _unitOfWork.Villa.GetAll();
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
                if (obj.Image is not null)//for Image uplode 
                {
                    string fileName = "VillaImage_" + obj.Name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string ImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, @"images\VillaImage");
                    using var fileStream = new FileStream(Path.Combine(ImagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400/black/red";
                }
                _unitOfWork.Villa.Create(obj);
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Update Villa
        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
                if (obj.Image is not null)//for Image Update 
                {
                    string fileName = "VillaImage_" + obj.Name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string ImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, @"images\VillaImage");
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath= Path.Combine(_WebHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    using var fileStream = new FileStream(Path.Combine(ImagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
               
                _unitOfWork.Villa.Update(obj);
                TempData["success"] = "The villa has been Update successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Delete Villa
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
            Villa? objFormDB = _unitOfWork.Villa.Get(u => u.Id == obj.Id);
            if (objFormDB != null)
            {

                if (!string.IsNullOrEmpty(objFormDB.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, objFormDB.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }
                _unitOfWork.Villa.Remove(objFormDB);
                TempData["success"] = "The villa has been Deleted successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion
    }
}