/*
   2017年8月26日11:15:55
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
ALTER TABLE dbo.LB_INFO
	DROP CONSTRAINT DF_LB_INFO_LBdelete
GO
ALTER TABLE dbo.LB_INFO ADD CONSTRAINT
	DF_LB_INFO_LBdelete DEFAULT 0 FOR LBdelete
GO
ALTER TABLE dbo.LB_INFO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'CONTROL') as Contr_Per 