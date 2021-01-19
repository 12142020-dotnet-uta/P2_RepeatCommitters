﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class FriendListLink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
