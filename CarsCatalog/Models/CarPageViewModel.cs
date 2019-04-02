using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Models
{
    public class CarPageViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public byte[] BrandPhoto { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelPhotoUrl { get; set; }
    }
}