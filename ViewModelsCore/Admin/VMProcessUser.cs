using DbModelsCore.Models.Admin;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.Admin
{
    public class VMProcessUser
    {
        public DBUser User { get; set; }
        public DBUserInfo UserInfo { get; set; }
        public virtual IFormFile NIDFront { get; set; }
        public virtual IFormFile NIDBack { get; set; }
        public virtual IFormFile UserProfilePic { get; set; }
    }
}
