using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
    public class CatalogCarViewModel
    {
        public int Id { get; set; }
        public byte[] BrandPhoto { get; set; }
        public string ModelPhotoUrl { get; set; }
        [Display(Name = "Color:")]
        public string Color { get; set; }
        [Display(Name ="Volume engine:")]
        public double VolEngine { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
        [Display(Name = "Price:")]
        public decimal Price { get; set; }
    }
}