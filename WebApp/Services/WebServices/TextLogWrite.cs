using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services.IWebServices;

namespace WebApp.Services.WebServices
{
    public class TextLogWrite : ITextLogWrite
    {
        public static IWebHostEnvironment _env;
        public TextLogWrite(IWebHostEnvironment env)
        {
            _env = env;
        }
        /// <summary>
        /// Save Log in Text File.
        /// </summary>
        /// <param name="Message">Exception Message</param>
        /// <param name="Id">KPI or MessageId or NotificationId (Optional)</param>
        /// <param name="Date">Exception Date</param>
        /// <param name="Remarks">Remarks is need</param>
        public void SaveLogInText(string Message, string Id, string Remarks, string StackTrace)
        {
            try
            {   //String strFilePath = System.IO.Path.GetFullPath(@"TextLog\\");
                //String strFilePath = "D:\\vFocus\\Development version 2\\vfocus-api Latest\\vfocus-api\\WebApi\\TextLog\\";
                //String strFilePath2 = AppDomain.CurrentDomain.GetData(@"TextLog\\").ToString();
                string strFilePath = (_env.WebRootPath + "\\TextLog\\").Trim();
                //string strFilePath = HttpContext.Current.Server.MapPath("~/TextLog/");
                DateTime Date = DateTime.Now;
                //depends on your application needs
                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                FileStream file;
                StreamWriter sw;
                if (!File.Exists(strFilePath + fileName))
                {
                    if (!Directory.Exists(strFilePath))
                    {
                        Directory.CreateDirectory(strFilePath);
                    }
                    try
                    {
                        file = new FileStream(strFilePath + fileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                        sw = new StreamWriter(file);

                        sw.WriteLine("=======  Exception : " + Date + "  =======");
                        sw.WriteLine("Id : " + Id);
                        sw.WriteLine("Remarks : " + Remarks);
                        sw.WriteLine("Error Message : " + Message);
                        sw.WriteLine("StackTrace : " + StackTrace);
                        sw.WriteLine("===================================================");
                        sw.WriteLine(Environment.NewLine);
                        sw.Close();
                        file.Close();
                    }
                    catch (Exception e) { }
                }
                else
                {
                    try
                    {
                        file = new FileStream(strFilePath + fileName, FileMode.Append, FileAccess.Write, FileShare.Read);
                        sw = new StreamWriter(file);

                        sw.WriteLine("=======  Exception : " + Date + "  =======");
                        sw.WriteLine("Id : " + Id);
                        sw.WriteLine("Remarks : " + Remarks);
                        sw.WriteLine("Error Message : " + Message);
                        sw.WriteLine("StackTrace : " + StackTrace);
                        sw.WriteLine("===================================================");
                        sw.WriteLine(Environment.NewLine);
                        sw.Close();
                        file.Close();
                    }
                    catch (Exception e) { }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
