using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblRate
    {
        [Key]
        public int RateId { get; set; }
        public double Rate { get; set; }
        public int ClientId { get; set; }
        public int? ProductId { get; set; }
        [Column("IP")]
        [StringLength(15)]
        public string Ip { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblRate))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblRate))]
        public virtual TblProduct Product { get; set; }
    }
}
