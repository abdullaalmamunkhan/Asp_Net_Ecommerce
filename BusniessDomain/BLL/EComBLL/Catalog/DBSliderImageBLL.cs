using BusniessDomain.IBLL.IEComBLL.Catalog;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace BusniessDomain.BLL.EComBLL.Catalog
{
    public class DBSliderImageBLL : IDBSliderImageBLL
    {
        private readonly IDBSliderImageRepo _iDBSliderImageRepo;
        public DBSliderImageBLL(IDBSliderImageRepo iDBSliderImageRepo)
        {
            _iDBSliderImageRepo = iDBSliderImageRepo;
        }


        public async Task<IEnumerable<DBSliderImage>> GetAllActiveSliders()
        {
            IEnumerable<DBSliderImage> sliderImages = new List<DBSliderImage>();
            try
            {
                sliderImages = await _iDBSliderImageRepo.GetAll(x => !x.IsDelete && x.ImageViewer);
            }
            catch (Exception ex)
            {

                throw;
            }

            return sliderImages;
        }


        public async Task<DBSliderImage> GetById(int id)
        {
            DBSliderImage dBSliderImage = new DBSliderImage();
            try
            {
                dBSliderImage = await _iDBSliderImageRepo.Details(x => x.ID == id);
            }
            catch (Exception ex)
            {

                throw;
            }

            return dBSliderImage;
        }
        public async Task<ProcessResponse> Insert(DBSliderImage model)
        {
            ProcessResponse response = new ProcessResponse();
            try
            {
                //var productTag = await _iDBSliderImageRepo.Details(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim());
                //if (productTag != null && productTag.ID > 0)
                //{
                //    response.IsExist = true;
                //    return response;
                //}

                model.ImageName = (model.UploadImage != null) ? Path.GetFileNameWithoutExtension(model.UploadImage.FileName) : "avatar-368";
                model.CreatedDate = DateTime.Now;
                model.IsDelete = false;
                await _iDBSliderImageRepo.Add(model);
                await _iDBSliderImageRepo.SaveChanges();
                response.IsSuccess = true;
                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public async Task<ProcessResponse> Update(DBSliderImage model)
        {

            ProcessResponse response = new ProcessResponse();
            try
            {
                var sliderImage = await _iDBSliderImageRepo.Details(x => x.ID == model.ID);

                if (sliderImage != null)
                {
                    sliderImage.ImageName = (model.UploadImage != null) ? Path.GetFileNameWithoutExtension(model.UploadImage.FileName) : sliderImage.ImageName;
                    sliderImage.ImageUrl = (!string.IsNullOrEmpty(model.ImageUrl)) ? model.ImageUrl : sliderImage.ImageUrl;
                    sliderImage.ImageTitle = model.ImageTitle;
                    sliderImage.ImageViewer = model.ImageViewer;
                    sliderImage.ColorCode = model.ColorCode;
                    sliderImage.CreatedBy = (model.CreatedBy > 0) ? model.CreatedBy : sliderImage.CreatedBy;
                    sliderImage.UpdatedBy = (model.UpdatedBy != null) ? model.UpdatedBy : sliderImage.UpdatedBy;
                    sliderImage.UpdatedDate = DateTime.Now;
                    sliderImage.IsDelete = false;

                    _iDBSliderImageRepo.Update(sliderImage);
                    await _iDBSliderImageRepo.SaveChanges();
                    response.IsSuccess = true;
                    return response;

                    /*
                 
        
        public string ImageUrl { get; set; }
        [NotMapped]
        public virtual IFormFile UploadImage { get; set; }
        public bool ImageViewer { get; set; }
                     */
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public IEnumerable<VMSliderImageInfo> GetSliderImageInfo()
        {
            IEnumerable<VMSliderImageInfo> listOfData = new List<VMSliderImageInfo>();
            try
            {
                listOfData = _iDBSliderImageRepo.GetSliderImageInfo();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listOfData;
        }
    }
}
