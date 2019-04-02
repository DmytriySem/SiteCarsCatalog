using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IGenericRepository<Brand> repoBrand;
        private readonly IGenericRepository<Car> repoCar;
        private readonly IGenericRepository<Cost> repoCost;
        private const string NAME_OF_TOMPOST_NODE = "ALL";
        public CatalogService()
        {
            repoBrand = new EFGenericRepository<Brand>();
            repoCar = new EFGenericRepository<Car>();
            repoCost = new EFGenericRepository<Cost>();
        }

        public IEnumerable<CatalogCarDTO> GetCars(string treeNodeName, string color, string volEngine, decimal minPrice, decimal maxPrice, DateTime dateTime)
        {
            var query = repoCost.Get()
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice && (DateTime.Compare(x.Date, dateTime.Date) <= 0));

            if (treeNodeName != NAME_OF_TOMPOST_NODE)
            {
                query =
                    repoCar.Get().Where(x => x.Brand.Name == treeNodeName).FirstOrDefault() != null ?
                    query.Where(x => x.Car.Brand.Name == treeNodeName) :
                    query.Where(x => x.Car.Model.Name == treeNodeName);
            }

            if (color != NAME_OF_TOMPOST_NODE)
            {
                query = query.Where(x => x.Car.Color == color);
            }

            if (volEngine != NAME_OF_TOMPOST_NODE)
            {
                var engine = Convert.ToDouble(volEngine);
                query = query.Where(x => x.Car.VolumeEngine == engine);
            }

            return query.Select(x => new CatalogCarDTO
            {
                BrandPhoto = x.Car.Brand.Photo,
                ModelPhotoUrl = x.Car.Model.PhotoUrl,
                Color = x.Car.Color,
                VolEngine = x.Car.VolumeEngine,
                Description = x.Car.Description,
                Price = x.Price
            }).ToList();
        }

        public CatalogPageDTO GetCatalogPageData()
        {
            var query = repoCar.Get();

            var colors = query.Select(c => c.Color).Distinct().ToList();
            colors.Insert(0, NAME_OF_TOMPOST_NODE);

            var volEngines = query.Select(c => c.VolumeEngine.ToString()).Distinct().ToList();
            volEngines.Insert(0, NAME_OF_TOMPOST_NODE);

            var prices = query.Select(c => c.Prices);
            var minPrice = prices.Min(v => v.Min(p => p.Price));
            var maxPrice = prices.Max(v => v.Max(p => p.Price));

            return new CatalogPageDTO()
            {
                Colors = colors,
                VolEngines = volEngines,
                MinPrice = minPrice.ToString(),
                MaxPrice = maxPrice.ToString()
            };
        }

        public IEnumerable<BrandDTO> GetTreeNodes()
        {
            return repoBrand.Include(x => x.Models).Select(p => new BrandDTO
            {
                Name = p.Name,
                Models = p.Models.Select(c => new ModelDTO { Name = c.Name })
            }).AsEnumerable();
        }
    }
}
