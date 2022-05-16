using BusniessDomain.IBLL.IAdminBLL;
using CommonProperties.BusinessMessage;
using CommonProperties.Encription;
using CommonProperties.Enums;
using DbModelsCore.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ViewModelsCore.Admin;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMProduct;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserBLL _userBLL;
        public UsersController(IUserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        #region Super Admin Related Code
        public IActionResult SuperAdmins()
        {
            return View();
        }
        #endregion

        #region Admin Related Code
        public IActionResult Admins()
        {
            return View();
        }
        #endregion


        #region Vendor Related Code
        public IActionResult Vendors()
        {
            return View();
        }
        #endregion


        #region Customer Related Code
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Create Update Related Code
        // GET: UsersController/Create
        public async Task<ActionResult> Create()
        {
            int roleId = 4;
            VMProcessUser processUser = new VMProcessUser();
            processUser.User = new DBUser();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["role"]))
                    roleId = int.Parse(HttpContext.Request.Query["role"].ToString());

                processUser.User.RoleId = roleId;

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(processUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMProcessUser model)
        {
            int roleId = model.User.RoleId;
            try
            {


                GetUserInformation();
                model.User.CreatedBy = UserId;
                if (model.NIDFront != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.NIDFront);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.NIDImageFront = imageURL;
                    }
                }

                if (model.NIDBack != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.NIDBack);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.NIDImageBack = imageURL;
                    }
                }

                if (model.UserProfilePic != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.UserProfilePic);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.ProfileImage = imageURL;
                    }
                }


                var result = await _userBLL.Insert(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.User.ToString(), ApplicationActivity.Create.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.SuccessMessage);

                    return RedirectToURL(roleId);
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.SuccessMessage, ApplicationModules.User.ToString(), ApplicationActivity.Create.ToString());
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                return RedirectToURL(roleId);

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return RedirectToURL(roleId);
        }


        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            VMProcessUser processUser= new VMProcessUser();
         
            try
            {
                processUser = _userBLL.GetUserById(id);


                processUser.User.Password = (!string.IsNullOrEmpty(processUser.User.Password)) ? SimpleCryptService.Factory().Decrypt(processUser.User.Password) : processUser.User.Password;

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(processUser);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VMProcessUser model)
        {
            int roleId = model.User.RoleId;
            try
            {
                GetUserInformation();

                model.User.UpdatedBy = UserId;
                model.UserInfo.UpdatedBy = UserId;
                if (model.NIDFront != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.NIDFront);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.NIDImageFront = imageURL;
                    }
                }

                if (model.NIDBack != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.NIDBack);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.NIDImageBack = imageURL;
                    }
                }

                if (model.UserProfilePic != null)
                {
                    var imageURL = UploaderHelper.UploadSingleImageAndReturnUrl(model.UserProfilePic);
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        if (model.UserInfo != null)
                            model.UserInfo.ProfileImage = imageURL;
                    }
                }

                var result = await _userBLL.Update(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.User.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                    return RedirectToURL(roleId);
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.User.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                return RedirectToURL(roleId);

            }

            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToURL(roleId);
        }

        private ActionResult RedirectToURL(int roleId)
        {
            if (roleId == 1)
            {
                return RedirectToAction("SuperAdmins", "Users", new { Area = "Admin" });
            }
            else if (roleId == 2)
            {
                return RedirectToAction("Admins", "Users", new { Area = "Admin" });
            }
            else if (roleId == 3)
            {
                return RedirectToAction("Vendors", "Users", new { Area = "Admin" });
            }
            else
            {
                return RedirectToAction("", "Users", new { Area = "Admin" });
            }
        }

        #endregion

        #region Get User Data By ID
        [HttpGet]
        [ActionName("all-users")]
        public ActionResult GetAllUserList(int roleId)
        {
            IEnumerable<VMUserList> userLists = new List<VMUserList>();
            try
            {
                GetUserInformation();
                userLists = _userBLL.GetAllUserList(roleId);
            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }

            return Json(new { Data = userLists });
        }

        #endregion

        #region Edit Profile
        [ActionName("edit-profile")]
        public IActionResult EditProfile(long Id)
        {
            VMProcessUser processUser = new VMProcessUser();

            try
            {
                processUser = _userBLL.GetUserById(Id);


                processUser.User.Password = (!string.IsNullOrEmpty(processUser.User.Password)) ? SimpleCryptService.Factory().Decrypt(processUser.User.Password) : processUser.User.Password;

            }
            catch (Exception ex)
            {

                throw;
            }
           // return View(processUser);
            return View("~/Areas/Admin/Views/Users/EditProfile.cshtml", processUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(VMProcessUser model)
        {
            int roleId = model.User.RoleId;
            try
            {
                GetUserInformation();

                model.User.UpdatedBy = UserId;
              
                var result = await _userBLL.EditProfile(model);

                if (result.IsSuccess)
                {
                    AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.User.ToString(), ApplicationActivity.Update.ToString());
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                    return RedirectToURL(UserId);
                }

                AppAccessLog.SaveAccessLog(UserId, UserName, ResponseMessage.EditMessage, ApplicationModules.User.ToString(), ApplicationActivity.Update.ToString());
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.EditMessage);
                return RedirectToURL(UserId);

            }

            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
            }
            return RedirectToURL(UserId);
        }

        [HttpPost]
        public ActionResult UploadPasswordData(string password, string userId)
        {
            try {
                long Id = Convert.ToInt32(userId);
                bool result = _userBLL.UpdateUserPassword(Id, password);
                return Json(new { isValid = result, error = "" });
            }
            catch (Exception) {
                return Json(new { isValid = false, error = "" });
            }
        }
            #endregion


        }
}
