/*
   2017年8月27日11:51:46
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
ALTER TABLE dbo.GroupUser
	DROP CONSTRAINT FK_GroupUser_USER_INFO
GO
ALTER TABLE dbo.meetingtable
	DROP CONSTRAINT FK_meetingtable_USER_INFO
GO
ALTER TABLE dbo.USER_INFO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.meetingtable
	DROP CONSTRAINT FK_meetingtable_chartgrouptable
GO
ALTER TABLE dbo.GroupUser
	DROP CONSTRAINT FK_GroupUser_chartgrouptable
GO
ALTER TABLE dbo.chartgrouptable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_GroupUser
	(
	UID int NULL,
	GroupID int NULL,
	groupname nvarchar(50) NULL,
	state int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_GroupUser SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.GroupUser)
	 EXEC('INSERT INTO dbo.Tmp_GroupUser (UID, GroupID, groupname, state)
		SELECT UID, GroupID, groupname, state FROM dbo.GroupUser WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.GroupUser
GO
EXECUTE sp_rename N'dbo.Tmp_GroupUser', N'GroupUser', 'OBJECT' 
GO
ALTER TABLE dbo.GroupUser ADD CONSTRAINT
	FK_GroupUser_chartgrouptable FOREIGN KEY
	(
	GroupID
	) REFERENCES dbo.chartgrouptable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GroupUser ADD CONSTRAINT
	FK_GroupUser_USER_INFO FOREIGN KEY
	(
	UID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.meetingtable
	DROP CONSTRAINT DF_meetingtable_isCheck
GO
CREATE TABLE dbo.Tmp_meetingtable
	(
	ID int NOT NULL IDENTITY (1, 1),
	UID int NULL,
	GID int NULL,
	Ttime datetime NULL,
	local nvarchar(50) NULL,
	contentTitle nvarchar(50) NULL,
	context ntext NULL,
	vnum int NULL,
	isCheck int NULL,
	lng float(53) NULL,
	lat float(53) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_meetingtable SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_meetingtable ADD CONSTRAINT
	DF_meetingtable_isCheck DEFAULT ((1)) FOR isCheck
GO
SET IDENTITY_INSERT dbo.Tmp_meetingtable ON
GO
IF EXISTS(SELECT * FROM dbo.meetingtable)
	 EXEC('INSERT INTO dbo.Tmp_meetingtable (ID, UID, GID, Ttime, local, context, vnum, isCheck, lng, lat)
		SELECT ID, UID, GID, Ttime, local, context, vnum, isCheck, lng, lat FROM dbo.meetingtable WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_meetingtable OFF
GO
ALTER TABLE dbo.travetable
	DROP CONSTRAINT FK_travetable_meetingtable
GO
DROP TABLE dbo.meetingtable
GO
EXECUTE sp_rename N'dbo.Tmp_meetingtable', N'meetingtable', 'OBJECT' 
GO
ALTER TABLE dbo.meetingtable ADD CONSTRAINT
	PK_meetingtable PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.meetingtable ADD CONSTRAINT
	FK_meetingtable_chartgrouptable FOREIGN KEY
	(
	GID
	) REFERENCES dbo.chartgrouptable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.meetingtable ADD CONSTRAINT
	FK_meetingtable_USER_INFO FOREIGN KEY
	(
	UID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.travetable ADD CONSTRAINT
	FK_travetable_meetingtable FOREIGN KEY
	(
	meetID
	) REFERENCES dbo.meetingtable
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.travetable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.travetable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'CONTROL') as Contr_Per 