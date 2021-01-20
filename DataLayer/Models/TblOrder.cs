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
        }

        [Key]
        public int OrdeId { get; set; }
        public int? DiscountId { get; set; }
        public bool? IsPayed { get; set; }
        public int FinalPrice { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; }
        public int Status { get; set; }
        public int SendStatus { get; set; }

        [ForeignKey(nameof(DiscountId))]
        [InverseProperty(nameof(TblDiscount.TblOrder))]
        public virtual TblDiscount Discount { get; set; }
        [InverseProperty("FinalOrder")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
    }
}
