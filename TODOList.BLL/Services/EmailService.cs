//using MimeKit;
//using MailKit.Net.Smtp;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using System.Text;
using System;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace TODOList.BLL.Services
{
    public class EmailService
    {

        //public async Task SendEmailAsync(EmailDTO email)
        public void SendmailWithIcsAttachment(EmailDTO email)
        {

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("todo@gmail.com", "ABC");
            msg.To.Add(new MailAddress(email.EmailTo, "BCD"));
            msg.Subject = "Notification with ICS file as an Attachment";
            msg.Body = email.Message;

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Schedule a reminder");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", email.DateStart));
            //str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", email.));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", email.DateEnd));
            str.AppendLine("LOCATION:BELARUS");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
            MemoryStream stream = new MemoryStream(byteArray);

            Attachment attachment = new Attachment(stream, "event.ics");

            msg.Attachments.Add(attachment);

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.EmailFrom, email.Password)

            };

            System.Net.Mime.ContentType type = new System.Net.Mime.ContentType("text/calendar");
            type.Parameters.Add("method", "REQUEST");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), type);
            smtpClient.Send(msg);



            //MimeMessage emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress("Администрация сайта", "nastial2211@yandex.by"));
            //emailMessage.To.Add(new MailboxAddress("", email.EmailVal));
            //emailMessage.Subject = "Рассылка";
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = email.Message
            //};

            //using (SmtpClient client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.yandex.ru", 25, false);
            //    await client.AuthenticateAsync("nastial2211@yandex.by", "49504828NaSt");
            //    await client.SendAsync(emailMessage);
            //    await client.DisconnectAsync(true);
            //}

            //var emailMessage = new MimeMessage();

            //emailMessage.Cc.Add(new MailboxAddress("Администрация сайта", "nastial2211@yandex.by"));


            //emailMessage.From.Add(new MailboxAddress("SenderName", email.EmailFrom));
            //emailMessage.To.Add(new MailboxAddress("", email.EmailTo));
            //emailMessage.Subject = "Рассылка";
            //var builder = new BodyBuilder { TextBody = email.Message };

            ////Fetch the attachments from db
            ////considering one or more attachments
            //builder.Attachments.Add()

        }
    }
}