﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
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
        [Display(Name="Number Of Plays")]
        public int NumberOfPlays { get; set; }
        [Display(Name = "Lyrics")]
        public string Lyrics { get; set; } = null;
        [Display(Name ="URL Path")]
        public string UrlPath { get; set; }
        public Boolean isOriginal { get; set; }
    }
}
