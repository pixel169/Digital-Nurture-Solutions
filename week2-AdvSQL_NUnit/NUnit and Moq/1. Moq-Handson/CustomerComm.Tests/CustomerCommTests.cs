using Moq;
using NUnit.Framework;
using CustomerCommLib; 

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        [Test]
        public void SendMailToCustomer_ShouldReturnTrue()
        {
            var mockMailSender = new Mock<IMailSender>();

            mockMailSender.Setup(ms => ms.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(true);
            var customerComm = new CustomerCommLib.CustomerComm(mockMailSender.Object);
            bool result = customerComm.SendMailToCustomer();
            Assert.That(result, Is.True);
            mockMailSender.Verify(ms => ms.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}