/*
   2017年8月26日11:58:37
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
CREATE TABLE dbo.Tmp_travetable
	(
	ID int NOT NULL IDENTITY (1, 1),
	Utime datetime NULL,
	UID int NULL,
	meetID int NULL,
	context nvarchar(2000) NULL,
	local nvarchar(50) NULL,
	Vtime nvarchar(50) NULL,
	lng float(53) NULL,
	lat float(53) NULL,
	point geography NULL,
	isOK int NULL,
	nOK int NULL,
	nY int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_travetable SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_travetable ON
GO
IF EXISTS(SELECT * FROM dbo.travetable)
	 EXEC('INSERT INTO dbo.Tmp_travetable (ID, Utime, UID, meetID, context, local, Vtime, lng, lat, point, isOK, nOK, nY)
		SELECT ID, Utime, UID, meetID, context, local, Vtime, lng, lat, point, isOK, nOK, nY FROM dbo.travetable WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_travetable OFF
GO
DROP TABLE dbo.travetable
GO
EXECUTE sp_rename N'dbo.Tmp_travetable', N'travetable', 'OBJECT' 
GO
ALTER TABLE dbo.travetable ADD CONSTRAINT
	PK_travetable PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.travetable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.travetable', 'Object', 'CONTROL') as Contr_Per 