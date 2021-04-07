using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class ConfigVm
    {
        [Required(ErrorMessage = "لطفا تماس با ما را کامل کنید")]
        public string TamasBaMa { get; set; }

        [Required(ErrorMessage = "لطفا تماس با ما کوتاه را کامل کنید")]
        public string ShortTamasBaMa { get; set; }

        [Required(ErrorMessage = "لطفا درباره ما را کامل کنید")]
        public string DarbareyeMa { get; set; }

        [Required(ErrorMessage = "لطفا توضیحات نمایندگی را کامل کنید")]
        public string StoreDescription { get; set; }

        [Required(ErrorMessage = "لطفا سقف هزینه ارسال رایگان را وارد کنید")]
        public string SagfePost { get; set; }

        [Required(ErrorMessage = "لطفا متن خرید نهایی را وارد کنید")]
        public string FinalTextKharid { get; set; }

        [Required(ErrorMessage = "لطفا درباره ما کوتاه را وارد کنید")]
        public string ShortDarbareyeMa { get; set; }

        [Required(ErrorMessage = "لطفا لینک اینستا را وارد کنید")]
        public string LinkInsta { get; set; }

        [Required(ErrorMessage = "لطفا لینک تلگرم را وارد کنید")]
        public string LinkTelegram { get; set; }

        [Required(ErrorMessage = "لطفا لینک تلگرم را وارد کنید")]
        public string LinkEmail { get; set; }

        [Required(ErrorMessage = "لطفا لینک تلگرم را وارد کنید")]
        public string Linkwhatsapp { get; set; }

        [Required(ErrorMessage = "لطفا قوانین را وارد کنید")]
        public string Gavanin { get; set; }
        
        [Required(ErrorMessage = "لطفا توضیح خرید اقساطی را وارد کنید")]
        public string KharidAgsady { get; set; }
        
        [Required(ErrorMessage = "لطفا توضیح خرید اقساطی را وارد کنید")]
        public string SharayeteAghsati { get; set; }

    }
}
