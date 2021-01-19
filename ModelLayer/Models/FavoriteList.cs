using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class FavoriteList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FavoriteListLink { get; set; }
        [Required]
        public int SongId { get; set; }
    }
}
