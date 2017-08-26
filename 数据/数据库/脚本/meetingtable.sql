/*
   2017年8月26日12:06:14
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
	 EXEC('INSERT INTO dbo.Tmp_meetingtable (ID, UID, Ttime, local, context, vnum, isCheck, lng, lat)
		SELECT ID, UID, Ttime, local, context, vnum, isCheck, lng, lat FROM dbo.meetingtable WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_meetingtable OFF
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
COMMIT
select Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.meetingtable', 'Object', 'CONTROL') as Contr_Per 