using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class ActiveVm:CaptchaVm
    {
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(5)]
        public string Auth { get; set; }
        public string Tell { get; set; }
    }
}
