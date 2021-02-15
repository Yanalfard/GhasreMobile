using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblSpecialOffer
    {
        [Key]
        public int SpecialOfferId { get; set; }
        public int ProductId { get; set; }
        public int Discount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidTill { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblSpecialOffer))]
        public virtual TblProduct Product { get; set; }
    }
}
