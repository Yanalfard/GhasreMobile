using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblBlogKeywordRel
    {
        [Key]
        public int BlogKeywordRelId { get; set; }
        public int BlogId { get; set; }
        public int KeywordId { get; set; }

        [ForeignKey(nameof(BlogId))]
        [InverseProperty(nameof(TblBlog.TblBlogKeywordRel))]
        public virtual TblBlog Blog { get; set; }
        [ForeignKey(nameof(KeywordId))]
        [InverseProperty(nameof(TblKeyword.TblBlogKeywordRel))]
        public virtual TblKeyword Keyword { get; set; }
    }
}
