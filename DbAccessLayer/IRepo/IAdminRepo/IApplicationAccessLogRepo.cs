using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.IRepo.IAdminRepo
{
    public interface IApplicationAccessLogRepo : IGenericRepo<DBApplicationAccessLog>
    {
    }
}
