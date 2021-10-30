using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblProductPropertyRel", Schema = "dbo")]
    public partial class TblProductPropertyRel
    {
        [Key]
        public int ProductPropertyRelId { get; set; }
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
        [Required]
        [StringLength(150)]
        public string Value { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblProductPropertyRel))]
        public virtual TblProduct Product { get; set; }
        [ForeignKey(nameof(PropertyId))]
        [InverseProperty(nameof(TblProperty.TblProductPropertyRel))]
        public virtual TblProperty Property { get; set; }
    }
}
