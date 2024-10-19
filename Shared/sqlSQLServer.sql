USE [JCS]
-- [Ts_Field] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Field' AND xtype='U')

BEGIN

    CREATE TABLE Ts_Field ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Desc] NVARCHAR(MAX)
        ,[Project] BIGINT
        ,[Table] BIGINT
, CONSTRAINT [PK_Ts_Field] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Field NVARCHAR(64)
DECLARE cursor_Ts_Field CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc','Project','Table'))

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
        ,[DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DirVsShared] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_Ts_HostConfig] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_HostConfig NVARCHAR(64)
DECLARE cursor_Ts_HostConfig CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Hostname','DatabaseName','DatabaseConn','DirVsShared','DirVsCodeWeb','Project'))

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

-- [Ts_HostConfig.DirVsShared] -------------


-- [Ts_HostConfig.DirVsShared] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_HostConfig') AND name='DirVsShared')
    BEGIN
     ALTER TABLE Ts_HostConfig ALTER COLUMN [DirVsShared] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_HostConfig_DirVsShared NVARCHAR(MAX);
    SET @sql_add_Ts_HostConfig_DirVsShared = 'ALTER TABLE Ts_HostConfig ADD [DirVsShared] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_HostConfig_DirVsShared
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_HostConfigDirVsShared')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [Constraint_Ts_HostConfigDirVsShared]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_HostConfigDirVsShared')
    BEGIN
    ALTER TABLE Ts_HostConfig DROP  CONSTRAINT [UniqueNonclustered_Ts_HostConfigDirVsShared]
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
, CONSTRAINT [PK_Ts_Project] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Project NVARCHAR(64)
DECLARE cursor_Ts_Project CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Project') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption'))

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
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','OgTitle','OgDesc','OgImage','Template','Project'))

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