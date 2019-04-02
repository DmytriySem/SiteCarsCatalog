using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CatalogCarDTO
    {
        public int Id { get; set; }
        public byte[] BrandPhoto { get; set; }
        public string ModelPhotoUrl { get; set; }
        public string Color { get; set; }
        public double VolEngine { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
