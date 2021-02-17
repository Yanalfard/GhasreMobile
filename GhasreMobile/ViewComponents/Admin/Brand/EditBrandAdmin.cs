using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Catagory
{
    public class EditBrandAdmin:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Brand/Components/Edit.cshtml"));
        }
    }
}
