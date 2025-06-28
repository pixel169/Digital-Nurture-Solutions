namespace CustomerCommLib
{
    public class CustomerComm
    {
        // This holds our dependency. It can be a REAL MailSender
        // or a MOCK MailSender.
        private readonly IMailSender _mailSender;

        // This is Constructor Injection.
        // We are "injecting" the mail sender dependency from the outside.
        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool SendMailToCustomer()
        {
            // Actual application logic would go here to determine
            // the customer's address and the message.
            string email = "cust123@abc.com";
            string message = "Some Message";

            // We then use the injected dependency to do the work.
            return _mailSender.SendMail(email, message);
        }
    }
}