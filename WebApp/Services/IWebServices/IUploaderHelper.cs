using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services.IWebServices
{
    public interface IUploaderHelper
    {
        public string UploadSingleImageAndReturnUrl(IFormFile file);
    }
}
