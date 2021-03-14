using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class LoginVm: CaptchaVm
    {
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MinLength(11, ErrorMessage = "تعداد کاراکتر کم است")]
        //[MaxLength(11, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        //[StringLength(11)]
        public string TellNo { get; set; }
        [Display(Name = "کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
