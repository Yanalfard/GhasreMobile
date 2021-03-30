using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class CreateColorVm
    {
        public int id { get; set; }
        [Required(ErrorMessage ="نام را وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage ="کد رنگ را وارد کنید")]
        public string Code { get; set; }
        [Required(ErrorMessage ="تعداد را وارد کنید")]
        public int count { get; set; }
    }
}
