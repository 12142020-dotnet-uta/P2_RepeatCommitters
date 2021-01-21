using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int ToUserId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int FromUserId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
