using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using System.Collections.Generic;

namespace SocialNetwork.BLL.Services
{
    public interface IFriendService
    {
        IEnumerable<Friend> GetFriendByUserId(int userId);
        FriendEntity SendFriend(FriendSendingData friendSendingData);
    }
}