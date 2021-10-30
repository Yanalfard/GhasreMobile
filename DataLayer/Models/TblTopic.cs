using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblTopic", Schema = "dbo")]
    public partial class TblTopic
    {
        public TblTopic()
        {
            TblTopicCommentRel = new HashSet<TblTopicCommentRel>();
        }
        [Key]
        public int TopicId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Body { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public int ClientId { get; set; }
        public int VoteCount { get; set; }
        public bool IsValid { get; set; }
        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblTopic))]
        public virtual TblClient Client { get; set; }
        [InverseProperty("Topic")]
        public virtual ICollection<TblTopicCommentRel> TblTopicCommentRel { get; set; }
    }
}
