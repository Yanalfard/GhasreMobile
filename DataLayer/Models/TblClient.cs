using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblClient
    {
        public TblClient()
        {
            TblAlertWhenReady = new HashSet<TblAlertWhenReady>();
            TblBookMark = new HashSet<TblBookMark>();
            TblComment = new HashSet<TblComment>();
            TblNotification = new HashSet<TblNotification>();
            TblOnlineOrder = new HashSet<TblOnlineOrder>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblRate = new HashSet<TblRate>();
            TblTicket = new HashSet<TblTicket>();
            TblTopic = new HashSet<TblTopic>();
        }

        [Key]
        public int ClientId { get; set; }
        [Required]
        [StringLength(15)]
        public string TellNo { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        public string Auth { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public int RoleId { get; set; }
        public long Balance { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblClient))]
        public virtual TblRole Role { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblAlertWhenReady> TblAlertWhenReady { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblBookMark> TblBookMark { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblComment> TblComment { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblNotification> TblNotification { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblOnlineOrder> TblOnlineOrder { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblRate> TblRate { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblTicket> TblTicket { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblTopic> TblTopic { get; set; }
    }
}
