/*
   2017年8月27日20:33:18
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
ALTER TABLE dbo.USER_INFO
	DROP CONSTRAINT DF_USER_INFO_ISPASS
GO
CREATE TABLE dbo.Tmp_USER_INFO
	(
	USER_ID int NOT NULL IDENTITY (1, 1),
	USER_NAME nvarchar(50) NULL,
	USER_SEX nvarchar(50) NULL,
	USER_JOB nvarchar(50) NULL,
	USER_BIRTHDAY nvarchar(50) NULL,
	USER_ADDRESS nvarchar(50) NULL,
	USER_PHONE nvarchar(20) NULL,
	USER_EMEIL nvarchar(60) NULL,
	USER_NETNAME nvarchar(50) NULL,
	USER_IMG nvarchar(255) NULL,
	USER_PSWD nvarchar(50) NULL,
	ISPASS int NULL,
	MD5 nvarchar(80) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_USER_INFO SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_USER_INFO ADD CONSTRAINT
	DF_USER_INFO_ISPASS DEFAULT ((0)) FOR ISPASS
GO
SET IDENTITY_INSERT dbo.Tmp_USER_INFO ON
GO
IF EXISTS(SELECT * FROM dbo.USER_INFO)
	 EXEC('INSERT INTO dbo.Tmp_USER_INFO (USER_ID, USER_NAME, USER_SEX, USER_JOB, USER_BIRTHDAY, USER_ADDRESS, USER_PHONE, USER_EMEIL, USER_NETNAME, USER_IMG, USER_PSWD, ISPASS, MD5)
		SELECT USER_ID, USER_NAME, USER_SEX, USER_JOB, USER_BIRTHDAY, USER_ADDRESS, USER_PHONE, USER_EMEIL, USER_NETNAME, USER_IMG, USER_PSWD, ISPASS, MD5 FROM dbo.USER_INFO WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_USER_INFO OFF
GO
ALTER TABLE dbo.travetable
	DROP CONSTRAINT FK_travetable_USER_INFO
GO
ALTER TABLE dbo.multimessagepooltable
	DROP CONSTRAINT FK_multimessagepooltable_USER_INFO
GO
ALTER TABLE dbo.singlemessagepooltable
	DROP CONSTRAINT FK_singlemessagepooltable_USER_INFO
GO
ALTER TABLE dbo.singlemessagepooltable
	DROP CONSTRAINT FK_singlemessagepooltable_USER_INFO1
GO
ALTER TABLE dbo.moduleBBS_Table
	DROP CONSTRAINT FK_moduleBBS_Table_USER_INFO
GO
ALTER TABLE dbo.BBSTITLE_TABLE
	DROP CONSTRAINT FK_BBSTITLE_TABLE_USER_INFO
GO
ALTER TABLE dbo.SHAPE_TABLE
	DROP CONSTRAINT FK_SHAPE_TABLE_USER_INFO
GO
ALTER TABLE dbo.FILE_TABLE
	DROP CONSTRAINT FK_FILE_TABLE_USER_INFO
GO
ALTER TABLE dbo.CTBBS_TABLE
	DROP CONSTRAINT FK_CTBBS_TABLE_USER_INFO
GO
ALTER TABLE dbo.CCBBS_TABLE
	DROP CONSTRAINT FK_CCBBS_TABLE_USER_INFO
GO
ALTER TABLE dbo.invGoodUser
	DROP CONSTRAINT FK_invGoodUser_USER_INFO
GO
ALTER TABLE dbo.invGoodUser
	DROP CONSTRAINT FK_invGoodUser_USER_INFO1
GO
ALTER TABLE dbo.GoodUser
	DROP CONSTRAINT FK_GoodUser_USER_INFO
GO
ALTER TABLE dbo.GoodUser
	DROP CONSTRAINT FK_GoodUser_USER_INFO1
GO
ALTER TABLE dbo.GroupUser
	DROP CONSTRAINT FK_GroupUser_USER_INFO
GO
ALTER TABLE dbo.meetingtable
	DROP CONSTRAINT FK_meetingtable_USER_INFO
GO
ALTER TABLE dbo.chartgrouptable
	DROP CONSTRAINT FK_chartgrouptable_USER_INFO
GO
DROP TABLE dbo.USER_INFO
GO
EXECUTE sp_rename N'dbo.Tmp_USER_INFO', N'USER_INFO', 'OBJECT' 
GO
ALTER TABLE dbo.USER_INFO ADD CONSTRAINT
	PK_USER_INFO PRIMARY KEY CLUSTERED 
	(
	USER_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.USER_INFO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.chartgrouptable ADD CONSTRAINT
	FK_chartgrouptable_USER_INFO FOREIGN KEY
	(
	UID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.chartgrouptable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
ALTER TABLE dbo.meetingtable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
ALTER TABLE dbo.GroupUser SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.GoodUser ADD CONSTRAINT
	FK_GoodUser_USER_INFO FOREIGN KEY
	(
	LUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GoodUser ADD CONSTRAINT
	FK_GoodUser_USER_INFO1 FOREIGN KEY
	(
	RUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GoodUser SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.GoodUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GoodUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GoodUser', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.invGoodUser ADD CONSTRAINT
	FK_invGoodUser_USER_INFO FOREIGN KEY
	(
	LUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.invGoodUser ADD CONSTRAINT
	FK_invGoodUser_USER_INFO1 FOREIGN KEY
	(
	RUID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.invGoodUser SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.invGoodUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.invGoodUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.invGoodUser', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CCBBS_TABLE ADD CONSTRAINT
	FK_CCBBS_TABLE_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CCBBS_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CCBBS_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CCBBS_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CCBBS_TABLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CTBBS_TABLE ADD CONSTRAINT
	FK_CTBBS_TABLE_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CTBBS_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CTBBS_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CTBBS_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CTBBS_TABLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.FILE_TABLE ADD CONSTRAINT
	FK_FILE_TABLE_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.FILE_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.FILE_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.FILE_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.FILE_TABLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SHAPE_TABLE ADD CONSTRAINT
	FK_SHAPE_TABLE_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SHAPE_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.BBSTITLE_TABLE ADD CONSTRAINT
	FK_BBSTITLE_TABLE_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BBSTITLE_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.BBSTITLE_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BBSTITLE_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BBSTITLE_TABLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.moduleBBS_Table ADD CONSTRAINT
	FK_moduleBBS_Table_USER_INFO FOREIGN KEY
	(
	U_ID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.moduleBBS_Table SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.moduleBBS_Table', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.moduleBBS_Table', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.moduleBBS_Table', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
ALTER TABLE dbo.multimessagepooltable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.multimessagepooltable', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.travetable ADD CONSTRAINT
	FK_travetable_USER_INFO FOREIGN KEY
	(
	UID
	) REFERENCES dbo.USER_INFO
	(
	USER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.travetable SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.travetable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'CONTROL') as Contr_Per 