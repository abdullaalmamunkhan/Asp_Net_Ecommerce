using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents.Components
{
    [ViewComponent(Name = "CategoryThumb")]
    public class CategoryThumbViewComponent : ViewComponent
    {
        private readonly ICategoryBLL _categoryBLL;
        public CategoryThumbViewComponent(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<DBProductCategory> dBProductCategories = new List<DBProductCategory>();
            try
            {
                dBProductCategories = await _categoryBLL.GetTopTweentyFourCategoryList();

            }
            catch (Exception ex)
            {

                throw;
            }
            return View("~/ViewComponents/Views/CategoryThumbView.cshtml", dBProductCategories);
        }
    }
}
