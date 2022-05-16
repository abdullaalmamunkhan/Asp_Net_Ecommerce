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
    public class AttributeitemsController : BaseController<AttributeitemsController>
    {
        private readonly IProductAttributeBLL _productAttributeBLL;
        private readonly IAttributeItemsBLL _attributeItemsBLL;
        public AttributeitemsController(IProductAttributeBLL productAttributeBLL, IAttributeItemsBLL attributeItemsBLL)
        {
            _productAttributeBLL = productAttributeBLL;
            _attributeItemsBLL = attributeItemsBLL;
        }

        [HttpGet]
        [ActionName("all-attribute-items")]
        public ActionResult AllCategories()
        {
            IEnumerable<VMAttributeItemList> listOfData = new List<VMAttributeItemList>();
            try
            {
                GetUserInformation();
                listOfData = _attributeItemsBLL.GetAllAttributeItemList();
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = listOfData });
        }

        // GET: AttributeitemsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AttributeitemsController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            DBProductAttributeItems attributeItems = new DBProductAttributeItems();
            try
            {

                ViewBag.ListOfProductAttribute = await _productAttributeBLL.GetProductAttributeDropdown();

                attributeItems = await _attributeItemsBLL.GetById(id);

            }
            catch (Exception ex)
            {
                throw;
            }

            return View(attributeItems);
        }

        // GET: AttributeitemsController/Create
        public async Task<ActionResult> Create()
        {
            DBProductAttributeItems attributeItems = new DBProductAttributeItems();
            try
            {

                ViewBag.ListOfProductAttribute = await _productAttributeBLL.GetProductAttributeDropdown();

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(attributeItems);
        }


        // POST: AttributeitemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DBProductAttributeItems model)
        {
            try
            {
                GetUserInformation();
                model.CreatedBy = UserId;

                var result = await _attributeItemsBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.AttributeItems.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "Attributeitems", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.AttributeItems.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "Attributeitems", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "Attributeitems", new { Area = "admin" });
        }

        // GET: AttributeitemsController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            DBProductAttributeItems attributeItems = new DBProductAttributeItems();
            try
            {

                ViewBag.ListOfProductAttribute = await _productAttributeBLL.GetProductAttributeDropdown();

                attributeItems = await _attributeItemsBLL.GetById(id);

            }
            catch (Exception ex)
            {
                throw;
            }

            return View(attributeItems);
        }

        // POST: AttributeitemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DBProductAttributeItems model)
        {
            try
            {
                GetUserInformation();
                model.UpdatedBy = UserId;

                var result = await _attributeItemsBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.AttributeItems.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);
                    return RedirectToAction("", "Attributeitems", new { Area = "admin" });
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.AttributeItems.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToAction("", "Attributeitems", new { Area = "admin" });

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToAction("", "Attributeitems", new { Area = "admin" });
        }



        // GET: AttributeitemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttributeitemsController/Delete/5
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
