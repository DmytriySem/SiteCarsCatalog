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
    public class CarService : ICarService
    {
        private readonly IGenericRepository<Car> repo;
        public CarService()
        {
            repo = new EFGenericRepository<Car>();
        }

        public void AddCar(CarDTO carDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>()
            .ForMember(p => p.Prices, opt => opt.Ignore()))
            .CreateMapper();

            var car = mapper.Map<CarDTO, Car>(carDTO);
            car.Prices.Add(new Cost { Date = DateTime.Now.Date, Price = carDTO.Price });
            repo.Create(car);
        }

        public void DeleteCar(CarDTO carDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>()
            .ForMember(p => p.Prices, opt => opt.Ignore()))
            .CreateMapper();
            repo.Delete(mapper.Map<CarDTO, Car>(carDTO));
        }

        public IEnumerable<CarDTO> GetAllCars(int modelId)
        {
            return repo.Get()
                .Where(x => x.ModelId == modelId)
                .Select(x => new CarDTO {
                    Id = x.Id,
                    BrandId = x.BrandId,
                    ModelId = x.ModelId,
                    Color = x.Color,
                    VolumeEngine = x.VolumeEngine,
                    Description = x.Description,
                    Price = x.Prices.OrderByDescending(p=>p.Date).FirstOrDefault().Price
                }).AsEnumerable();

        }

        public CarPageDTO GetCarPageData(int modelId)
        {
            var carPageData = repo.Include(x => x.BrandId, x => x.ModelId)
                .Where(x=>x.ModelId == modelId)
                .Select(x => new CarPageDTO
            {
                BrandId = x.BrandId,
                BrandName = x.Brand.Name,
                BrandPhoto = x.Brand.Photo,
                ModelId = x.ModelId,
                ModelName = x.Model.Name,
                ModelPhotoUrl = x.Model.PhotoUrl
            }).FirstOrDefault();

            if (carPageData == null)
            {
                var repoModel = new EFGenericRepository<Model>();
                carPageData = repoModel.Include(x=>x.Brand)
                    .Where(x => x.Id == modelId)
                .Select(x => new CarPageDTO
                {
                    BrandId = x.BrandId,
                    BrandName = x.Brand.Name,
                    BrandPhoto = x.Brand.Photo,
                    ModelId = x.Id,
                    ModelName = x.Name,
                    ModelPhotoUrl = x.PhotoUrl
                }).FirstOrDefault();
            }

            return carPageData;
        }

        public void UpdateCar(CarDTO carDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>()
            .ForMember(p => p.Prices, opt => opt.Ignore()))
            .CreateMapper();

            var car = mapper.Map<CarDTO, Car>(carDTO);

            var repoCost = new EFGenericRepository<Cost>();
            car.Prices = repoCost.Get().Where(p => p.CarId == car.Id).ToList();

            //repo.Update(car);

            var cost = repoCost.Get().Where(p => p.CarId == car.Id).ToList().Last().Price;
            if (carDTO.Price != cost)
            {
                car.Prices.Add(new Cost { CarId = car.Id, Date = DateTime.Now.Date, Price = carDTO.Price });
                //repoCost.Update(new Cost { Date = DateTime.Now.Date, Price = carDTO.Price });
                repoCost.Create(new Cost { CarId = car.Id, Date = DateTime.Now.Date, Price = carDTO.Price });
            }

            repo.Update(car);


            //var prices = repo.Include(p=>p.Prices).Where(p => p.Id == car.Id).Select(p => p.Prices).ToList();
            //var repoCost = new EFGenericRepository<Cost>();
            //var costs = repoCost.Get().Where(p => p.CarId == car.Id).ToList();

            //if (costs.Last().Price != carDTO.Price)
            //{
            //    var c = new Cost { CarId = car.Id, Date = DateTime.Now.Date, Price = carDTO.Price };
            //    costs.Add(new Cost { CarId = car.Id, Date = DateTime.Now.Date, Price = carDTO.Price });
            //    //car.Prices = costs;

            //    repoCost.Update(c);
            //}

            //repo.Update(car);
        }
    }
}
