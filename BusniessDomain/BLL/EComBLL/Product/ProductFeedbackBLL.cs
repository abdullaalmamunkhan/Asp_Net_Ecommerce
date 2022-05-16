using BusniessDomain.IBLL.IEComBLL.Product;
using DbAccessLayer.IRepo.IEComRepo.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusniessDomain.BLL.EComBLL.Product
{
    public class ProductFeedbackBLL : IProductFeedbackBLL
    {
        private readonly IProductFeedbackRepo _productFeedbackRepo;

        public ProductFeedbackBLL(IProductFeedbackRepo productFeedbackRepo)
        {
            _productFeedbackRepo = productFeedbackRepo;
        }

    }
}
