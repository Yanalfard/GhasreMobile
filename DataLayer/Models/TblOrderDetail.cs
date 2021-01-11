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
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }
        public int? FinalOrderId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblOrderDetail))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(FinalOrderId))]
        [InverseProperty(nameof(TblOrder.TblOrderDetail))]
        public virtual TblOrder FinalOrder { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblOrderDetail))]
        public virtual TblProduct Product { get; set; }
    }
}
