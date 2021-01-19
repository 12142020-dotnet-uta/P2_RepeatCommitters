using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Artist")]
        public int ArtistId { get; set; }
        [Required]
        [Display(Name="Genre")]
        public int GenreId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name="Number Of Plays")]
        public int NumberOfPlays { get; set; }
    }
}
