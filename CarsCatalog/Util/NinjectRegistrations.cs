﻿//using BLL.Interfaces;
//using BLL.Services;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBrandService>().To<BrandService>();
            Bind<IModelService>().To<ModelService>();
            Bind<ICarService>().To<CarService>();
            Bind<ICatalogService>().To<CatalogService>();
        }
    }
}