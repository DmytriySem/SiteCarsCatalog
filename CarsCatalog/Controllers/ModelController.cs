using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsCatalog.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelService modelService;
        public ModelController(IModelService _modelService)
        {
            modelService = _modelService;
        }

        public ActionResult Models(int brandId, string brandName)
        {
            TempData["BrandName"] = brandName.ToUpper();
            TempData["BrandId"] = brandId;

            var models = Mapper.Map<IEnumerable<ModelDTO>, IEnumerable<ModelViewModel>>(modelService.GetAllModels(brandId));

            return View(models);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (modelService.CheckIfModelNameExist(model.Name))
                {
                    ModelState.AddModelError(string.Empty, "Such a model is already exists");

                    return PartialView(model);
                }
                if (model.File != null)
                {
                    SaveImage(model.Name, model.File);
                    model.PhotoUrl = model.Name + ".jpg";
                }

                model.BrandId = (int)TempData.Peek("BrandId");
                modelService.AddModel(Mapper.Map<ModelViewModel, ModelDTO>(model));

                //return PartialView("SuccessCreate");
                return RedirectToAction("Models", new { brandId = TempData.Peek("BrandId"), brandName = TempData.Peek("BrandName") });
            }

            return PartialView(model);
        }

        private void SaveImage(string name, HttpPostedFileBase file)
        {
            string writePath = Server.MapPath(@"~/Content/Images/Models/") + name + ".jpg";
            Image img = Image.FromStream(file.InputStream);
            img.Save(writePath);
        }

        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<ModelDTO, ModelViewModel>(modelService.GetModel(id));
            TempData["PhotoUrl"] = model.PhotoUrl;

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    SaveImage(model.Name, model.File);
                    model.PhotoUrl = model.Name + ".jpg";
                }
                else
                {
                    model.PhotoUrl = TempData.Peek("PhotoUrl") as string;
                }

                model.BrandId = (int)TempData.Peek("BrandId");
                modelService.UpdateModel(Mapper.Map<ModelViewModel, ModelDTO>(model));

                //return PartialView("SuccessUpdate");
                return RedirectToAction("Models", new { brandId = TempData["BrandId"], brandName = TempData["BrandName"] });
            }

            model.PhotoUrl = TempData["PhotoUrl"] as string;

            return PartialView(model);

        }

        public ActionResult Delete(int id)
        {
            var model = Mapper.Map<ModelDTO, ModelViewModel>(modelService.GetModel(id));

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModelViewModel model)
        {
            modelService.DeleteModel(Mapper.Map<ModelViewModel, ModelDTO>(model));

            return RedirectToAction("Models", new { brandId = model.BrandId, brandName = TempData["BrandName"] });
        }
    }
}