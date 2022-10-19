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
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IBaseResponse<Product>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.Desciption = "Продукт не найден";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                }
                baseResponse.Data = product;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetProducr] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
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
                }

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = products;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetProducrs] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = await productRepository.Get(id);
                baseResponse.Data = true;

                if (product == null)
                {
                    baseResponse.Desciption = "Продукт не найден";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                    baseResponse.Data = false;
                }

                await productRepository.Remove(product);
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[DeleteProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        public async Task<IBaseResponse<bool>> CreateProduct(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
