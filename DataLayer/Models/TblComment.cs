using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblComment
    {
        public TblComment()
        {
            InverseParent = new HashSet<TblComment>();
            TblBlogCommentRel = new HashSet<TblBlogCommentRel>();
            TblProductCommentRel = new HashSet<TblProductCommentRel>();
        }

        [Key]
        public int CommentId { get; set; }
        public int ClientId { get; set; }
        [Required]
        public string Body { get; set; }
        public bool? IsValid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateSubmited { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblComment))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(TblComment.InverseParent))]
        public virtual TblComment Parent { get; set; }
        [InverseProperty(nameof(TblComment.Parent))]
        public virtual ICollection<TblComment> InverseParent { get; set; }
        [InverseProperty("Comment")]
        public virtual ICollection<TblBlogCommentRel> TblBlogCommentRel { get; set; }
        [InverseProperty("Comment")]
        public virtual ICollection<TblProductCommentRel> TblProductCommentRel { get; set; }
    }
}
