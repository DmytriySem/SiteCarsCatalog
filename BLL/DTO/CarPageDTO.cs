using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CarPageDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public byte[] BrandPhoto { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelPhotoUrl { get; set; }
    }
}
