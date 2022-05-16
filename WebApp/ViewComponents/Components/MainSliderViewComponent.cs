using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents.Components
{
    [ViewComponent(Name = "MainSlider")]
    public class MainSliderViewComponent: ViewComponent
    {
        private readonly IDBSliderImageBLL _iDBSliderImageBLL;
        public MainSliderViewComponent(IDBSliderImageBLL iDBSliderImageBLL)
        {
            _iDBSliderImageBLL = iDBSliderImageBLL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<DBSliderImage> sliderImages = new List<DBSliderImage>();
            try
            {
                sliderImages = await  _iDBSliderImageBLL.GetAllActiveSliders();

            }
            catch (Exception ex)
            {

                throw;
            }
            return View("~/ViewComponents/Views/MainSliderView.cshtml", sliderImages);
        }
    }
}
