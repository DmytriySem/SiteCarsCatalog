using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CatalogPageDTO
    {
        public IList<string> Colors { get; set; }
        public IList<string> VolEngines { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
    }
}
