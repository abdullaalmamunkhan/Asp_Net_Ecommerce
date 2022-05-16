using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Admin
{
    public class DBApplicationAccessLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long UserId { get; set; }
        [StringLength(200)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(2000)]
        public string ModuleName { get; set; }
        [StringLength(2000)]
        public string ActivityName { get; set; }
        [StringLength(2000)]
        public string UserIpHostName { get; set; }
        [StringLength(2000)]
        public string ActionType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? AccessDateTime { get; set; }
    }
}
