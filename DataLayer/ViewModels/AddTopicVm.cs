using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class AddTopicVm
    {
        [Display(Name = "موضوع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200)]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        public string Title { get; set; }
        [Display(Name = "متن ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000)]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        public string Body { get; set; }
    }
}
