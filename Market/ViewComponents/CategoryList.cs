using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Market.Service.Interfaces;

namespace Market.Views.Shared.Components
{
    public class CategoryListViewComponent : DropDownListViewComponent
    {
        public CategoryListViewComponent(ICategoryService categoryService) : base(categoryService)
        {
        }
    }
}
