using BusniessDomain.IBLL.IEComBLL.Catalog;
using CommonProperties.BusinessMessage;
using CommonProperties.Enums;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SliderImagesController : BaseController<SliderImagesController>
    {
        private readonly IDBSliderImageBLL _iDBSliderImageBLL;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderImagesController(IDBSliderImageBLL iDBSliderImageBLL, IWebHostEnvironment webHostEnvironment)
        {
            _iDBSliderImageBLL = iDBSliderImageBLL;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [ActionName("all-slider-image-list")]
        public ActionResult GetSliderImageInfo()
        {
            IEnumerable<VMSliderImageInfo> listOfData = new List<VMSliderImageInfo>();
            try
            {
                GetUserInformation();
                listOfData = _iDBSliderImageBLL.GetSliderImageInfo();
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }

        // GET: SliderImagesController
        public async Task<ActionResult> Index()
        {
            int imageId = 0;
            DBSliderImage dBSliderImage = new DBSliderImage();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["id"]))
                    imageId = int.Parse(HttpContext.Request.Query["id"].ToString());

                if (imageId > 0)
                    dBSliderImage = await _iDBSliderImageBLL.GetById(imageId);

            }
            catch (Exception ex)
            {
                throw;
            }

            return View(dBSliderImage);
        }

        // GET: SliderImagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SliderImagesController/Create
        public ActionResult Create()
        {
            DBSliderImage dBSliderImage = new DBSliderImage();
            dBSliderImage.ImageViewer = true;
            return View(dBSliderImage);
        }

        // POST: SliderImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DBSliderImage model)
        {
            try
            {
                GetUserInformation();
                model.CreatedBy = UserId;
                if (model.UploadImage != null)
                {
                    var userUrl = UploaderHelper.UploadSingleImageAndReturnUrl(model.UploadImage);
                    if (userUrl != null)
                    {
                        model.ImageUrl = userUrl;
                    }
                }
                else
                {
                    model.ImageUrl = "/img/avatar-368.png";
                }


                var result = await _iDBSliderImageBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.SliderImage.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "sliderimages", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.SliderImage.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "sliderimages", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "sliderimages", new { Area = "admin" });
        }


        // POST: SliderImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DBSliderImage model)
        {
            try
            {
                GetUserInformation();
                model.UpdatedBy = UserId;
                if (model.UploadImage != null)
                {
                    var userUrl = UploaderHelper.UploadSingleImageAndReturnUrl(model.UploadImage);
                    if (userUrl != null)
                    {
                        model.ImageUrl = userUrl;
                    }
                }

                var result = await _iDBSliderImageBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.SliderImage.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "sliderimages", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.SliderImage.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "sliderimages", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "sliderimages", new { Area = "admin" });
        }

        // GET: SliderImagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SliderImagesController/Delete/5
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
