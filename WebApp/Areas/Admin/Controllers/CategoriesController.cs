using BusniessDomain.IBLL.IEComBLL.Catalog;
using CommonProperties.BusinessMessage;
using CommonProperties.Enums;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CategoriesController : BaseController<CategoriesController>
    {
        private readonly ICategoryBLL _categoryBLL;
        public CategoriesController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }

        [HttpGet]
        [ActionName("all-categories")]
        public ActionResult AllCategories()
        {
            IEnumerable<VMCategoryList> listOfData = new List<VMCategoryList>();
            try
            {
                GetUserInformation();
                listOfData = _categoryBLL.GetAllCategories(UserId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this,  ex);
            }

            return Json(new { Data = listOfData });
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            int categoryId = 0;
            DBProductCategory category = new DBProductCategory();
            try
            {

                ViewBag.ListOfCategory = await _categoryBLL.GetCategoryDropdown();

                if (!string.IsNullOrEmpty(HttpContext.Request.Query["id"]))
                    categoryId = int.Parse(HttpContext.Request.Query["id"].ToString());

                if (categoryId > 0)
                    category = await _categoryBLL.GetById(categoryId);
            }
            catch (Exception ex)
            {

                throw;
            }

            return View("~/Areas/Admin/Views/Categories/Index.cshtml", category);
        }


        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DBProductCategory model)
        {
            try
            {
                GetUserInformation();
                model.CreatedBy = UserId;

                if (model.CategoryImage != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.CategoryImage);
                    if (imageURL != null)
                    {
                        model.ImageUrl = imageURL;
                    }
                }
                else
                {
                    model.ImageUrl = "/img/avatar-368.png";
                }

                var result = await _categoryBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "Categories", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "Categories", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "Categories", new { Area = "admin" });
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DBProductCategory model)
        {
            try
            {
                GetUserInformation();
                model.UpdatedBy = UserId;
                if (model.CategoryImage != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.CategoryImage);
                    if (imageURL != null)
                    {
                        model.ImageUrl = imageURL;
                    }
                }
                var result = await _categoryBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "Categories", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.Category.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "Categories", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "Categories", new { Area = "admin" });
        }


        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
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
