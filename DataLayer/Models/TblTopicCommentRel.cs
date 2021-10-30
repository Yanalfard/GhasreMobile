using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblTopicCommentRel", Schema = "dbo")]
    public partial class TblTopicCommentRel
    {
        [Key]
        public int TopicCommentRelId { get; set; }
        public int TopicId { get; set; }
        public int CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty(nameof(TblComment.TblTopicCommentRel))]
        public virtual TblComment Comment { get; set; }
        [ForeignKey(nameof(TopicId))]
        [InverseProperty(nameof(TblTopic.TblTopicCommentRel))]
        public virtual TblTopic Topic { get; set; }
    }
}
