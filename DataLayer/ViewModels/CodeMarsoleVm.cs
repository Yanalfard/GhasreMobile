using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class CodeMarsoleVm
    {
        public int OrdeId { get; set; }
        [StringLength(45)]
        [Display(Name = "کد مرسوله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SentNo { get; set; }
        public string ReturnUrl { get; set; }

    }
}
