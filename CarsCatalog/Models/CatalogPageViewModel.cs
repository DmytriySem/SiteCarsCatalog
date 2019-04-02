using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Models
{
    public class CatalogPageViewModel
    {
        [DisplayName("Color:")]
        public SelectList Colors { get; set; }
        [DisplayName("Volume engine:")]
        public SelectList VolEngines { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public SelectList TileCounts { get; set; }
    }
}