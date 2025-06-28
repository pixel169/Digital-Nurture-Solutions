// Import the necessary libraries
using Moq;
using NUnit.Framework;
using CustomerCommLib; // Your own project with the classes to test

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        [Test]
        public void SendMailToCustomer_ShouldReturnTrue()
        {
            // ARRANGE
            var mockMailSender = new Mock<IMailSender>();

            mockMailSender.Setup(ms => ms.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(true);

            // FIX 1: Use the fully qualified name to avoid namespace collision.
            // We are creating an instance of the CustomerComm class from the CustomerCommLib namespace.
            var customerComm = new CustomerCommLib.CustomerComm(mockMailSender.Object);

            // ACT
            bool result = customerComm.SendMailToCustomer();

            // ASSERT

            // FIX 2: Use the modern NUnit "Constraint Model" for assertions.
            // This is the recommended syntax.
            Assert.That(result, Is.True);

            // This verification call remains the same and is still good practice.
            mockMailSender.Verify(ms => ms.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}