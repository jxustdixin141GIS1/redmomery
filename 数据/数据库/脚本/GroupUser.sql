/*
   2017年8月26日16:16:05
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
CREATE TABLE dbo.Tmp_GroupUser
	(
	UID int NULL,
	GroupID int NULL,
	groupname nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_GroupUser SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.GroupUser)
	 EXEC('INSERT INTO dbo.Tmp_GroupUser (UID)
		SELECT UID FROM dbo.GroupUser WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.GroupUser
GO
EXECUTE sp_rename N'dbo.Tmp_GroupUser', N'GroupUser', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GroupUser', 'Object', 'CONTROL') as Contr_Per 