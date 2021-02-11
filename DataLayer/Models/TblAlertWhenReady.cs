using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblAlertWhenReady
    {
        [Key]
        public int AlertWhenReady { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(TblClient.TblAlertWhenReady))]
        public virtual TblClient Client { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblAlertWhenReady))]
        public virtual TblProduct Product { get; set; }
    }
}
