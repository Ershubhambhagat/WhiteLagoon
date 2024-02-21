using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.web.Models;
using WhiteLagoon.web.Models.ViewModels;

namespace WhiteLagoon.web.Controllers
{
    public class HomeController : Controller
    {
        #region CTOR
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region index


        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                CheckOutDate = DateOnly.FromDateTime(DateTime.Now),
                Nights = 1
            };
            return View(homeVM);
        }


        [HttpPost]
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
            foreach(var villa in  homeVM.VillaList)
            {
                if(villa.Id %2==0) 
                {
                    villa.IsAvailable = true;
                }
            }
            return View(homeVM);
               
        }


        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}