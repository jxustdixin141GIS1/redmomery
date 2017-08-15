using System;

namespace  redmomery.Common
{
    public static class MailPage
    {
        #region 邮件配置

        /// <summary>
        ///     发件人密码
        /// </summary>
        private static readonly string s_mailPwd = ConfigHelper.GetAppSettings("mailPwd"); //"";

        /// <summary>
        ///     SMTP邮件服务器
        /// </summary>
        private static readonly string s_host = ConfigHelper.GetAppSettings("mailHost");

        /// <summary>
        ///     发件人邮箱
        /// </summary>
        private static readonly string s_mailFrom = ConfigHelper.GetAppSettings("mailFrom");

        #endregion

        /// <summary>
        ///     发送邮件
        /// </summary>
        /// <param name="content">正文</param>
        /// <returns></returns>
        public static string CreateMailPage(string userName,string mailType, string content,string PostMail)
        {
            var email = new EmailHelper
            {
                mailPwd = s_mailPwd,
                host = s_host,
                mailFrom = s_mailFrom,
                mailSubject = "[你在哪]" + mailType,
                mailBody = @"<!DOCTYPE html>
<html>
	<head>		
		<title></title>
		<style type='text/css'>
		.mainForm{
		 border:1px solid #ECECEC;
	 width:600px;
	 height:450px;
	 margin:0 auto;
		}
		.MailHead{
		 height:70px;
		 background-color:#6f5499;
		}
		.content{
		 border:1px solid #ECECEC;
	    height:300px;
		}
		.bottom{
		 border:1px solid #ECECEC;
		  height:80px;
		  background-color:#f5f5f5f;
		  font-size: 12px;
		  color: #999;
		}
		.Title{
		font-size:30px;
		font-weight:bold;
		color:white;
		}
		p{
		font-size:12px;
		}
		</style>
	</head>
	<body>
	<div class='mainForm'>
	<div class='MailHead'>
	<div class='Title'>红色记忆</div>
	</div>
	<div class='content'>
	<h2>尊敬的用户" + userName+@"，你好：</h2>
	" + content + @"
	<br/>
	<p>如非本人操作，请不要理会此邮件，对此为您带来的不便深表歉意。 </p>
	<br/>
	<p align='right'>你在哪 官方团队</p>
	<p align='right'>
<span times=' 20:50' t='5' style='border-bottom: 1px dashed rgb(204, 204, 204); position: relative;' isout='0'>" +
                           DateTime.Now + @"</span>
</p>
	</div>
	<div class='bottom'>
	<p > 如有疑问，请发邮件到
<a target='_blank' href='mailto:postmaster@sharegis.cn'>postmaster@sharegis.cn</a>
，感谢您的支持。 </p>
	</div>
	</div>
	</body>
</html>",
                mailToArray = new[] { PostMail }
            };
            try
            {
                email.Send();
            }
            catch (Exception ex)
            {
               
                redmomery.command.createlog.createlogs(ex.StackTrace.ToString() + "\n\r" + ex.Message);
                return "no";
            }
            return "ok";
        }
    }
}