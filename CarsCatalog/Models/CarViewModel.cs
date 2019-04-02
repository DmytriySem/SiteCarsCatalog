using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
    public class CarViewModel
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