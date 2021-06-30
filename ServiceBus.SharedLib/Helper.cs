using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.SharedLib
{
   public static class Helper
    {
        public static async Task SendEmail(Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(email.EmailTo);
                mail.Subject = email.Subject;
                mail.Body = email.Body;

                await Send(mail);

                Console.WriteLine($"Email sent to {email.EmailTo}");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task SendPaymentEmail(PaymentEmail email)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(email.EmailTo);
                mail.Subject = $"***Payment Received {email.Amount} ***";
                mail.Body = email.Message;

                await Send(mail);

                Console.WriteLine($"Payment Confirmation Email sent to {email.EmailTo}");
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static async Task SendReminderEmail(PaymentReminderEmail email)
        {
            try
            {
                MailMessage mail = new MailMessage();

               
                mail.To.Add(email.EmailTo);
                mail.Subject = $"***Reminder to pay {email.Amount} ***";
                mail.Body = email.Message;

                await Send(mail);

                Console.WriteLine($"Reminder Email sent to {email.EmailTo}");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task Send(MailMessage mail)
        {
            try
            {
                
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    mail.From = new MailAddress("mukesh.kanojia.5053@gmail.com");
                    //make sure to enable settings in gmail https://www.google.com/settings/security/lesssecureapps
                    smtp.Credentials = new NetworkCredential("username", "password");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
