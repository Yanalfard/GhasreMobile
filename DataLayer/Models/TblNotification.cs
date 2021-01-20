using System;
using System.Collections.Generic;
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

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblNotification))]
        public virtual TblClient Client { get; set; }
    }
}
