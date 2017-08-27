using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UtilSp.ClassLib;
namespace redmomery.Common
{
    public class EmailHelper
    {
        #region Eail 属性

        private string _mailFrom = "postmaster@redmomery.cn";
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get { return _mailFrom; } set { _mailFrom = value; } }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] mailToArray { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string[] mailCcArray { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailPwd { get; set; }

        private string _host = "smtp.redmomery.cn";
        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string host { get { return _host; } set { _host = value; } }

        private bool _isbodyHtml = true;
        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get { return _isbodyHtml; } set { _isbodyHtml = value; } }

        private string _nickname = "红色记忆 系统通知";
        /// <summary>
        /// 发送者昵称
        /// </summary>
        public string nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public string[] attachmentsPath { get; set; }

        //优先级别
        private MailPriority _Priority = MailPriority.Normal;
        /// <summary>
        /// 优先级别  默认正常优先级
        /// </summary>
        public MailPriority Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
            }
        }
        #endregion

        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(mailFrom, nickname);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();

            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            //邮件优先级
            myMail.Priority = Priority;

            myMail.IsBodyHtml = isbodyHtml;

            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);//115                 //设置SMTP邮件服务器
            smtp.Host = host;
            // smtp.EnableSsl = true;
            //smtp.Port = 587;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                redmomery.command.createlog.createlogs(ex.StackTrace.ToString() + "\n\r" + ex.Message);
                return false;
            }

        }
    }
    public  class PageMail
    {
      public string senderAddress = "";//System.Configuration.ConfigurationManager.AppSettings["EmailAdress"].ToString();
      public List<string> reciveaddres = new List<string>();
      public string subject = "";
      public string content = "";
      public string userName = "";
      public string password = "";
      public string servehf = "";
      public int port = 25;
      public bool ishtml = false;
        public PageMail()
        {
            senderAddress =System.Configuration.ConfigurationManager.AppSettings["EmailAdress"].ToString();
            userName = System.Configuration.ConfigurationManager.AppSettings["Emailname"].ToString();
            password = System.Configuration.ConfigurationManager.AppSettings["Emailpassword"].ToString();
            servehf = System.Configuration.ConfigurationManager.AppSettings["EmailHost"].ToString();
            port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
        }

        public  bool Sends()
        {
            SmtpSp smtpSp = new SmtpSp();
            smtpSp.ishtml = ishtml;
            SmtpSp.MailInfo mailInfo = new SmtpSp.MailInfo();
            mailInfo.senderAddress_pro = senderAddress;
            mailInfo.receiverAddresses_pro = reciveaddres;
            mailInfo.subject_pro = subject;
            mailInfo.content_pro = content;
            mailInfo.userName_pro = userName;
            mailInfo.password_pro = password;
            bool isok=  smtpSp.send(servehf,port,mailInfo);
            return isok;
        }
    }
}
