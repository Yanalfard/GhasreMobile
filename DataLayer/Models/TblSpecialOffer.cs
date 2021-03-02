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
        [Required(ErrorMessage ="لطفا کد تخفیف را وارد کنید")]
        [Range(1, 99, ErrorMessage = "از 1 تا 99 مجاز میباشد")]
        public int? Discount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidTill { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblSpecialOffer))]
        public virtual TblProduct Product { get; set; }
    }
}
