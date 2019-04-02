using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Models
{
    public class BrandViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Brand name:")]
        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(13, ErrorMessage = "Exceeded 13 characters")]
        public string Name { get; set; }

        [Display(Name = "Image:")]
        public byte[] Photo { get; set; }
         
        [DataType(DataType.Upload)]
        [FileExtensions(ErrorMessage = "Only Image files allowed")]
        public HttpPostedFileBase File { get; set; }
    }
}