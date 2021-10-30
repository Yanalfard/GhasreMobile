using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblClient", Schema = "dbo")]
    public partial class TblClient
    {
        public TblClient()
        {
            TblAlertWhenReady = new HashSet<TblAlertWhenReady>();
            TblBookMark = new HashSet<TblBookMark>();
            TblComment = new HashSet<TblComment>();
            TblNotificationClient = new HashSet<TblNotification>();
            TblNotificationSender = new HashSet<TblNotification>();
            TblOnlineOrder = new HashSet<TblOnlineOrder>();
            TblOrder = new HashSet<TblOrder>();
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
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public int RoleId { get; set; }
        public long Balance { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string MainImage { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblClient))]
        public virtual TblRole Role { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblAlertWhenReady> TblAlertWhenReady { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblBookMark> TblBookMark { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblComment> TblComment { get; set; }
        [InverseProperty(nameof(TblNotification.Client))]
        public virtual ICollection<TblNotification> TblNotificationClient { get; set; }
        [InverseProperty(nameof(TblNotification.Sender))]
        public virtual ICollection<TblNotification> TblNotificationSender { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblOnlineOrder> TblOnlineOrder { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblRate> TblRate { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblTicket> TblTicket { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblTopic> TblTopic { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblWallet> TblWallet { get; set; }

        [InverseProperty("Client")]
        public virtual ICollection<TblOrder> TblOrder { get; set; }
    }
}
