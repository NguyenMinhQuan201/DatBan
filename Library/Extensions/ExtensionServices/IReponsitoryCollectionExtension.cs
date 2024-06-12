using Domain.Features;
using Infrastructure.Reponsitories.BlogRepository;
using Infrastructure.Reponsitories.CategoryReponsitories;
using Infrastructure.Reponsitories.DiscountReponsitories;
using Infrastructure.Reponsitories.ModuleReponsitories;
using Infrastructure.Reponsitories.OperationReponsitories;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using Infrastructure.Reponsitories.OrderTableRepository;
using Infrastructure.Reponsitories.RoleOperationReponsitories;
using Infrastructure.Reponsitories.RoleOperationRepository;
using Infrastructure.Reponsitories.TableReponsitory;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Extensions.ExtensionServices
{
    public static class IReponsitoryCollectionExtension
    {
        public static IServiceCollection AddReponsitories(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IOperationRepository, OperationReponsitories>();
            services.AddTransient<IRoleOperationRepository, RoleOperationReponsitories>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IOrderTableRepository, OrderTableRepository>();
            services.AddTransient<ITableRepository, TableRepository>();

            return services;

        }
    }
}
