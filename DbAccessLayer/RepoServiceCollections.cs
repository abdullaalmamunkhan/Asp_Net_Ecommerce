#region"Name spaces"

using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IAdminRepo;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbAccessLayer.IRepo.IEComRepo.Product;
using DbAccessLayer.IRepo.ITestRepo;
using DbAccessLayer.Repo.AdminRepo;
using DbAccessLayer.Repo.EComRepo.Catalog;
using DbAccessLayer.Repo.EComRepo.Product;
using DbAccessLayer.Repo.TestRepo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace DbAccessLayer
{
    public static class RepoServiceCollections
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            /*
             * Admin Repo
             */
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserInfoRepo, UserInfoRepo>();
            services.AddTransient<IApplicationAccessLogRepo, ApplicationAccessLogRepo>();

            /*
             * Test Repo
             */
            services.AddTransient<ITestTableModelRepo, TestTableModelRepo>();


            #region "Ecommerce ============================"

            #region"Catalog"
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IProductTagRepo, ProductTagRepo>();
            services.AddTransient<IProductAttributeRepo, ProductAttributeRepo>();
            services.AddTransient<IAttributeItemsRepo, AttributeItemsRepo>();
            services.AddTransient<IDBCategoryAttributeMaperRepo, DBCategoryAttributeMaperRepo>();
            services.AddTransient<IDBSliderImageRepo, DBSliderImageRepo>();
            services.AddTransient<IAttributeItemCategoryRepo, AttributeItemCategoryRepo>();
            #endregion

            #region "Product "
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IProductAttributeItemMapRepo, ProductAttributeItemMapRepo>();
            services.AddTransient<IProductAttributeMapRepo, ProductAttributeMapRepo>();
            services.AddTransient<IProductCategoryMapRepo, ProductCategoryMapRepo>();
            services.AddTransient<IProductTagMapRepo, ProductTagMapRepo>();
            services.AddTransient<IProdutImageMapRepo, ProdutImageMapRepo>();

            #endregion


            #endregion



            return services;
        }
    }
}
