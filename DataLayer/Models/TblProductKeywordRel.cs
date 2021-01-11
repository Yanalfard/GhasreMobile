using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblProductKeywordRel
    {
        [Key]
        public int ProductKeywordRelId { get; set; }
        public int ProductId { get; set; }
        public int KeywordId { get; set; }

        [ForeignKey(nameof(KeywordId))]
        [InverseProperty(nameof(TblKeyword.TblProductKeywordRel))]
        public virtual TblKeyword Keyword { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblProductKeywordRel))]
        public virtual TblProduct Product { get; set; }
    }
}
