using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Business;
using Core.DataAccess;
using Core.UnitOfWork;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using DataAccess.EntityFramework;
using DataAccess.UnitOfWork;
using System;
using System.Reflection;
using Module = Autofac.Module;

namespace WebAPI.DependencyResolvers.Autofac
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfEntityRepositoryBase<>)).As(typeof(IEntityRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
            .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            builder.RegisterType<RegisterModelRepository>().As<IRegisterModelRepository>();
            builder.RegisterType<RegisterModelService>().As<IRegisterModelService>();

            builder.RegisterType<WalletRepository>().As<IWalletRepository>();
            builder.RegisterType<WalletService>().As<IWalletService>();

            builder.RegisterType<ShoppingCartRepository>().As<IShoppingCartRepository>();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderService>().As<IOrderService>();

            builder.RegisterType<CartsProductRepository>().As<ICartsProductRepository>();
            builder.RegisterType<CartsProductService>().As<ICartsProductService>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(CoinoCaseDbContext));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
