using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Artist")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int ArtistId { get; set; }
        [Required]
        [Display(Name="Genre")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int GenreId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name="Number Of Plays")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int NumberOfPlays { get; set; }
        [Display(Name = "Lyrics")]
        public string Lyrics { get; set; } = null;
        [Display(Name = "URL Path")]
        public string UrlPath { get; set; }
        [Display(Name = "Original")]
        public Boolean isOriginal { get; set; }
    }
}
