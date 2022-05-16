using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services.IWebServices
{
    public interface IAppAccessLog
    {
        public void SaveAccessLog(long userId, string loginName, string activityName, string moduleName, string activityType);
        public void SaveAndViewException(Controller controller, Exception exception);
    }
}
