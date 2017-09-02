/*
   2017年9月2日22:07:29
   用户: chenzenghui
   服务器: USER-20160610ZA
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
ALTER TABLE dbo.LB_INFO ADD
	Location geometry NULL
GO
ALTER TABLE dbo.LB_INFO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'CONTROL') as Contr_Per 