using BusniessDomain.IBLL.IEComBLL.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;
using X.PagedList;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProdcutBLL _prodcutBLL;
        public ProductsController(IProdcutBLL prodcutBLL)
        {
            _prodcutBLL = prodcutBLL;
        }

        public IActionResult Index(int? page)
        {
            string categoryName = string.Empty;
            string itemIds = string.Empty;
            string mins = string.Empty;
            string maxs = string.Empty;
            string searchTtext = string.Empty;
            IEnumerable<VMProductFilterList> productLists = new List<VMProductFilterList>();
            long catId = 0;
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["name"]))
                    categoryName = HttpContext.Request.Query["name"].ToString();

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["s"]))
                    searchTtext = HttpContext.Request.Query["s"].ToString();

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["attr"]))
                    itemIds = HttpContext.Request.Query["attr"].ToString();

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["cat"]))
                    catId = long.Parse(HttpContext.Request.Query["cat"].ToString());

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["attr"]))
                    mins = HttpContext.Request.Query["attr"].ToString();

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["attr"]))
                    maxs = HttpContext.Request.Query["attr"].ToString();

                ViewBag.CategoryName = categoryName;
                ViewBag.CategoryId = catId;

                var pageNumber = page;
                int pageIndex = 1;
                int defaSize = 50;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                ProductFilterParam productFilterParam = new ProductFilterParam();
                productFilterParam.catId = catId;
                productFilterParam.itemId = itemIds.Replace(".", ",");
                productFilterParam.proId = string.Empty;
                productFilterParam.attId = string.Empty;
                productFilterParam.minValue = string.Empty;
                productFilterParam.maxValue = string.Empty;

                var productFilterLists = _prodcutBLL.GetProductFilterLists(productFilterParam);
                ViewBag.TotalProductCount = productFilterLists.Count();

                productLists = productFilterLists.ToPagedList(pageIndex, defaSize);
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(productLists);
        }



        [HttpGet]
        [ActionName("product-search-results")]
        public IActionResult ProductFilterResults(long catId, string itemIds, string searchString,string minValue,string maxValue)
        {
            string categoryName = string.Empty;
            string searchTtext = string.Empty;
            IEnumerable<VMProductFilterList> productLists = new List<VMProductFilterList>();
            
            try
            {

                ProductFilterParam productFilterParam = new ProductFilterParam();
                productFilterParam.catId = catId;
                productFilterParam.itemId = itemIds;
                productFilterParam.proId = string.Empty;
                productFilterParam.attId = string.Empty;
                if (minValue != null && maxValue != null) {
                    productFilterParam.minValue = minValue;
                    productFilterParam.maxValue = maxValue;
                }
                else {
                    productFilterParam.minValue = string.Empty;
                    productFilterParam.maxValue = string.Empty;
                }
                

                productFilterParam.searchstring = (string.IsNullOrEmpty(searchString))?"": searchString;

                productLists = _prodcutBLL.GetProductFilterLists(productFilterParam);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(new { Data = productLists });
        }


        public IActionResult Details(long id)
        {

            return View();
        }
    }
}
