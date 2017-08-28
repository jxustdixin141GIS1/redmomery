/*
   2017年8月28日17:06:12
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
ALTER TABLE dbo.track
	DROP CONSTRAINT DF_track_heroID
GO
CREATE TABLE dbo.Tmp_track
	(
	EID int NULL,
	Timetext nvarchar(100) NULL,
	Local nvarchar(255) NULL,
	context ntext NULL,
	x float(53) NULL,
	y float(53) NULL,
	heroID nvarchar(50) NULL,
	name nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_track SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_track ADD CONSTRAINT
	DF_track_heroID DEFAULT ((1)) FOR heroID
GO
IF EXISTS(SELECT * FROM dbo.track)
	 EXEC('INSERT INTO dbo.Tmp_track (EID, Timetext, Local, context, x, y, heroID, name)
		SELECT EID, Timetext, address, CONVERT(ntext, experience), x, y, heroID, name FROM dbo.track WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.track
GO
EXECUTE sp_rename N'dbo.Tmp_track', N'track', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.track', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.track', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.track', 'Object', 'CONTROL') as Contr_Per 