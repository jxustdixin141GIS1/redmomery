/*
   2017年7月14日2:14:46
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
CREATE TABLE dbo.SHAPE_TABLE
	(
	ID int NOT NULL,
	U_ID int NULL,
	TYPE int NULL,
	M_ID int NULL,
	T_ID int NULL,
	description text NULL,
	C_TIME datetime NULL,
	U_TIME datetime NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.SHAPE_TABLE ADD CONSTRAINT
	PK_SHAPE_TABLE PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SHAPE_TABLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SHAPE_TABLE', 'Object', 'CONTROL') as Contr_Per 