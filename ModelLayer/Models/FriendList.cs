using System;
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
<<<<<<< HEAD
        //[Required]
        //public int FriendListLink { get; set; }
=======
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int FriendListLink { get; set; }
>>>>>>> 1bc886efcb3a6e0662dedd4f31747778994b1122
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers are allowed.")]
        public int FriendId { get; set; }
        [Required]
        public int RequestedFriendId { get; set; }
        public string status { get; set; } = "pending";

        public FriendList() { }
        public FriendList(int userid, int requestedFriendid)
        {
            FriendId = userid;
            RequestedFriendId = requestedFriendid;
        }
    }
}
