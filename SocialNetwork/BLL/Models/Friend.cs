using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.BLL.Models
{
    public class Friend
    {
        public int Id { get; }
        public string UserId { get; }
        public string FriendId { get; set; }
        public Friend(int id, string userId, string friendId)
        {
            this.Id = id;
            this.UserId = userId;
            this.FriendId = friendId;
        }
    }   
}

