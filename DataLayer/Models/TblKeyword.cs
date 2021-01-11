using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblKeyword
    {
        public TblKeyword()
        {
            TblBlogKeywordRel = new HashSet<TblBlogKeywordRel>();
            TblProductKeywordRel = new HashSet<TblProductKeywordRel>();
        }

        [Key]
        public int KeywordId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Keyword")]
        public virtual ICollection<TblBlogKeywordRel> TblBlogKeywordRel { get; set; }
        [InverseProperty("Keyword")]
        public virtual ICollection<TblProductKeywordRel> TblProductKeywordRel { get; set; }
    }
}
