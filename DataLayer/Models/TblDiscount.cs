using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblDiscount
    {
        public TblDiscount()
        {
            TblOrder = new HashSet<TblOrder>();
        }

        [Key]
        public int DiscountId { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTill { get; set; }

        [InverseProperty("Discount")]
        public virtual ICollection<TblOrder> TblOrder { get; set; }
    }
}
