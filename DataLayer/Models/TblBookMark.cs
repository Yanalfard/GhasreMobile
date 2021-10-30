using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblBookMark", Schema = "dbo")]
    public partial class TblBookMark
    {
        [Key]
        public int BookMarkId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblBookMark))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblBookMark))]
        public virtual TblProduct Product { get; set; }
    }
}
