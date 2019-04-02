using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsCatalog.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService _brandService)
        {
            brandService = _brandService;
        }
        public ActionResult Brands()
        {
            var brands = Mapper.Map<IEnumerable<BrandDTO>, IEnumerable<BrandViewModel>>(brandService.GetAllBrands());

            return View(brands);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                if (brandService.CheckIfBrandNameExist(brand.Name))
                {
                    ModelState.AddModelError(string.Empty, "Such a brand is already exists");

                    return PartialView(brand);
                }

                if (brand.File != null)
                {
                    brand.Photo = ConvertPostedFileToByteMass(brand.File);
                }

                brandService.AddBrand(Mapper.Map<BrandViewModel, BrandDTO>(brand));

                //return PartialView("SuccessCreate");
                return RedirectToAction("Brands");
            }

            return PartialView(brand);
        }

        public ActionResult Edit(int id)
        {
            var brand = Mapper.Map<BrandDTO, BrandViewModel>(brandService.GetBrand(id));
            TempData["Photo"] = brand.Photo;

            return PartialView(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.File != null)
                {
                    brand.Photo = ConvertPostedFileToByteMass(brand.File);
                }
                else
                {
                    brand.Photo = TempData.Peek("Photo") as byte[];
                }

                brandService.UpdateBrand(Mapper.Map<BrandViewModel, BrandDTO>(brand));

                //return PartialView("SuccessUpdate");
                RedirectToAction("Brands");
            }

            brand.Photo = TempData["Photo"] as byte[];

            return PartialView(brand);
        }

        public ActionResult Delete(int id)
        {
            var brand = Mapper.Map<BrandDTO, BrandViewModel>(brandService.GetBrand(id));

            return PartialView(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BrandViewModel brand)
        {
            brandService.DeleteBrand(Mapper.Map<BrandViewModel, BrandDTO>(brand));

            return RedirectToAction("Brands");
        }

        private byte[] ConvertPostedFileToByteMass(HttpPostedFileBase file)
        {
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                return binaryReader.ReadBytes(file.ContentLength);
            }
        }
    }
}