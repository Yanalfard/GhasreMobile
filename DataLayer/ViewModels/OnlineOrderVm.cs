using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class OnlineOrderVm
    {
        [Display(Name = "موضوع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Title { get; set; }
        [Display(Name = "متن ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Body { get; set; }
    }
}
