using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Models
{
    public class ModelViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Model name is required")]
        [StringLength(13, ErrorMessage = "Exceeded 13 characters")]
        [Display(Name="Model name")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string PhotoUrl { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BrandId { get; set; }
    }
}