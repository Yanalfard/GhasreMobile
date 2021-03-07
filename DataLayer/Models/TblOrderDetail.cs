using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblOrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }
        public int? FinalOrderId { get; set; }
        public int? ColorId { get; set; }

        [ForeignKey(nameof(FinalOrderId))]
        [InverseProperty(nameof(TblOrder.TblOrderDetail))]
        public virtual TblOrder Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblOrderDetail))]
        public virtual TblProduct Product { get; set; }
    }
}
