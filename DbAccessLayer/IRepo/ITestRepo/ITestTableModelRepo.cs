using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Test;

namespace DbAccessLayer.IRepo.ITestRepo
{
    public interface ITestTableModelRepo : IGenericRepo<DBTestTableModel>
    {
        public List<VMTestTableList> GetAllFromSP(int id);
    }
}
