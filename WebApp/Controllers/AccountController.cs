using BusniessDomain.IBLL.IAdminBLL;
using CommonProperties.BusinessMessage;
using CommonProperties.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModelsCore.Admin;
using WebApp.Helper;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountsBLL _accountsBLL;

        public AccountController(IAccountsBLL accountsBLL)
        {
            _accountsBLL = accountsBLL;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("", "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(VMLoginProcess model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountsBLL.Login(model);
                    if (result.IsSuccess)
                    {

                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.LoginSuccessfull);

                        AppAccessLog.SaveAccessLog(result.User.ID, result.User.Email, ResponseMessage.LoginSuccessfull, ApplicationModules.Login.ToString(), ApplicationActivity.Login.ToString());


                        var clims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Authentication, result.User.ID.ToString()),
                        new Claim(ClaimTypes.Name, (!string.IsNullOrEmpty(result.User.FirstName))?result.User.FirstName:""),
                        new Claim(ClaimTypes.GivenName, (!string.IsNullOrEmpty(result.User.FullName))?result.User.FullName:""),
                        new Claim(ClaimTypes.Email, (!string.IsNullOrEmpty(result.User.Email))?result.User.Email:"" ),
                        new Claim(ClaimTypes.Locality, DateTime.Now.ToString("dd-MMM-yyyy hh:mmtt")),
                        new Claim(ClaimTypes.Role, result.Role.ToLower()),
                    };


                        var claimsIdentity = new ClaimsIdentity(clims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            // Remember me
                            IsPersistent = true,

                            //Till
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(40)

                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);



                        // return RedirectToAction("", "dashboard", new { Area = "admin" });
                        return Redirect("~/admin/dashboard");
                    }
                    else if (result.IsInvalidCredential)
                    {
                        AppAccessLog.SaveAccessLog(0, model.Email + "(" + model.Password + ")", ResponseMessage.LoginInvalidPassword, ApplicationModules.Login.ToString(), ApplicationActivity.Login.ToString());
                        DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.LoginInvalidPassword);
                        return RedirectToAction("login", "account", new { Area = "" });
                    }

                    DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                    return RedirectToAction("login", "account", new { Area = "" });
                }

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this,  ex);
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ex.Message);
            }
            return RedirectToAction("login", "account", new { Area = "" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(VMRegistrationProcess model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountsBLL.Register(model);
                    if (result.IsSuccess)
                    {

                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ResponseMessage.LoginSuccessfull);

                        AppAccessLog.SaveAccessLog(result.User.ID, result.User.Email, ResponseMessage.LoginSuccessfull, ApplicationModules.Login.ToString(), ApplicationActivity.Login.ToString());


                        var clims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Authentication, result.User.ID.ToString()),
                        new Claim(ClaimTypes.Name, (!string.IsNullOrEmpty(result.User.FirstName))?result.User.FirstName:""),
                        new Claim(ClaimTypes.GivenName, (!string.IsNullOrEmpty(result.User.FullName))?result.User.FullName:""),
                        new Claim(ClaimTypes.Email, (!string.IsNullOrEmpty(result.User.Email))?result.User.Email:"" ),
                        new Claim(ClaimTypes.Locality, DateTime.Now.ToString("dd-MMM-yyyy hh:mmtt")),
                        new Claim(ClaimTypes.Role, result.Role.ToLower()),
                    };


                        var claimsIdentity = new ClaimsIdentity(clims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            // Remember me
                            IsPersistent = true,

                            //Till
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(40)

                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);


                        return Redirect("~/admin/dashboard");
                        //return RedirectToAction("", "dashboard", new { Area = "admin" });

                    }
                    else if (result.IsInvalidCredential)
                    {
                        AppAccessLog.SaveAccessLog(0, model.Email + "(" + model.Password + ")", ResponseMessage.LoginInvalidPassword, ApplicationModules.Login.ToString(), ApplicationActivity.Login.ToString());
                        DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.LoginInvalidPassword);
                        return RedirectToAction("Register", "account", new { Area = "" });
                    }

                    DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ResponseMessage.ErrorMessage);
                    return RedirectToAction("Register", "account", new { Area = "" });
                }

            }
            catch (Exception ex)
            {
                AppAccessLog.SaveAndViewException(this, ex);
                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ex.Message);
            }
            return RedirectToAction("Register", "account", new { Area = "" });
        }
    }
}
