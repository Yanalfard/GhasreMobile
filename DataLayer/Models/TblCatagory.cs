using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            InverseParent = new HashSet<TblCatagory>();
            TblProduct = new HashSet<TblProduct>();
        }

        [Key]
        public int CatagoryId { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsOnFirstPage { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(TblCatagory.InverseParent))]
        public virtual TblCatagory Parent { get; set; }
        [InverseProperty(nameof(TblCatagory.Parent))]
        public virtual ICollection<TblCatagory> InverseParent { get; set; }
        [InverseProperty("Catagory")]
        public virtual ICollection<TblProduct> TblProduct { get; set; }
    }
}
