using BusniessDomain.IBLL.IEComBLL.Catalog;
using BusniessDomain.IBLL.IEComBLL.Product;
using CommonProperties.BusinessMessage;
using CommonProperties.Enums;
using DbModelsCore.Models.Ecommerce.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMProduct;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductsController : BaseController<ProductsController>
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IProductAttributeBLL _productAttributeBLL;
        private readonly IAttributeItemsBLL _attributeItemsBLL;
        private readonly IProductTagBLL _productTagBLL;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProdcutBLL _prodcutBLL;
        public ProductsController(ICategoryBLL categoryBLL, IProductAttributeBLL productAttributeBLL,
            IAttributeItemsBLL attributeItemsBLL, IProdcutBLL prodcutBLL, IProductTagBLL productTagBLL, IWebHostEnvironment webHostEnvironment)
        {
            _categoryBLL = categoryBLL;
            _productAttributeBLL = productAttributeBLL;
            _attributeItemsBLL = attributeItemsBLL;
            _productTagBLL = productTagBLL;
            _webHostEnvironment = webHostEnvironment;
            _prodcutBLL = prodcutBLL;
        }



        [HttpGet]
        [ActionName("all-products")]
        public ActionResult GetAllProductList()
        {
            IEnumerable<VMProductList> productLists = new List<VMProductList>();
            try
            {
                GetUserInformation();
                productLists = _prodcutBLL.GetAllProductList(UserId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = productLists });
        }


        [HttpGet]
        [ActionName("attribute-list-by-category")]
        public ActionResult GetAllAttributeListByCategory(int categoryId)
        {
            IEnumerable<VMDropdownModel> listOfData = new List<VMDropdownModel>();
            try
            {
                GetUserInformation();
                listOfData = _productAttributeBLL.GetAllAttributeListByCategory(categoryId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }


        [HttpGet]
        [ActionName("items-list-by-attribute")]
        public async Task<ActionResult> GetAllAttributeListByCategory(long attributeId)
        {
            IEnumerable<VMDropdownModel> listOfData = new List<VMDropdownModel>();
            try
            {
                GetUserInformation();
                listOfData = await _attributeItemsBLL.GetAttributeItemsByAttributeId(attributeId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            VMProcessProduct processProduct = new VMProcessProduct();
            processProduct.ProductAttributeItemMaps = new List<DBProductAttributeItemMap>()
            {
                new DBProductAttributeItemMap()
            };
            processProduct.ImageGalleryLists = new List<ImageGalleryList>();
            try
            {
                ViewBag.ListOfCategory = await _categoryBLL.GetCategoryDropdown();
                ViewBag.ListOfProductTags = await _productTagBLL.GetTagDropdown();

                var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
                var contents = provider.GetDirectoryContents(Path.Combine("products"));
                var objFiles = contents.OrderBy(m => m.LastModified);

                foreach (var item in objFiles.ToList())
                {
                    string toBeSearched = "wwwroot";
                    ImageGalleryList galleryList = new ImageGalleryList();
                    string code = item.PhysicalPath.Substring(item.PhysicalPath.IndexOf(toBeSearched) + toBeSearched.Length);
                    code = code.Replace('\\', '/');
                    galleryList.ImageURL = code;
                    galleryList.ImageName = item.Name;
                    processProduct.ImageGalleryLists.Add(galleryList);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(processProduct);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMProcessProduct model)
        {
            try
            {
                GetUserInformation();
                model.Product.CreatedBy = UserId;

                var result = await _prodcutBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Products.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "products", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Products.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "products", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "products", new { Area = "admin" });
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            VMProcessProduct processProduct = new VMProcessProduct();
            /*processProduct.ProductAttributeItemMaps = new List<DBProductAttributeItemMap>()
            {
                new DBProductAttributeItemMap()
            };*/

            try
            {
                ViewBag.ListOfCategory = await _categoryBLL.GetCategoryDropdown();
                ViewBag.ListOfProductTags = await _productTagBLL.GetTagDropdown();


                processProduct = _prodcutBLL.GetProductById(id);
                processProduct.ImageGalleryLists = new List<ImageGalleryList>();

                var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
                var contents = provider.GetDirectoryContents(Path.Combine("products"));
                var objFiles = contents.OrderBy(m => m.LastModified);

                var existingGalleryList = processProduct.Product.DBProdutImageMaps.ToList();
                foreach (var item in objFiles.ToList())
                {
                    string toBeSearched = "wwwroot";
                    ImageGalleryList galleryList = new ImageGalleryList();
                    string code = item.PhysicalPath.Substring(item.PhysicalPath.IndexOf(toBeSearched) + toBeSearched.Length);
                    code = code.Replace('\\', '/');
                    galleryList.ImageURL = code;
                    galleryList.ImageName = item.Name;
                    if (existingGalleryList != null && existingGalleryList.Count > 0)
                        galleryList.IsGallery = existingGalleryList.Any(x => x.ImageURL.ToLower() == code.ToLower());

                    if (!string.IsNullOrEmpty(processProduct.Product.FeatureImage) && code.ToLower() == processProduct.Product.FeatureImage.ToLower())
                        galleryList.IsFetured = true;

                    processProduct.ImageGalleryLists.Add(galleryList);
                }

                if (!string.IsNullOrEmpty(processProduct.Product.FullDescription))
                    processProduct.Product.FullDescription = HttpUtility.HtmlDecode(processProduct.Product.FullDescription);

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(processProduct);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VMProcessProduct model)
        {
            try
            {
                GetUserInformation();


                model.Product.UpdatedBy = UserId;

                var result = await _prodcutBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.Products.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                    return RedirectToAction("", "products", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.Products.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                return RedirectToAction("", "products", new { Area = "admin" });

            }

            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "products", new { Area = "admin" });
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
