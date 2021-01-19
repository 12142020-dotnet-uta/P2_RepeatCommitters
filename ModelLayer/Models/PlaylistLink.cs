using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class PlaylistLink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
