using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblTicket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(800)]
        public string Body { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateSubmited { get; set; }
        public int? ClientId { get; set; }
        public bool? IsAnswered { get; set; }
        public bool? IsAdmin { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblTicket))]
        public virtual TblClient Client { get; set; }
    }
}
