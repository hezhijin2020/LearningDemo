using System.Net;
using System.Net.Mail;

namespace HZJ.CommonCls.Email
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class EmailHelper
    {

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromAddress">发件地址</param>
        /// <param name="toAddresses">目的地址数组</param>
        /// <param name="ccAddress">抄送地址数组</param>
        /// <param name="smtpAddress">smtp地址</param>
        /// <param name="smtpPort">smtp端口</param>
        /// <param name="isSSL">是否启用SSL</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public static void SendMail(string fromAddress, string[] toAddresses, string[] ccAddress, string smtpAddress, int smtpPort, bool isSSL, string username, string password, string subject, string body)
        {
            MailMessage mailMessage = null;
            SmtpClient smtpClient = null;
            try
            {
                mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromAddress);
                for (int i = 0; i < toAddresses.Length; i++)
                {
                    string address = toAddresses[i];
                    mailMessage.To.Add(new MailAddress(address));
                }
                for (int j = 0; j < ccAddress.Length; j++)
                {
                    string text = ccAddress[j];
                    if (!(text == ""))
                    {
                        mailMessage.CC.Add(new MailAddress(text));
                    }
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                smtpClient = new SmtpClient(smtpAddress);
                smtpClient.Port = smtpPort;
                smtpClient.Timeout = 2147483647;
                smtpClient.EnableSsl = isSSL;
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.Send(mailMessage);
            }
            finally
            {
                if (mailMessage != null)
                {
                    mailMessage.Dispose();
                    mailMessage = null;
                }
                if (smtpClient != null)
                {
                    smtpClient = null;
                }
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromAddress">发件地址</param>
        /// <param name="toAddr">目的地址</param>
        /// <param name="ccAddr">抄送地址</param>
        /// <param name="smtpAddress">smtp地址</param>
        /// <param name="smtpPort">smtp端口</param>
        /// <param name="isSSL">是否启用SSL</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public static void SendMail(string fromAddress, string toAddr, string ccAddr, string smtpAddress, int smtpPort, bool isSSL, string username, string password, string subject, string body)
        {
            MailMessage mailMessage = null;
            System.Net.Mail.SmtpClient smtpClient = null;
            try
            {
                mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromAddress);
                string[] array = toAddr.Split(new char[]
                {
                    ';'
                });
                string[] array2 = ccAddr.Split(new char[]
                {
                    ';'
                });
                string[] array3 = array;
                for (int i = 0; i < array3.Length; i++)
                {
                    string address = array3[i];
                    mailMessage.To.Add(new MailAddress(address));
                }
                string[] array4 = array2;
                for (int j = 0; j < array4.Length; j++)
                {
                    string text = array4[j];
                    if (!(text == ""))
                    {
                        mailMessage.CC.Add(new MailAddress(text));
                    }
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                smtpClient = new SmtpClient(smtpAddress);
                smtpClient.Port = smtpPort;
                smtpClient.Timeout = 2147483647;
                smtpClient.EnableSsl = isSSL;
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.Send(mailMessage);
            }
            finally
            {
                if (mailMessage != null)
                {
                    mailMessage.Dispose();
                    mailMessage = null;
                }
                if (smtpClient != null)
                {
                    smtpClient = null;
                }
            }
        }
    }
}
