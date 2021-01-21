﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class FriendList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int FriendListLink { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int FriendId { get; set; }
    }
}
