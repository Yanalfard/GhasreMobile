using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblProductCommentRel", Schema = "dbo")]
    public partial class TblProductCommentRel
    {
        [Key]
        public int ProductCommentRelId { get; set; }
        public int ProductId { get; set; }
        public int CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty(nameof(TblComment.TblProductCommentRel))]
        public virtual TblComment Comment { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblProductCommentRel))]
        public virtual TblProduct Product { get; set; }
    }
}
