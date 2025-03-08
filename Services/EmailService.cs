using MailKit.Net.Smtp;
//using System.Net.Mail;
using MimeKit;

namespace eElection.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string messageBody)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Account Confirmation", "johnlemuel989@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = messageBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("johnlemuel989@gmail.com", "stfn jcht daxx fhzu");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
