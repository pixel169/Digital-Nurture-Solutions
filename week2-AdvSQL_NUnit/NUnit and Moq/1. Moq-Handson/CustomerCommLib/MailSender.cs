using System.Net;
using System.Net.Mail;

namespace CustomerCommLib
{
    public class MailSender : IMailSender
    {
        public bool SendMail(string toAddress, string message)
        {
            // NOTE: This is a sample implementation. In a real-world scenario,
            // these details would come from a configuration file.
            // This code is UNTESTABLE in isolation without a real SMTP server.
            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("your_email_address@gmail.com");
            mail.To.Add(toAddress);
            mail.Subject = "Test Mail";
            mail.Body = message;

            SmtpServer.Port = 587;
            // WARNING: Do not hardcode credentials in real applications!
            SmtpServer.Credentials = new NetworkCredential("username", "password");
            SmtpServer.EnableSsl = true;

            // This line would send a real email.
            // SmtpServer.Send(mail); 
            
            // For the purpose of this example, we'll assume it succeeds.
            return true;
        }
    }
}