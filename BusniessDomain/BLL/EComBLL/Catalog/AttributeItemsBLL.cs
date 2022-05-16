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

namespace BusniessDomain.BLL.EComBLL.Catalog
{
    public class AttributeItemsBLL : IAttributeItemsBLL
    {
        private readonly IAttributeItemsRepo _attributeItemsRepo;
        private readonly IAttributeItemCategoryRepo _attributeItemCategoryRepo;
        public AttributeItemsBLL(IAttributeItemsRepo attributeItemsRepo, IAttributeItemCategoryRepo attributeItemCategoryRepo)
        {
            _attributeItemsRepo = attributeItemsRepo;
            _attributeItemCategoryRepo = attributeItemCategoryRepo;
        }


        public async Task<IEnumerable<VMDropdownModel>> GetAttributeItemsByAttributeId(long attributeId)
        {
            IEnumerable<VMDropdownModel> dropdownModelList = new List<VMDropdownModel>();
            try
            {
                var listOfData = await _attributeItemsRepo.GetAll(x => x.ProductAttributeId== attributeId);
                dropdownModelList = listOfData.Select(x => new VMDropdownModel
                {
                    Id = x.ID,
                    Name = x.Name
                });

            }
            catch (Exception)
            {

                throw;
            }

            return dropdownModelList;
        }


        public IEnumerable<VMAttributeItemList> GetAllAttributeItemList()
        {
            IEnumerable<VMAttributeItemList> listOfData = new List<VMAttributeItemList>();
            try
            {
                listOfData = _attributeItemsRepo.GetAllAttributeItemList();
            }
            catch (Exception)
            {

                throw;
            }

            return listOfData;
        }

        public async Task<IEnumerable<VMDropdownModel>> GetAttributeItemDropdown()
        {
            IEnumerable<VMDropdownModel> dropdownModelList = new List<VMDropdownModel>();
            try
            {
                var listOfData = await _attributeItemsRepo.GetAll(x => !x.IsDelete);
                dropdownModelList = listOfData.Select(x => new VMDropdownModel
                {
                    Id = x.ID,
                    Name = x.Name
                });

            }
            catch (Exception)
            {

                throw;
            }

            return dropdownModelList;
        }

        public async Task<IEnumerable<DBProductAttributeItems>> GetAll()
        {
            IEnumerable<DBProductAttributeItems> attributeItems = new List<DBProductAttributeItems>();
            try
            {
                attributeItems = await _attributeItemsRepo.GetAll(x => !x.IsDelete);
            }
            catch (Exception)
            {

                throw;
            }

            return attributeItems;
        }

        public async Task<ProcessResponse> Insert(DBProductAttributeItems model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var attributeItems = await _attributeItemsRepo.Details(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim() && x.ProductAttributeId == model.ProductAttributeId);
                if (attributeItems != null && attributeItems.ID > 0)
                {
                    response.IsExist = true;
                    return response;
                }

                List<DBAttributeItemCategoryMap> attributeItemCategoryMaps = new List<DBAttributeItemCategoryMap>();

                if (!string.IsNullOrEmpty(model.Categories))
                {
                    var splitArray = model.Categories.Split(",");

                    foreach (var item in splitArray)
                    {
                        DBAttributeItemCategoryMap dBAttribute = new DBAttributeItemCategoryMap();
                        dBAttribute.CategoryId = long.Parse(item);
                        dBAttribute.ID = 0;
                        dBAttribute.ItemAttributeId = model.ID;

                        attributeItemCategoryMaps.Add(dBAttribute);
                    }
                }

                model.AttributeItemCategories = attributeItemCategoryMaps;

                await _attributeItemsRepo.Add(model);
                await _attributeItemsRepo.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<DBProductAttributeItems> GetById(long id)
        {
            DBProductAttributeItems productTag = new DBProductAttributeItems();
            try
            {
                productTag = _attributeItemsRepo.GetInclude(x => x.ID == id).Include(x => x.AttributeItemCategories).FirstOrDefault();
                productTag.Categories= string.Join(",", productTag.AttributeItemCategories.Select(x => x.CategoryId).ToList());
            }
            catch (Exception ex)
            {

                throw;
            }

            return productTag;
        }


        public async Task<ProcessResponse> Update(DBProductAttributeItems model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var othersId = await _attributeItemsRepo.IsExist(x => x.ID != model.ID && (x.Name.ToLower().Trim() == model.Name.ToLower().Trim() && x.ProductAttributeId == model.ProductAttributeId));
                if (othersId)
                {
                    response.IsExist = true;
                    return response;
                }


                var dBAttributeItemCategoryMaps = await _attributeItemCategoryRepo.GetAll(x => x.ItemAttributeId == model.ID);
                if (dBAttributeItemCategoryMaps != null)
                {
                    foreach (var item in dBAttributeItemCategoryMaps)
                    {
                        await _attributeItemCategoryRepo.Delete(item);
                    }
                    await _attributeItemCategoryRepo.SaveChanges();
                }


                var attributeItems = await _attributeItemsRepo.GetInclude(x => x.ID == model.ID)
                    .Include(x => x.AttributeItemCategories).FirstOrDefaultAsync();

                if (attributeItems != null)
                {

                    attributeItems.Name = model.Name;
                    attributeItems.ProductAttributeId = model.ProductAttributeId;

                    attributeItems.CreatedBy = (model.CreatedBy > 0) ? model.CreatedBy : attributeItems.CreatedBy;
                    attributeItems.UpdatedBy = (model.UpdatedBy != null) ? model.UpdatedBy : attributeItems.UpdatedBy;


                    List<DBAttributeItemCategoryMap> attributeItemCategoryMaps = new List<DBAttributeItemCategoryMap>();

                    if (!string.IsNullOrEmpty(model.Categories))
                    {
                        var splitArray = model.Categories.Split(",");

                        foreach (var item in splitArray)
                        {
                            DBAttributeItemCategoryMap dBAttribute = new DBAttributeItemCategoryMap();
                            dBAttribute.CategoryId = long.Parse(item);
                            dBAttribute.ID = 0;
                            dBAttribute.ItemAttributeId = model.ID;

                            attributeItemCategoryMaps.Add(dBAttribute);
                        }
                    }

                    attributeItems.AttributeItemCategories = attributeItemCategoryMaps;

                    _attributeItemsRepo.Update(attributeItems);
                    await _attributeItemsRepo.SaveChanges();
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
                var attributeItems = await _attributeItemsRepo.Details(x => x.ID == id);

                if (attributeItems != null)
                {
                    attributeItems.IsDelete = true;
                    _attributeItemsRepo.Update(attributeItems);
                    await _attributeItemsRepo.SaveChanges();
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
