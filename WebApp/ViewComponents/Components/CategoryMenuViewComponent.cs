using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents.Components
{
    [ViewComponent(Name = "CategoryMenu")]
    public class CategoryMenuViewComponent: ViewComponent
    {
        private readonly ICategoryBLL _categoryBLL;
        public CategoryMenuViewComponent(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<DBProductCategory> dBProductCategories = new List<DBProductCategory>();
            try
            {
                dBProductCategories = _categoryBLL.GetCategoryMenuList();

            }
            catch (Exception ex)
            {

                throw;
            }
            return View("~/ViewComponents/Views/CategoryMenuView.cshtml", dBProductCategories);
        }
    }
}
