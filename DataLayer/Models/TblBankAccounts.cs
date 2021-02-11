using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class TblBankAccounts
    {
        [Key]
        public int BankAccountId { get; set; }
        [Required]
        [StringLength(19)]
        public string AccountNo { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
