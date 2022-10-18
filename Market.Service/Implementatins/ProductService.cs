using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Implementatins
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = await productRepository.GetAll();
                if (products.Count == 0)
                {
                    baseResponse.Desciption = "[GetProducrs] : Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetProducrs] : {ex.Message}";               
            }

            return baseResponse;
        }
    }
}
