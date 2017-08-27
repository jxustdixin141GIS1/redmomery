using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using redmomery.librarys;
using redmomery.Model;
using redmomery.DAL;
using redmomery.command;
using redmomery.Common;

namespace redmomery.librarys
{
   public  class UserBLL
    {
       
    }
}
namespace redmomery.librarys
{
    public class Userlib
    {
      
        public static USER_INFO RegisterUser(string activehf,string name, string sex, string job, string birthday, string address, string photo, string Email, string Netname, string pwd, string Img = "~/resource/image/head/41afadb7665e2cf76660dc9870f75bdf.jpg")
        {
            USER_INFO lb = CreateUserInfo(name,sex,job,birthday,address,photo,Email,Netname,pwd,Img);
            //通过对应激活MD5值进行激活，并调用对应的Email组件，进行发送验证邮箱
            try
            {
                if (sendregist(lb.USER_EMEIL, activehf + lb.MD5, lb.USER_NAME))
                {
                    lb.ISPASS =3;
                }
                else
                {
                    lb.ISPASS = 4;
                }
                USER_INFODAL dal = new USER_INFODAL();
                int id = dal.addNew(lb);
                lb = dal.get(id);
            }
            catch(Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message.ToString() + ";" + ex.Source.ToString() + ";" + ex.StackTrace + ";");
            }
            //然后将用户注册结果添加到数据库中

            return lb;
        }
      
    }
    partial class Userlib
    {
        private static USER_INFO CreateUserInfo(string name, string sex, string job, string birthday, string address, string photo, string Email, string Netname, string pwd, string Img = "~/resource/image/head/41afadb7665e2cf76660dc9870f75bdf.jpg")
        {
            USER_INFO newuser = new USER_INFO();
            newuser.USER_NAME = name;
            newuser.USER_SEX = sex;
            newuser.USER_JOB = job;
            newuser.USER_BIRTHDAY = birthday;
            newuser.USER_ADDRESS = address;
            newuser.USER_PHONE = photo;
            newuser.USER_EMEIL = Email;
            newuser.USER_NETNAME = Netname;
            newuser.USER_EMEIL = Email;
            newuser.USER_IMG = Img;
            newuser.USER_PSWD = pwd;
            newuser.ISPASS = 1;
            newuser.MD5 = MD5Helper.EncryptString(Common.SerializerHelper.SerializeToString(newuser));
            return newuser;
        }
        private static bool sendregist(string usermail, string MD5_hf, string username)
        {
            PageMail pagemail = new PageMail();
            pagemail.ishtml = true;
            string body =
            #region
 @"<!DOCTYPE html>
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
	<h2>尊敬的用户" + username + @"，你好：</h2>
	" + "请点击下面的连接激活你的账号：<a href=\"" + MD5_hf + "\">" + MD5_hf + "</a>\""
      + @"
	<br/>
	<p>如非本人操作，请不要理会此邮件，对此为您带来的不便深表歉意。 </p>
	<br/>
	<p align='right'>红色记忆 官方团队</p>
	<p align='right'>
<span times=' 20:50' t='5' style='border-bottom: 1px dashed rgb(204, 204, 204); position: relative;' isout='0'>" +
                           DateTime.Now + @"</span>
</p>
	</div>
	<div class='bottom'>
	<p > 如有疑问，请发邮件到
<a target='_blank' href='mailto:18720728252@163.cn'>postmaster@sharegis.cn</a>
，感谢您的支持。 </p>
	</div>
	</div>
	</body>
</html>";
            #endregion            ;
            ;

            pagemail.reciveaddres.Add(usermail);
            pagemail.subject = "尊敬的用户，这是来源于红色记忆网站的注册邮件，若非本人操作请忽略";
            pagemail.content = body;
            return pagemail.Sends();
        }

    }
}
