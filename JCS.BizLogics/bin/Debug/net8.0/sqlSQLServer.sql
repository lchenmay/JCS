USE [JCS]
-- [Ca_Book] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Book' AND xtype='U')

BEGIN

    CREATE TABLE Ca_Book ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Message] NVARCHAR(MAX)
, CONSTRAINT [PK_Ca_Book] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_Book NVARCHAR(64)
DECLARE cursor_Ca_Book CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_Book') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Email','Message'))

OPEN cursor_Ca_Book
FETCH NEXT FROM cursor_Ca_Book INTO @name_Ca_Book

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_Book.' + @name_Ca_Book;
    
    DECLARE @sql_Ca_Book NVARCHAR(MAX);
    SET @sql_Ca_Book = 'ALTER TABLE Ca_Book DROP COLUMN ' + QUOTENAME(@name_Ca_Book)
    EXEC sp_executesql @sql_Ca_Book
    
    
    FETCH NEXT FROM cursor_Ca_Book INTO @name_Ca_Book
END

CLOSE cursor_Ca_Book;
DEALLOCATE cursor_Ca_Book;


-- [Ca_Book.Caption] -------------


-- [Ca_Book.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Book') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_Book ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Book_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_Book_Caption = 'ALTER TABLE Ca_Book ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Book_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BookCaption')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [Constraint_Ca_BookCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BookCaption')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [UniqueNonclustered_Ca_BookCaption]
    END

-- [Ca_Book.Email] -------------


-- [Ca_Book.Email] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Book') AND name='Email')
    BEGIN
     ALTER TABLE Ca_Book ALTER COLUMN [Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Book_Email NVARCHAR(MAX);
    SET @sql_add_Ca_Book_Email = 'ALTER TABLE Ca_Book ADD [Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_Book_Email
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BookEmail')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [Constraint_Ca_BookEmail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BookEmail')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [UniqueNonclustered_Ca_BookEmail]
    END

-- [Ca_Book.Message] -------------


-- [Ca_Book.Message] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_Book') AND name='Message')
    BEGIN
     ALTER TABLE Ca_Book ALTER COLUMN [Message] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_Book_Message NVARCHAR(MAX);
    SET @sql_add_Ca_Book_Message = 'ALTER TABLE Ca_Book ADD [Message] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_Book_Message
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_BookMessage')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [Constraint_Ca_BookMessage]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_BookMessage')
    BEGIN
    ALTER TABLE Ca_Book DROP  CONSTRAINT [UniqueNonclustered_Ca_BookMessage]
    END
-- [Ca_EndUser] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_EndUser' AND xtype='U')

BEGIN

    CREATE TABLE Ca_EndUser ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[AuthType] INT
, CONSTRAINT [PK_Ca_EndUser] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_EndUser NVARCHAR(64)
DECLARE cursor_Ca_EndUser CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','AuthType'))

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

-- [Ca_EndUser.AuthType] -------------


-- [Ca_EndUser.AuthType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_EndUser') AND name='AuthType')
    BEGIN
     ALTER TABLE Ca_EndUser ALTER COLUMN [AuthType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_EndUser_AuthType NVARCHAR(MAX);
    SET @sql_add_Ca_EndUser_AuthType = 'ALTER TABLE Ca_EndUser ADD [AuthType] INT'
    EXEC sp_executesql @sql_add_Ca_EndUser_AuthType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_EndUserAuthType')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [Constraint_Ca_EndUserAuthType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_EndUserAuthType')
    BEGIN
    ALTER TABLE Ca_EndUser DROP  CONSTRAINT [UniqueNonclustered_Ca_EndUserAuthType]
    END
-- [Ca_File] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_File' AND xtype='U')

BEGIN

    CREATE TABLE Ca_File ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(MAX)
        ,[Desc] NVARCHAR(MAX)
        ,[Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
        ,[Size] BIGINT
        ,[Thumbnail] VARBINARY(MAX)
        ,[Owner] BIGINT
, CONSTRAINT [PK_Ca_File] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Ca_File NVARCHAR(64)
DECLARE cursor_Ca_File CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Desc','Suffix','Size','Thumbnail','Owner'))

OPEN cursor_Ca_File
FETCH NEXT FROM cursor_Ca_File INTO @name_Ca_File

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Ca_File.' + @name_Ca_File;
    
    DECLARE @sql_Ca_File NVARCHAR(MAX);
    SET @sql_Ca_File = 'ALTER TABLE Ca_File DROP COLUMN ' + QUOTENAME(@name_Ca_File)
    EXEC sp_executesql @sql_Ca_File
    
    
    FETCH NEXT FROM cursor_Ca_File INTO @name_Ca_File
END

CLOSE cursor_Ca_File;
DEALLOCATE cursor_Ca_File;


-- [Ca_File.Caption] -------------


-- [Ca_File.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Caption')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Caption] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Caption NVARCHAR(MAX);
    SET @sql_add_Ca_File_Caption = 'ALTER TABLE Ca_File ADD [Caption] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_File_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileCaption')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileCaption')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileCaption]
    END

-- [Ca_File.Desc] -------------


-- [Ca_File.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Desc')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Desc NVARCHAR(MAX);
    SET @sql_add_Ca_File_Desc = 'ALTER TABLE Ca_File ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Ca_File_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileDesc')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileDesc')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileDesc]
    END

-- [Ca_File.Suffix] -------------


-- [Ca_File.Suffix] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Suffix')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Suffix NVARCHAR(MAX);
    SET @sql_add_Ca_File_Suffix = 'ALTER TABLE Ca_File ADD [Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Ca_File_Suffix
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileSuffix')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileSuffix]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileSuffix')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileSuffix]
    END

-- [Ca_File.Size] -------------


-- [Ca_File.Size] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Size')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Size] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Size NVARCHAR(MAX);
    SET @sql_add_Ca_File_Size = 'ALTER TABLE Ca_File ADD [Size] BIGINT'
    EXEC sp_executesql @sql_add_Ca_File_Size
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileSize')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileSize]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileSize')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileSize]
    END

-- [Ca_File.Thumbnail] -------------


-- [Ca_File.Thumbnail] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Thumbnail')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Thumbnail] VARBINARY(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Thumbnail NVARCHAR(MAX);
    SET @sql_add_Ca_File_Thumbnail = 'ALTER TABLE Ca_File ADD [Thumbnail] VARBINARY(MAX)'
    EXEC sp_executesql @sql_add_Ca_File_Thumbnail
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileThumbnail')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileThumbnail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileThumbnail')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileThumbnail]
    END

-- [Ca_File.Owner] -------------


-- [Ca_File.Owner] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Ca_File') AND name='Owner')
    BEGIN
     ALTER TABLE Ca_File ALTER COLUMN [Owner] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Ca_File_Owner NVARCHAR(MAX);
    SET @sql_add_Ca_File_Owner = 'ALTER TABLE Ca_File ADD [Owner] BIGINT'
    EXEC sp_executesql @sql_add_Ca_File_Owner
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Ca_FileOwner')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [Constraint_Ca_FileOwner]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Ca_FileOwner')
    BEGIN
    ALTER TABLE Ca_File DROP  CONSTRAINT [UniqueNonclustered_Ca_FileOwner]
    END
-- [Social_FileBind] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Social_FileBind' AND xtype='U')

BEGIN

    CREATE TABLE Social_FileBind ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [File] BIGINT
        ,[Moment] BIGINT
        ,[Desc] NVARCHAR(MAX)
, CONSTRAINT [PK_Social_FileBind] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Social_FileBind NVARCHAR(64)
DECLARE cursor_Social_FileBind CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Social_FileBind') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','File','Moment','Desc'))

OPEN cursor_Social_FileBind
FETCH NEXT FROM cursor_Social_FileBind INTO @name_Social_FileBind

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Social_FileBind.' + @name_Social_FileBind;
    
    DECLARE @sql_Social_FileBind NVARCHAR(MAX);
    SET @sql_Social_FileBind = 'ALTER TABLE Social_FileBind DROP COLUMN ' + QUOTENAME(@name_Social_FileBind)
    EXEC sp_executesql @sql_Social_FileBind
    
    
    FETCH NEXT FROM cursor_Social_FileBind INTO @name_Social_FileBind
END

CLOSE cursor_Social_FileBind;
DEALLOCATE cursor_Social_FileBind;


-- [Social_FileBind.File] -------------


-- [Social_FileBind.File] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_FileBind') AND name='File')
    BEGIN
     ALTER TABLE Social_FileBind ALTER COLUMN [File] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_FileBind_File NVARCHAR(MAX);
    SET @sql_add_Social_FileBind_File = 'ALTER TABLE Social_FileBind ADD [File] BIGINT'
    EXEC sp_executesql @sql_add_Social_FileBind_File
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_FileBindFile')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [Constraint_Social_FileBindFile]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_FileBindFile')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [UniqueNonclustered_Social_FileBindFile]
    END

-- [Social_FileBind.Moment] -------------


-- [Social_FileBind.Moment] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_FileBind') AND name='Moment')
    BEGIN
     ALTER TABLE Social_FileBind ALTER COLUMN [Moment] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_FileBind_Moment NVARCHAR(MAX);
    SET @sql_add_Social_FileBind_Moment = 'ALTER TABLE Social_FileBind ADD [Moment] BIGINT'
    EXEC sp_executesql @sql_add_Social_FileBind_Moment
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_FileBindMoment')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [Constraint_Social_FileBindMoment]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_FileBindMoment')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [UniqueNonclustered_Social_FileBindMoment]
    END

-- [Social_FileBind.Desc] -------------


-- [Social_FileBind.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_FileBind') AND name='Desc')
    BEGIN
     ALTER TABLE Social_FileBind ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_FileBind_Desc NVARCHAR(MAX);
    SET @sql_add_Social_FileBind_Desc = 'ALTER TABLE Social_FileBind ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_FileBind_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_FileBindDesc')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [Constraint_Social_FileBindDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_FileBindDesc')
    BEGIN
    ALTER TABLE Social_FileBind DROP  CONSTRAINT [UniqueNonclustered_Social_FileBindDesc]
    END
-- [Social_Moment] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Social_Moment' AND xtype='U')

BEGIN

    CREATE TABLE Social_Moment ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Title] NVARCHAR(MAX)
        ,[Summary] NVARCHAR(MAX)
        ,[FullText] NVARCHAR(MAX)
        ,[PreviewImgUrl] NVARCHAR(MAX)
        ,[Link] NVARCHAR(MAX)
        ,[Type] INT
        ,[State] INT
        ,[MediaType] INT
, CONSTRAINT [PK_Social_Moment] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Social_Moment NVARCHAR(64)
DECLARE cursor_Social_Moment CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Title','Summary','FullText','PreviewImgUrl','Link','Type','State','MediaType'))

OPEN cursor_Social_Moment
FETCH NEXT FROM cursor_Social_Moment INTO @name_Social_Moment

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Social_Moment.' + @name_Social_Moment;
    
    DECLARE @sql_Social_Moment NVARCHAR(MAX);
    SET @sql_Social_Moment = 'ALTER TABLE Social_Moment DROP COLUMN ' + QUOTENAME(@name_Social_Moment)
    EXEC sp_executesql @sql_Social_Moment
    
    
    FETCH NEXT FROM cursor_Social_Moment INTO @name_Social_Moment
END

CLOSE cursor_Social_Moment;
DEALLOCATE cursor_Social_Moment;


-- [Social_Moment.Title] -------------


-- [Social_Moment.Title] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='Title')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [Title] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_Title NVARCHAR(MAX);
    SET @sql_add_Social_Moment_Title = 'ALTER TABLE Social_Moment ADD [Title] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_Moment_Title
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentTitle')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentTitle]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentTitle')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentTitle]
    END

-- [Social_Moment.Summary] -------------


-- [Social_Moment.Summary] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='Summary')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [Summary] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_Summary NVARCHAR(MAX);
    SET @sql_add_Social_Moment_Summary = 'ALTER TABLE Social_Moment ADD [Summary] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_Moment_Summary
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentSummary')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentSummary]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentSummary')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentSummary]
    END

-- [Social_Moment.FullText] -------------


-- [Social_Moment.FullText] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='FullText')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [FullText] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_FullText NVARCHAR(MAX);
    SET @sql_add_Social_Moment_FullText = 'ALTER TABLE Social_Moment ADD [FullText] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_Moment_FullText
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentFullText')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentFullText]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentFullText')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentFullText]
    END

-- [Social_Moment.PreviewImgUrl] -------------


-- [Social_Moment.PreviewImgUrl] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='PreviewImgUrl')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [PreviewImgUrl] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_PreviewImgUrl NVARCHAR(MAX);
    SET @sql_add_Social_Moment_PreviewImgUrl = 'ALTER TABLE Social_Moment ADD [PreviewImgUrl] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_Moment_PreviewImgUrl
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentPreviewImgUrl')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentPreviewImgUrl]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentPreviewImgUrl')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentPreviewImgUrl]
    END

-- [Social_Moment.Link] -------------


-- [Social_Moment.Link] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='Link')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [Link] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_Link NVARCHAR(MAX);
    SET @sql_add_Social_Moment_Link = 'ALTER TABLE Social_Moment ADD [Link] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Social_Moment_Link
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentLink')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentLink]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentLink')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentLink]
    END

-- [Social_Moment.Type] -------------


-- [Social_Moment.Type] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='Type')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [Type] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_Type NVARCHAR(MAX);
    SET @sql_add_Social_Moment_Type = 'ALTER TABLE Social_Moment ADD [Type] INT'
    EXEC sp_executesql @sql_add_Social_Moment_Type
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentType')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentType')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentType]
    END

-- [Social_Moment.State] -------------


-- [Social_Moment.State] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='State')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [State] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_State NVARCHAR(MAX);
    SET @sql_add_Social_Moment_State = 'ALTER TABLE Social_Moment ADD [State] INT'
    EXEC sp_executesql @sql_add_Social_Moment_State
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentState')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentState]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentState')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentState]
    END

-- [Social_Moment.MediaType] -------------


-- [Social_Moment.MediaType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Social_Moment') AND name='MediaType')
    BEGIN
     ALTER TABLE Social_Moment ALTER COLUMN [MediaType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_Social_Moment_MediaType NVARCHAR(MAX);
    SET @sql_add_Social_Moment_MediaType = 'ALTER TABLE Social_Moment ADD [MediaType] INT'
    EXEC sp_executesql @sql_add_Social_Moment_MediaType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Social_MomentMediaType')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [Constraint_Social_MomentMediaType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Social_MomentMediaType')
    BEGIN
    ALTER TABLE Social_Moment DROP  CONSTRAINT [UniqueNonclustered_Social_MomentMediaType]
    END
-- [Sys_Log] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Sys_Log' AND xtype='U')

BEGIN

    CREATE TABLE Sys_Log ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Location] NVARCHAR(MAX)
        ,[Content] NVARCHAR(MAX)
        ,[Sql] NVARCHAR(MAX)
, CONSTRAINT [PK_Sys_Log] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Sys_Log NVARCHAR(64)
DECLARE cursor_Sys_Log CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Sys_Log') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Location','Content','Sql'))

OPEN cursor_Sys_Log
FETCH NEXT FROM cursor_Sys_Log INTO @name_Sys_Log

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Sys_Log.' + @name_Sys_Log;
    
    DECLARE @sql_Sys_Log NVARCHAR(MAX);
    SET @sql_Sys_Log = 'ALTER TABLE Sys_Log DROP COLUMN ' + QUOTENAME(@name_Sys_Log)
    EXEC sp_executesql @sql_Sys_Log
    
    
    FETCH NEXT FROM cursor_Sys_Log INTO @name_Sys_Log
END

CLOSE cursor_Sys_Log;
DEALLOCATE cursor_Sys_Log;


-- [Sys_Log.Location] -------------


-- [Sys_Log.Location] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Sys_Log') AND name='Location')
    BEGIN
     ALTER TABLE Sys_Log ALTER COLUMN [Location] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Sys_Log_Location NVARCHAR(MAX);
    SET @sql_add_Sys_Log_Location = 'ALTER TABLE Sys_Log ADD [Location] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Sys_Log_Location
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Sys_LogLocation')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [Constraint_Sys_LogLocation]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Sys_LogLocation')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [UniqueNonclustered_Sys_LogLocation]
    END

-- [Sys_Log.Content] -------------


-- [Sys_Log.Content] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Sys_Log') AND name='Content')
    BEGIN
     ALTER TABLE Sys_Log ALTER COLUMN [Content] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Sys_Log_Content NVARCHAR(MAX);
    SET @sql_add_Sys_Log_Content = 'ALTER TABLE Sys_Log ADD [Content] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Sys_Log_Content
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Sys_LogContent')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [Constraint_Sys_LogContent]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Sys_LogContent')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [UniqueNonclustered_Sys_LogContent]
    END

-- [Sys_Log.Sql] -------------


-- [Sys_Log.Sql] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Sys_Log') AND name='Sql')
    BEGIN
     ALTER TABLE Sys_Log ALTER COLUMN [Sql] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Sys_Log_Sql NVARCHAR(MAX);
    SET @sql_add_Sys_Log_Sql = 'ALTER TABLE Sys_Log ADD [Sql] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Sys_Log_Sql
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Sys_LogSql')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [Constraint_Sys_LogSql]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Sys_LogSql')
    BEGIN
    ALTER TABLE Sys_Log DROP  CONSTRAINT [UniqueNonclustered_Sys_LogSql]
    END
-- [Sys_PageLog] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Sys_PageLog' AND xtype='U')

BEGIN

    CREATE TABLE Sys_PageLog ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Request] NVARCHAR(MAX)
, CONSTRAINT [PK_Sys_PageLog] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_Sys_PageLog NVARCHAR(64)
DECLARE cursor_Sys_PageLog CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('Sys_PageLog') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Ip','Request'))

OPEN cursor_Sys_PageLog
FETCH NEXT FROM cursor_Sys_PageLog INTO @name_Sys_PageLog

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping Sys_PageLog.' + @name_Sys_PageLog;
    
    DECLARE @sql_Sys_PageLog NVARCHAR(MAX);
    SET @sql_Sys_PageLog = 'ALTER TABLE Sys_PageLog DROP COLUMN ' + QUOTENAME(@name_Sys_PageLog)
    EXEC sp_executesql @sql_Sys_PageLog
    
    
    FETCH NEXT FROM cursor_Sys_PageLog INTO @name_Sys_PageLog
END

CLOSE cursor_Sys_PageLog;
DEALLOCATE cursor_Sys_PageLog;


-- [Sys_PageLog.Ip] -------------


-- [Sys_PageLog.Ip] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Sys_PageLog') AND name='Ip')
    BEGIN
     ALTER TABLE Sys_PageLog ALTER COLUMN [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_Sys_PageLog_Ip NVARCHAR(MAX);
    SET @sql_add_Sys_PageLog_Ip = 'ALTER TABLE Sys_PageLog ADD [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_Sys_PageLog_Ip
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Sys_PageLogIp')
    BEGIN
    ALTER TABLE Sys_PageLog DROP  CONSTRAINT [Constraint_Sys_PageLogIp]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Sys_PageLogIp')
    BEGIN
    ALTER TABLE Sys_PageLog DROP  CONSTRAINT [UniqueNonclustered_Sys_PageLogIp]
    END

-- [Sys_PageLog.Request] -------------


-- [Sys_PageLog.Request] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('Sys_PageLog') AND name='Request')
    BEGIN
     ALTER TABLE Sys_PageLog ALTER COLUMN [Request] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_Sys_PageLog_Request NVARCHAR(MAX);
    SET @sql_add_Sys_PageLog_Request = 'ALTER TABLE Sys_PageLog ADD [Request] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_Sys_PageLog_Request
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_Sys_PageLogRequest')
    BEGIN
    ALTER TABLE Sys_PageLog DROP  CONSTRAINT [Constraint_Sys_PageLogRequest]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_Sys_PageLogRequest')
    BEGIN
    ALTER TABLE Sys_PageLog DROP  CONSTRAINT [UniqueNonclustered_Sys_PageLogRequest]
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