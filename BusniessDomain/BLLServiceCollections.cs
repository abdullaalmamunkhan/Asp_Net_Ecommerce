using BusniessDomain.BLL.AdminBLL;
using BusniessDomain.BLL.EComBLL.Catalog;
using BusniessDomain.BLL.EComBLL.Product;
using BusniessDomain.BLL.TestBLL;
using BusniessDomain.IBLL.IAdminBLL;
using BusniessDomain.IBLL.IEComBLL.Catalog;
using BusniessDomain.IBLL.IEComBLL.Product;
using BusniessDomain.IBLL.ITestBLL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusniessDomain
{
    public static class BLLServiceCollections
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            /*
             * Admin
             */
            services.AddTransient<IUserBLL, UserBLL>();
            services.AddTransient<IAccountsBLL, AccountsBLL>();
            services.AddTransient<IApplicationAccessLogBLL, ApplicationAccessLogBLL>();



            services.AddTransient<ITestTableBLL, TestTableBLL>();



            #region "Ecommerce ============================"


            #region"Catalog"
            services.AddTransient<ICategoryBLL, CategoryBLL>();
            services.AddTransient<IProductTagBLL, ProductTagBLL>();
            services.AddTransient<IProductAttributeBLL, ProductAttributeBLL>();
            services.AddTransient<IAttributeItemsBLL, AttributeItemsBLL>();
            services.AddTransient<IDBSliderImageBLL, DBSliderImageBLL>();
            #endregion

            #region "Product "
            services.AddTransient<IProdcutBLL, ProdcutBLL>();
            #endregion


            #endregion
            return services;
        }
    }
}
