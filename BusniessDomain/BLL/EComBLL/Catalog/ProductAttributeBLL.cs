using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMCatalog;

namespace BusniessDomain.BLL.EComBLL.Catalog
{
    public class ProductAttributeBLL: IProductAttributeBLL
    {
        private readonly IProductAttributeRepo _productAttributeRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IDBCategoryAttributeMaperRepo _categoryAttributeMaperRepo;
        private readonly IAttributeItemsRepo _attributeItemsRepo;
        public ProductAttributeBLL(IProductAttributeRepo productAttributeRepo, ICategoryRepo categoryRepo,
            IDBCategoryAttributeMaperRepo categoryAttributeMaperRepo, IAttributeItemsRepo attributeItemsRepo)
        {
            _productAttributeRepo = productAttributeRepo;
            _categoryRepo = categoryRepo;
            _categoryAttributeMaperRepo = categoryAttributeMaperRepo;
            _attributeItemsRepo = attributeItemsRepo;
        }


        public async Task<IEnumerable<DBProductAttribute>> GetProductAttributesList(long categoryId)
        {
            IEnumerable<DBProductAttribute> productAttributes = new List<DBProductAttribute>();
            try
            {
                
                productAttributes = await _productAttributeRepo.GetInclude(x => !x.IsDelete && x.CategoryAttributeMapers.Any(c => c.CategoryId == categoryId))
                    .Include(x => x.AttributeItems).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return productAttributes;
        }

        public IEnumerable<VMDropdownModel> GetAllAttributeListByCategory(int categoryId)
        {
            IEnumerable<VMDropdownModel> listOfData = new List<VMDropdownModel>();
            try
            {
                listOfData = _productAttributeRepo.GetProductAttributeListsByCategory(categoryId);
            }
            catch (Exception)
            {

                throw;
            }

            return listOfData;
        }


        public IEnumerable<VMProductAttributeList> GetProductAttributeList()
        {
            IEnumerable<VMProductAttributeList> listOfData = new List<VMProductAttributeList>();
            try
            {
                listOfData = _productAttributeRepo.GetProductAttributeList();
            }
            catch (Exception)
            {

                throw;
            }

            return listOfData;
        }

        public async Task<IEnumerable<VMDropdownModel>> GetProductAttributeDropdown()
        {
            IEnumerable<VMDropdownModel> productAttributes = new List<VMDropdownModel>();
            try
            {
                var listOfData = await _productAttributeRepo.GetAll(x => !x.IsDelete);
                productAttributes = listOfData.Select(x => new VMDropdownModel
                {
                    Id = x.ID,
                    Name = x.Name
                });

            }
            catch (Exception)
            {

                throw;
            }

            return productAttributes;
        }

        public async Task<IEnumerable<DBProductAttribute>> GetAll()
        {
            IEnumerable<DBProductAttribute> listOfData = new List<DBProductAttribute>();
            try
            {
                listOfData = await _productAttributeRepo.GetAll(x => !x.IsDelete);
            }
            catch (Exception)
            {

                throw;
            }

            return listOfData;
        }


        private VMProductAttributeProcess MapProductAttributeViewModel(DBProductAttribute model)
        {
            VMProductAttributeProcess viewModel = new VMProductAttributeProcess();
            viewModel.SelectedCategories = new List<VMCheckBoxList>();
            viewModel.ProductAttribute = new DBProductAttribute();
            try
            {
                viewModel.ProductAttribute.ID = model.ID;
                viewModel.ProductAttribute.Name = model.Name;

                var existingCategory = model.CategoryAttributeMapers.ToList();

                var listOfData = _categoryRepo.GetAllCategories(0);
               var allCategories = listOfData.Select(x => new VMCheckBoxList
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IsCheck = existingCategory.Any( y => y.CategoryId == x.Id)
                }).ToList();

                viewModel.SelectedCategories = allCategories;

            }
            catch (Exception ex)
            {

                throw;
            }

            return viewModel;
        }

        private DBProductAttribute MapProductAttributeDB(DBProductAttribute model, VMProductAttributeProcess viewModel)
        {
            try
            {
                model.ID = viewModel.ProductAttribute.ID;
                model.Name = viewModel.ProductAttribute.Name;

                /*
                 * User Log
                 */
                model.CreatedBy = (viewModel.ProductAttribute.CreatedBy != null) ? viewModel.ProductAttribute.CreatedBy : model.CreatedBy;
                model.UpdatedBy = (viewModel.ProductAttribute.UpdatedBy != null) ? viewModel.ProductAttribute.UpdatedBy : model.UpdatedBy;

                IList<DBCategoryAttributeMaper> dBCategoryAttributes = new List<DBCategoryAttributeMaper>();

                if(viewModel.SelectedCategories != null)
                {
                    foreach (var item in viewModel.SelectedCategories)
                    {
                        if (item.IsCheck)
                        {
                            DBCategoryAttributeMaper categoryAttributeMaper = new DBCategoryAttributeMaper();
                            categoryAttributeMaper.CategoryId = item.Id;
                            categoryAttributeMaper.AttributeId = model.ID;
                            dBCategoryAttributes.Add(categoryAttributeMaper);
                        }
                    }
                }

                model.CategoryAttributeMapers = dBCategoryAttributes;

            }
            catch (Exception ex)
            {

                throw;
            }

            return model;
        }


        public async Task<IEnumerable<VMDropdownModel>> GetProdcutAttributeCategoriesByAttributeId(long attributeId)
        {
            IEnumerable<VMDropdownModel> dropdownModels = new List<VMDropdownModel>();
            try
            {
                var existingCategories = await _categoryAttributeMaperRepo.GetInclude(x => x.AttributeId == attributeId)
                    .Include(x => x.Category)
                    .ToListAsync();

                dropdownModels = existingCategories.Select(x => new VMDropdownModel
                {
                    Id = x.CategoryId,
                    Name = x.Category.Name
                }).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }

            return dropdownModels;
        }

        public async Task<ProcessResponse> Insert(VMProductAttributeProcess model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var oldRecord = await _productAttributeRepo.Details(x => x.Name.ToLower().Trim() == model.ProductAttribute.Name.ToLower().Trim());
                if (oldRecord != null && oldRecord.ID > 0)
                {
                    response.IsExist = true;
                    return response;
                }

                var newRecord = MapProductAttributeDB(new DBProductAttribute(), model);

                await _productAttributeRepo.Add(newRecord);
                await _productAttributeRepo.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<VMProductAttributeProcess> GetById(long id)
        {
            VMProductAttributeProcess viewModel = new VMProductAttributeProcess();
            DBProductAttribute productAttribute = new DBProductAttribute();
            try
            {
                productAttribute = await _productAttributeRepo.GetInclude(x => x.ID == id)
                    .Include(x => x.CategoryAttributeMapers).FirstOrDefaultAsync();
                viewModel = MapProductAttributeViewModel(productAttribute);
            }
            catch (Exception ex)
            {

                throw;
            }

            return viewModel;
        }


        public async Task<ProcessResponse> Update(VMProductAttributeProcess model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var existingCategories = await _categoryAttributeMaperRepo.GetInclude(x => x.AttributeId == model.ProductAttribute.ID).ToListAsync();


                if(existingCategories != null)
                {
                    foreach(var item in existingCategories)
                    {
                       await _categoryAttributeMaperRepo.Delete(item);
                    }

                    await _categoryAttributeMaperRepo.SaveChanges();
                }


                var oldRecord = await _productAttributeRepo.Details(x => x.ID == model.ProductAttribute.ID);

                if (oldRecord != null)
                {
                    oldRecord = MapProductAttributeDB(oldRecord, model);
                    _productAttributeRepo.Update(oldRecord);
                    await _productAttributeRepo.SaveChanges();
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
                var productAttribute = await _productAttributeRepo.Details(x => x.ID == id);

                if (productAttribute != null)
                {
                    productAttribute.IsDelete = true;
                    _productAttributeRepo.Update(productAttribute);
                    await _productAttributeRepo.SaveChanges();
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
