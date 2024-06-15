using AutoMapper;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using System.Reflection;

namespace Library.Extensions.ExtensionServices
{
    public static class AutoMapperExtensions
    {
        public static void CreateMapByNamingConvention(this IMapperConfigurationExpression configuration)
        {
            var assembly = Assembly.Load("Domain");
            var assembly2 = Assembly.Load("Infrastructure");
            var types = assembly2.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .ToList();
            
            foreach (var type in types)
            {
                var dtoType = assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(t => t.Name == type.Name + "Dto");
                if (dtoType != null)
                {
                    configuration.CreateMap(dtoType, type);
                    configuration.CreateMap(type, dtoType);
                }
            }
            //configuration.CreateMap<Order, OrderDto>();
            //configuration.CreateMap<OrderDto, Order>();
            //configuration.CreateMap<OrderDetail, OrderDetailDto>();
            //configuration.CreateMap<OrderDetailDto, OrderDetail>();
            //configuration.CreateMap<ProductImg, ImageDto>();
            //configuration.CreateMap<ImageDto, ProductImg>();
            //configuration.CreateMap(assembly2.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(x=>x.Name=="Product"),
            //    assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(t => t.Name == "GetProductDto"));
            //configuration.CreateMap(assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(t => t.Name == "GetProductDto"),
            //    assembly2.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(x => x.Name == "Product"));
        }
    }
}
