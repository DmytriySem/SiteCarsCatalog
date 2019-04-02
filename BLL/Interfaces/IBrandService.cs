using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandDTO> GetAllBrands();
        BrandDTO GetBrand(int id);
        void AddBrand(BrandDTO brandDTO);
        void UpdateBrand(BrandDTO brandDTO);
        void DeleteBrand(BrandDTO brandDTO);
        bool CheckIfBrandNameExist(string brandName);
    }
}
