using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblAlbum
    {
        public TblAlbum()
        {
            TblImage = new HashSet<TblImage>();
        }

        [Key]
        public int AlbumId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("Album")]
        public virtual ICollection<TblImage> TblImage { get; set; }
    }
}
