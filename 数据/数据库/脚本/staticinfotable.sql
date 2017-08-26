/*
   2017年8月16日15:47:16
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
CREATE TABLE dbo.Tmp_staticinfotable
	(
	ID int NOT NULL IDENTITY (1, 1),
	citycode nvarchar(50) NOT NULL,
	cityname nvarchar(50) NOT NULL,
	province nvarchar(50) NULL,
	country nvarchar(50) NULL,
	countrycode nvarchar(50) NULL,
	lng nvarchar(50) NULL,
	lat nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_staticinfotable SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_staticinfotable ON
GO
IF EXISTS(SELECT * FROM dbo.staticinfotable)
	 EXEC('INSERT INTO dbo.Tmp_staticinfotable (ID, citycode, cityname, province, country, countrycode, lng, lat)
		SELECT ID, citycode, cityname, province, country, countrycode, lng, lat FROM dbo.staticinfotable WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_staticinfotable OFF
GO
DROP TABLE dbo.staticinfotable
GO
EXECUTE sp_rename N'dbo.Tmp_staticinfotable', N'staticinfotable', 'OBJECT' 
GO
ALTER TABLE dbo.staticinfotable ADD CONSTRAINT
	PK_staticinfotable PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.staticinfotable', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.staticinfotable', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.staticinfotable', 'Object', 'CONTROL') as Contr_Per 