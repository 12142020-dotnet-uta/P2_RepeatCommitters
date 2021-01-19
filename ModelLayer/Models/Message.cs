using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ToUserId { get; set; }
        [Required]
        public int FromUserId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
