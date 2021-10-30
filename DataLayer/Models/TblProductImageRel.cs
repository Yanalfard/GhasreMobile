using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblProductImageRel", Schema = "dbo")]
    public partial class TblProductImageRel
    {
        [Key]
        public int ProductImageRelId { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        [InverseProperty(nameof(TblImage.TblProductImageRel))]
        public virtual TblImage Image { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblProductImageRel))]
        public virtual TblProduct Product { get; set; }
    }
}
