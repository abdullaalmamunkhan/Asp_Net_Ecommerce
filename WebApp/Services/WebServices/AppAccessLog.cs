using BusniessDomain.IBLL.IAdminBLL;
using DbModelsCore.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Services.IWebServices;

namespace WebApp.Services.WebServices
{
    public class AppAccessLog: IAppAccessLog
    {
        private readonly IApplicationAccessLogBLL _applicationAccessLogBLL;
        private ITextLogWrite _writeError;
        public AppAccessLog(IApplicationAccessLogBLL applicationAccessLogBLL, ITextLogWrite textLogWrite)
        {
            _applicationAccessLogBLL = applicationAccessLogBLL;
            _writeError = textLogWrite;
        }

        public void SaveAccessLog(long userId, string loginName, string activityName, string moduleName, string activityType)
        {
            try
            {

                DBApplicationAccessLog accesslog = new DBApplicationAccessLog()
                {

                    UserId = userId,
                    UserName = loginName,
                    ModuleName = moduleName,
                    ActivityName = activityName,
                    UserIpHostName = ApplicationHelper.GetBrowserIpAddress(),
                    ActionType = activityType
                };

                _applicationAccessLogBLL.Insert(accesslog);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveAndViewException(Controller controller, Exception exception)
        {
            _writeError.SaveLogInText(exception.Message, exception.GetHashCode().ToString(), exception.Source, exception.StackTrace);

        }

    }
}
