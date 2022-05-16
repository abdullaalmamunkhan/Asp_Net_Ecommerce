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
    public class CategoryBLL: ICategoryBLL
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryBLL(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        public async Task<IEnumerable<DBProductCategory>> GetTopTweentyFourCategoryList()
        {
            IEnumerable<DBProductCategory> categories = new List<DBProductCategory>();
            try
            {
                categories = await _categoryRepo.GetAll(x => !x.IsDelete && x.IsShowOnHome);

            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }


        public IEnumerable<DBProductCategory> GetCategoryMenuList()
        {
            IEnumerable<DBProductCategory> categoryLists = new List<DBProductCategory>();
            try
            {
                categoryLists = _categoryRepo.GetInclude(x => !x.IsDelete && x.ParentId == null)
                    .Include(x => x.Categories)
                    .ThenInclude(sub => sub.Categories)
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return categoryLists;
        }

        public IEnumerable<VMCategoryList> GetAllCategories(long userId)
        {
            IEnumerable<VMCategoryList> categoryLists = new List<VMCategoryList>();
            try
            {
                categoryLists = _categoryRepo.GetAllCategories(userId).OrderBy(x => x.Name);
            }
            catch (Exception)
            {

                throw;
            }

            return categoryLists;
        }


        public IEnumerable<VMCheckBoxList> GetCategoryCheckBoxListForAdmin()
        {
            IEnumerable<VMCheckBoxList> categories = new List<VMCheckBoxList>();
            try
            {
                var listOfData = _categoryRepo.GetAllCategories(0);
                categories = listOfData.Select(x => new VMCheckBoxList
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IsCheck=true
                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }


        public async Task<IEnumerable<VMDropdownModel>> GetCategoryDropdown()
        {
            IEnumerable<VMDropdownModel> categories = new List<VMDropdownModel>();
            try
            {
                var listOfData = _categoryRepo.GetAllCategories(0);
                categories = listOfData.Select(x => new VMDropdownModel
                {
                    Id = x.Id,
                    Name = x.FullName
                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }

        public  async Task<IEnumerable<DBProductCategory>> GetAll()
        {
            IEnumerable<DBProductCategory> categories = new List<DBProductCategory>();
            try
            {
                categories = await _categoryRepo.GetAll(x => !x.IsDelete);
            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }

        public async Task<ProcessResponse> Insert(DBProductCategory model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var category = await _categoryRepo.Details(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim());
                if(category != null && category.ID > 0)
                {
                    response.IsExist = true;
                    return response;
                }
                await _categoryRepo.Add(model);
                await _categoryRepo.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<DBProductCategory> GetById(int id)
        {
            DBProductCategory category = new DBProductCategory();
            try
            {
                category = await _categoryRepo.Details(x => x.ID == id);
            }
            catch (Exception ex)
            {

                throw;
            }

            return category;
        }


        public async Task<ProcessResponse> Update(DBProductCategory model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                var category = await _categoryRepo.Details(x => x.ID == model.ID);

                if (category != null)
                {
                    category.Name = model.Name;
                    category.ParentId = model.ParentId;
                    category.Description = model.Description;
                    category.IsShowOnHome = model.IsShowOnHome;
                    category.ImageUrl = (!string.IsNullOrEmpty(model.ImageUrl))? model.ImageUrl: category.ImageUrl;

                    category.CreatedBy = (model.CreatedBy > 0) ? model.CreatedBy : category.CreatedBy;
                    category.UpdatedBy = (model.UpdatedBy != null) ? model.UpdatedBy : category.UpdatedBy;

                   _categoryRepo.Update(category);
                    await _categoryRepo.SaveChanges();
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
                var category = await _categoryRepo.Details(x => x.ID == id);

                if (category != null)
                {
                    category.IsDelete = true;
                    _categoryRepo.Update(category);
                    await _categoryRepo.SaveChanges();
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
