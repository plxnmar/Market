﻿using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetProducts();
        Task<IBaseResponse<ProductViewModel>> GetProductViewModel(int id);
        Task<IBaseResponse<Product>> GetProduct(int id);
        Task<IBaseResponse<IEnumerable<Product>>> GetCategoryProducts(int id);
        Task<IBaseResponse<Product>> DeleteProduct(int id);
        Task<IBaseResponse<bool>> AddProduct(ProductViewModel productViewModel);
        Task<IBaseResponse<Product>> EditProduct(int id, ProductViewModel model);
    }
}
