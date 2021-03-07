using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class SendTicketVm
    {
        [Display(Name = "موضوع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(100)]
        public string Title { get; set; }
        [Display(Name = "متن ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(500)]
        public string Body { get; set; }
    }
}
