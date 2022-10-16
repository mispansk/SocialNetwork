using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.BLL.Services
{
    public class FrendService : IFriendService
    {
        IUserRepository userRepository;
        IFriendRepository friendRepository;

        public FrendService()
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
        }

        /// <summary>
        /// поиск списка друзей в таблице friend
        /// </summary>
        /// <param name="userId"> пользователь </param>
        /// <returns></returns>
        public IEnumerable<Friend> GetFriendByUserId(int userId)
        {
            var friends = new List<Friend>();
            var friendsEmail = new List<string>();

            friendRepository.FindAllByUserId(userId).ToList().ForEach(m =>
            {
                var userUserEntity = userRepository.FindById(m.user_id);
                var friendUserEntity = userRepository.FindById(m.friend_id);

                friends.Add(new Friend(m.id, userUserEntity.email, friendUserEntity.email));
                friendsEmail.Add(friendUserEntity.email);
            });

            return friends;
        }

        /// <summary>
        /// поиск будущего друга в табллице user, запись в таблицу friend
        /// </summary>
        /// <param name="friendSendingData"></param>
        public FriendEntity SendFriend(FriendSendingData friendSendingData)
        {

            var findUserEntity = this.userRepository.FindByEmail(friendSendingData.RecipientEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (friendSendingData.SenderId == findUserEntity.id) throw new ThisIException();

            var friendEntity = new FriendEntity()
            {
                user_id = friendSendingData.SenderId,
                friend_id = findUserEntity.id,
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();

            return friendEntity;
        }
    }
}
