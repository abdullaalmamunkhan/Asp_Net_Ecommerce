using BusniessDomain.IBLL.IEComBLL.Product;
using DbAccessLayer.IRepo.IEComRepo.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusniessDomain.BLL.EComBLL.Product
{
    public class ProductAttributeItemMapBLL : IProductAttributeItemMapBLL
    {
        private readonly IProductAttributeItemMapRepo _productAttributeItemMapRepo;
        public ProductAttributeItemMapBLL(IProductAttributeItemMapRepo productAttributeItemMapRepo)
        {
            _productAttributeItemMapRepo = productAttributeItemMapRepo;
        }
    }
}
