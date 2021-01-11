using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblBlog
    {
        public TblBlog()
        {
            TblBlogCommentRel = new HashSet<TblBlogCommentRel>();
            TblBlogKeywordRel = new HashSet<TblBlogKeywordRel>();
            TblRate = new HashSet<TblRate>();
        }

        [Key]
        public int BlogId { get; set; }
        [StringLength(200)]
        public string MainImage { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public string BodyHtml { get; set; }
        public int LikeCount { get; set; }
        public int ViewCount { get; set; }

        [InverseProperty("Blog")]
        public virtual ICollection<TblBlogCommentRel> TblBlogCommentRel { get; set; }
        [InverseProperty("Blog")]
        public virtual ICollection<TblBlogKeywordRel> TblBlogKeywordRel { get; set; }
        [InverseProperty("Blog")]
        public virtual ICollection<TblRate> TblRate { get; set; }
    }
}
