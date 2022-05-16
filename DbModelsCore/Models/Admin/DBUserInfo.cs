using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Admin
{
   public class DBUserInfo : BaseClassInfo
    {
        public string PermanentAddress { get; set; }
        public string PermanentApartment { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public string PermanentCountry { get; set; }
        public string PermanentPostalCode { get; set; }
        public string TemporaryAddress { get; set; }
        public string TemporaryApartment { get; set; }
        public string TemporaryCity { get; set; }
        public string TemporaryState { get; set; }
        public string TemporaryCountry { get; set; }
        public string TemporaryPostalCode { get; set; }
        public string NID { get; set; }
        public string NIDImageFront { get; set; }
        public string NIDImageBack { get; set; }
        public string ProfileImage { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual DBUser User { get; set; }
    }
}
