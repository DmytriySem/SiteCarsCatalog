using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IModelService
    {
        ModelDTO GetModel(int id);
        IEnumerable<ModelDTO> GetAllModels(int brandId);
        void AddModel(ModelDTO modelDTO);
        void UpdateModel(ModelDTO modelDTO);
        void DeleteModel(ModelDTO modelDTO);
        bool CheckIfModelNameExist(string modelName);
    }
}
