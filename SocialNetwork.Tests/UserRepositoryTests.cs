using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;
using Xunit;

namespace SocialNetwork.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
       
        [Fact]
        public void SendFriendTest1() // этот тест не пройдет, т.к. user 1 имеет email 123@mail.ru
        {                             // будет ошибка ThisIException (себ€ не нужно добавл€ть в друзь€)
            var service = new FrendService(); 

            var entity = service.SendFriend(new BLL.Models.FriendSendingData() { SenderId = 1, RecipientEmail = "123@mail.ru" });
            Xunit.Assert.NotNull(entity);
        }

        [Fact]
        public void SendFriendTest2() // этот тест пройдет, т.к. user 1 имеет друга 321@mail.ru
        {
            var service = new FrendService();

            var entity = service.SendFriend(new BLL.Models.FriendSendingData() { SenderId = 1, RecipientEmail = "321@mail.ru" });
            Xunit.Assert.NotNull(entity);
        }
    }
}
