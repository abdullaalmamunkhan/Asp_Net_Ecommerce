using BusniessDomain.IBLL.ITestBLL;
using CommonProperties.BusinessMessage;
using CommonProperties.BusinessMethods;
using CommonProperties.Enums;
using DbAccessLayer.IRepo.ITestRepo;
using DbModelsCore.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace BusniessDomain.BLL.TestBLL
{
    public class TestTableBLL : ITestTableBLL
    {
        private readonly ITestTableModelRepo _testTableModelRepo;
        public TestTableBLL(ITestTableModelRepo testTableModelRepo)
        {
            _testTableModelRepo = testTableModelRepo;
        }


        public async Task<APIResponseMessage> Delete(int id)
        {
            APIResponseMessage responseMessage = new APIResponseMessage();

            try
            {
                var testTable = await _testTableModelRepo.Details(x => x.ID == id);

                if (testTable != null && testTable.ID > 0)
                {
                    await _testTableModelRepo.Delete(testTable);
                    await _testTableModelRepo.SaveChanges();

                    return responseMessage = ResponseMapping.GetAPIResponseMessage(testTable, (int)ResponseStatus.Success, ResponseMessage.DeleteMessage);
                }  
                else
                    return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Success, ResponseMessage.FailRetrieve);
            }
            catch (Exception ex)
            {
                return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<APIResponseMessage> Details(int id)
        {
            APIResponseMessage responseMessage = new APIResponseMessage();

            try
            {
                var testTable = await _testTableModelRepo.Details(x => x.ID == id);

                if (testTable != null && testTable.ID > 0)
                    return responseMessage = ResponseMapping.GetAPIResponseMessage(testTable, (int)ResponseStatus.Success, ResponseMessage.RetrieveSuccess);
                else
                    return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Success, ResponseMessage.FailRetrieve);
            }
            catch (Exception ex)
            {
                return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<APIResponseMessage> GetAll()
        {
            APIResponseMessage responseMessage = new APIResponseMessage();

            try
            {
                var listOfTestData = await _testTableModelRepo.GetAll();
                return responseMessage = ResponseMapping.GetAPIResponseMessage(listOfTestData, (int)ResponseStatus.Success, ResponseMessage.RetrieveSuccess);
            }
            catch (Exception ex)
            {
                return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public APIResponseMessage GetAllFromSP(int id)
        {
            APIResponseMessage responseMessage = new APIResponseMessage();

            try
            {
                var listOfTestData = _testTableModelRepo.GetAllFromSP(id);
                return responseMessage = ResponseMapping.GetAPIResponseMessage(listOfTestData, (int)ResponseStatus.Success, ResponseMessage.RetrieveSuccess);
            }
            catch (Exception ex)
            {
                return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<APIResponseMessage> SaveUpdate(DBTestTableModel model)
        {
            APIResponseMessage responseMessage = new APIResponseMessage();
            long result = 0;
            try
            {
                if (model.ID > 0)
                {
                    var tableRecord = await _testTableModelRepo.Details(x => x.ID == model.ID);

                    if (tableRecord != null)
                    {
                        /*
                        * Will be use auto mapper
                        */
                        tableRecord = MapTestTableModel(tableRecord, model);
                    }
                    _testTableModelRepo.Update(tableRecord);
                    await _testTableModelRepo.SaveChanges();

                    result = tableRecord.ID;
                }
                else
                {
                    /*
                     * Will be use auto mapper
                     */
                    var tableRecord = MapTestTableModel(new DBTestTableModel(), model);
                    await _testTableModelRepo.Add(model);
                    await _testTableModelRepo.SaveChanges();

                    result = tableRecord.ID;
                }

                return responseMessage = ResponseMapping.GetAPIResponseMessage(result, (int)ResponseStatus.Success, ResponseMessage.SuccessMessage);

            }
            catch (Exception ex)
            {
                return responseMessage = ResponseMapping.GetAPIResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        private DBTestTableModel MapTestTableModel(DBTestTableModel table, DBTestTableModel model)
        {
            try
            {
                table.ID = model.ID;
                table.Name = (!string.IsNullOrEmpty(model.Name)) ? model.Name : table.Name;
            }
            catch (Exception ex)
            {

                throw;
            }

            return table;
        }

    }
}
