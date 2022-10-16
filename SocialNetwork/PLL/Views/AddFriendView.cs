using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SocialNetwork.PLL.Views
{/// <summary>
/// добавление в друзья
/// </summary>
    public class AddFriendView
    {
        UserService userService;
        FrendService frendService;
        public AddFriendView(FrendService frendService, UserService userService)
        {
            this.frendService = frendService;
            this.userService = userService;
        }
        public void Show(User user)
        {
            var friendSendingData = new FriendSendingData();
            friendSendingData.SenderId = user.Id;

            Console.WriteLine("Мои друзья:");
            IEnumerable<Friend> friends = frendService.GetFriendByUserId(friendSendingData.SenderId);
            if (friends.Count() == 0)
                Console.WriteLine("У меня пока нет друзей ");
            else
            {
                friends.ToList().ForEach(friends =>
                {
                    Console.WriteLine(friends.FriendId);  // выводим друзей в консоль
                });           
            }

            Console.Write("Введите почтовый адрес будущего друга: ");
            friendSendingData.RecipientEmail = Console.ReadLine();
            
            try
            {
                bool УжеЕсть = friends.Select(f => f.FriendId).Contains(friendSendingData.RecipientEmail);
                if (УжеЕсть)  // если добавляемый контакт уже есть в друзьях, выводим ошибку
                    throw new FriendDoubleException();

                frendService.SendFriend(friendSendingData);
                
                SuccessMessage.Show("Друг успешно добавлен");
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Адрес не найден!");
            }

            catch (FriendDoubleException)
            {
                AlertMessage.Show("Контакт уже в друзьях");
            }

            catch (ThisIException)
            {
                AlertMessage.Show("Не нужно себя добавлять в друзья");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при добавлении друга");
            }
        }
    }
}
