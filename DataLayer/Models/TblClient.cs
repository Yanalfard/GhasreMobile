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
            TblComment = new HashSet<TblComment>();
            TblOnlineOrder = new HashSet<TblOnlineOrder>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblRate = new HashSet<TblRate>();
            TblTicket = new HashSet<TblTicket>();
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

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblClient))]
        public virtual TblRole Role { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblComment> TblComment { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblOnlineOrder> TblOnlineOrder { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblRate> TblRate { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<TblTicket> TblTicket { get; set; }
    }
}
