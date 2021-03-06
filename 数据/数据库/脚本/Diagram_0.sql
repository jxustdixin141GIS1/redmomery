/*
   2017年8月26日16:31:15
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
ALTER TABLE dbo.singlemessagepooltable ADD CONSTRAINT
	FK_singlemessagepooltable_USER_INFO FOREIGN KEY
	(
	FUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.singlemessagepooltable ADD CONSTRAINT
	FK_singlemessagepooltable_USER_INFO1 FOREIGN KEY
	(
	TUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.singlemessagepooltable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.singlemessagepooltable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.singlemessagepooltable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.singlemessagepooltable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.chartgrouptable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.multimessagepooltable ADD CONSTRAINT
	FK_multimessagepooltable_USER_INFO FOREIGN KEY
	(
	FUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.multimessagepooltable ADD CONSTRAINT
	FK_multimessagepooltable_chartgrouptable FOREIGN KEY
	(
	TGID
	) REFERENCES dbo.chartgrouptable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.multimessagepooltable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'CONTROL') as Contr_Per 