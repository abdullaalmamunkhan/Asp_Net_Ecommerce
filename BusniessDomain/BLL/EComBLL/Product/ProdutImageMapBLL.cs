using BusniessDomain.IBLL.IEComBLL.Product;
using DbAccessLayer.IRepo.IEComRepo.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusniessDomain.BLL.EComBLL.Product
{
    public class ProdutImageMapBLL : IProdutImageMapBLL
    {
        private readonly IProdutImageMapRepo _produtImageMapRepo;
        public ProdutImageMapBLL(IProdutImageMapRepo produtImageMapRepo)
        {
            _produtImageMapRepo = produtImageMapRepo;
        }

    }
}
