using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblColor
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        [StringLength(150)]
        [MaxLength(100,ErrorMessage ="نام معتبر وارد کنید")]
        [MinLength(1,ErrorMessage ="نام معتبر وارد کنید")]
        public string Name { get; set; }
        [StringLength(7)]
        [MaxLength(10,ErrorMessage ="لطفا رنگ مورد نظر را انتخاب کنید")]
        public string ColorCode { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblColor))]
        public virtual TblProduct Product { get; set; }
    }
}
