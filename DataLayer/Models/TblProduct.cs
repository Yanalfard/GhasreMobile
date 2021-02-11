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
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string MainImage { get; set; }
        public long PriceBeforeDiscount { get; set; }
        public string DescriptionShortHtml { get; set; }
        public string DescriptionLongHtml { get; set; }
        public int? CatagoryId { get; set; }
        public int Count { get; set; }
        public long PriceAfterDiscount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        [StringLength(500)]
        public string SearchText { get; set; }
        public bool IsFractional { get; set; }
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
