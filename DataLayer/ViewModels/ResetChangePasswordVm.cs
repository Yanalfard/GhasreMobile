using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class ResetChangePasswordVm
    {
        [Display(Name = "کد واژه قدیمی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [StringLength(25)]
        public string RePassword { get; set; }
    }
}
