using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICatalogService
    {
        IEnumerable<BrandDTO> GetTreeNodes();
        CatalogPageDTO GetCatalogPageData();
        IEnumerable<CatalogCarDTO> GetCars(string treeNodeName, string color,
            string volEngine, decimal minPrice,
            decimal maxPrice, DateTime dateTime);
    }
}
