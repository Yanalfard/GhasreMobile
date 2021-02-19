using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Catagory
{
    public class EditBrandAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Brand/Components/Edit.cshtml", _core.Brand.GetById(Id)));
        }
    }
}
