/*
   2017年8月26日15:28:46
   用户: sa
   服务器: CHENZENGHUI
   数据库: redmomery
   应用程序: 
*/

/* 为了防止任何可能出现的数据丢失问题，您应该先仔细检查此脚本，然后再在数据库设计器的上下文之外运行此脚本。*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.multimessagepooltable
	(
	FUID int NULL,
	TGID int NULL,
	Ftime datetime NULL,
	context nvarchar(1000) NULL,
	Rnum int NULL,
	Snum int NULL,
	MD5 nvarchar(200) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.multimessagepooltable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'CONTROL') as Contr_Per 