/*
   2017年8月26日16:14:57
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
CREATE TABLE dbo.Tmp_chartgrouptable
	(
	ID int NOT NULL IDENTITY (1, 1),
	UID int NULL,
	Ctime datetime NULL,
	description nvarchar(300) NULL,
	vnum int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_chartgrouptable SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_chartgrouptable ON
GO
IF EXISTS(SELECT * FROM dbo.chartgrouptable)
	 EXEC('INSERT INTO dbo.Tmp_chartgrouptable (ID, UID, Ctime, description, vnum)
		SELECT ID, UID, Ctime, description, vnum FROM dbo.chartgrouptable WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_chartgrouptable OFF
GO
DROP TABLE dbo.chartgrouptable
GO
EXECUTE sp_rename N'dbo.Tmp_chartgrouptable', N'chartgrouptable', 'OBJECT' 
GO
ALTER TABLE dbo.chartgrouptable ADD CONSTRAINT
	PK_chartgrouptable PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.chartgrouptable', 'Object', 'CONTROL') as Contr_Per 