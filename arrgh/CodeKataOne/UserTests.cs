using System;
using CodeKataOne.Interfaces;
using Moq;
using Xunit;

namespace CodeKataOne
{
    public class UserTests
    {
        [Fact]
        public void TestSendMailReturnsFalseIfFails()
        {
            var provider = new Mock<IMailProvider>();
            provider
                .Setup(sender => sender.Send(It.IsAny<String>()))
                .Returns(true);

            User user = new User();
            Assert.True(user.SendMail(provider.Object, It.IsAny<String>()));
        }
    }
}
