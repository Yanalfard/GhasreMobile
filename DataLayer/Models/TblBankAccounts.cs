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
        public bool IsActive { get; set; }
    }
}
