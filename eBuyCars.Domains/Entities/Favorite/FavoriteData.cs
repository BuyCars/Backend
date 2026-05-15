using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyCars.Domain.Entities.Favorite
{
    public class FavoriteData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
