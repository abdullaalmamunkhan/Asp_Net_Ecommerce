using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services.IWebServices
{
    public interface ITextLogWrite
    {
        public void SaveLogInText(string Message, string Id, string Remarks, string StackTrace);
    }
}
