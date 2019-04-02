using AutoMapper;
using BLL.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsCatalog.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BrandDTO, BrandViewModel>().ForMember(p => p.File, opt => opt.Ignore());
                cfg.CreateMap<ModelDTO, ModelViewModel>().ForMember(p => p.File, opt => opt.Ignore());
                cfg.CreateMap<BrandViewModel, BrandDTO>().ForMember(p => p.Models, opt => opt.Ignore());
            });
        }
    }
}