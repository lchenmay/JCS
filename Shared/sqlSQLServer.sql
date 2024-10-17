USE [JCS]
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