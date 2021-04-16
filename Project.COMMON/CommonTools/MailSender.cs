using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.CommonTools
{
    public static class MailSender
    {
        public static void Send(string receiver, string password = "sifregelcek", string body = "Deneme", string subject = "Test", string sender = "bayms3132@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);

            MailAddress receiverEmail = new MailAddress(receiver);//burada item.Email kullanılır

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            using (var mesaj = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                //using scope'u
                smtp.Send(mesaj); //Mail'i gönderdik.
            }
        }
    }
}
