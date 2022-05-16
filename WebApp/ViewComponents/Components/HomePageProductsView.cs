using BusniessDomain.IBLL.IEComBLL.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace WebApp.ViewComponents.Components
{
    [ViewComponent(Name = "HomePageProducts")]
    public class HomePageProductsView : ViewComponent
    {
        private readonly IProdcutBLL _prodcutBLL;
        public HomePageProductsView(IProdcutBLL prodcutBLL)
        {
            _prodcutBLL = prodcutBLL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<VMProductFilterList> productFilterLists = new List<VMProductFilterList>();
            try
            {
                productFilterLists = _prodcutBLL.GetHomePageProducts();

            }
            catch (Exception ex)
            {

                throw;
            }
            return View("~/ViewComponents/Views/HomePageProductView.cshtml", productFilterLists);
        }
    }
}
