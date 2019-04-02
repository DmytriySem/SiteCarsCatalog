using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Color { get; set; }
        public double VolumeEngine { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
