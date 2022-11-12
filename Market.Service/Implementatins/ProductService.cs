using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Domain.ViewModels.ProductsCart;
using Market.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var product = await productRepository.Get(id);

                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "[GetProductViewModel]: Продукт не найден",
                        StatusCode = Domain.Enum.StatusCode.ProductNotFound,
                    };
                }

                return new BaseResponse<ProductViewModel>()
                {
                    Description = "[GetProductViewModel]: ProductViewModel создана",
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Data = new ProductViewModel()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Category = product.Category,
                        CategoryId = product.Category.Id,
                        ImgPath = product.ImgPath,
                        Id = product.Id,
                    },
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProductViewModel] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = productRepository.GetAll();
                if (products.Count() == 0)
                {
                    baseResponse.Description = "[GetProducts] : Найдено 0 элементов";
                }

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = products;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetProducts] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }

            return baseResponse;
        }


        public async Task<IBaseResponse<IEnumerable<Product>>> GetCategoryProducts(int id)
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = productRepository.GetAll().Where(x => x.CategoryId == id);
                if (products.Count() == 0)
                {
                    baseResponse.Description = "[GetCategoryProducts] : Найдено 0 элементов";
                }

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = products;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetCategoryProducts] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        public async Task<IBaseResponse<Product>> DeleteProduct(int id, string webRootPath)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await productRepository.Get(id);


                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "[DeleteProduct]: Продукт не найден",
                        StatusCode = Domain.Enum.StatusCode.ProductNotFound,
                    };
                }

                await DeleteImage(product.ImgPath, webRootPath);
                await productRepository.Remove(product);


                return new BaseResponse<Product>()
                {
                    Description = "[DeleteProduct]: Товар удален",
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Data = product,
                };
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[DeleteProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> AddProduct(ProductViewModel model, string webRootPath)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (model.UploadedImage != null)
                {
                    await SaveImage(model, webRootPath);
                }

                var product = new Product()
                {
                    Description = model.Description,
                    Category = model.Category,
                    CategoryId = model.Category.Id,
                    ImgPath = model.ImgPath,
                    Price = model.Price,
                    Name = model.Name
                };

   
                await productRepository.Create(product);
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Description = "[AddProduct]: Товар добавлен";
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[AddProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<Product>> EditProduct(int id, ProductViewModel model, string webRootPath)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                    baseResponse.Description = "[EditProduct]: Продукт не найден";
                    return baseResponse;
                }

                //удаление старого изображения товара и сохранение нового
                //установка нового пути изображения
                if (model.UploadedImage != null)
                {
                    if (product.ImgPath != null && product.ImgPath != "")
                    {
                        await DeleteImage(product.ImgPath, webRootPath);
                    }
                    await SaveImage(model, webRootPath);
                }

                product.Description = model.Description;
                product.Price = model.Price;
                product.Id = model.Id;
                product.Name = model.Name;
                product.Category = model.Category;

                if (model.UploadedImage != null)
                {
                    product.ImgPath = model.ImgPath;
                }

                product.CategoryId = model.Category.Id;

                await productRepository.Update(product);

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Description = "[EditProduct]: Товар изменен";
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[EditProduct] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        private async Task<bool> SaveImage(ProductViewModel model, string webRootPath)
        {
            //удаление старого изображения 
         //   DeleteImage(model.ImgPath, webRootPath);

            // путь к папке img в root
            string path = "/img/" + model.UploadedImage.FileName;

            // сохраняем файл в папку img в каталоге wwwroot
            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await model.UploadedImage.CopyToAsync(fileStream);
                model.ImgPath = path;
                return true;
            }
            return false;
        }

        private async Task<bool> DeleteImage(string path, string webRootPath)
        {
            var fullPath = webRootPath + path;

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return false;
        }

    }
}
