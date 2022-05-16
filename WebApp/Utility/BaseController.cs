using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Services.IWebServices;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Utility
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IAppAccessLog _appAccessLog;
        private IHttpContextAccessor _contextAccessor;
        private IUploaderHelper _uploaderHelper;

        protected IAppAccessLog AppAccessLog => _appAccessLog ?? (_appAccessLog = HttpContext?.RequestServices.GetService<IAppAccessLog>());
        protected IHttpContextAccessor ContextAccessor => _contextAccessor ?? (_contextAccessor = HttpContext?.RequestServices.GetService<IHttpContextAccessor>());
        protected IUploaderHelper UploaderHelper => _uploaderHelper ?? (_uploaderHelper = HttpContext?.RequestServices.GetService<IUploaderHelper>());


        protected int UserId = 0;
        protected string UserName = "";
        protected string UserFullName = "";
        protected string UserEmail = "";
        protected string MobilePhone = "";
        protected string BaseURL = "";

        protected void GetUserInformation()
        {
            if (ContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Authentication).Value);
                UserName = User.Claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Name).Value;
                UserFullName = User.Claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.GivenName).Value;
                UserEmail = User.Claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Email).Value;
            }
        }
    }
}
