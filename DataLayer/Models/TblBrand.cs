using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblBrand
    {
        public TblBrand()
        {
            TblProduct = new HashSet<TblProduct>();
        }

        [Key]
        public int BrandId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string MainImage { get; set; }

        [InverseProperty("Brand")]
        public virtual ICollection<TblProduct> TblProduct { get; set; }
    }
}
