using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Catalog.Models;
using jsTree3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService catalogService;
        private readonly IEnumerable<int> tileCounts = Enumerable.Range(1, 5);

        public CatalogController(ICatalogService _catalogService)
        {
            catalogService = _catalogService;
        }
        // GET: Catalog
        public ActionResult Index()
        {
            var catalogDTO = catalogService.GetCatalogPageData();

            var catalog = new CatalogPageViewModel
            {
                Colors = new SelectList(catalogDTO.Colors, catalogDTO.Colors.First()),
                VolEngines = new SelectList(catalogDTO.VolEngines, catalogDTO.VolEngines.First()),
                MinPrice = catalogDTO.MinPrice,
                MaxPrice = catalogDTO.MaxPrice,
                TileCounts = new SelectList(tileCounts, 5)
            };


            return View(catalog);
        }
        public JsonResult GetTree()
        {
            var nodes = catalogService.GetTreeNodes();

            var tree = JsTree3Node.NewNode("ALL");
            tree.text = "ALL";
            //tree.state = new State(true, false, true); //(bool Opened, bool Disabled, bool Selected)

            var items = nodes.Select(p => new JsTree3Node
            {
                text = p.Name,
                children = p.Models.Select(c => new JsTree3Node
                {
                    text = c.Name
                }).ToList()
            }).ToList();


            tree.children = items;

            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCars(string treeNodeName, string color, string volEngine, string minPrice,
            string maxPrice, string date, int colsNum)
        {
            var cars = Mapper.Map<IEnumerable<CatalogCarDTO>, IEnumerable<CatalogCarViewModel>>(catalogService.GetCars(
                treeNodeName,
                color,
                volEngine,
                Convert.ToDecimal(minPrice),
                Convert.ToDecimal(maxPrice),
                DateTime.ParseExact(date, "ddd MMM dd yyyy", CultureInfo.InvariantCulture)
                ));

            ViewBag.NumOfCols = colsNum;

            return PartialView("FoundCars", cars);
        }
    }
}