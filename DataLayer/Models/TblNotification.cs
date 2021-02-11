using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblNotification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        [StringLength(50)]
        public string Message { get; set; }
        public bool IsSeen { get; set; }
        public int ClientId { get; set; }
        public int SenderId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblNotificationClient))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(TblClient.TblNotificationSender))]
        public virtual TblClient Sender { get; set; }
    }
}
