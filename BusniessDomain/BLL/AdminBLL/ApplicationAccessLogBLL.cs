using BusniessDomain.IBLL.IAdminBLL;
using DbAccessLayer.IRepo.IAdminRepo;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace BusniessDomain.BLL.AdminBLL
{
    public class ApplicationAccessLogBLL: IApplicationAccessLogBLL
    {
        private readonly IApplicationAccessLogRepo _applicationAccessLog;
        public ApplicationAccessLogBLL(IApplicationAccessLogRepo applicationAccessLog)
        {
            _applicationAccessLog = applicationAccessLog;
        }

        public async Task<ProcessResponse> Insert(DBApplicationAccessLog model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                await _applicationAccessLog.Add(model);
                await _applicationAccessLog.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
