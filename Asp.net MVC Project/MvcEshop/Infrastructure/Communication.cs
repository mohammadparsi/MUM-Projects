using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Infrastructure
{
    public class Communication
    {
        public static void SendEmail(string smtpServer, int smtpServerPort, string fromAddress,
            string fromPassword, string fromDisplayName, string toAddress,
            string subject, string body, string attachmentFilePath)
        {
            try
            {
                MailMessage oMailMessage = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpClient SmtpServer = new SmtpClient(smtpServer);
                oMailMessage.From = new MailAddress(fromAddress, fromDisplayName);
                oMailMessage.To.Add(toAddress);
                oMailMessage.Subject = subject;
                oMailMessage.Body = body;
                oMailMessage.IsBodyHtml = true;

                //System.Net.Mail.Attachment attachment;
                // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
                // mail.Attachments.Add(attachment);

                SmtpServer.Port = smtpServerPort;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);

                MvcEshop.Models.DFEntities oDFEntities =
                    new MvcEshop.Models.DFEntities();

                SmtpServer.EnableSsl = oDFEntities.Settings
                    .FirstOrDefault().EnableSsl;

                if (attachmentFilePath != null)
                {
                    Attachment attachment = new Attachment(attachmentFilePath, System.Net.Mime.MediaTypeNames.Application.Octet);
                    System.Net.Mime.ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(attachmentFilePath);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachmentFilePath);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(attachmentFilePath);
                    disposition.FileName = System.IO.Path.GetFileName(attachmentFilePath);
                    disposition.Size = new System.IO.FileInfo(attachmentFilePath).Length;
                    disposition.DispositionType = System.Net.Mime.DispositionTypeNames.Attachment;
                    oMailMessage.Attachments.Add(attachment);
                }


                SmtpServer.Send(oMailMessage);

            }
            catch (Exception ex)
            {

                using (MvcEshop.Models.DFEntities oDFEntities=new MvcEshop.Models.DFEntities())
                {
                    MvcEshop.Models.Setting oSetting = oDFEntities.Settings.FirstOrDefault();

                    Infrastructure.Communication.SendEmail
                                   (oSetting.SmtpServer, oSetting.SmtpServerPort, oSetting.SenderEmailAddress,
                                   oSetting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                                   oDFEntities.Settings.FirstOrDefault().SupportTeamEmail, "خطا در ارسال ایمیل",
                                   ex.Message, null); 
                }
            }
        }
    }
}