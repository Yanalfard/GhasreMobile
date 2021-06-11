using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblWallet = new HashSet<TblWallet>();
        }

        [Key]
        public int OrdeId { get; set; }
        public int? DiscountId { get; set; }
        public int ClientId { get; set; }
        public bool IsPayed { get; set; }
        public int FinalPrice { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; }
        public int Status { get; set; }
        public int SendStatus { get; set; }
        public int? SendPrice { get; set; }
        public int PaymentStatus { get; set; }
        [StringLength(50)]
        public string SentNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateSubmited { get; set; }
        public bool? IsFractional { get; set; }
        public int? SentId { get; set; }
        public long? FractionalPartPayed { get; set; }
        [StringLength(15)]
        public string? IpV4 { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblOrder))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(DiscountId))]
        [InverseProperty(nameof(TblDiscount.TblOrder))]
        public virtual TblDiscount Discount { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<TblWallet> TblWallet { get; set; }

        [ForeignKey(nameof(SentId))]
        [InverseProperty(nameof(TblPostOption.TblOrder))]
        public virtual TblPostOption Sent { get; set; }
        [InverseProperty("FinalOrder")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }


    }
}
