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
    public class ModelService : IModelService
    {
        private readonly IGenericRepository<Model> repo;
        private readonly IMapper mapper;
        public ModelService()
        {
            repo = new EFGenericRepository<Model>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ModelDTO, Model>()
            .ForMember(p => p.Cars, opt => opt.Ignore())
            .ForMember(p => p.Brand, opt => opt.Ignore()))
            .CreateMapper();
        }
        public void AddModel(ModelDTO modelDTO)
        {
            repo.Create(mapper.Map<ModelDTO, Model>(modelDTO));
        }

        public bool CheckIfModelNameExist(string modelName)
        {
            return repo.Get().FirstOrDefault(x => x.Name == modelName) == null ? false : true;
        }

        public void DeleteModel(ModelDTO modelDTO)
        {
            repo.Delete(mapper.Map<ModelDTO, Model>(modelDTO));
        }

        public IEnumerable<ModelDTO> GetAllModels(int brandId)
        {
            return Mapper.Map<IEnumerable<Model>, IEnumerable<ModelDTO>>(repo.Get().Where(x => x.BrandId == brandId).AsEnumerable());
        }

        public ModelDTO GetModel(int id)
        {
            return Mapper.Map<Model, ModelDTO>(repo.Get(id));
        }

        public void UpdateModel(ModelDTO modelDTO)
        {
            repo.Update(mapper.Map<ModelDTO, Model>(modelDTO));
        }
    }
}
