using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblWallet
    {
        [Key]
        public int WalletId { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public bool IsDeposit { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int ClientId { get; set; }
        public bool IsFinaly { get; set; }
        public int? OrderId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblWallet))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(TblOrder.TblWallet))]
        public virtual TblOrder Order { get; set; }
    }
}
