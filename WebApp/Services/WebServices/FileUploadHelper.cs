using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Services.IWebServices;

namespace WebApp.Services.WebServices
{
    public class FileUploadHelper : IUploaderHelper
    {
        public static IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FileUploadHelper(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public string UploadSingleImageAndReturnUrl(IFormFile file)

        {
            var fileUrl = "";
            if (file != null)
            {
                string folderName = "Images";

                fileUrl = UploadFileAndGetUrl(file, folderName);
            }
            return fileUrl;
        }
        public string UploadFileAndGetUrl(IFormFile file, string directoryName)
        {
            string returnPath = "";
            try
            {
                int userId = int.Parse(this._httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Authentication).Value);
                //&& file.Length > 0
                if (file != null)
                {
                    string fileName = "";
                    string savingPath = (_env.WebRootPath + "\\uploads\\" + directoryName + "\\").Trim();
                    string dynamicName = DateTime.Now.ToString("ddMMyyhhmmssffff") + userId;

                    if (!Directory.Exists(savingPath))
                    {
                        Directory.CreateDirectory(savingPath);
                    }

                    //float fileSizeInKB = (file.ContentLength / 1024);
                    float fileSizeinMb = (file.Length / 1048576);
                    //file.Length > 0 && 
                    if (fileSizeinMb <= 5)
                    {
                        //Use Namespace called :  System.IO  
                        fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        //To Get File Extension  
                        string extension = Path.GetExtension(file.FileName);

                        //Add Current Date To Attached File Name  
                        fileName = dynamicName + "-" + fileName.Trim() + extension;

                        savingPath = savingPath + fileName;
                        returnPath = "/uploads/" + directoryName + "/" + fileName;


                        using (var fileSteam = new FileStream(savingPath, FileMode.Create))
                        {
                            file.CopyTo(fileSteam);
                        }

                    }
                }

                return returnPath;

            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                throw;
            }
        }

    }
}
