using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GhasreMobile.Utilities
{
    public static class ImageValidator
    {
        public static bool IsImages(this IFormFile file)
        {
            try
            {
                var img = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
