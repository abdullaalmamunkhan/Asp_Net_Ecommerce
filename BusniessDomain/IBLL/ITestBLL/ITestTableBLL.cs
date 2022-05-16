using DbModelsCore.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace BusniessDomain.IBLL.ITestBLL
{
    public interface ITestTableBLL
    {
        public Task<APIResponseMessage> Delete(int id);
        public Task<APIResponseMessage> GetAll();
        public APIResponseMessage GetAllFromSP(int id);
        public Task<APIResponseMessage> Details(int id);
        public  Task<APIResponseMessage> SaveUpdate(DBTestTableModel model);
    }
}
