using Market.DAL.Interfaces;
using Market.DAL.Repositories;
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
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> baseRepository;

        public CategoryService(IBaseRepository<Category> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Category>>> GetCategories()
        {
            var baseResponse = new BaseResponse<IEnumerable<Category>>();
            try
            {
                var categories = baseRepository.GetAll();
                if (categories.Count() == 0)
                {
                    baseResponse.Desciption = "[GetCategories] : Найдено 0 элементов";
                }
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = categories;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetCategories] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Category>> GetCategory(int id)
        {
            var baseResponse = new BaseResponse<Category>();
            try
            {
                var product = await baseRepository.Get(id);

                if (product == null)
                {
                    baseResponse.Desciption = "Категория не найдена";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.ProductNotFound;
                }
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = product;

            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetCategory] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }

            return baseResponse;
        }
    }
}
