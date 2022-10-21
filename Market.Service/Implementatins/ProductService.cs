using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
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
        private readonly IBaseRepository<Product> productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IBaseResponse<ProductViewModel>> GetProductViewModel(int id)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var product = await productRepository.Get(id);

                if (product == null)
                {
                    baseResponse.Desciption = "Продукт не найден";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                }
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = new ProductViewModel()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    CategoryId = product.Category.Id,
                    ImgPath = product.ImgPath,
                };
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
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
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = product;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = productRepository.GetAll();
                if (products.Count() == 0)
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
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
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

        public async Task<IBaseResponse<bool>> AddProduct(ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = new Product()
                {
                    Description = productViewModel.Description,
                    Category = productViewModel.Category,
                    CategoryId = productViewModel.Category.Id,
                    Price = productViewModel.Price,
                    Name = productViewModel.Name
                };

                await productRepository.Create(product);

            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[CreateProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<Product>> EditProduct(int id, ProductViewModel model)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                    baseResponse.Desciption = "Продукт не найден";
                    return baseResponse;
                }

                product.Description = model.Description;
                product.Price = model.Price;
                product.Id = model.Id;
                product.Name = model.Name;
                product.Category = model.Category;
                product.CategoryId = model.Category.Id;

                await productRepository.Update(product);
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[EditProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }
    }
}
