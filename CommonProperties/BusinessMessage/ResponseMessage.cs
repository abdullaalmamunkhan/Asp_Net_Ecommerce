﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProperties.BusinessMessage
{
    public static class ResponseMessage
    {
        public const string SuccessMessage = "Data successfully saved.";
        public const string EditMessage = "Data update successfully.";
        public const string DeleteMessage = "Data successfully deleted.";
        public const string FailMessage = "Data save failed.";
        public const string ExistingData = "Existing data found.";
        public const string RetrieveSuccess = "Data retrieved Successfully.";
        public const string FailRetrieve = "Data not found.";
        public const string TrainSuccess = "Image trained Successfully.";
        public const string TrainFail = "Image trained failed.";
        public const string Verification_Success = "Verification Success.";

        public const string Email_Notification = "Data sent to the email.";
        public const string LoginSuccessfull = "Log in successfully.";
        public const string LoginInvalidPassword = "Invalid Credential.";
        public const string AccountLocked = "Your account has been locked.";
        public const string AccountSuspend = "Your account has been suspend.";
        public const string ErrorMessage = "Error occured, Please contact your admin!";
    }
}
