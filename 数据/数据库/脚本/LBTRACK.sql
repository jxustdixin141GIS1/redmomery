/*
   2017年8月27日21:52:23
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
CREATE TABLE dbo.Tmp_LBTRACK
	(
	ID int NOT NULL,
	T_time datetime NULL,
	Timetext nvarchar(50) NULL,
	Local nvarchar(50) NULL,
	context nvarchar(MAX) NULL,
	x nvarchar(50) NULL,
	y nvarchar(50) NULL,
	LBID int NULL,
	isCurrent int NULL,
	name nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_LBTRACK SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.LBTRACK)
	 EXEC('INSERT INTO dbo.Tmp_LBTRACK (ID, T_time, Timetext, Local, context, x, y, LBID, isCurrent, name)
		SELECT CONVERT(int, ID), T_time, Timetext, Local, context, x, y, LBID, isCurrent, name FROM dbo.LBTRACK WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.LBTRACK
GO
EXECUTE sp_rename N'dbo.Tmp_LBTRACK', N'LBTRACK', 'OBJECT' 
GO
ALTER TABLE dbo.LBTRACK ADD CONSTRAINT
	PK__LBTRACK__F4B70D85833D3A4A PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.LBTRACK', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.LBTRACK', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.LBTRACK', 'Object', 'CONTROL') as Contr_Per 