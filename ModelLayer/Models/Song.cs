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
        public string ArtistName { get; set; }
        [Required]
        [Display(Name="Genre")]
        public string Genre { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name = "Number Of Plays")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int NumberOfPlays { get; set; } = 0;
        [Display(Name = "Lyrics")]
        public string Lyrics { get; set; } = null;
        [Display(Name = "URL Path")]
        public string UrlPath { get; set; }
        [Display(Name = "Original")]
        public Boolean isOriginal { get; set; } = true;


        public Song(string artist, string genre, string title, string lyrics, string urlPath, bool isOriginal)
        {
            this.ArtistName = artist;
            this.Genre = genre;
            this.Title = title;
            this.Lyrics = lyrics;
            this.UrlPath = urlPath;
            this.isOriginal = isOriginal;
        }

        public Song()
        {
        }
    }
}
