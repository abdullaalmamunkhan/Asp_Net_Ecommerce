using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace CommonProperties.BusinessMethods
{
    public class ResponseMapping
    {
        public static APIResponseMessage GetAPIResponseMessage(object any, int statusCode, string messge)
        {
            APIResponseMessage responseMessage = new APIResponseMessage();
            responseMessage.data = any;
            responseMessage.statusCode = statusCode;
            responseMessage.message = messge;
            responseMessage.isSuccess = (statusCode == 1) ? true : false;

            return responseMessage;
        }
    }
}
