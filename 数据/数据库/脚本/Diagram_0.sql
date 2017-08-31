/*
   2017年8月31日15:47:28
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
ALTER TABLE dbo.chartgrouptable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.chargoupTimetable ADD CONSTRAINT
	FK_chargoupTimetable_chartgrouptable FOREIGN KEY
	(
	GID
	) REFERENCES dbo.chartgrouptable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.chargoupTimetable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.chargoupTimetable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chargoupTimetable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chargoupTimetable', 'Object', 'CONTROL') as Contr_Per 