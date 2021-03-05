using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblBankAccounts
    {
        [Key]
        public int BankAccountId { get; set; }
        [Required(ErrorMessage = "شماره کارت اجباری است")]
        [MaxLength(16, ErrorMessage = "لطفا کارت 16 رقمی را وارد کنید")]
        [MinLength(16, ErrorMessage = "لطفا کارت 16 رقمی را وارد کنید")]
        public string AccountNo { get; set; }
        [Required(ErrorMessage = "لطفا نام دارنده حساب را وارد کنید")]
        [MaxLength(15, ErrorMessage = "لطفا نام معتبر وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage ="لطفا نام بانک را وارد نمایید")]
        [MaxLength(100,ErrorMessage ="لطفا نام بانک معتبری وارد کنید")]
        public string BankName { get; set; }

        public bool IsActive { get; set; }
    }
}
