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
, CONSTRAINT [PK_Ts_Field] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Field NVARCHAR(64)
DECLARE cursor_Ts_Field CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Field') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc'))

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
, CONSTRAINT [PK_Ts_Table] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_Table NVARCHAR(64)
DECLARE cursor_Ts_Table CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_Table') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc'))

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
-- [Ts_UiComponent] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiComponent' AND xtype='U')

BEGIN

    CREATE TABLE Ts_UiComponent ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
, CONSTRAINT [PK_Ts_UiComponent] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_UiComponent NVARCHAR(64)
DECLARE cursor_Ts_UiComponent CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption'))

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


-- [Ts_UiComponent.Code] -------------


-- [Ts_UiComponent.Code] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiComponent') AND name='Code')
    BEGIN
     ALTER TABLE Ts_UiComponent ALTER COLUMN [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiComponent_Code NVARCHAR(MAX);
    SET @sql_add_Ts_UiComponent_Code = 'ALTER TABLE Ts_UiComponent ADD [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiComponent_Code
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiComponentCode')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [Constraint_Ts_UiComponentCode]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiComponentCode')
    BEGIN
    ALTER TABLE Ts_UiComponent DROP  CONSTRAINT [UniqueNonclustered_Ts_UiComponentCode]
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
-- [Ts_UiPage] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiPage' AND xtype='U')

BEGIN

    CREATE TABLE Ts_UiPage ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
, CONSTRAINT [PK_Ts_UiPage] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ts_UiPage NVARCHAR(64)
DECLARE cursor_Ts_UiPage CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption'))

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


-- [Ts_UiPage.Code] -------------


-- [Ts_UiPage.Code] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ts_UiPage') AND name='Code')
    BEGIN
     ALTER TABLE Ts_UiPage ALTER COLUMN [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ts_UiPage_Code NVARCHAR(MAX);
    SET @sql_add_Ts_UiPage_Code = 'ALTER TABLE Ts_UiPage ADD [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ts_UiPage_Code
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ts_UiPageCode')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [Constraint_Ts_UiPageCode]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ts_UiPageCode')
    BEGIN
    ALTER TABLE Ts_UiPage DROP  CONSTRAINT [UniqueNonclustered_Ts_UiPageCode]
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