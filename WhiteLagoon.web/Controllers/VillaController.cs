using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.web.Controllers
{
    
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db= db;
        }
        public IActionResult Index()
        {
            var villa=_db.Villas.ToList();
            return View(villa);
        }
      
        
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

       
        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u=>u.Id == villaId);
                
            if (obj == null)
            {
                return RedirectToAction("Error","Home");
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
                return RedirectToAction(nameof(Index));
            }
            return View();
        }






    }









}
