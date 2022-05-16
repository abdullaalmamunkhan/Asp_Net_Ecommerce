using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace BusniessDomain.BLL.EComBLL.Catalog
{
    public class ProductTagBLL: IProductTagBLL
    {
        private readonly IProductTagRepo _productTagRepo;
        public ProductTagBLL(IProductTagRepo productTagRepo)
        {
            _productTagRepo = productTagRepo;
        }


        public IEnumerable<VMProductTag> GetAllProductTags()
        {
            IEnumerable<VMProductTag> listOfData = new List<VMProductTag>();
            try
            {
                listOfData = _productTagRepo.GetAllProductTags();
            }
            catch (Exception)
            {

                throw;
            }

            return listOfData;
        }

        public async Task<IEnumerable<VMDropdownModel>> GetTagDropdown()
        {
            IEnumerable<VMDropdownModel> categories = new List<VMDropdownModel>();
            try
            {
                var listOfData = await _productTagRepo.GetAll(x => !x.IsDelete);
                categories = listOfData.Select(x => new VMDropdownModel
                {
                    Id = x.ID,
                    Name = x.Name
                });

            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }

        public  async Task<IEnumerable<DBProductTag>> GetAll()
        {
            IEnumerable<DBProductTag> productTags = new List<DBProductTag>();
            try
            {
                productTags = await _productTagRepo.GetAll(x => !x.IsDelete);
            }
            catch (Exception)
            {

                throw;
            }

            return productTags;
        }

        public async Task<ProcessResponse> Insert(DBProductTag model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var productTag = await _productTagRepo.Details(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim());
                if(productTag != null && productTag.ID > 0)
                {
                    response.IsExist = true;
                    return response;
                }
                await _productTagRepo.Add(model);
                await _productTagRepo.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<DBProductTag> GetById(int id)
        {
            DBProductTag productTag = new DBProductTag();
            try
            {
                productTag = await _productTagRepo.Details(x => x.ID == id);
            }
            catch (Exception ex)
            {

                throw;
            }

            return productTag;
        }


        public async Task<ProcessResponse> Update(DBProductTag model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var category = await _productTagRepo.Details(x => x.ID == model.ID);

                if (category != null)
                {
                    category.Name = model.Name;

                    category.CreatedBy = (model.CreatedBy > 0) ? model.CreatedBy : category.CreatedBy;
                    category.UpdatedBy = (model.UpdatedBy != null) ? model.UpdatedBy : category.UpdatedBy;

                    _productTagRepo.Update(category);
                    await _productTagRepo.SaveChanges();
                    response.IsSuccess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }



        public async Task<ProcessResponse> Delete(int id)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var productTag = await _productTagRepo.Details(x => x.ID == id);

                if (productTag != null)
                {
                    productTag.IsDelete = true;
                    _productTagRepo.Update(productTag);
                    await _productTagRepo.SaveChanges();
                    response.IsSuccess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
