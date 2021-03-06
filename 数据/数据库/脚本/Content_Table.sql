/*
   2017年7月14日1:03:15
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
CREATE TABLE dbo.Tmp_Content_Table
	(
	ID bigint NOT NULL IDENTITY (1, 1),
	B_ID bigint NULL,
	U_ID bigint NULL,
	B_TITLE nvarchar(500) NULL,
	B_describe text NOT NULL,
	C_TIME datetime NULL,
	U_TIME datetime NULL,
	N_TITLE_T int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Content_Table SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Content_Table ON
GO
IF EXISTS(SELECT * FROM dbo.Content_Table)
	 EXEC('INSERT INTO dbo.Tmp_Content_Table (ID, B_ID, U_ID)
		SELECT ID, B_ID, U_ID FROM dbo.Content_Table WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Content_Table OFF
GO
DROP TABLE dbo.Content_Table
GO
EXECUTE sp_rename N'dbo.Tmp_Content_Table', N'Content_Table', 'OBJECT' 
GO
ALTER TABLE dbo.Content_Table ADD CONSTRAINT
	PK_Content_Table PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.Content_Table', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Content_Table', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Content_Table', 'Object', 'CONTROL') as Contr_Per 