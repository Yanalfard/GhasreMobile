using System;
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
        [Display(Name ="نام دسته بندی")]
        [Required(ErrorMessage = "نام دسته بندی اجباری میباشد")]
        [StringLength(256)]
        [MaxLength(50, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        [MinLength(5, ErrorMessage = "لطفا کارکتر های بیشتری وارد کنید")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [Display(Name ="نمایش در ثبت محصول")]
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
