/*
   2017年8月28日16:11:04
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
ALTER TABLE dbo.USER_INFO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.meetingtable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.UATIMeettable ADD CONSTRAINT
	FK_UATIMeettable_USER_INFO FOREIGN KEY
	(
	UID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UATIMeettable ADD CONSTRAINT
	FK_UATIMeettable_USER_INFO1 FOREIGN KEY
	(
	dealUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UATIMeettable ADD CONSTRAINT
	FK_UATIMeettable_meetingtable FOREIGN KEY
	(
	meetID
	) REFERENCES dbo.meetingtable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UATIMeettable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UATIMeettable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UATIMeettable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UATIMeettable', 'Object', 'CONTROL') as Contr_Per 