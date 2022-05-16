using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents.Components
{

    [ViewComponent(Name = "ProductSidebarAttribute")]
    public class ProductSidebarAttributeViewComponent : ViewComponent
    {
        private readonly IProductAttributeBLL _productAttributeBLL;
        public ProductSidebarAttributeViewComponent(IProductAttributeBLL productAttributeBLL)
        {
            _productAttributeBLL = productAttributeBLL;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? id)
        {
            long categoryId = 0;
            IEnumerable<DBProductAttribute> productAttributes = new List<DBProductAttribute>();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["cat"]))
                    categoryId = int.Parse(HttpContext.Request.Query["cat"].ToString());


                productAttributes = await _productAttributeBLL.GetProductAttributesList(categoryId);

            }
            catch (Exception ex)
            {

                throw;
            }
            return View("~/ViewComponents/Views/ProductSidebarAttributeView.cshtml", productAttributes);
        }

    }
}
