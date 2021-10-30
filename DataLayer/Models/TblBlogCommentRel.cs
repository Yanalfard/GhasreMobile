using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblBlogCommentRel", Schema = "dbo")]
    public partial class TblBlogCommentRel
    {
        [Key]
        public int BlogCommentRelId { get; set; }
        public int BlogId { get; set; }
        public int CommentId { get; set; }

        [ForeignKey(nameof(BlogId))]
        [InverseProperty(nameof(TblBlog.TblBlogCommentRel))]
        public virtual TblBlog Blog { get; set; }
        [ForeignKey(nameof(CommentId))]
        [InverseProperty(nameof(TblComment.TblBlogCommentRel))]
        public virtual TblComment Comment { get; set; }
    }
}
