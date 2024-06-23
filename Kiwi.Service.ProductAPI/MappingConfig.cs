using AutoMapper;
using Kiwi.Service.ProductAPI.Models;
using Kiwi.Service.ProductAPI.Models.Dto;

namespace Kiwi.Service.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
