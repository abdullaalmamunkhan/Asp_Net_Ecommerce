using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Admin
{
    public class DBUser: BaseClassInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual DBUserRole UserRole { get; set; }
        public virtual ICollection<DBUserInfo> UserInfos { get; set; }
    }
}
