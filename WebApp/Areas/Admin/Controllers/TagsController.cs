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
using ViewModelsCore.VMEcom.VMList;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class TagsController : BaseController<CategoriesController>
    {
        private readonly IProductTagBLL _productTagBLL;
        public TagsController(IProductTagBLL productTagBLL)
        {
            _productTagBLL = productTagBLL;
        }

        [HttpGet]
        [ActionName("all-product-tags")]
        public ActionResult AllProductTags()
        {
            IEnumerable<VMProductTag> listOfData = new List<VMProductTag>();
            try
            {
                GetUserInformation();
                listOfData = _productTagBLL.GetAllProductTags();
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }
        // GET: TagsController
        public async Task<ActionResult> Index()
        {
            int tagId = 0;
            DBProductTag productTag = new DBProductTag();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["id"]))
                    tagId = int.Parse(HttpContext.Request.Query["id"].ToString());

                if (tagId > 0)
                    productTag = await _productTagBLL.GetById(tagId);

            }
            catch (Exception ex)
            {
                throw;
            }

            return View(productTag);

        }

       
        // POST: TagsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DBProductTag model)
        {
            try
            {
                GetUserInformation();
                model.CreatedBy = UserId;

                var result = await _productTagBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "tags", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "tags", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "tags", new { Area = "admin" });
        }

      
        // POST: TagsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DBProductTag model)
        {
            try
            {
                GetUserInformation();
                model.UpdatedBy = UserId;

                var result = await _productTagBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "tags", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "tags", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "tags", new { Area = "admin" });
        }

        // GET: TagsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TagsController/Delete/5
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
