using BusniessDomain.IBLL.IEComBLL.Catalog;
using CommonProperties.BusinessMessage;
using CommonProperties.Enums;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMCatalog;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductAttributesController : BaseController<ProductAttributesController>
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IProductAttributeBLL _productAttributeBLL;
        public ProductAttributesController(ICategoryBLL categoryBLL, IProductAttributeBLL productAttributeBLL)
        {
            _categoryBLL = categoryBLL;
            _productAttributeBLL = productAttributeBLL;
        }

        [HttpGet]
        [ActionName("attribute-categories")]
        public async Task<ActionResult> AllProductAttributes(long attributeId)
        {
            IEnumerable<VMDropdownModel> listOfData = new List<VMDropdownModel>();
            try
            {

                listOfData = await _productAttributeBLL.GetProdcutAttributeCategoriesByAttributeId(attributeId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(listOfData);
        }


        [HttpGet]
        [ActionName("all-product-attributes")]
        public ActionResult AllProductAttributes()
        {
            IEnumerable<VMProductAttributeList> listOfData = new List<VMProductAttributeList>();
            try
            {

                listOfData = _productAttributeBLL.GetProductAttributeList();
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }

        public async Task<ActionResult> Details(long id)
        {
            VMProductAttributeProcess productAttribute = new VMProductAttributeProcess();
            try
            {

                productAttribute = await _productAttributeBLL.GetById(id);

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(productAttribute);
        }

        // GET: ProductAttributesController
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Create()
        {
            VMProductAttributeProcess productAttribute = new VMProductAttributeProcess();
            try
            {

                productAttribute.SelectedCategories = _categoryBLL.GetCategoryCheckBoxListForAdmin().ToList();

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(productAttribute);
        }

        // POST: ProductAttributesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMProductAttributeProcess model)
        {
            try
            {
                GetUserInformation();
                model.ProductAttribute.CreatedBy = UserId;

                var result = await _productAttributeBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.ProductAttributes.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "ProductAttributes", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.ProductAttributes.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "ProductAttributes", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "ProductAttributes", new { Area = "admin" });
        }


        public async Task<ActionResult> Edit(long id)
        {
            VMProductAttributeProcess productAttribute = new VMProductAttributeProcess();
            try
            {

                productAttribute = await _productAttributeBLL.GetById(id);

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(productAttribute);
        }

        // POST: ProductAttributesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VMProductAttributeProcess model)
        {
            try
            {
                GetUserInformation();
                model.ProductAttribute.UpdatedBy = UserId;

                var result = await _productAttributeBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "ProductAttributes", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "ProductAttributes", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "ProductAttributes", new { Area = "admin" });
        }

        // GET: ProductAttributesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductAttributesController/Delete/5
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
