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
    public class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> repo;
        private readonly IMapper mapper;

        public BrandService()
        {
            repo = new EFGenericRepository<Brand>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<BrandDTO, Brand>()
            .ForMember(p => p.Models, opt => opt.Ignore()))
            .CreateMapper();
        }

        public void AddBrand(BrandDTO brandDTO)
        {
            repo.Create(mapper.Map<BrandDTO, Brand>(brandDTO));
        }

        public void DeleteBrand(BrandDTO brandDTO)
        {
            repo.Delete(mapper.Map<BrandDTO, Brand>(brandDTO));
        }

        public IEnumerable<BrandDTO> GetAllBrands()
        {
            return Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDTO>>(repo.Get().AsEnumerable());
        }

        public BrandDTO GetBrand(int id)
        {
            return Mapper.Map<Brand, BrandDTO>(repo.Get(id));
        }

        public bool CheckIfBrandNameExist(string brandName)
        {
            return repo.Get().FirstOrDefault(p => p.Name == brandName) == null ? false : true;
        }

        public void UpdateBrand(BrandDTO brandDTO)
        {
            repo.Update(mapper.Map<BrandDTO, Brand>(brandDTO));
        }
    }
}
