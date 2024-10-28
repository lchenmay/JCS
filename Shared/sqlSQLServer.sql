USE [JCS]
-- [Ca_Address] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Address' AND xtype='U')

BEGIN

    CREATE TABLE Ca_Address ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Bind] BIGINT
        ,[AddressType] INT
        ,[Line1] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS
        ,[Line2] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS
        ,[State] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
        ,[County] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
        ,[Town] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
        ,[Contact] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Tel] NVARCHAR(20) COLLATE Chinese_PRC_CI_AS
        ,[Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Zip] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
        ,[City] BIGINT
        ,[Country] BIGINT
        ,[Remarks] NVARCHAR(MAX)
, CONSTRAINT [PK_Ca_Address] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_Address NVARCHAR(64)
DECLARE cursor_Ca_Address CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Bind','AddressType','Line1','Line2','State','County','Town','Contact','Tel','Email','Zip','City','Country','Remarks'))

OPEN cursor_Ca_Address
FETCH NEXT FROM cursor_Ca_Address INTO @name_Ca_Address

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_Address.' + @name_Ca_Address;
    
    DECLARE @sql_Ca_Address NVARCHAR(MAX);
    SET @sql_Ca_Address = 'ALTER TABLE Ca_Address DROP COLUMN ' + QUOTENAME(@name_Ca_Address)
    EXEC sp_executesql @sql_Ca_Address
    
    
    FETCH NEXT FROM cursor_Ca_Address INTO @name_Ca_Address
END

CLOSE cursor_Ca_Address;
DEALLOCATE cursor_Ca_Address;


-- [Ca_Address.Caption] -------------


-- [Ca_Address.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Caption = 'ALTER TABLE Ca_Address ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressCaption')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressCaption')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressCaption]
    END

-- [Ca_Address.Bind] -------------


-- [Ca_Address.Bind] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Bind')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Bind] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Bind NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Bind = 'ALTER TABLE Ca_Address ADD [Bind] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Address_Bind
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressBind')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressBind]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressBind')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressBind]
    END

-- [Ca_Address.AddressType] -------------


-- [Ca_Address.AddressType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='AddressType')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [AddressType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_AddressType NVARCHAR(MAX);
    SET @sql_add_Ca_Address_AddressType = 'ALTER TABLE Ca_Address ADD [AddressType] INT'
    EXEC sp_executesql @sql_add_Ca_Address_AddressType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressAddressType')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressAddressType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressAddressType')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressAddressType]
    END

-- [Ca_Address.Line1] -------------


-- [Ca_Address.Line1] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Line1')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Line1] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Line1 NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Line1 = 'ALTER TABLE Ca_Address ADD [Line1] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Line1
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressLine1')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressLine1]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressLine1')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressLine1]
    END

-- [Ca_Address.Line2] -------------


-- [Ca_Address.Line2] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Line2')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Line2] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Line2 NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Line2 = 'ALTER TABLE Ca_Address ADD [Line2] NVARCHAR(300) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Line2
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressLine2')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressLine2]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressLine2')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressLine2]
    END

-- [Ca_Address.State] -------------


-- [Ca_Address.State] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='State')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [State] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_State NVARCHAR(MAX);
    SET @sql_add_Ca_Address_State = 'ALTER TABLE Ca_Address ADD [State] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_State
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressState')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressState]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressState')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressState]
    END

-- [Ca_Address.County] -------------


-- [Ca_Address.County] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='County')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [County] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_County NVARCHAR(MAX);
    SET @sql_add_Ca_Address_County = 'ALTER TABLE Ca_Address ADD [County] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_County
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressCounty')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressCounty]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressCounty')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressCounty]
    END

-- [Ca_Address.Town] -------------


-- [Ca_Address.Town] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Town')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Town] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Town NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Town = 'ALTER TABLE Ca_Address ADD [Town] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Town
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressTown')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressTown]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressTown')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressTown]
    END

-- [Ca_Address.Contact] -------------


-- [Ca_Address.Contact] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Contact')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Contact] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Contact NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Contact = 'ALTER TABLE Ca_Address ADD [Contact] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Contact
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressContact')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressContact]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressContact')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressContact]
    END

-- [Ca_Address.Tel] -------------


-- [Ca_Address.Tel] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Tel')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Tel] NVARCHAR(20) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Tel NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Tel = 'ALTER TABLE Ca_Address ADD [Tel] NVARCHAR(20) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Tel
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressTel')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressTel]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressTel')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressTel]
    END

-- [Ca_Address.Email] -------------


-- [Ca_Address.Email] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Email')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Email NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Email = 'ALTER TABLE Ca_Address ADD [Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Email
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressEmail')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressEmail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressEmail')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressEmail]
    END

-- [Ca_Address.Zip] -------------


-- [Ca_Address.Zip] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Zip')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Zip] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Zip NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Zip = 'ALTER TABLE Ca_Address ADD [Zip] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Address_Zip
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressZip')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressZip]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressZip')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressZip]
    END

-- [Ca_Address.City] -------------


-- [Ca_Address.City] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='City')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [City] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_City NVARCHAR(MAX);
    SET @sql_add_Ca_Address_City = 'ALTER TABLE Ca_Address ADD [City] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Address_City
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressCity')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressCity]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressCity')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressCity]
    END

-- [Ca_Address.Country] -------------


-- [Ca_Address.Country] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Country')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Country] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Country NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Country = 'ALTER TABLE Ca_Address ADD [Country] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Address_Country
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressCountry')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressCountry]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressCountry')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressCountry]
    END

-- [Ca_Address.Remarks] -------------


-- [Ca_Address.Remarks] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Address') AND name='Remarks')
    BEGIN
     ALTER TABLE Ca_Address ALTER COLUMN [Remarks] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Address_Remarks NVARCHAR(MAX);
    SET @sql_add_Ca_Address_Remarks = 'ALTER TABLE Ca_Address ADD [Remarks] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_Address_Remarks
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_AddressRemarks')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [Constraint_Ca_AddressRemarks]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_AddressRemarks')
    BEGIN
    ALTER TABLE Ca_Address DROP  CONSTRAINT [UniqueNonclustered_Ca_AddressRemarks]
    END
-- [Ca_Biz] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Biz' AND xtype='U')

BEGIN

    CREATE TABLE Ca_Biz ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Parent] BIGINT
        ,[BasicAcct] BIGINT
        ,[DescTxt] NVARCHAR(MAX)
        ,[Website] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[City] BIGINT
        ,[Country] BIGINT
        ,[Lang] BIGINT
        ,[IsSocialPlatform] BIT
        ,[IsCmsSource] BIT
        ,[IsPayGateway] BIT
, CONSTRAINT [PK_Ca_Biz] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_Biz NVARCHAR(64)
DECLARE cursor_Ca_Biz CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption','Parent','BasicAcct','DescTxt','Website','Icon','City','Country','Lang','IsSocialPlatform','IsCmsSource','IsPayGateway'))

OPEN cursor_Ca_Biz
FETCH NEXT FROM cursor_Ca_Biz INTO @name_Ca_Biz

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_Biz.' + @name_Ca_Biz;
    
    DECLARE @sql_Ca_Biz NVARCHAR(MAX);
    SET @sql_Ca_Biz = 'ALTER TABLE Ca_Biz DROP COLUMN ' + QUOTENAME(@name_Ca_Biz)
    EXEC sp_executesql @sql_Ca_Biz
    
    
    FETCH NEXT FROM cursor_Ca_Biz INTO @name_Ca_Biz
END

CLOSE cursor_Ca_Biz;
DEALLOCATE cursor_Ca_Biz;


-- [Ca_Biz.Code] -------------


-- [Ca_Biz.Code] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Code')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Code NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Code = 'ALTER TABLE Ca_Biz ADD [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Biz_Code
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizCode')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizCode]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizCode')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizCode]
    END

-- [Ca_Biz.Caption] -------------


-- [Ca_Biz.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Caption = 'ALTER TABLE Ca_Biz ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Biz_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizCaption')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizCaption')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizCaption]
    END

-- [Ca_Biz.Parent] -------------


-- [Ca_Biz.Parent] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Parent')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Parent] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Parent NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Parent = 'ALTER TABLE Ca_Biz ADD [Parent] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Biz_Parent
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizParent')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizParent]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizParent')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizParent]
    END

-- [Ca_Biz.BasicAcct] -------------


-- [Ca_Biz.BasicAcct] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='BasicAcct')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [BasicAcct] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_BasicAcct NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_BasicAcct = 'ALTER TABLE Ca_Biz ADD [BasicAcct] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Biz_BasicAcct
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizBasicAcct')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizBasicAcct]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizBasicAcct')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizBasicAcct]
    END

-- [Ca_Biz.DescTxt] -------------


-- [Ca_Biz.DescTxt] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='DescTxt')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [DescTxt] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_DescTxt NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_DescTxt = 'ALTER TABLE Ca_Biz ADD [DescTxt] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_Biz_DescTxt
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizDescTxt')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizDescTxt]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizDescTxt')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizDescTxt]
    END

-- [Ca_Biz.Website] -------------


-- [Ca_Biz.Website] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Website')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Website] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Website NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Website = 'ALTER TABLE Ca_Biz ADD [Website] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Biz_Website
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizWebsite')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizWebsite]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizWebsite')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizWebsite]
    END

-- [Ca_Biz.Icon] -------------


-- [Ca_Biz.Icon] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Icon')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Icon NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Icon = 'ALTER TABLE Ca_Biz ADD [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Biz_Icon
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizIcon')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizIcon]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizIcon')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizIcon]
    END

-- [Ca_Biz.City] -------------


-- [Ca_Biz.City] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='City')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [City] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_City NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_City = 'ALTER TABLE Ca_Biz ADD [City] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Biz_City
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizCity')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizCity]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizCity')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizCity]
    END

-- [Ca_Biz.Country] -------------


-- [Ca_Biz.Country] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Country')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Country] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Country NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Country = 'ALTER TABLE Ca_Biz ADD [Country] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Biz_Country
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizCountry')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizCountry]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizCountry')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizCountry]
    END

-- [Ca_Biz.Lang] -------------


-- [Ca_Biz.Lang] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='Lang')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [Lang] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_Lang NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_Lang = 'ALTER TABLE Ca_Biz ADD [Lang] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Biz_Lang
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizLang')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizLang]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizLang')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizLang]
    END

-- [Ca_Biz.IsSocialPlatform] -------------


-- [Ca_Biz.IsSocialPlatform] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='IsSocialPlatform')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [IsSocialPlatform] BIT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_IsSocialPlatform NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_IsSocialPlatform = 'ALTER TABLE Ca_Biz ADD [IsSocialPlatform] BIT'
    EXEC sp_executesql @sql_add_Ca_Biz_IsSocialPlatform
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizIsSocialPlatform')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizIsSocialPlatform]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizIsSocialPlatform')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizIsSocialPlatform]
    END

-- [Ca_Biz.IsCmsSource] -------------


-- [Ca_Biz.IsCmsSource] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='IsCmsSource')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [IsCmsSource] BIT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_IsCmsSource NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_IsCmsSource = 'ALTER TABLE Ca_Biz ADD [IsCmsSource] BIT'
    EXEC sp_executesql @sql_add_Ca_Biz_IsCmsSource
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizIsCmsSource')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizIsCmsSource]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizIsCmsSource')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizIsCmsSource]
    END

-- [Ca_Biz.IsPayGateway] -------------


-- [Ca_Biz.IsPayGateway] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Biz') AND name='IsPayGateway')
    BEGIN
     ALTER TABLE Ca_Biz ALTER COLUMN [IsPayGateway] BIT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Biz_IsPayGateway NVARCHAR(MAX);
    SET @sql_add_Ca_Biz_IsPayGateway = 'ALTER TABLE Ca_Biz ADD [IsPayGateway] BIT'
    EXEC sp_executesql @sql_add_Ca_Biz_IsPayGateway
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BizIsPayGateway')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [Constraint_Ca_BizIsPayGateway]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BizIsPayGateway')
    BEGIN
    ALTER TABLE Ca_Biz DROP  CONSTRAINT [UniqueNonclustered_Ca_BizIsPayGateway]
    END
-- [Ca_Cat] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Cat' AND xtype='U')

BEGIN

    CREATE TABLE Ca_Cat ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Lang] BIGINT
        ,[Zh] BIGINT
        ,[Parent] BIGINT
        ,[CatState] INT
, CONSTRAINT [PK_Ca_Cat] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_Cat NVARCHAR(64)
DECLARE cursor_Ca_Cat CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Lang','Zh','Parent','CatState'))

OPEN cursor_Ca_Cat
FETCH NEXT FROM cursor_Ca_Cat INTO @name_Ca_Cat

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_Cat.' + @name_Ca_Cat;
    
    DECLARE @sql_Ca_Cat NVARCHAR(MAX);
    SET @sql_Ca_Cat = 'ALTER TABLE Ca_Cat DROP COLUMN ' + QUOTENAME(@name_Ca_Cat)
    EXEC sp_executesql @sql_Ca_Cat
    
    
    FETCH NEXT FROM cursor_Ca_Cat INTO @name_Ca_Cat
END

CLOSE cursor_Ca_Cat;
DEALLOCATE cursor_Ca_Cat;


-- [Ca_Cat.Caption] -------------


-- [Ca_Cat.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_Cat ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Cat_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_Cat_Caption = 'ALTER TABLE Ca_Cat ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Cat_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CatCaption')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [Constraint_Ca_CatCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CatCaption')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [UniqueNonclustered_Ca_CatCaption]
    END

-- [Ca_Cat.Lang] -------------


-- [Ca_Cat.Lang] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND name='Lang')
    BEGIN
     ALTER TABLE Ca_Cat ALTER COLUMN [Lang] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Cat_Lang NVARCHAR(MAX);
    SET @sql_add_Ca_Cat_Lang = 'ALTER TABLE Ca_Cat ADD [Lang] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Cat_Lang
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CatLang')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [Constraint_Ca_CatLang]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CatLang')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [UniqueNonclustered_Ca_CatLang]
    END

-- [Ca_Cat.Zh] -------------


-- [Ca_Cat.Zh] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND name='Zh')
    BEGIN
     ALTER TABLE Ca_Cat ALTER COLUMN [Zh] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Cat_Zh NVARCHAR(MAX);
    SET @sql_add_Ca_Cat_Zh = 'ALTER TABLE Ca_Cat ADD [Zh] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Cat_Zh
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CatZh')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [Constraint_Ca_CatZh]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CatZh')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [UniqueNonclustered_Ca_CatZh]
    END

-- [Ca_Cat.Parent] -------------


-- [Ca_Cat.Parent] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND name='Parent')
    BEGIN
     ALTER TABLE Ca_Cat ALTER COLUMN [Parent] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Cat_Parent NVARCHAR(MAX);
    SET @sql_add_Ca_Cat_Parent = 'ALTER TABLE Ca_Cat ADD [Parent] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Cat_Parent
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CatParent')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [Constraint_Ca_CatParent]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CatParent')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [UniqueNonclustered_Ca_CatParent]
    END

-- [Ca_Cat.CatState] -------------


-- [Ca_Cat.CatState] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Cat') AND name='CatState')
    BEGIN
     ALTER TABLE Ca_Cat ALTER COLUMN [CatState] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Cat_CatState NVARCHAR(MAX);
    SET @sql_add_Ca_Cat_CatState = 'ALTER TABLE Ca_Cat ADD [CatState] INT'
    EXEC sp_executesql @sql_add_Ca_Cat_CatState
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CatCatState')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [Constraint_Ca_CatCatState]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CatCatState')
    BEGIN
    ALTER TABLE Ca_Cat DROP  CONSTRAINT [UniqueNonclustered_Ca_CatCatState]
    END
-- [Ca_City] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_City' AND xtype='U')

BEGIN

    CREATE TABLE Ca_City ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Fullname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[MetropolitanCode3IATA] NVARCHAR(3) COLLATE Chinese_PRC_CI_AS
        ,[NameEn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Country] BIGINT
        ,[Place] BIGINT
        ,[Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
, CONSTRAINT [PK_Ca_City] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_City NVARCHAR(64)
DECLARE cursor_Ca_City CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Fullname','MetropolitanCode3IATA','NameEn','Country','Place','Icon','Tel'))

OPEN cursor_Ca_City
FETCH NEXT FROM cursor_Ca_City INTO @name_Ca_City

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_City.' + @name_Ca_City;
    
    DECLARE @sql_Ca_City NVARCHAR(MAX);
    SET @sql_Ca_City = 'ALTER TABLE Ca_City DROP COLUMN ' + QUOTENAME(@name_Ca_City)
    EXEC sp_executesql @sql_Ca_City
    
    
    FETCH NEXT FROM cursor_Ca_City INTO @name_Ca_City
END

CLOSE cursor_Ca_City;
DEALLOCATE cursor_Ca_City;


-- [Ca_City.Fullname] -------------


-- [Ca_City.Fullname] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='Fullname')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [Fullname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_Fullname NVARCHAR(MAX);
    SET @sql_add_Ca_City_Fullname = 'ALTER TABLE Ca_City ADD [Fullname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_City_Fullname
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityFullname')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityFullname]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityFullname')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityFullname]
    END

-- [Ca_City.MetropolitanCode3IATA] -------------


-- [Ca_City.MetropolitanCode3IATA] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='MetropolitanCode3IATA')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [MetropolitanCode3IATA] NVARCHAR(3) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_MetropolitanCode3IATA NVARCHAR(MAX);
    SET @sql_add_Ca_City_MetropolitanCode3IATA = 'ALTER TABLE Ca_City ADD [MetropolitanCode3IATA] NVARCHAR(3) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_City_MetropolitanCode3IATA
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityMetropolitanCode3IATA')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityMetropolitanCode3IATA]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityMetropolitanCode3IATA')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityMetropolitanCode3IATA]
    END

-- [Ca_City.NameEn] -------------


-- [Ca_City.NameEn] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='NameEn')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [NameEn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_NameEn NVARCHAR(MAX);
    SET @sql_add_Ca_City_NameEn = 'ALTER TABLE Ca_City ADD [NameEn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_City_NameEn
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityNameEn')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityNameEn]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityNameEn')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityNameEn]
    END

-- [Ca_City.Country] -------------


-- [Ca_City.Country] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='Country')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [Country] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_Country NVARCHAR(MAX);
    SET @sql_add_Ca_City_Country = 'ALTER TABLE Ca_City ADD [Country] BIGINT'
    EXEC sp_executesql @sql_add_Ca_City_Country
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityCountry')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityCountry]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityCountry')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityCountry]
    END

-- [Ca_City.Place] -------------


-- [Ca_City.Place] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='Place')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [Place] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_Place NVARCHAR(MAX);
    SET @sql_add_Ca_City_Place = 'ALTER TABLE Ca_City ADD [Place] BIGINT'
    EXEC sp_executesql @sql_add_Ca_City_Place
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityPlace')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityPlace]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityPlace')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityPlace]
    END

-- [Ca_City.Icon] -------------


-- [Ca_City.Icon] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='Icon')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_Icon NVARCHAR(MAX);
    SET @sql_add_Ca_City_Icon = 'ALTER TABLE Ca_City ADD [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_City_Icon
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityIcon')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityIcon]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityIcon')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityIcon]
    END

-- [Ca_City.Tel] -------------


-- [Ca_City.Tel] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_City') AND name='Tel')
    BEGIN
     ALTER TABLE Ca_City ALTER COLUMN [Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_City_Tel NVARCHAR(MAX);
    SET @sql_add_Ca_City_Tel = 'ALTER TABLE Ca_City ADD [Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_City_Tel
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CityTel')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [Constraint_Ca_CityTel]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CityTel')
    BEGIN
    ALTER TABLE Ca_City DROP  CONSTRAINT [UniqueNonclustered_Ca_CityTel]
    END
-- [Ca_Country] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Country' AND xtype='U')

BEGIN

    CREATE TABLE Ca_Country ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code2] NVARCHAR(2) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Fullname] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
        ,[Cur] BIGINT
        ,[Capital] BIGINT
        ,[Place] BIGINT
        ,[Lang] BIGINT
, CONSTRAINT [PK_Ca_Country] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_Country NVARCHAR(64)
DECLARE cursor_Ca_Country CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code2','Caption','Fullname','Icon','Tel','Cur','Capital','Place','Lang'))

OPEN cursor_Ca_Country
FETCH NEXT FROM cursor_Ca_Country INTO @name_Ca_Country

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_Country.' + @name_Ca_Country;
    
    DECLARE @sql_Ca_Country NVARCHAR(MAX);
    SET @sql_Ca_Country = 'ALTER TABLE Ca_Country DROP COLUMN ' + QUOTENAME(@name_Ca_Country)
    EXEC sp_executesql @sql_Ca_Country
    
    
    FETCH NEXT FROM cursor_Ca_Country INTO @name_Ca_Country
END

CLOSE cursor_Ca_Country;
DEALLOCATE cursor_Ca_Country;


-- [Ca_Country.Code2] -------------


-- [Ca_Country.Code2] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Code2')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Code2] NVARCHAR(2) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Code2 NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Code2 = 'ALTER TABLE Ca_Country ADD [Code2] NVARCHAR(2) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Country_Code2
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryCode2')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryCode2]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryCode2')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryCode2]
    END

-- [Ca_Country.Caption] -------------


-- [Ca_Country.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Caption = 'ALTER TABLE Ca_Country ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Country_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryCaption')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryCaption')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryCaption]
    END

-- [Ca_Country.Fullname] -------------


-- [Ca_Country.Fullname] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Fullname')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Fullname] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Fullname NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Fullname = 'ALTER TABLE Ca_Country ADD [Fullname] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Country_Fullname
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryFullname')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryFullname]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryFullname')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryFullname]
    END

-- [Ca_Country.Icon] -------------


-- [Ca_Country.Icon] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Icon')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Icon NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Icon = 'ALTER TABLE Ca_Country ADD [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Country_Icon
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryIcon')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryIcon]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryIcon')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryIcon]
    END

-- [Ca_Country.Tel] -------------


-- [Ca_Country.Tel] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Tel')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Tel NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Tel = 'ALTER TABLE Ca_Country ADD [Tel] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Country_Tel
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryTel')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryTel]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryTel')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryTel]
    END

-- [Ca_Country.Cur] -------------


-- [Ca_Country.Cur] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Cur')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Cur] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Cur NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Cur = 'ALTER TABLE Ca_Country ADD [Cur] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Country_Cur
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryCur')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryCur]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryCur')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryCur]
    END

-- [Ca_Country.Capital] -------------


-- [Ca_Country.Capital] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Capital')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Capital] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Capital NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Capital = 'ALTER TABLE Ca_Country ADD [Capital] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Country_Capital
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryCapital')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryCapital]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryCapital')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryCapital]
    END

-- [Ca_Country.Place] -------------


-- [Ca_Country.Place] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Place')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Place] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Place NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Place = 'ALTER TABLE Ca_Country ADD [Place] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Country_Place
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryPlace')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryPlace]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryPlace')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryPlace]
    END

-- [Ca_Country.Lang] -------------


-- [Ca_Country.Lang] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Country') AND name='Lang')
    BEGIN
     ALTER TABLE Ca_Country ALTER COLUMN [Lang] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Country_Lang NVARCHAR(MAX);
    SET @sql_add_Ca_Country_Lang = 'ALTER TABLE Ca_Country ADD [Lang] BIGINT'
    EXEC sp_executesql @sql_add_Ca_Country_Lang
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_CountryLang')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [Constraint_Ca_CountryLang]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_CountryLang')
    BEGIN
    ALTER TABLE Ca_Country DROP  CONSTRAINT [UniqueNonclustered_Ca_CountryLang]
    END
-- [Ca_EndUser] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_EndUser' AND xtype='U')

BEGIN

    CREATE TABLE Ca_EndUser ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Username] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[SocialAuthBiz] BIGINT
        ,[SocialAuthId] NVARCHAR(MAX)
        ,[SocialAuthAvatar] NVARCHAR(MAX)
        ,[Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Tel] NVARCHAR(32) COLLATE Chinese_PRC_CI_AS
        ,[Gender] INT
        ,[Status] INT
        ,[Admin] INT
        ,[BizPartner] INT
        ,[Privilege] BIGINT
        ,[Verify] INT
        ,[Pwd] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
        ,[Online] BIT
        ,[Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Background] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[BasicAcct] BIGINT
        ,[Citizen] BIGINT
        ,[Refer] NVARCHAR(9) COLLATE Chinese_PRC_CI_AS
        ,[Referer] BIGINT
        ,[Url] NVARCHAR(MAX)
        ,[About] NVARCHAR(MAX)
, CONSTRAINT [PK_Ca_EndUser] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_EndUser NVARCHAR(64)
DECLARE cursor_Ca_EndUser CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Username','SocialAuthBiz','SocialAuthId','SocialAuthAvatar','Email','Tel','Gender','Status','Admin','BizPartner','Privilege','Verify','Pwd','Online','Icon','Background','BasicAcct','Citizen','Refer','Referer','Url','About'))

OPEN cursor_Ca_EndUser
FETCH NEXT FROM cursor_Ca_EndUser INTO @name_Ca_EndUser

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_EndUser.' + @name_Ca_EndUser;
    
    DECLARE @sql_Ca_EndUser NVARCHAR(MAX);
    SET @sql_Ca_EndUser = 'ALTER TABLE Ca_EndUser DROP COLUMN ' + QUOTENAME(@name_Ca_EndUser)
    EXEC sp_executesql @sql_Ca_EndUser
    
    
    FETCH NEXT FROM cursor_Ca_EndUser INTO @name_Ca_EndUser
END

CLOSE cursor_Ca_EndUser;
DEALLOCATE cursor_Ca_EndUser;


-- [Ca_EndUser.Caption] -------------


-- [Ca_EndUser.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Caption = 'ALTER TABLE Ca_EndUser ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserCaption')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserCaption')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserCaption]
    END

-- [Ca_EndUser.Username] -------------


-- [Ca_EndUser.Username] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Username')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Username] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Username NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Username = 'ALTER TABLE Ca_EndUser ADD [Username] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Username
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserUsername')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserUsername]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserUsername')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserUsername]
    END

-- [Ca_EndUser.SocialAuthBiz] -------------


-- [Ca_EndUser.SocialAuthBiz] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='SocialAuthBiz')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [SocialAuthBiz] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_SocialAuthBiz NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_SocialAuthBiz = 'ALTER TABLE Ca_EndUser ADD [SocialAuthBiz] BIGINT'
    EXEC sp_executesql @sql_add_Ca_EndUser_SocialAuthBiz
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserSocialAuthBiz')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserSocialAuthBiz]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserSocialAuthBiz')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserSocialAuthBiz]
    END

-- [Ca_EndUser.SocialAuthId] -------------


-- [Ca_EndUser.SocialAuthId] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='SocialAuthId')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [SocialAuthId] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_SocialAuthId NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_SocialAuthId = 'ALTER TABLE Ca_EndUser ADD [SocialAuthId] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_EndUser_SocialAuthId
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserSocialAuthId')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserSocialAuthId]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserSocialAuthId')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserSocialAuthId]
    END

-- [Ca_EndUser.SocialAuthAvatar] -------------


-- [Ca_EndUser.SocialAuthAvatar] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='SocialAuthAvatar')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [SocialAuthAvatar] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_SocialAuthAvatar NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_SocialAuthAvatar = 'ALTER TABLE Ca_EndUser ADD [SocialAuthAvatar] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_EndUser_SocialAuthAvatar
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserSocialAuthAvatar')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserSocialAuthAvatar]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserSocialAuthAvatar')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserSocialAuthAvatar]
    END

-- [Ca_EndUser.Email] -------------


-- [Ca_EndUser.Email] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Email')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Email NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Email = 'ALTER TABLE Ca_EndUser ADD [Email] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Email
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserEmail')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserEmail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserEmail')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserEmail]
    END

-- [Ca_EndUser.Tel] -------------


-- [Ca_EndUser.Tel] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Tel')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Tel] NVARCHAR(32) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Tel NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Tel = 'ALTER TABLE Ca_EndUser ADD [Tel] NVARCHAR(32) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Tel
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserTel')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserTel]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserTel')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserTel]
    END

-- [Ca_EndUser.Gender] -------------


-- [Ca_EndUser.Gender] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Gender')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Gender] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Gender NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Gender = 'ALTER TABLE Ca_EndUser ADD [Gender] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Gender
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserGender')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserGender]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserGender')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserGender]
    END

-- [Ca_EndUser.Status] -------------


-- [Ca_EndUser.Status] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Status')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Status] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Status NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Status = 'ALTER TABLE Ca_EndUser ADD [Status] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Status
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserStatus')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserStatus]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserStatus')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserStatus]
    END

-- [Ca_EndUser.Admin] -------------


-- [Ca_EndUser.Admin] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Admin')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Admin] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Admin NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Admin = 'ALTER TABLE Ca_EndUser ADD [Admin] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Admin
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserAdmin')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserAdmin]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserAdmin')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserAdmin]
    END

-- [Ca_EndUser.BizPartner] -------------


-- [Ca_EndUser.BizPartner] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='BizPartner')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [BizPartner] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_BizPartner NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_BizPartner = 'ALTER TABLE Ca_EndUser ADD [BizPartner] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_BizPartner
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserBizPartner')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserBizPartner]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserBizPartner')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserBizPartner]
    END

-- [Ca_EndUser.Privilege] -------------


-- [Ca_EndUser.Privilege] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Privilege')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Privilege] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Privilege NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Privilege = 'ALTER TABLE Ca_EndUser ADD [Privilege] BIGINT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Privilege
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserPrivilege')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserPrivilege]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserPrivilege')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserPrivilege]
    END

-- [Ca_EndUser.Verify] -------------


-- [Ca_EndUser.Verify] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Verify')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Verify] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Verify NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Verify = 'ALTER TABLE Ca_EndUser ADD [Verify] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Verify
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserVerify')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserVerify]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserVerify')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserVerify]
    END

-- [Ca_EndUser.Pwd] -------------


-- [Ca_EndUser.Pwd] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Pwd')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Pwd] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Pwd NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Pwd = 'ALTER TABLE Ca_EndUser ADD [Pwd] NVARCHAR(16) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Pwd
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserPwd')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserPwd]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserPwd')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserPwd]
    END

-- [Ca_EndUser.Online] -------------


-- [Ca_EndUser.Online] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Online')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Online] BIT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Online NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Online = 'ALTER TABLE Ca_EndUser ADD [Online] BIT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Online
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserOnline')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserOnline]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserOnline')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserOnline]
    END

-- [Ca_EndUser.Icon] -------------


-- [Ca_EndUser.Icon] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Icon')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Icon NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Icon = 'ALTER TABLE Ca_EndUser ADD [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Icon
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserIcon')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserIcon]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserIcon')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserIcon]
    END

-- [Ca_EndUser.Background] -------------


-- [Ca_EndUser.Background] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Background')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Background] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Background NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Background = 'ALTER TABLE Ca_EndUser ADD [Background] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Background
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserBackground')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserBackground]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserBackground')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserBackground]
    END

-- [Ca_EndUser.BasicAcct] -------------


-- [Ca_EndUser.BasicAcct] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='BasicAcct')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [BasicAcct] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_BasicAcct NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_BasicAcct = 'ALTER TABLE Ca_EndUser ADD [BasicAcct] BIGINT'
    EXEC sp_executesql @sql_add_Ca_EndUser_BasicAcct
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserBasicAcct')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserBasicAcct]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserBasicAcct')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserBasicAcct]
    END

-- [Ca_EndUser.Citizen] -------------


-- [Ca_EndUser.Citizen] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Citizen')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Citizen] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Citizen NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Citizen = 'ALTER TABLE Ca_EndUser ADD [Citizen] BIGINT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Citizen
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserCitizen')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserCitizen]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserCitizen')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserCitizen]
    END

-- [Ca_EndUser.Refer] -------------


-- [Ca_EndUser.Refer] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Refer')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Refer] NVARCHAR(9) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Refer NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Refer = 'ALTER TABLE Ca_EndUser ADD [Refer] NVARCHAR(9) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_EndUser_Refer
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserRefer')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserRefer]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserRefer')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserRefer]
    END

-- [Ca_EndUser.Referer] -------------


-- [Ca_EndUser.Referer] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Referer')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Referer] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Referer NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Referer = 'ALTER TABLE Ca_EndUser ADD [Referer] BIGINT'
    EXEC sp_executesql @sql_add_Ca_EndUser_Referer
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserReferer')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserReferer]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserReferer')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserReferer]
    END

-- [Ca_EndUser.Url] -------------


-- [Ca_EndUser.Url] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='Url')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [Url] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_Url NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_Url = 'ALTER TABLE Ca_EndUser ADD [Url] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_EndUser_Url
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserUrl')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserUrl]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserUrl')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserUrl]
    END

-- [Ca_EndUser.About] -------------


-- [Ca_EndUser.About] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='About')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [About] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_About NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_About = 'ALTER TABLE Ca_EndUser ADD [About] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_EndUser_About
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserAbout')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserAbout]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserAbout')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserAbout]
    END
-- [Ca_SpecialItem] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_SpecialItem' AND xtype='U')

BEGIN

    CREATE TABLE Ca_SpecialItem ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Type] INT
        ,[Lang] BIGINT
        ,[Bind] BIGINT
, CONSTRAINT [PK_Ca_SpecialItem] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_SpecialItem NVARCHAR(64)
DECLARE cursor_Ca_SpecialItem CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_SpecialItem') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Type','Lang','Bind'))

OPEN cursor_Ca_SpecialItem
FETCH NEXT FROM cursor_Ca_SpecialItem INTO @name_Ca_SpecialItem

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_SpecialItem.' + @name_Ca_SpecialItem;
    
    DECLARE @sql_Ca_SpecialItem NVARCHAR(MAX);
    SET @sql_Ca_SpecialItem = 'ALTER TABLE Ca_SpecialItem DROP COLUMN ' + QUOTENAME(@name_Ca_SpecialItem)
    EXEC sp_executesql @sql_Ca_SpecialItem
    
    
    FETCH NEXT FROM cursor_Ca_SpecialItem INTO @name_Ca_SpecialItem
END

CLOSE cursor_Ca_SpecialItem;
DEALLOCATE cursor_Ca_SpecialItem;


-- [Ca_SpecialItem.Type] -------------


-- [Ca_SpecialItem.Type] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_SpecialItem') AND name='Type')
    BEGIN
     ALTER TABLE Ca_SpecialItem ALTER COLUMN [Type] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_SpecialItem_Type NVARCHAR(MAX);
    SET @sql_add_Ca_SpecialItem_Type = 'ALTER TABLE Ca_SpecialItem ADD [Type] INT'
    EXEC sp_executesql @sql_add_Ca_SpecialItem_Type
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_SpecialItemType')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [Constraint_Ca_SpecialItemType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_SpecialItemType')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [UniqueNonclustered_Ca_SpecialItemType]
    END

-- [Ca_SpecialItem.Lang] -------------


-- [Ca_SpecialItem.Lang] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_SpecialItem') AND name='Lang')
    BEGIN
     ALTER TABLE Ca_SpecialItem ALTER COLUMN [Lang] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_SpecialItem_Lang NVARCHAR(MAX);
    SET @sql_add_Ca_SpecialItem_Lang = 'ALTER TABLE Ca_SpecialItem ADD [Lang] BIGINT'
    EXEC sp_executesql @sql_add_Ca_SpecialItem_Lang
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_SpecialItemLang')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [Constraint_Ca_SpecialItemLang]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_SpecialItemLang')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [UniqueNonclustered_Ca_SpecialItemLang]
    END

-- [Ca_SpecialItem.Bind] -------------


-- [Ca_SpecialItem.Bind] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_SpecialItem') AND name='Bind')
    BEGIN
     ALTER TABLE Ca_SpecialItem ALTER COLUMN [Bind] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_SpecialItem_Bind NVARCHAR(MAX);
    SET @sql_add_Ca_SpecialItem_Bind = 'ALTER TABLE Ca_SpecialItem ADD [Bind] BIGINT'
    EXEC sp_executesql @sql_add_Ca_SpecialItem_Bind
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_SpecialItemBind')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [Constraint_Ca_SpecialItemBind]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_SpecialItemBind')
    BEGIN
    ALTER TABLE Ca_SpecialItem DROP  CONSTRAINT [UniqueNonclustered_Ca_SpecialItemBind]
    END
-- [Ca_WebCredential] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_WebCredential' AND xtype='U')

BEGIN

    CREATE TABLE Ca_WebCredential ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[ExternalId] BIGINT
        ,[Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[EU] BIGINT
        ,[Biz] BIGINT
        ,[Json] NVARCHAR(MAX)
, CONSTRAINT [PK_Ca_WebCredential] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_WebCredential NVARCHAR(64)
DECLARE cursor_Ca_WebCredential CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','ExternalId','Icon','EU','Biz','Json'))

OPEN cursor_Ca_WebCredential
FETCH NEXT FROM cursor_Ca_WebCredential INTO @name_Ca_WebCredential

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_WebCredential.' + @name_Ca_WebCredential;
    
    DECLARE @sql_Ca_WebCredential NVARCHAR(MAX);
    SET @sql_Ca_WebCredential = 'ALTER TABLE Ca_WebCredential DROP COLUMN ' + QUOTENAME(@name_Ca_WebCredential)
    EXEC sp_executesql @sql_Ca_WebCredential
    
    
    FETCH NEXT FROM cursor_Ca_WebCredential INTO @name_Ca_WebCredential
END

CLOSE cursor_Ca_WebCredential;
DEALLOCATE cursor_Ca_WebCredential;


-- [Ca_WebCredential.Caption] -------------


-- [Ca_WebCredential.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_Caption = 'ALTER TABLE Ca_WebCredential ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_WebCredential_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialCaption')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialCaption')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialCaption]
    END

-- [Ca_WebCredential.ExternalId] -------------


-- [Ca_WebCredential.ExternalId] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='ExternalId')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [ExternalId] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_ExternalId NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_ExternalId = 'ALTER TABLE Ca_WebCredential ADD [ExternalId] BIGINT'
    EXEC sp_executesql @sql_add_Ca_WebCredential_ExternalId
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialExternalId')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialExternalId]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialExternalId')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialExternalId]
    END

-- [Ca_WebCredential.Icon] -------------


-- [Ca_WebCredential.Icon] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='Icon')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_Icon NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_Icon = 'ALTER TABLE Ca_WebCredential ADD [Icon] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_WebCredential_Icon
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialIcon')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialIcon]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialIcon')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialIcon]
    END

-- [Ca_WebCredential.EU] -------------


-- [Ca_WebCredential.EU] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='EU')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [EU] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_EU NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_EU = 'ALTER TABLE Ca_WebCredential ADD [EU] BIGINT'
    EXEC sp_executesql @sql_add_Ca_WebCredential_EU
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialEU')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialEU]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialEU')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialEU]
    END

-- [Ca_WebCredential.Biz] -------------


-- [Ca_WebCredential.Biz] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='Biz')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [Biz] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_Biz NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_Biz = 'ALTER TABLE Ca_WebCredential ADD [Biz] BIGINT'
    EXEC sp_executesql @sql_add_Ca_WebCredential_Biz
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialBiz')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialBiz]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialBiz')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialBiz]
    END

-- [Ca_WebCredential.Json] -------------


-- [Ca_WebCredential.Json] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_WebCredential') AND name='Json')
    BEGIN
     ALTER TABLE Ca_WebCredential ALTER COLUMN [Json] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_WebCredential_Json NVARCHAR(MAX);
    SET @sql_add_Ca_WebCredential_Json = 'ALTER TABLE Ca_WebCredential ADD [Json] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_WebCredential_Json
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_WebCredentialJson')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [Constraint_Ca_WebCredentialJson]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_WebCredentialJson')
    BEGIN
    ALTER TABLE Ca_WebCredential DROP  CONSTRAINT [UniqueNonclustered_Ca_WebCredentialJson]
    END
-- [Ts_Api] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Api' AND xtype='U')

BEGIN

    CREATE TABLE Ts_Api ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_Api] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Api NVARCHAR(64)
DECLARE cursor_Ts_Api CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Api') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Project'))

OPEN cursor_Ts_Api
FETCH NEXT FROM cursor_Ts_Api INTO @name_Ts_Api

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_Api.' + @name_Ts_Api;
    
    DECLARE @sql_Ts_Api NVARCHAR(MAX);
    SET @sql_Ts_Api = 'ALTER TABLE Ts_Api DROP COLUMN ' + QUOTENAME(@name_Ts_Api)
    EXEC sp_executesql @sql_Ts_Api
    
    
    FETCH NEXT FROM cursor_Ts_Api INTO @name_Ts_Api
END

CLOSE cursor_Ts_Api;
DEALLOCATE cursor_Ts_Api;


-- [Ts_Api.Name] -------------


-- [Ts_Api.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Api') AND name='Name')
    BEGIN
     ALTER TABLE Ts_Api ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Api_Name NVARCHAR(MAX);
    SET @sql_add_Ts_Api_Name = 'ALTER TABLE Ts_Api ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Api_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_ApiName')
    BEGIN
    ALTER TABLE Ts_Api DROP  CONSTRAINT [Constraint_Ts_ApiName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_ApiName')
    BEGIN
    ALTER TABLE Ts_Api DROP  CONSTRAINT [UniqueNonclustered_Ts_ApiName]
    END

-- [Ts_Api.Project] -------------


-- [Ts_Api.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Api') AND name='Project')
    BEGIN
     ALTER TABLE Ts_Api ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Api_Project NVARCHAR(MAX);
    SET @sql_add_Ts_Api_Project = 'ALTER TABLE Ts_Api ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_Api_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_ApiProject')
    BEGIN
    ALTER TABLE Ts_Api DROP  CONSTRAINT [Constraint_Ts_ApiProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_ApiProject')
    BEGIN
    ALTER TABLE Ts_Api DROP  CONSTRAINT [UniqueNonclustered_Ts_ApiProject]
    END
-- [Ts_Field] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Field' AND xtype='U')

BEGIN

    CREATE TABLE Ts_Field ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Desc] NVARCHAR(MAX)
        ,[FieldType] INT
        ,[Length] BIGINT
        ,[SelectLines] NVARCHAR(MAX)
        ,[Project] BIGINT
        ,[Table] BIGINT
, CONSTRAINT [PK_Ts_Field] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Field NVARCHAR(64)
DECLARE cursor_Ts_Field CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc','FieldType','Length','SelectLines','Project','Table'))

OPEN cursor_Ts_Field
FETCH NEXT FROM cursor_Ts_Field INTO @name_Ts_Field

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_Field.' + @name_Ts_Field;
    
    DECLARE @sql_Ts_Field NVARCHAR(MAX);
    SET @sql_Ts_Field = 'ALTER TABLE Ts_Field DROP COLUMN ' + QUOTENAME(@name_Ts_Field)
    EXEC sp_executesql @sql_Ts_Field
    
    
    FETCH NEXT FROM cursor_Ts_Field INTO @name_Ts_Field
END

CLOSE cursor_Ts_Field;
DEALLOCATE cursor_Ts_Field;


-- [Ts_Field.Name] -------------


-- [Ts_Field.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='Name')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_Name NVARCHAR(MAX);
    SET @sql_add_Ts_Field_Name = 'ALTER TABLE Ts_Field ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Field_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldName')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldName')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldName]
    END

-- [Ts_Field.Desc] -------------


-- [Ts_Field.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='Desc')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_Desc NVARCHAR(MAX);
    SET @sql_add_Ts_Field_Desc = 'ALTER TABLE Ts_Field ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_Field_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldDesc')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldDesc')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldDesc]
    END

-- [Ts_Field.FieldType] -------------


-- [Ts_Field.FieldType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='FieldType')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [FieldType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_FieldType NVARCHAR(MAX);
    SET @sql_add_Ts_Field_FieldType = 'ALTER TABLE Ts_Field ADD [FieldType] INT'
    EXEC sp_executesql @sql_add_Ts_Field_FieldType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldFieldType')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldFieldType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldFieldType')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldFieldType]
    END

-- [Ts_Field.Length] -------------


-- [Ts_Field.Length] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='Length')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [Length] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_Length NVARCHAR(MAX);
    SET @sql_add_Ts_Field_Length = 'ALTER TABLE Ts_Field ADD [Length] BIGINT'
    EXEC sp_executesql @sql_add_Ts_Field_Length
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldLength')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldLength]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldLength')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldLength]
    END

-- [Ts_Field.SelectLines] -------------


-- [Ts_Field.SelectLines] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='SelectLines')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [SelectLines] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_SelectLines NVARCHAR(MAX);
    SET @sql_add_Ts_Field_SelectLines = 'ALTER TABLE Ts_Field ADD [SelectLines] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_Field_SelectLines
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldSelectLines')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldSelectLines]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldSelectLines')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldSelectLines]
    END

-- [Ts_Field.Project] -------------


-- [Ts_Field.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='Project')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_Project NVARCHAR(MAX);
    SET @sql_add_Ts_Field_Project = 'ALTER TABLE Ts_Field ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_Field_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldProject')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldProject')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldProject]
    END

-- [Ts_Field.Table] -------------


-- [Ts_Field.Table] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND name='Table')
    BEGIN
     ALTER TABLE Ts_Field ALTER COLUMN [Table] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Field_Table NVARCHAR(MAX);
    SET @sql_add_Ts_Field_Table = 'ALTER TABLE Ts_Field ADD [Table] BIGINT'
    EXEC sp_executesql @sql_add_Ts_Field_Table
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_FieldTable')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [Constraint_Ts_FieldTable]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_FieldTable')
    BEGIN
    ALTER TABLE Ts_Field DROP  CONSTRAINT [UniqueNonclustered_Ts_FieldTable]
    END
-- [Ts_HostConfig] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_HostConfig' AND xtype='U')

BEGIN

    CREATE TABLE Ts_HostConfig ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Hostname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Database] INT
        ,[DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DirVs] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_HostConfig] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_HostConfig NVARCHAR(64)
DECLARE cursor_Ts_HostConfig CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Hostname','Database','DatabaseName','DatabaseConn','DirVs','DirVsCodeWeb','Project'))

OPEN cursor_Ts_HostConfig
FETCH NEXT FROM cursor_Ts_HostConfig INTO @name_Ts_HostConfig

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_HostConfig.' + @name_Ts_HostConfig;
    
    DECLARE @sql_Ts_HostConfig NVARCHAR(MAX);
    SET @sql_Ts_HostConfig = 'ALTER TABLE Ts_HostConfig DROP COLUMN ' + QUOTENAME(@name_Ts_HostConfig)
    EXEC sp_executesql @sql_Ts_HostConfig
    
    
    FETCH NEXT FROM cursor_Ts_HostConfig INTO @name_Ts_HostConfig
END

CLOSE cursor_Ts_HostConfig;
DEALLOCATE cursor_Ts_HostConfig;


-- [Ts_HostConfig.Hostname] -------------


-- [Ts_HostConfig.Hostname] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='Hostname')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [Hostname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_Hostname NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_Hostname = 'ALTER TABLE Ts_HostConfig ADD [Hostname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_Hostname
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigHostname')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigHostname]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigHostname')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigHostname]
    END

-- [Ts_HostConfig.Database] -------------


-- [Ts_HostConfig.Database] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='Database')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [Database] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_Database NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_Database = 'ALTER TABLE Ts_HostConfig ADD [Database] INT'
    EXEC sp_executesql @sql_add_Ts_HostConfig_Database
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDatabase')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDatabase]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDatabase')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDatabase]
    END

-- [Ts_HostConfig.DatabaseName] -------------


-- [Ts_HostConfig.DatabaseName] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='DatabaseName')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_DatabaseName NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_DatabaseName = 'ALTER TABLE Ts_HostConfig ADD [DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_DatabaseName
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDatabaseName')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDatabaseName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDatabaseName')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDatabaseName]
    END

-- [Ts_HostConfig.DatabaseConn] -------------


-- [Ts_HostConfig.DatabaseConn] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='DatabaseConn')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_DatabaseConn NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_DatabaseConn = 'ALTER TABLE Ts_HostConfig ADD [DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_DatabaseConn
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDatabaseConn')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDatabaseConn]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDatabaseConn')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDatabaseConn]
    END

-- [Ts_HostConfig.DirVs] -------------


-- [Ts_HostConfig.DirVs] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='DirVs')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [DirVs] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_DirVs NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_DirVs = 'ALTER TABLE Ts_HostConfig ADD [DirVs] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_DirVs
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDirVs')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDirVs]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDirVs')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDirVs]
    END

-- [Ts_HostConfig.DirVsCodeWeb] -------------


-- [Ts_HostConfig.DirVsCodeWeb] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='DirVsCodeWeb')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_DirVsCodeWeb NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_DirVsCodeWeb = 'ALTER TABLE Ts_HostConfig ADD [DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_DirVsCodeWeb
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDirVsCodeWeb')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDirVsCodeWeb]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDirVsCodeWeb')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDirVsCodeWeb]
    END

-- [Ts_HostConfig.Project] -------------


-- [Ts_HostConfig.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='Project')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_Project NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_Project = 'ALTER TABLE Ts_HostConfig ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_HostConfig_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigProject')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigProject')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigProject]
    END
-- [Ts_Project] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Project' AND xtype='U')

BEGIN

    CREATE TABLE Ts_Project ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
, CONSTRAINT [PK_Ts_Project] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Project NVARCHAR(64)
DECLARE cursor_Ts_Project CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Project') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption','TypeSessionUser'))

OPEN cursor_Ts_Project
FETCH NEXT FROM cursor_Ts_Project INTO @name_Ts_Project

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_Project.' + @name_Ts_Project;
    
    DECLARE @sql_Ts_Project NVARCHAR(MAX);
    SET @sql_Ts_Project = 'ALTER TABLE Ts_Project DROP COLUMN ' + QUOTENAME(@name_Ts_Project)
    EXEC sp_executesql @sql_Ts_Project
    
    
    FETCH NEXT FROM cursor_Ts_Project INTO @name_Ts_Project
END

CLOSE cursor_Ts_Project;
DEALLOCATE cursor_Ts_Project;


-- [Ts_Project.Code] -------------


-- [Ts_Project.Code] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Project') AND name='Code')
    BEGIN
     ALTER TABLE Ts_Project ALTER COLUMN [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Project_Code NVARCHAR(MAX);
    SET @sql_add_Ts_Project_Code = 'ALTER TABLE Ts_Project ADD [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Project_Code
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_ProjectCode')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [Constraint_Ts_ProjectCode]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_ProjectCode')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [UniqueNonclustered_Ts_ProjectCode]
    END

-- [Ts_Project.Caption] -------------


-- [Ts_Project.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Project') AND name='Caption')
    BEGIN
     ALTER TABLE Ts_Project ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Project_Caption NVARCHAR(MAX);
    SET @sql_add_Ts_Project_Caption = 'ALTER TABLE Ts_Project ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Project_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_ProjectCaption')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [Constraint_Ts_ProjectCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_ProjectCaption')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [UniqueNonclustered_Ts_ProjectCaption]
    END

-- [Ts_Project.TypeSessionUser] -------------


-- [Ts_Project.TypeSessionUser] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Project') AND name='TypeSessionUser')
    BEGIN
     ALTER TABLE Ts_Project ALTER COLUMN [TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Project_TypeSessionUser NVARCHAR(MAX);
    SET @sql_add_Ts_Project_TypeSessionUser = 'ALTER TABLE Ts_Project ADD [TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Project_TypeSessionUser
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_ProjectTypeSessionUser')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [Constraint_Ts_ProjectTypeSessionUser]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_ProjectTypeSessionUser')
    BEGIN
    ALTER TABLE Ts_Project DROP  CONSTRAINT [UniqueNonclustered_Ts_ProjectTypeSessionUser]
    END
-- [Ts_Table] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Table' AND xtype='U')

BEGIN

    CREATE TABLE Ts_Table ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Desc] NVARCHAR(MAX)
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_Table] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Table NVARCHAR(64)
DECLARE cursor_Ts_Table CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Table') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc','Project'))

OPEN cursor_Ts_Table
FETCH NEXT FROM cursor_Ts_Table INTO @name_Ts_Table

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_Table.' + @name_Ts_Table;
    
    DECLARE @sql_Ts_Table NVARCHAR(MAX);
    SET @sql_Ts_Table = 'ALTER TABLE Ts_Table DROP COLUMN ' + QUOTENAME(@name_Ts_Table)
    EXEC sp_executesql @sql_Ts_Table
    
    
    FETCH NEXT FROM cursor_Ts_Table INTO @name_Ts_Table
END

CLOSE cursor_Ts_Table;
DEALLOCATE cursor_Ts_Table;


-- [Ts_Table.Name] -------------


-- [Ts_Table.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Table') AND name='Name')
    BEGIN
     ALTER TABLE Ts_Table ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Table_Name NVARCHAR(MAX);
    SET @sql_add_Ts_Table_Name = 'ALTER TABLE Ts_Table ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_Table_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_TableName')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [Constraint_Ts_TableName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_TableName')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [UniqueNonclustered_Ts_TableName]
    END

-- [Ts_Table.Desc] -------------


-- [Ts_Table.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Table') AND name='Desc')
    BEGIN
     ALTER TABLE Ts_Table ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Table_Desc NVARCHAR(MAX);
    SET @sql_add_Ts_Table_Desc = 'ALTER TABLE Ts_Table ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_Table_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_TableDesc')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [Constraint_Ts_TableDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_TableDesc')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [UniqueNonclustered_Ts_TableDesc]
    END

-- [Ts_Table.Project] -------------


-- [Ts_Table.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_Table') AND name='Project')
    BEGIN
     ALTER TABLE Ts_Table ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_Table_Project NVARCHAR(MAX);
    SET @sql_add_Ts_Table_Project = 'ALTER TABLE Ts_Table ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_Table_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_TableProject')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [Constraint_Ts_TableProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_TableProject')
    BEGIN
    ALTER TABLE Ts_Table DROP  CONSTRAINT [UniqueNonclustered_Ts_TableProject]
    END
-- [Ts_UiComponent] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiComponent' AND xtype='U')

BEGIN

    CREATE TABLE Ts_UiComponent ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_UiComponent] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_UiComponent NVARCHAR(64)
DECLARE cursor_Ts_UiComponent CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Project'))

OPEN cursor_Ts_UiComponent
FETCH NEXT FROM cursor_Ts_UiComponent INTO @name_Ts_UiComponent

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_UiComponent.' + @name_Ts_UiComponent;
    
    DECLARE @sql_Ts_UiComponent NVARCHAR(MAX);
    SET @sql_Ts_UiComponent = 'ALTER TABLE Ts_UiComponent DROP COLUMN ' + QUOTENAME(@name_Ts_UiComponent)
    EXEC sp_executesql @sql_Ts_UiComponent
    
    
    FETCH NEXT FROM cursor_Ts_UiComponent INTO @name_Ts_UiComponent
END

CLOSE cursor_Ts_UiComponent;
DEALLOCATE cursor_Ts_UiComponent;


-- [Ts_UiComponent.Name] -------------


-- [Ts_UiComponent.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND name='Name')
    BEGIN
     ALTER TABLE Ts_UiComponent ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiComponent_Name NVARCHAR(MAX);
    SET @sql_add_Ts_UiComponent_Name = 'ALTER TABLE Ts_UiComponent ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiComponent_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiComponentName')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [Constraint_Ts_UiComponentName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiComponentName')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [UniqueNonclustered_Ts_UiComponentName]
    END

-- [Ts_UiComponent.Caption] -------------


-- [Ts_UiComponent.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND name='Caption')
    BEGIN
     ALTER TABLE Ts_UiComponent ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiComponent_Caption NVARCHAR(MAX);
    SET @sql_add_Ts_UiComponent_Caption = 'ALTER TABLE Ts_UiComponent ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiComponent_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiComponentCaption')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [Constraint_Ts_UiComponentCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiComponentCaption')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [UniqueNonclustered_Ts_UiComponentCaption]
    END

-- [Ts_UiComponent.Project] -------------


-- [Ts_UiComponent.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND name='Project')
    BEGIN
     ALTER TABLE Ts_UiComponent ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiComponent_Project NVARCHAR(MAX);
    SET @sql_add_Ts_UiComponent_Project = 'ALTER TABLE Ts_UiComponent ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_UiComponent_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiComponentProject')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [Constraint_Ts_UiComponentProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiComponentProject')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [UniqueNonclustered_Ts_UiComponentProject]
    END
-- [Ts_UiPage] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiPage' AND xtype='U')

BEGIN

    CREATE TABLE Ts_UiPage ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Route] NVARCHAR(MAX)
        ,[OgTitle] NVARCHAR(MAX)
        ,[OgDesc] NVARCHAR(MAX)
        ,[OgImage] NVARCHAR(MAX)
        ,[Template] BIGINT
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_UiPage] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_UiPage NVARCHAR(64)
DECLARE cursor_Ts_UiPage CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Route','OgTitle','OgDesc','OgImage','Template','Project'))

OPEN cursor_Ts_UiPage
FETCH NEXT FROM cursor_Ts_UiPage INTO @name_Ts_UiPage

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_UiPage.' + @name_Ts_UiPage;
    
    DECLARE @sql_Ts_UiPage NVARCHAR(MAX);
    SET @sql_Ts_UiPage = 'ALTER TABLE Ts_UiPage DROP COLUMN ' + QUOTENAME(@name_Ts_UiPage)
    EXEC sp_executesql @sql_Ts_UiPage
    
    
    FETCH NEXT FROM cursor_Ts_UiPage INTO @name_Ts_UiPage
END

CLOSE cursor_Ts_UiPage;
DEALLOCATE cursor_Ts_UiPage;


-- [Ts_UiPage.Name] -------------


-- [Ts_UiPage.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Name')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Name NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Name = 'ALTER TABLE Ts_UiPage ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiPage_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageName')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageName')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageName]
    END

-- [Ts_UiPage.Caption] -------------


-- [Ts_UiPage.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Caption')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Caption NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Caption = 'ALTER TABLE Ts_UiPage ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiPage_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageCaption')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageCaption')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageCaption]
    END

-- [Ts_UiPage.Route] -------------


-- [Ts_UiPage.Route] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Route')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Route] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Route NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Route = 'ALTER TABLE Ts_UiPage ADD [Route] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_UiPage_Route
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageRoute')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageRoute]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageRoute')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageRoute]
    END

-- [Ts_UiPage.OgTitle] -------------


-- [Ts_UiPage.OgTitle] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='OgTitle')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [OgTitle] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_OgTitle NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_OgTitle = 'ALTER TABLE Ts_UiPage ADD [OgTitle] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_UiPage_OgTitle
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageOgTitle')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageOgTitle]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageOgTitle')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageOgTitle]
    END

-- [Ts_UiPage.OgDesc] -------------


-- [Ts_UiPage.OgDesc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='OgDesc')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [OgDesc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_OgDesc NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_OgDesc = 'ALTER TABLE Ts_UiPage ADD [OgDesc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_UiPage_OgDesc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageOgDesc')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageOgDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageOgDesc')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageOgDesc]
    END

-- [Ts_UiPage.OgImage] -------------


-- [Ts_UiPage.OgImage] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='OgImage')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [OgImage] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_OgImage NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_OgImage = 'ALTER TABLE Ts_UiPage ADD [OgImage] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_UiPage_OgImage
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageOgImage')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageOgImage]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageOgImage')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageOgImage]
    END

-- [Ts_UiPage.Template] -------------


-- [Ts_UiPage.Template] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Template')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Template] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Template NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Template = 'ALTER TABLE Ts_UiPage ADD [Template] BIGINT'
    EXEC sp_executesql @sql_add_Ts_UiPage_Template
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageTemplate')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageTemplate]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageTemplate')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageTemplate]
    END

-- [Ts_UiPage.Project] -------------


-- [Ts_UiPage.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Project')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Project NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Project = 'ALTER TABLE Ts_UiPage ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_UiPage_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageProject')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageProject')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageProject]
    END
-- [Ts_UiTemplate] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiTemplate' AND xtype='U')

BEGIN

    CREATE TABLE Ts_UiTemplate ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_UiTemplate] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_UiTemplate NVARCHAR(64)
DECLARE cursor_Ts_UiTemplate CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiTemplate') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Project'))

OPEN cursor_Ts_UiTemplate
FETCH NEXT FROM cursor_Ts_UiTemplate INTO @name_Ts_UiTemplate

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_UiTemplate.' + @name_Ts_UiTemplate;
    
    DECLARE @sql_Ts_UiTemplate NVARCHAR(MAX);
    SET @sql_Ts_UiTemplate = 'ALTER TABLE Ts_UiTemplate DROP COLUMN ' + QUOTENAME(@name_Ts_UiTemplate)
    EXEC sp_executesql @sql_Ts_UiTemplate
    
    
    FETCH NEXT FROM cursor_Ts_UiTemplate INTO @name_Ts_UiTemplate
END

CLOSE cursor_Ts_UiTemplate;
DEALLOCATE cursor_Ts_UiTemplate;


-- [Ts_UiTemplate.Name] -------------


-- [Ts_UiTemplate.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiTemplate') AND name='Name')
    BEGIN
     ALTER TABLE Ts_UiTemplate ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiTemplate_Name NVARCHAR(MAX);
    SET @sql_add_Ts_UiTemplate_Name = 'ALTER TABLE Ts_UiTemplate ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiTemplate_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiTemplateName')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [Constraint_Ts_UiTemplateName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiTemplateName')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [UniqueNonclustered_Ts_UiTemplateName]
    END

-- [Ts_UiTemplate.Caption] -------------


-- [Ts_UiTemplate.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiTemplate') AND name='Caption')
    BEGIN
     ALTER TABLE Ts_UiTemplate ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiTemplate_Caption NVARCHAR(MAX);
    SET @sql_add_Ts_UiTemplate_Caption = 'ALTER TABLE Ts_UiTemplate ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiTemplate_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiTemplateCaption')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [Constraint_Ts_UiTemplateCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiTemplateCaption')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [UniqueNonclustered_Ts_UiTemplateCaption]
    END

-- [Ts_UiTemplate.Project] -------------


-- [Ts_UiTemplate.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiTemplate') AND name='Project')
    BEGIN
     ALTER TABLE Ts_UiTemplate ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiTemplate_Project NVARCHAR(MAX);
    SET @sql_add_Ts_UiTemplate_Project = 'ALTER TABLE Ts_UiTemplate ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_UiTemplate_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiTemplateProject')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [Constraint_Ts_UiTemplateProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiTemplateProject')
    BEGIN
    ALTER TABLE Ts_UiTemplate DROP  CONSTRAINT [UniqueNonclustered_Ts_UiTemplateProject]
    END
-- [Ts_VarType] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_VarType' AND xtype='U')

BEGIN

    CREATE TABLE Ts_VarType ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Val] NVARCHAR(MAX)
        ,[BindType] INT
        ,[Bind] BIGINT
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_VarType] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_VarType NVARCHAR(64)
DECLARE cursor_Ts_VarType CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Type','Val','BindType','Bind','Project'))

OPEN cursor_Ts_VarType
FETCH NEXT FROM cursor_Ts_VarType INTO @name_Ts_VarType

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ts_VarType.' + @name_Ts_VarType;
    
    DECLARE @sql_Ts_VarType NVARCHAR(MAX);
    SET @sql_Ts_VarType = 'ALTER TABLE Ts_VarType DROP COLUMN ' + QUOTENAME(@name_Ts_VarType)
    EXEC sp_executesql @sql_Ts_VarType
    
    
    FETCH NEXT FROM cursor_Ts_VarType INTO @name_Ts_VarType
END

CLOSE cursor_Ts_VarType;
DEALLOCATE cursor_Ts_VarType;


-- [Ts_VarType.Name] -------------


-- [Ts_VarType.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='Name')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_Name NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_Name = 'ALTER TABLE Ts_VarType ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_VarType_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeName')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeName')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeName]
    END

-- [Ts_VarType.Type] -------------


-- [Ts_VarType.Type] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='Type')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_Type NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_Type = 'ALTER TABLE Ts_VarType ADD [Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_VarType_Type
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeType')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeType')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeType]
    END

-- [Ts_VarType.Val] -------------


-- [Ts_VarType.Val] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='Val')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [Val] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_Val NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_Val = 'ALTER TABLE Ts_VarType ADD [Val] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ts_VarType_Val
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeVal')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeVal]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeVal')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeVal]
    END

-- [Ts_VarType.BindType] -------------


-- [Ts_VarType.BindType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='BindType')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [BindType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_BindType NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_BindType = 'ALTER TABLE Ts_VarType ADD [BindType] INT'
    EXEC sp_executesql @sql_add_Ts_VarType_BindType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeBindType')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeBindType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeBindType')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeBindType]
    END

-- [Ts_VarType.Bind] -------------


-- [Ts_VarType.Bind] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='Bind')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [Bind] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_Bind NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_Bind = 'ALTER TABLE Ts_VarType ADD [Bind] BIGINT'
    EXEC sp_executesql @sql_add_Ts_VarType_Bind
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeBind')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeBind]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeBind')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeBind]
    END

-- [Ts_VarType.Project] -------------


-- [Ts_VarType.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_VarType') AND name='Project')
    BEGIN
     ALTER TABLE Ts_VarType ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_VarType_Project NVARCHAR(MAX);
    SET @sql_add_Ts_VarType_Project = 'ALTER TABLE Ts_VarType ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_Ts_VarType_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_VarTypeProject')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [Constraint_Ts_VarTypeProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_VarTypeProject')
    BEGIN
    ALTER TABLE Ts_VarType DROP  CONSTRAINT [UniqueNonclustered_Ts_VarTypeProject]
    END