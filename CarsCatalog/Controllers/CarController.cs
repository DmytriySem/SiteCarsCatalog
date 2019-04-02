using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Catalog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsCatalog.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        public CarController(ICarService _carService)
        {
            carService = _carService;
        }
        // GET: Car
        public ActionResult Cars(int modelId)
        {
            var car = Mapper.Map<CarPageDTO, CarPageViewModel>(carService.GetCarPageData(modelId));

            TempData["BrandId"] = car.BrandId;
            TempData["ModelId"] = car.ModelId;

            return View(car);
        }

        public string GetCars(int modelId, string sidx, int page, int rows, string sord, string searchString)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var cars = Mapper.Map<IEnumerable<CarDTO>, IEnumerable<CarViewModel>>(carService.GetAllCars(modelId));

            int totalRecords = cars.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                cars = cars.OrderByDescending(s => s.Id).ToList();
                cars = cars.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                cars = cars.OrderBy(s => s.Id).ToList();
                cars = cars.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                if (float.TryParse(searchString, out float volEngine))
                {
                    cars = cars.Where(m => m.VolumeEngine == Math.Round(volEngine, 2)).ToList();
                }
                else
                {
                cars = cars.Where(m => m.Color == searchString).ToList();
                }
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = cars
            };

            return JsonConvert.SerializeObject(jsonData);
        }

        // POST: Car/Create
        [HttpPost]
        public void Create(CarViewModel car)
        {
            car.BrandId = (int)TempData["BrandId"];
            car.ModelId = (int)TempData["ModelId"];

            carService.AddCar(Mapper.Map<CarViewModel, CarDTO>(car));
        }

        // POST: Car/Edit/5
        [HttpPost]
        public void Edit(CarViewModel car)
        {
            carService.UpdateCar(Mapper.Map<CarViewModel, CarDTO>(car));
        }

        // POST: Car/Delete/5
        [HttpPost]
        public void Delete(int id)
        {
            carService.DeleteCar(new CarDTO { Id = id });
        }
    }
}