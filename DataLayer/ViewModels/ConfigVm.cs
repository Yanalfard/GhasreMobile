using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class ConfigVm
    {
        [Required(ErrorMessage = "لطفا هزینه ارسال با پست را کامل کنید")]
        public int? HazineErsalBaPost { get; set; }

        [Required(ErrorMessage = "لطفا هزینه ارسال با تیپاکس را کامل کنید")]
        public int? HazineErsalBaTipaxChapare { get; set; }

        [Required(ErrorMessage = "لطفا تماس با ما را کامل کنید")]
        public string TamasBaMa { get; set; }

        [Required(ErrorMessage = "لطفا درباره ما را کامل کنید")]
        public string DarbareyeMa { get; set; }

        [Required(ErrorMessage = "لطفا هزینه ارسال با پیک را کامل کنید")]
        public int HazineErsalPeyk { get; set; }

        [Required(ErrorMessage = "لطفا توضیحات نمایندگی را کامل کنید")]
        public string StoreDescription { get; set; }

        [Required(ErrorMessage = "لطفا سقف هزینه ارسال رایگان را وارد کنید")]
        public string SagfePost { get; set; }

        [Required(ErrorMessage = "لطفا متن خرید نهایی را وارد کنید")]
        public string FinalTextKharid { get; set; }

        [Required(ErrorMessage = "لطفا درباره ما کوتاه را وارد کنید")]
        public string ShortDarbareyeMa { get; set; }
    }
}
