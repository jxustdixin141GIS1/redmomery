/*
   2017年9月3日17:53:43
   用户: chenzenghui
   服务器: USER-20160610ZA
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
ALTER TABLE dbo.trajectory
	DROP CONSTRAINT FK_trajectory_LB_INFO
GO
ALTER TABLE dbo.LB_INFO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.LB_INFO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_trajectory
	(
	ID int NOT NULL IDENTITY (1, 1),
	T_time datetime NULL,
	Timetext nvarchar(50) NULL,
	Local nvarchar(50) NULL,
	context ntext NULL,
	x nvarchar(50) NULL,
	y nvarchar(50) NULL,
	LBID int NULL,
	isCurrent int NULL,
	name nvarchar(50) NULL,
	location geometry NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_trajectory SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_trajectory ON
GO
IF EXISTS(SELECT * FROM dbo.trajectory)
	 EXEC('INSERT INTO dbo.Tmp_trajectory (ID, T_time, Timetext, Local, context, x, y, LBID, isCurrent, name, location)
		SELECT CONVERT(int, ID), T_time, Timetext, Local, context, x, y, LBID, isCurrent, name, location FROM dbo.trajectory WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_trajectory OFF
GO
DROP TABLE dbo.trajectory
GO
EXECUTE sp_rename N'dbo.Tmp_trajectory', N'trajectory', 'OBJECT' 
GO
ALTER TABLE dbo.trajectory ADD CONSTRAINT
	PK_trajectory PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.trajectory ADD CONSTRAINT
	FK_trajectory_LB_INFO FOREIGN KEY
	(
	LBID
	) REFERENCES dbo.LB_INFO
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.trajectory', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.trajectory', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.trajectory', 'Object', 'CONTROL') as Contr_Per 