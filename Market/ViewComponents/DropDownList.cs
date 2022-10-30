using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Market.Service.Interfaces;

namespace Market.Views.Shared.Components
{
    public class DropDownListViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;
        public DropDownListViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await categoryService.GetCategories();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                // ViewBag.NameDropDownList = new SelectList(response.Data, "Name", "Name");

                ViewBag.NameDropDownList = response.Data.ToList();

                return View();
            }
            return View();
        }



    }
}
