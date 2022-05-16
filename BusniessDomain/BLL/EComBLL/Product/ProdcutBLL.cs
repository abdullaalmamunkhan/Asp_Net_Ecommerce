using BusniessDomain.IBLL.IEComBLL.Product;
using DbAccessLayer.IRepo.IEComRepo.Product;
using DbModelsCore.Models.Ecommerce.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMProduct;

namespace BusniessDomain.BLL.EComBLL.Product
{
    public class ProdcutBLL : IProdcutBLL
    {
        private readonly IProductRepo _productRepo;
        private readonly IProductAttributeItemMapRepo _productAttributeItemMapRepo;
        private readonly IProductAttributeMapRepo _productAttributeMapRepo;
        private readonly IProductCategoryMapRepo _productCategoryMapRepo;
        private readonly IProductTagMapRepo _productTagMapRepo;
        private readonly IProdutImageMapRepo _produtImageMapRepo;
        public ProdcutBLL(IProductRepo productRepo, IProductAttributeItemMapRepo productAttributeItemMapRepo, IProductAttributeMapRepo productAttributeMapRepo,
            IProductCategoryMapRepo productCategoryMapRepo, IProductTagMapRepo productTagMapRepo, IProdutImageMapRepo produtImageMapRepo)
        {
            _productRepo = productRepo;
            _productAttributeItemMapRepo = productAttributeItemMapRepo;
            _productAttributeMapRepo = productAttributeMapRepo;
            _productCategoryMapRepo = productCategoryMapRepo;
            _productTagMapRepo = productTagMapRepo;
            _produtImageMapRepo = produtImageMapRepo;
        }

        public IEnumerable<VMProductFilterList> GetHomePageProducts()
        {
            IEnumerable<VMProductFilterList> productLists = new List<VMProductFilterList>();

            try
            {
                productLists = _productRepo.GetHomePageProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productLists;
        }

        public IEnumerable<VMProductFilterList> GetProductFilterLists(ProductFilterParam param)
        {
            IEnumerable<VMProductFilterList> productLists = new List<VMProductFilterList>();

            try
            {
                productLists = _productRepo.GetProductFilterLists(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productLists;
        }


        public IEnumerable<VMProductList> GetAllProductList(long userId)
        {
            IEnumerable<VMProductList> productLists = new List<VMProductList>();

            try
            {
                productLists = _productRepo.GetAllProductList(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productLists;
        }


        public VMProcessProduct GetProductById(long id)
        {
            VMProcessProduct processProduct = new VMProcessProduct();
            processProduct.Product = new DBProduct();
            processProduct.ProductAttributeItemMaps = new List<DBProductAttributeItemMap>();
            processProduct.ImageGalleryLists = new List<ImageGalleryList>();
            try
            {
                var product = _productRepo.GetInclude(x => x.ID == id)
                    .Include(x => x.ProductAttributeItemMaps)
                    .Include(x => x.ProductTagMaps)
                    .Include(x => x.DBProdutImageMaps)
                    .FirstOrDefault();

                processProduct.Product = product;
                processProduct.ProductAttributeItemMaps = product.ProductAttributeItemMaps.ToList();
                processProduct.SelectedTags = string.Join(",", product.ProductTagMaps.Select(x => x.TagId).ToList());
            }
            catch (Exception ex)
            {

                throw;
            }

            return processProduct;
        }

        public async Task<ProcessResponse> Insert(VMProcessProduct model)
        {
            ProcessResponse result = new ProcessResponse();
            try
            {
                var dBProducts = GetMapProducts(new DBProduct(), model);

                await _productRepo.Add(dBProducts);
                await _productRepo.SaveChanges();

                result.IsSuccess = true;

            }
            catch (Exception ex)
            {

                result.ErrorMessage = ex.HResult.ToString();
            }
            return result;
        }
        public async Task<ProcessResponse> Update(VMProcessProduct model)
        {
            ProcessResponse result = new ProcessResponse();
            try
            {
                var productAttributeItemList = await _productAttributeItemMapRepo.GetAll(x => x.ProductId == model.Product.ID);
                if (productAttributeItemList != null)
                {
                    foreach (var item in productAttributeItemList)
                    {
                        await _productAttributeItemMapRepo.Delete(item);
                    }
                    await _productAttributeItemMapRepo.SaveChanges();
                }

                var produtImageList = await _produtImageMapRepo.GetAll(x => x.ProductId == model.Product.ID);
                if (produtImageList != null)
                {

                    foreach (var item in produtImageList)
                    {
                        await _produtImageMapRepo.Delete(item);
                    }
                    await _produtImageMapRepo.SaveChanges();
                }
                    
                var productTagList = await _productTagMapRepo.GetAll(x => x.ProductId == model.Product.ID);
                if (productTagList != null)
                {
                    foreach (var item in productTagList)
                    {
                        await _productTagMapRepo.Delete(item);
                    }
                    await _productTagMapRepo.SaveChanges();
                }

                var oldDbRecord = await _productRepo.Details(x => x.ID == model.Product.ID);



                if (oldDbRecord != null)
                {

                    oldDbRecord = GetMapProducts(oldDbRecord, model);
                    oldDbRecord.UpdatedDate = DateTime.Now;
 
                    _productRepo.Update(oldDbRecord);
                    _productRepo.SaveChangesSync();
                    result.IsSuccess = true;
                    return result;
                }
            }
            catch (Exception ex)
            {

                result.ErrorMessage = ex.HResult.ToString();
            }
            return result;
        }
        private DBProduct GetMapProducts(DBProduct data, VMProcessProduct model)
        {
            try
            {
                data.ProductAttributeItemMaps = new List<DBProductAttributeItemMap>();
                data.DBProdutImageMaps = new List<DBProdutImageMap>();

                data.ID = model.Product.ID;
                data.Name = model.Product.Name;
                data.Slug = model.Product.Slug;
                data.ShortDescription = model.Product.ShortDescription;
                data.FullDescription = model.Product.FullDescription;

                data.SKU = model.Product.SKU;
                data.GTIN = model.Product.GTIN;
                data.AdminComment = model.Product.AdminComment;
                data.IsShowOnHome = model.Product.IsShowOnHome;
                data.IsOpenReview = model.Product.IsOpenReview;
                data.IsDraft = model.Product.IsDraft;
                data.OldPrice = model.Product.OldPrice;
                data.NewPrice = model.Product.NewPrice;
                data.DiscountInPercent = model.Product.DiscountInPercent;
                data.DiscountAmount = model.Product.DiscountAmount;
                data.IsEnableShop = model.Product.IsEnableShop;
                data.IsTrackStoke = model.Product.IsTrackStoke;
                data.StokeAmount = model.Product.StokeAmount;
                data.MinimumStokeLimit = model.Product.MinimumStokeLimit;
                data.IsMultipleWareHouse = model.Product.IsMultipleWareHouse;
                data.IsDisplayAvaiable = model.Product.IsDisplayAvaiable;
                data.IsReturnAble = model.Product.IsReturnAble;
                data.IsNew = model.Product.IsNew;
                data.CategoryId = model.Product.CategoryId;

                data.CreatedBy = (model.Product.CreatedBy > 0) ? model.Product.CreatedBy : data.CreatedBy;
                data.UpdatedBy = (model.Product.UpdatedBy != null) ? model.Product.UpdatedBy : data.UpdatedBy;

                if (model.ImageGalleryLists != null && model.ImageGalleryLists.Count > 0)
                    data.FeatureImage = model.ImageGalleryLists.Where(x => x.IsFetured == true).FirstOrDefault().ImageURL;

                //data.HSCodes = model.ListOfHSCode;


                List<DBProductAttributeItemMap> dBProductAttributeItemMaps = new List<DBProductAttributeItemMap>();

                if (model.ProductAttributeItemMaps != null)
                {
                    foreach (var item in model.ProductAttributeItemMaps)
                    {
                        item.ID = 0;
                        item.ProductId = data.ID;
                        dBProductAttributeItemMaps.Add(item);
                    }
                }
                data.ProductAttributeItemMaps = dBProductAttributeItemMaps;


                List<DBProdutImageMap> dBProdutImageMaps = new List<DBProdutImageMap>();


                if (model.ImageGalleryLists != null)
                {
                    var selectGelary = model.ImageGalleryLists.Where(x => x.IsGallery == true);
                    foreach (var item in selectGelary)
                    {
                        DBProdutImageMap dBProdutImageMap = new DBProdutImageMap();
                        dBProdutImageMap.ID = 0;
                        dBProdutImageMap.ProductId = data.ID;
                        dBProdutImageMap.ImageURL = item.ImageURL;
                        dBProdutImageMaps.Add(dBProdutImageMap);
                    }
                }
                data.DBProdutImageMaps = dBProdutImageMaps;


                List<DBProductTagMap> productTagMaps = new List<DBProductTagMap>();

                if (!string.IsNullOrEmpty(model.SelectedTags))
                {
                    var tags = model.SelectedTags.Split(",");

                    foreach (var tag in tags)
                    {
                        DBProductTagMap tagMap = new DBProductTagMap();
                        tagMap.TagId = long.Parse(tag);
                        tagMap.ID = 0;
                        tagMap.ProductId = data.ID;

                        productTagMaps.Add(tagMap);
                    }

                }

                data.ProductTagMaps = productTagMaps;



            }
            catch (Exception ex)
            {

                throw;
            }

            return data;
        }


    }
}
