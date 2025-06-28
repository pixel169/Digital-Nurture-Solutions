namespace CustomerCommLib
{
    public class CustomerComm
    {
        private readonly IMailSender _mailSender;
        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool SendMailToCustomer()
        {
            string email = "sample mail";
            string message = "Heil Cognizanr lords";
            return _mailSender.SendMail(email, message);
        }
    }
}