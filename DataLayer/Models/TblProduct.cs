using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblAlertWhenReady = new HashSet<TblAlertWhenReady>();
            TblBookMark = new HashSet<TblBookMark>();
            TblColor = new HashSet<TblColor>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblProductCommentRel = new HashSet<TblProductCommentRel>();
            TblProductImageRel = new HashSet<TblProductImageRel>();
            TblProductKeywordRel = new HashSet<TblProductKeywordRel>();
            TblProductPropertyRel = new HashSet<TblProductPropertyRel>();
            TblRate = new HashSet<TblRate>();
            TblSpecialOffer = new HashSet<TblSpecialOffer>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "نام محصول را وارد کنید")]
        [MaxLength(100, ErrorMessage = "نام محصول را معتبر وارد کنید")]
        [MinLength(3, ErrorMessage = "نام محصول را معتبر وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "تصویر محصول را وارد کنید")]
        public string MainImage { get; set; }
        [Required(ErrorMessage = "لطفا قیمت محصول را وارد کنید")]
        public long? PriceBeforeDiscount { get; set; }
        [Required(ErrorMessage = "لطفا توضیحات کوتاه محصول را وارد کنید")]
        [MaxLength(600, ErrorMessage = "نهایتا 600 کارکتر وارد کنید")]
        public string DescriptionShortHtml { get; set; }
        [Required(ErrorMessage = "لطفا توضیحات کامل محصول را وارد کنید")]
        public string DescriptionLongHtml { get; set; }
        [Required(ErrorMessage = "دسته بندی را انتخاب کنید")]
        public int? CatagoryId { get; set; }
        public long? PriceAfterDiscount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        [Required(ErrorMessage = "متن جستجو را وارد کنید")]
        public string SearchText { get; set; }
        public bool IsFractional { get; set; }
        [Required(ErrorMessage = "برند را انتخاب کنید")]
        public int BrandId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(BrandId))]
        [InverseProperty(nameof(TblBrand.TblProduct))]
        public virtual TblBrand Brand { get; set; }
        [ForeignKey(nameof(CatagoryId))]
        [InverseProperty(nameof(TblCatagory.TblProduct))]
        public virtual TblCatagory Catagory { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblAlertWhenReady> TblAlertWhenReady { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblBookMark> TblBookMark { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblColor> TblColor { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblProductCommentRel> TblProductCommentRel { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblProductImageRel> TblProductImageRel { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblProductKeywordRel> TblProductKeywordRel { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblProductPropertyRel> TblProductPropertyRel { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblRate> TblRate { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TblSpecialOffer> TblSpecialOffer { get; set; }
    }
}
