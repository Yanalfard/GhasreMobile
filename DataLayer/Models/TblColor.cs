using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblColor
    {
        public TblColor()
        {
            TblOrderDetail = new HashSet<TblOrderDetail>();
        }

        [Key]
        public int ColorId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(7)]
        public string ColorCode { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblColor))]
        public virtual TblProduct Product { get; set; }
        [InverseProperty("Color")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
    }
}
