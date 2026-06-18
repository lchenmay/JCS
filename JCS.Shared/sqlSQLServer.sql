USE [jcs]
-- [ca_book] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ca_book' AND xtype='U')

BEGIN

    CREATE TABLE ca_book ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Message] NVARCHAR(MAX)
, CONSTRAINT [PK_ca_book] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ca_book NVARCHAR(64)
DECLARE cursor_ca_book CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ca_book') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Email','Message'))

OPEN cursor_ca_book
FETCH NEXT FROM cursor_ca_book INTO @name_ca_book

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ca_book.' + @name_ca_book;
    
    DECLARE @sql_ca_book NVARCHAR(MAX);
    SET @sql_ca_book = 'ALTER TABLE ca_book DROP COLUMN ' + QUOTENAME(@name_ca_book)
    EXEC sp_executesql @sql_ca_book
    
    
    FETCH NEXT FROM cursor_ca_book INTO @name_ca_book
END

CLOSE cursor_ca_book;
DEALLOCATE cursor_ca_book;


-- [ca_book.Caption] -------------


-- [ca_book.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_book') AND name='Caption')
    BEGIN
     ALTER TABLE ca_book ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_book_Caption NVARCHAR(MAX);
    SET @sql_add_ca_book_Caption = 'ALTER TABLE ca_book ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ca_book_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_bookCaption')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [Constraint_ca_bookCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_bookCaption')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [UniqueNonclustered_ca_bookCaption]
    END

-- [ca_book.Email] -------------


-- [ca_book.Email] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_book') AND name='Email')
    BEGIN
     ALTER TABLE ca_book ALTER COLUMN [Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_book_Email NVARCHAR(MAX);
    SET @sql_add_ca_book_Email = 'ALTER TABLE ca_book ADD [Email] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ca_book_Email
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_bookEmail')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [Constraint_ca_bookEmail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_bookEmail')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [UniqueNonclustered_ca_bookEmail]
    END

-- [ca_book.Message] -------------


-- [ca_book.Message] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_book') AND name='Message')
    BEGIN
     ALTER TABLE ca_book ALTER COLUMN [Message] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_book_Message NVARCHAR(MAX);
    SET @sql_add_ca_book_Message = 'ALTER TABLE ca_book ADD [Message] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ca_book_Message
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_bookMessage')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [Constraint_ca_bookMessage]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_bookMessage')
    BEGIN
    ALTER TABLE ca_book DROP  CONSTRAINT [UniqueNonclustered_ca_bookMessage]
    END
-- [ca_enduser] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ca_enduser' AND xtype='U')

BEGIN

    CREATE TABLE ca_enduser ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[AuthType] INT
, CONSTRAINT [PK_ca_enduser] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ca_enduser NVARCHAR(64)
DECLARE cursor_ca_enduser CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ca_enduser') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','AuthType'))

OPEN cursor_ca_enduser
FETCH NEXT FROM cursor_ca_enduser INTO @name_ca_enduser

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ca_enduser.' + @name_ca_enduser;
    
    DECLARE @sql_ca_enduser NVARCHAR(MAX);
    SET @sql_ca_enduser = 'ALTER TABLE ca_enduser DROP COLUMN ' + QUOTENAME(@name_ca_enduser)
    EXEC sp_executesql @sql_ca_enduser
    
    
    FETCH NEXT FROM cursor_ca_enduser INTO @name_ca_enduser
END

CLOSE cursor_ca_enduser;
DEALLOCATE cursor_ca_enduser;


-- [ca_enduser.Caption] -------------


-- [ca_enduser.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_enduser') AND name='Caption')
    BEGIN
     ALTER TABLE ca_enduser ALTER COLUMN [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_enduser_Caption NVARCHAR(MAX);
    SET @sql_add_ca_enduser_Caption = 'ALTER TABLE ca_enduser ADD [Caption] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ca_enduser_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_enduserCaption')
    BEGIN
    ALTER TABLE ca_enduser DROP  CONSTRAINT [Constraint_ca_enduserCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_enduserCaption')
    BEGIN
    ALTER TABLE ca_enduser DROP  CONSTRAINT [UniqueNonclustered_ca_enduserCaption]
    END

-- [ca_enduser.AuthType] -------------


-- [ca_enduser.AuthType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_enduser') AND name='AuthType')
    BEGIN
     ALTER TABLE ca_enduser ALTER COLUMN [AuthType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_enduser_AuthType NVARCHAR(MAX);
    SET @sql_add_ca_enduser_AuthType = 'ALTER TABLE ca_enduser ADD [AuthType] INT'
    EXEC sp_executesql @sql_add_ca_enduser_AuthType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_enduserAuthType')
    BEGIN
    ALTER TABLE ca_enduser DROP  CONSTRAINT [Constraint_ca_enduserAuthType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_enduserAuthType')
    BEGIN
    ALTER TABLE ca_enduser DROP  CONSTRAINT [UniqueNonclustered_ca_enduserAuthType]
    END
-- [ca_file] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ca_file' AND xtype='U')

BEGIN

    CREATE TABLE ca_file ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Caption] NVARCHAR(MAX)
        ,[Desc] NVARCHAR(MAX)
        ,[Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
        ,[Size] BIGINT
        ,[Thumbnail] VARBINARY(MAX)
        ,[Owner] BIGINT
, CONSTRAINT [PK_ca_file] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ca_file NVARCHAR(64)
DECLARE cursor_ca_file CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Caption','Desc','Suffix','Size','Thumbnail','Owner'))

OPEN cursor_ca_file
FETCH NEXT FROM cursor_ca_file INTO @name_ca_file

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ca_file.' + @name_ca_file;
    
    DECLARE @sql_ca_file NVARCHAR(MAX);
    SET @sql_ca_file = 'ALTER TABLE ca_file DROP COLUMN ' + QUOTENAME(@name_ca_file)
    EXEC sp_executesql @sql_ca_file
    
    
    FETCH NEXT FROM cursor_ca_file INTO @name_ca_file
END

CLOSE cursor_ca_file;
DEALLOCATE cursor_ca_file;


-- [ca_file.Caption] -------------


-- [ca_file.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Caption')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Caption] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Caption NVARCHAR(MAX);
    SET @sql_add_ca_file_Caption = 'ALTER TABLE ca_file ADD [Caption] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ca_file_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileCaption')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileCaption')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileCaption]
    END

-- [ca_file.Desc] -------------


-- [ca_file.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Desc')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Desc NVARCHAR(MAX);
    SET @sql_add_ca_file_Desc = 'ALTER TABLE ca_file ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ca_file_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileDesc')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileDesc')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileDesc]
    END

-- [ca_file.Suffix] -------------


-- [ca_file.Suffix] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Suffix')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Suffix NVARCHAR(MAX);
    SET @sql_add_ca_file_Suffix = 'ALTER TABLE ca_file ADD [Suffix] NVARCHAR(4) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ca_file_Suffix
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileSuffix')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileSuffix]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileSuffix')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileSuffix]
    END

-- [ca_file.Size] -------------


-- [ca_file.Size] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Size')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Size] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Size NVARCHAR(MAX);
    SET @sql_add_ca_file_Size = 'ALTER TABLE ca_file ADD [Size] BIGINT'
    EXEC sp_executesql @sql_add_ca_file_Size
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileSize')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileSize]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileSize')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileSize]
    END

-- [ca_file.Thumbnail] -------------


-- [ca_file.Thumbnail] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Thumbnail')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Thumbnail] VARBINARY(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Thumbnail NVARCHAR(MAX);
    SET @sql_add_ca_file_Thumbnail = 'ALTER TABLE ca_file ADD [Thumbnail] VARBINARY(MAX)'
    EXEC sp_executesql @sql_add_ca_file_Thumbnail
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileThumbnail')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileThumbnail]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileThumbnail')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileThumbnail]
    END

-- [ca_file.Owner] -------------


-- [ca_file.Owner] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ca_file') AND name='Owner')
    BEGIN
     ALTER TABLE ca_file ALTER COLUMN [Owner] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ca_file_Owner NVARCHAR(MAX);
    SET @sql_add_ca_file_Owner = 'ALTER TABLE ca_file ADD [Owner] BIGINT'
    EXEC sp_executesql @sql_add_ca_file_Owner
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ca_fileOwner')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [Constraint_ca_fileOwner]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ca_fileOwner')
    BEGIN
    ALTER TABLE ca_file DROP  CONSTRAINT [UniqueNonclustered_ca_fileOwner]
    END
-- [social_filebind] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='social_filebind' AND xtype='U')

BEGIN

    CREATE TABLE social_filebind ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [File] BIGINT
        ,[Moment] BIGINT
        ,[Desc] NVARCHAR(MAX)
, CONSTRAINT [PK_social_filebind] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_social_filebind NVARCHAR(64)
DECLARE cursor_social_filebind CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('social_filebind') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','File','Moment','Desc'))

OPEN cursor_social_filebind
FETCH NEXT FROM cursor_social_filebind INTO @name_social_filebind

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping social_filebind.' + @name_social_filebind;
    
    DECLARE @sql_social_filebind NVARCHAR(MAX);
    SET @sql_social_filebind = 'ALTER TABLE social_filebind DROP COLUMN ' + QUOTENAME(@name_social_filebind)
    EXEC sp_executesql @sql_social_filebind
    
    
    FETCH NEXT FROM cursor_social_filebind INTO @name_social_filebind
END

CLOSE cursor_social_filebind;
DEALLOCATE cursor_social_filebind;


-- [social_filebind.File] -------------


-- [social_filebind.File] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_filebind') AND name='File')
    BEGIN
     ALTER TABLE social_filebind ALTER COLUMN [File] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_filebind_File NVARCHAR(MAX);
    SET @sql_add_social_filebind_File = 'ALTER TABLE social_filebind ADD [File] BIGINT'
    EXEC sp_executesql @sql_add_social_filebind_File
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_filebindFile')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [Constraint_social_filebindFile]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_filebindFile')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [UniqueNonclustered_social_filebindFile]
    END

-- [social_filebind.Moment] -------------


-- [social_filebind.Moment] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_filebind') AND name='Moment')
    BEGIN
     ALTER TABLE social_filebind ALTER COLUMN [Moment] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_filebind_Moment NVARCHAR(MAX);
    SET @sql_add_social_filebind_Moment = 'ALTER TABLE social_filebind ADD [Moment] BIGINT'
    EXEC sp_executesql @sql_add_social_filebind_Moment
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_filebindMoment')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [Constraint_social_filebindMoment]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_filebindMoment')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [UniqueNonclustered_social_filebindMoment]
    END

-- [social_filebind.Desc] -------------


-- [social_filebind.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_filebind') AND name='Desc')
    BEGIN
     ALTER TABLE social_filebind ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_filebind_Desc NVARCHAR(MAX);
    SET @sql_add_social_filebind_Desc = 'ALTER TABLE social_filebind ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_filebind_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_filebindDesc')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [Constraint_social_filebindDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_filebindDesc')
    BEGIN
    ALTER TABLE social_filebind DROP  CONSTRAINT [UniqueNonclustered_social_filebindDesc]
    END
-- [social_moment] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='social_moment' AND xtype='U')

BEGIN

    CREATE TABLE social_moment ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Title] NVARCHAR(MAX)
        ,[Summary] NVARCHAR(MAX)
        ,[FullText] NVARCHAR(MAX)
        ,[Tags] NVARCHAR(MAX)
        ,[PreviewImgUrl] NVARCHAR(MAX)
        ,[Link] NVARCHAR(MAX)
        ,[Type] INT
        ,[State] INT
        ,[MediaType] INT
, CONSTRAINT [PK_social_moment] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_social_moment NVARCHAR(64)
DECLARE cursor_social_moment CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Title','Summary','FullText','Tags','PreviewImgUrl','Link','Type','State','MediaType'))

OPEN cursor_social_moment
FETCH NEXT FROM cursor_social_moment INTO @name_social_moment

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping social_moment.' + @name_social_moment;
    
    DECLARE @sql_social_moment NVARCHAR(MAX);
    SET @sql_social_moment = 'ALTER TABLE social_moment DROP COLUMN ' + QUOTENAME(@name_social_moment)
    EXEC sp_executesql @sql_social_moment
    
    
    FETCH NEXT FROM cursor_social_moment INTO @name_social_moment
END

CLOSE cursor_social_moment;
DEALLOCATE cursor_social_moment;


-- [social_moment.Title] -------------


-- [social_moment.Title] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='Title')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [Title] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_Title NVARCHAR(MAX);
    SET @sql_add_social_moment_Title = 'ALTER TABLE social_moment ADD [Title] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_Title
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentTitle')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentTitle]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentTitle')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentTitle]
    END

-- [social_moment.Summary] -------------


-- [social_moment.Summary] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='Summary')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [Summary] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_Summary NVARCHAR(MAX);
    SET @sql_add_social_moment_Summary = 'ALTER TABLE social_moment ADD [Summary] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_Summary
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentSummary')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentSummary]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentSummary')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentSummary]
    END

-- [social_moment.FullText] -------------


-- [social_moment.FullText] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='FullText')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [FullText] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_FullText NVARCHAR(MAX);
    SET @sql_add_social_moment_FullText = 'ALTER TABLE social_moment ADD [FullText] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_FullText
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentFullText')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentFullText]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentFullText')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentFullText]
    END

-- [social_moment.Tags] -------------


-- [social_moment.Tags] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='Tags')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [Tags] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_Tags NVARCHAR(MAX);
    SET @sql_add_social_moment_Tags = 'ALTER TABLE social_moment ADD [Tags] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_Tags
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentTags')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentTags]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentTags')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentTags]
    END

-- [social_moment.PreviewImgUrl] -------------


-- [social_moment.PreviewImgUrl] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='PreviewImgUrl')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [PreviewImgUrl] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_PreviewImgUrl NVARCHAR(MAX);
    SET @sql_add_social_moment_PreviewImgUrl = 'ALTER TABLE social_moment ADD [PreviewImgUrl] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_PreviewImgUrl
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentPreviewImgUrl')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentPreviewImgUrl]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentPreviewImgUrl')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentPreviewImgUrl]
    END

-- [social_moment.Link] -------------


-- [social_moment.Link] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='Link')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [Link] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_Link NVARCHAR(MAX);
    SET @sql_add_social_moment_Link = 'ALTER TABLE social_moment ADD [Link] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_social_moment_Link
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentLink')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentLink]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentLink')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentLink]
    END

-- [social_moment.Type] -------------


-- [social_moment.Type] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='Type')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [Type] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_Type NVARCHAR(MAX);
    SET @sql_add_social_moment_Type = 'ALTER TABLE social_moment ADD [Type] INT'
    EXEC sp_executesql @sql_add_social_moment_Type
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentType')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentType')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentType]
    END

-- [social_moment.State] -------------


-- [social_moment.State] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='State')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [State] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_State NVARCHAR(MAX);
    SET @sql_add_social_moment_State = 'ALTER TABLE social_moment ADD [State] INT'
    EXEC sp_executesql @sql_add_social_moment_State
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentState')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentState]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentState')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentState]
    END

-- [social_moment.MediaType] -------------


-- [social_moment.MediaType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('social_moment') AND name='MediaType')
    BEGIN
     ALTER TABLE social_moment ALTER COLUMN [MediaType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_social_moment_MediaType NVARCHAR(MAX);
    SET @sql_add_social_moment_MediaType = 'ALTER TABLE social_moment ADD [MediaType] INT'
    EXEC sp_executesql @sql_add_social_moment_MediaType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_social_momentMediaType')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [Constraint_social_momentMediaType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_social_momentMediaType')
    BEGIN
    ALTER TABLE social_moment DROP  CONSTRAINT [UniqueNonclustered_social_momentMediaType]
    END
-- [sys_log] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='sys_log' AND xtype='U')

BEGIN

    CREATE TABLE sys_log ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Location] NVARCHAR(MAX)
        ,[Content] NVARCHAR(MAX)
        ,[Sql] NVARCHAR(MAX)
, CONSTRAINT [PK_sys_log] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_sys_log NVARCHAR(64)
DECLARE cursor_sys_log CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('sys_log') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Location','Content','Sql'))

OPEN cursor_sys_log
FETCH NEXT FROM cursor_sys_log INTO @name_sys_log

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping sys_log.' + @name_sys_log;
    
    DECLARE @sql_sys_log NVARCHAR(MAX);
    SET @sql_sys_log = 'ALTER TABLE sys_log DROP COLUMN ' + QUOTENAME(@name_sys_log)
    EXEC sp_executesql @sql_sys_log
    
    
    FETCH NEXT FROM cursor_sys_log INTO @name_sys_log
END

CLOSE cursor_sys_log;
DEALLOCATE cursor_sys_log;


-- [sys_log.Location] -------------


-- [sys_log.Location] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('sys_log') AND name='Location')
    BEGIN
     ALTER TABLE sys_log ALTER COLUMN [Location] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_sys_log_Location NVARCHAR(MAX);
    SET @sql_add_sys_log_Location = 'ALTER TABLE sys_log ADD [Location] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_sys_log_Location
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_sys_logLocation')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [Constraint_sys_logLocation]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_sys_logLocation')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [UniqueNonclustered_sys_logLocation]
    END

-- [sys_log.Content] -------------


-- [sys_log.Content] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('sys_log') AND name='Content')
    BEGIN
     ALTER TABLE sys_log ALTER COLUMN [Content] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_sys_log_Content NVARCHAR(MAX);
    SET @sql_add_sys_log_Content = 'ALTER TABLE sys_log ADD [Content] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_sys_log_Content
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_sys_logContent')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [Constraint_sys_logContent]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_sys_logContent')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [UniqueNonclustered_sys_logContent]
    END

-- [sys_log.Sql] -------------


-- [sys_log.Sql] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('sys_log') AND name='Sql')
    BEGIN
     ALTER TABLE sys_log ALTER COLUMN [Sql] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_sys_log_Sql NVARCHAR(MAX);
    SET @sql_add_sys_log_Sql = 'ALTER TABLE sys_log ADD [Sql] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_sys_log_Sql
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_sys_logSql')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [Constraint_sys_logSql]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_sys_logSql')
    BEGIN
    ALTER TABLE sys_log DROP  CONSTRAINT [UniqueNonclustered_sys_logSql]
    END
-- [sys_pagelog] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='sys_pagelog' AND xtype='U')

BEGIN

    CREATE TABLE sys_pagelog ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Request] NVARCHAR(MAX)
, CONSTRAINT [PK_sys_pagelog] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_sys_pagelog NVARCHAR(64)
DECLARE cursor_sys_pagelog CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('sys_pagelog') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Ip','Request'))

OPEN cursor_sys_pagelog
FETCH NEXT FROM cursor_sys_pagelog INTO @name_sys_pagelog

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping sys_pagelog.' + @name_sys_pagelog;
    
    DECLARE @sql_sys_pagelog NVARCHAR(MAX);
    SET @sql_sys_pagelog = 'ALTER TABLE sys_pagelog DROP COLUMN ' + QUOTENAME(@name_sys_pagelog)
    EXEC sp_executesql @sql_sys_pagelog
    
    
    FETCH NEXT FROM cursor_sys_pagelog INTO @name_sys_pagelog
END

CLOSE cursor_sys_pagelog;
DEALLOCATE cursor_sys_pagelog;


-- [sys_pagelog.Ip] -------------


-- [sys_pagelog.Ip] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('sys_pagelog') AND name='Ip')
    BEGIN
     ALTER TABLE sys_pagelog ALTER COLUMN [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_sys_pagelog_Ip NVARCHAR(MAX);
    SET @sql_add_sys_pagelog_Ip = 'ALTER TABLE sys_pagelog ADD [Ip] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_sys_pagelog_Ip
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_sys_pagelogIp')
    BEGIN
    ALTER TABLE sys_pagelog DROP  CONSTRAINT [Constraint_sys_pagelogIp]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_sys_pagelogIp')
    BEGIN
    ALTER TABLE sys_pagelog DROP  CONSTRAINT [UniqueNonclustered_sys_pagelogIp]
    END

-- [sys_pagelog.Request] -------------


-- [sys_pagelog.Request] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('sys_pagelog') AND name='Request')
    BEGIN
     ALTER TABLE sys_pagelog ALTER COLUMN [Request] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_sys_pagelog_Request NVARCHAR(MAX);
    SET @sql_add_sys_pagelog_Request = 'ALTER TABLE sys_pagelog ADD [Request] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_sys_pagelog_Request
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_sys_pagelogRequest')
    BEGIN
    ALTER TABLE sys_pagelog DROP  CONSTRAINT [Constraint_sys_pagelogRequest]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_sys_pagelogRequest')
    BEGIN
    ALTER TABLE sys_pagelog DROP  CONSTRAINT [UniqueNonclustered_sys_pagelogRequest]
    END
-- [ts_api] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_api' AND xtype='U')

BEGIN

    CREATE TABLE ts_api ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_ts_api] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_api NVARCHAR(64)
DECLARE cursor_ts_api CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_api') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Project'))

OPEN cursor_ts_api
FETCH NEXT FROM cursor_ts_api INTO @name_ts_api

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_api.' + @name_ts_api;
    
    DECLARE @sql_ts_api NVARCHAR(MAX);
    SET @sql_ts_api = 'ALTER TABLE ts_api DROP COLUMN ' + QUOTENAME(@name_ts_api)
    EXEC sp_executesql @sql_ts_api
    
    
    FETCH NEXT FROM cursor_ts_api INTO @name_ts_api
END

CLOSE cursor_ts_api;
DEALLOCATE cursor_ts_api;


-- [ts_api.Name] -------------


-- [ts_api.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_api') AND name='Name')
    BEGIN
     ALTER TABLE ts_api ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_api_Name NVARCHAR(MAX);
    SET @sql_add_ts_api_Name = 'ALTER TABLE ts_api ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_api_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_apiName')
    BEGIN
    ALTER TABLE ts_api DROP  CONSTRAINT [Constraint_ts_apiName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_apiName')
    BEGIN
    ALTER TABLE ts_api DROP  CONSTRAINT [UniqueNonclustered_ts_apiName]
    END

-- [ts_api.Project] -------------


-- [ts_api.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_api') AND name='Project')
    BEGIN
     ALTER TABLE ts_api ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_api_Project NVARCHAR(MAX);
    SET @sql_add_ts_api_Project = 'ALTER TABLE ts_api ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_api_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_apiProject')
    BEGIN
    ALTER TABLE ts_api DROP  CONSTRAINT [Constraint_ts_apiProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_apiProject')
    BEGIN
    ALTER TABLE ts_api DROP  CONSTRAINT [UniqueNonclustered_ts_apiProject]
    END
-- [ts_field] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_field' AND xtype='U')

BEGIN

    CREATE TABLE ts_field ([ID] BIGINT NOT NULL
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
, CONSTRAINT [PK_ts_field] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_field NVARCHAR(64)
DECLARE cursor_ts_field CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc','FieldType','Length','SelectLines','Project','Table'))

OPEN cursor_ts_field
FETCH NEXT FROM cursor_ts_field INTO @name_ts_field

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_field.' + @name_ts_field;
    
    DECLARE @sql_ts_field NVARCHAR(MAX);
    SET @sql_ts_field = 'ALTER TABLE ts_field DROP COLUMN ' + QUOTENAME(@name_ts_field)
    EXEC sp_executesql @sql_ts_field
    
    
    FETCH NEXT FROM cursor_ts_field INTO @name_ts_field
END

CLOSE cursor_ts_field;
DEALLOCATE cursor_ts_field;


-- [ts_field.Name] -------------


-- [ts_field.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='Name')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_Name NVARCHAR(MAX);
    SET @sql_add_ts_field_Name = 'ALTER TABLE ts_field ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_field_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldName')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldName')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldName]
    END

-- [ts_field.Desc] -------------


-- [ts_field.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='Desc')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_Desc NVARCHAR(MAX);
    SET @sql_add_ts_field_Desc = 'ALTER TABLE ts_field ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_field_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldDesc')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldDesc')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldDesc]
    END

-- [ts_field.FieldType] -------------


-- [ts_field.FieldType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='FieldType')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [FieldType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_FieldType NVARCHAR(MAX);
    SET @sql_add_ts_field_FieldType = 'ALTER TABLE ts_field ADD [FieldType] INT'
    EXEC sp_executesql @sql_add_ts_field_FieldType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldFieldType')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldFieldType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldFieldType')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldFieldType]
    END

-- [ts_field.Length] -------------


-- [ts_field.Length] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='Length')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [Length] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_Length NVARCHAR(MAX);
    SET @sql_add_ts_field_Length = 'ALTER TABLE ts_field ADD [Length] BIGINT'
    EXEC sp_executesql @sql_add_ts_field_Length
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldLength')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldLength]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldLength')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldLength]
    END

-- [ts_field.SelectLines] -------------


-- [ts_field.SelectLines] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='SelectLines')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [SelectLines] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_SelectLines NVARCHAR(MAX);
    SET @sql_add_ts_field_SelectLines = 'ALTER TABLE ts_field ADD [SelectLines] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_field_SelectLines
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldSelectLines')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldSelectLines]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldSelectLines')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldSelectLines]
    END

-- [ts_field.Project] -------------


-- [ts_field.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='Project')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_Project NVARCHAR(MAX);
    SET @sql_add_ts_field_Project = 'ALTER TABLE ts_field ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_field_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldProject')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldProject')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldProject]
    END

-- [ts_field.Table] -------------


-- [ts_field.Table] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_field') AND name='Table')
    BEGIN
     ALTER TABLE ts_field ALTER COLUMN [Table] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_field_Table NVARCHAR(MAX);
    SET @sql_add_ts_field_Table = 'ALTER TABLE ts_field ADD [Table] BIGINT'
    EXEC sp_executesql @sql_add_ts_field_Table
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_fieldTable')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [Constraint_ts_fieldTable]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_fieldTable')
    BEGIN
    ALTER TABLE ts_field DROP  CONSTRAINT [UniqueNonclustered_ts_fieldTable]
    END
-- [ts_hostconfig] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_hostconfig' AND xtype='U')

BEGIN

    CREATE TABLE ts_hostconfig ([ID] BIGINT NOT NULL
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
, CONSTRAINT [PK_ts_hostconfig] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_hostconfig NVARCHAR(64)
DECLARE cursor_ts_hostconfig CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Hostname','Database','DatabaseName','DatabaseConn','DirVs','DirVsCodeWeb','Project'))

OPEN cursor_ts_hostconfig
FETCH NEXT FROM cursor_ts_hostconfig INTO @name_ts_hostconfig

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_hostconfig.' + @name_ts_hostconfig;
    
    DECLARE @sql_ts_hostconfig NVARCHAR(MAX);
    SET @sql_ts_hostconfig = 'ALTER TABLE ts_hostconfig DROP COLUMN ' + QUOTENAME(@name_ts_hostconfig)
    EXEC sp_executesql @sql_ts_hostconfig
    
    
    FETCH NEXT FROM cursor_ts_hostconfig INTO @name_ts_hostconfig
END

CLOSE cursor_ts_hostconfig;
DEALLOCATE cursor_ts_hostconfig;


-- [ts_hostconfig.Hostname] -------------


-- [ts_hostconfig.Hostname] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='Hostname')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [Hostname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_Hostname NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_Hostname = 'ALTER TABLE ts_hostconfig ADD [Hostname] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_hostconfig_Hostname
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigHostname')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigHostname]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigHostname')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigHostname]
    END

-- [ts_hostconfig.Database] -------------


-- [ts_hostconfig.Database] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='Database')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [Database] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_Database NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_Database = 'ALTER TABLE ts_hostconfig ADD [Database] INT'
    EXEC sp_executesql @sql_add_ts_hostconfig_Database
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigDatabase')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigDatabase]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigDatabase')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigDatabase]
    END

-- [ts_hostconfig.DatabaseName] -------------


-- [ts_hostconfig.DatabaseName] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='DatabaseName')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_DatabaseName NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_DatabaseName = 'ALTER TABLE ts_hostconfig ADD [DatabaseName] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_hostconfig_DatabaseName
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigDatabaseName')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigDatabaseName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigDatabaseName')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigDatabaseName]
    END

-- [ts_hostconfig.DatabaseConn] -------------


-- [ts_hostconfig.DatabaseConn] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='DatabaseConn')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_DatabaseConn NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_DatabaseConn = 'ALTER TABLE ts_hostconfig ADD [DatabaseConn] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_hostconfig_DatabaseConn
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigDatabaseConn')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigDatabaseConn]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigDatabaseConn')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigDatabaseConn]
    END

-- [ts_hostconfig.DirVs] -------------


-- [ts_hostconfig.DirVs] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='DirVs')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [DirVs] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_DirVs NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_DirVs = 'ALTER TABLE ts_hostconfig ADD [DirVs] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_hostconfig_DirVs
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigDirVs')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigDirVs]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigDirVs')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigDirVs]
    END

-- [ts_hostconfig.DirVsCodeWeb] -------------


-- [ts_hostconfig.DirVsCodeWeb] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='DirVsCodeWeb')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_DirVsCodeWeb NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_DirVsCodeWeb = 'ALTER TABLE ts_hostconfig ADD [DirVsCodeWeb] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_hostconfig_DirVsCodeWeb
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigDirVsCodeWeb')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigDirVsCodeWeb]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigDirVsCodeWeb')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigDirVsCodeWeb]
    END

-- [ts_hostconfig.Project] -------------


-- [ts_hostconfig.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_hostconfig') AND name='Project')
    BEGIN
     ALTER TABLE ts_hostconfig ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_hostconfig_Project NVARCHAR(MAX);
    SET @sql_add_ts_hostconfig_Project = 'ALTER TABLE ts_hostconfig ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_hostconfig_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_hostconfigProject')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [Constraint_ts_hostconfigProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_hostconfigProject')
    BEGIN
    ALTER TABLE ts_hostconfig DROP  CONSTRAINT [UniqueNonclustered_ts_hostconfigProject]
    END
-- [ts_project] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_project' AND xtype='U')

BEGIN

    CREATE TABLE ts_project ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
, CONSTRAINT [PK_ts_project] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_project NVARCHAR(64)
DECLARE cursor_ts_project CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_project') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Code','Caption','TypeSessionUser'))

OPEN cursor_ts_project
FETCH NEXT FROM cursor_ts_project INTO @name_ts_project

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_project.' + @name_ts_project;
    
    DECLARE @sql_ts_project NVARCHAR(MAX);
    SET @sql_ts_project = 'ALTER TABLE ts_project DROP COLUMN ' + QUOTENAME(@name_ts_project)
    EXEC sp_executesql @sql_ts_project
    
    
    FETCH NEXT FROM cursor_ts_project INTO @name_ts_project
END

CLOSE cursor_ts_project;
DEALLOCATE cursor_ts_project;


-- [ts_project.Code] -------------


-- [ts_project.Code] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_project') AND name='Code')
    BEGIN
     ALTER TABLE ts_project ALTER COLUMN [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_project_Code NVARCHAR(MAX);
    SET @sql_add_ts_project_Code = 'ALTER TABLE ts_project ADD [Code] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_project_Code
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_projectCode')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [Constraint_ts_projectCode]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_projectCode')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [UniqueNonclustered_ts_projectCode]
    END

-- [ts_project.Caption] -------------


-- [ts_project.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_project') AND name='Caption')
    BEGIN
     ALTER TABLE ts_project ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_project_Caption NVARCHAR(MAX);
    SET @sql_add_ts_project_Caption = 'ALTER TABLE ts_project ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_project_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_projectCaption')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [Constraint_ts_projectCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_projectCaption')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [UniqueNonclustered_ts_projectCaption]
    END

-- [ts_project.TypeSessionUser] -------------


-- [ts_project.TypeSessionUser] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_project') AND name='TypeSessionUser')
    BEGIN
     ALTER TABLE ts_project ALTER COLUMN [TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_project_TypeSessionUser NVARCHAR(MAX);
    SET @sql_add_ts_project_TypeSessionUser = 'ALTER TABLE ts_project ADD [TypeSessionUser] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_project_TypeSessionUser
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_projectTypeSessionUser')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [Constraint_ts_projectTypeSessionUser]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_projectTypeSessionUser')
    BEGIN
    ALTER TABLE ts_project DROP  CONSTRAINT [UniqueNonclustered_ts_projectTypeSessionUser]
    END
-- [ts_table] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_table' AND xtype='U')

BEGIN

    CREATE TABLE ts_table ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Desc] NVARCHAR(MAX)
        ,[Project] BIGINT
, CONSTRAINT [PK_ts_table] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_table NVARCHAR(64)
DECLARE cursor_ts_table CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_table') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Desc','Project'))

OPEN cursor_ts_table
FETCH NEXT FROM cursor_ts_table INTO @name_ts_table

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_table.' + @name_ts_table;
    
    DECLARE @sql_ts_table NVARCHAR(MAX);
    SET @sql_ts_table = 'ALTER TABLE ts_table DROP COLUMN ' + QUOTENAME(@name_ts_table)
    EXEC sp_executesql @sql_ts_table
    
    
    FETCH NEXT FROM cursor_ts_table INTO @name_ts_table
END

CLOSE cursor_ts_table;
DEALLOCATE cursor_ts_table;


-- [ts_table.Name] -------------


-- [ts_table.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_table') AND name='Name')
    BEGIN
     ALTER TABLE ts_table ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_table_Name NVARCHAR(MAX);
    SET @sql_add_ts_table_Name = 'ALTER TABLE ts_table ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_table_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_tableName')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [Constraint_ts_tableName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_tableName')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [UniqueNonclustered_ts_tableName]
    END

-- [ts_table.Desc] -------------


-- [ts_table.Desc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_table') AND name='Desc')
    BEGIN
     ALTER TABLE ts_table ALTER COLUMN [Desc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_table_Desc NVARCHAR(MAX);
    SET @sql_add_ts_table_Desc = 'ALTER TABLE ts_table ADD [Desc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_table_Desc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_tableDesc')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [Constraint_ts_tableDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_tableDesc')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [UniqueNonclustered_ts_tableDesc]
    END

-- [ts_table.Project] -------------


-- [ts_table.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_table') AND name='Project')
    BEGIN
     ALTER TABLE ts_table ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_table_Project NVARCHAR(MAX);
    SET @sql_add_ts_table_Project = 'ALTER TABLE ts_table ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_table_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_tableProject')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [Constraint_ts_tableProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_tableProject')
    BEGIN
    ALTER TABLE ts_table DROP  CONSTRAINT [UniqueNonclustered_ts_tableProject]
    END
-- [ts_uicomponent] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_uicomponent' AND xtype='U')

BEGIN

    CREATE TABLE ts_uicomponent ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_ts_uicomponent] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_uicomponent NVARCHAR(64)
DECLARE cursor_ts_uicomponent CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_uicomponent') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Project'))

OPEN cursor_ts_uicomponent
FETCH NEXT FROM cursor_ts_uicomponent INTO @name_ts_uicomponent

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_uicomponent.' + @name_ts_uicomponent;
    
    DECLARE @sql_ts_uicomponent NVARCHAR(MAX);
    SET @sql_ts_uicomponent = 'ALTER TABLE ts_uicomponent DROP COLUMN ' + QUOTENAME(@name_ts_uicomponent)
    EXEC sp_executesql @sql_ts_uicomponent
    
    
    FETCH NEXT FROM cursor_ts_uicomponent INTO @name_ts_uicomponent
END

CLOSE cursor_ts_uicomponent;
DEALLOCATE cursor_ts_uicomponent;


-- [ts_uicomponent.Name] -------------


-- [ts_uicomponent.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uicomponent') AND name='Name')
    BEGIN
     ALTER TABLE ts_uicomponent ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uicomponent_Name NVARCHAR(MAX);
    SET @sql_add_ts_uicomponent_Name = 'ALTER TABLE ts_uicomponent ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uicomponent_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uicomponentName')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [Constraint_ts_uicomponentName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uicomponentName')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [UniqueNonclustered_ts_uicomponentName]
    END

-- [ts_uicomponent.Caption] -------------


-- [ts_uicomponent.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uicomponent') AND name='Caption')
    BEGIN
     ALTER TABLE ts_uicomponent ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uicomponent_Caption NVARCHAR(MAX);
    SET @sql_add_ts_uicomponent_Caption = 'ALTER TABLE ts_uicomponent ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uicomponent_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uicomponentCaption')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [Constraint_ts_uicomponentCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uicomponentCaption')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [UniqueNonclustered_ts_uicomponentCaption]
    END

-- [ts_uicomponent.Project] -------------


-- [ts_uicomponent.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uicomponent') AND name='Project')
    BEGIN
     ALTER TABLE ts_uicomponent ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uicomponent_Project NVARCHAR(MAX);
    SET @sql_add_ts_uicomponent_Project = 'ALTER TABLE ts_uicomponent ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_uicomponent_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uicomponentProject')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [Constraint_ts_uicomponentProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uicomponentProject')
    BEGIN
    ALTER TABLE ts_uicomponent DROP  CONSTRAINT [UniqueNonclustered_ts_uicomponentProject]
    END
-- [ts_uipage] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_uipage' AND xtype='U')

BEGIN

    CREATE TABLE ts_uipage ([ID] BIGINT NOT NULL
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
, CONSTRAINT [PK_ts_uipage] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_uipage NVARCHAR(64)
DECLARE cursor_ts_uipage CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Route','OgTitle','OgDesc','OgImage','Template','Project'))

OPEN cursor_ts_uipage
FETCH NEXT FROM cursor_ts_uipage INTO @name_ts_uipage

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_uipage.' + @name_ts_uipage;
    
    DECLARE @sql_ts_uipage NVARCHAR(MAX);
    SET @sql_ts_uipage = 'ALTER TABLE ts_uipage DROP COLUMN ' + QUOTENAME(@name_ts_uipage)
    EXEC sp_executesql @sql_ts_uipage
    
    
    FETCH NEXT FROM cursor_ts_uipage INTO @name_ts_uipage
END

CLOSE cursor_ts_uipage;
DEALLOCATE cursor_ts_uipage;


-- [ts_uipage.Name] -------------


-- [ts_uipage.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='Name')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_Name NVARCHAR(MAX);
    SET @sql_add_ts_uipage_Name = 'ALTER TABLE ts_uipage ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uipage_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageName')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageName')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageName]
    END

-- [ts_uipage.Caption] -------------


-- [ts_uipage.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='Caption')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_Caption NVARCHAR(MAX);
    SET @sql_add_ts_uipage_Caption = 'ALTER TABLE ts_uipage ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uipage_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageCaption')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageCaption')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageCaption]
    END

-- [ts_uipage.Route] -------------


-- [ts_uipage.Route] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='Route')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [Route] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_Route NVARCHAR(MAX);
    SET @sql_add_ts_uipage_Route = 'ALTER TABLE ts_uipage ADD [Route] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_uipage_Route
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageRoute')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageRoute]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageRoute')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageRoute]
    END

-- [ts_uipage.OgTitle] -------------


-- [ts_uipage.OgTitle] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='OgTitle')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [OgTitle] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_OgTitle NVARCHAR(MAX);
    SET @sql_add_ts_uipage_OgTitle = 'ALTER TABLE ts_uipage ADD [OgTitle] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_uipage_OgTitle
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageOgTitle')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageOgTitle]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageOgTitle')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageOgTitle]
    END

-- [ts_uipage.OgDesc] -------------


-- [ts_uipage.OgDesc] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='OgDesc')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [OgDesc] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_OgDesc NVARCHAR(MAX);
    SET @sql_add_ts_uipage_OgDesc = 'ALTER TABLE ts_uipage ADD [OgDesc] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_uipage_OgDesc
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageOgDesc')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageOgDesc]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageOgDesc')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageOgDesc]
    END

-- [ts_uipage.OgImage] -------------


-- [ts_uipage.OgImage] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='OgImage')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [OgImage] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_OgImage NVARCHAR(MAX);
    SET @sql_add_ts_uipage_OgImage = 'ALTER TABLE ts_uipage ADD [OgImage] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_uipage_OgImage
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageOgImage')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageOgImage]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageOgImage')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageOgImage]
    END

-- [ts_uipage.Template] -------------


-- [ts_uipage.Template] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='Template')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [Template] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_Template NVARCHAR(MAX);
    SET @sql_add_ts_uipage_Template = 'ALTER TABLE ts_uipage ADD [Template] BIGINT'
    EXEC sp_executesql @sql_add_ts_uipage_Template
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageTemplate')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageTemplate]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageTemplate')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageTemplate]
    END

-- [ts_uipage.Project] -------------


-- [ts_uipage.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uipage') AND name='Project')
    BEGIN
     ALTER TABLE ts_uipage ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uipage_Project NVARCHAR(MAX);
    SET @sql_add_ts_uipage_Project = 'ALTER TABLE ts_uipage ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_uipage_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uipageProject')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [Constraint_ts_uipageProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uipageProject')
    BEGIN
    ALTER TABLE ts_uipage DROP  CONSTRAINT [UniqueNonclustered_ts_uipageProject]
    END
-- [ts_uitemplate] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_uitemplate' AND xtype='U')

BEGIN

    CREATE TABLE ts_uitemplate ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
        ,[Project] BIGINT
, CONSTRAINT [PK_ts_uitemplate] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_uitemplate NVARCHAR(64)
DECLARE cursor_ts_uitemplate CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_uitemplate') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Caption','Project'))

OPEN cursor_ts_uitemplate
FETCH NEXT FROM cursor_ts_uitemplate INTO @name_ts_uitemplate

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_uitemplate.' + @name_ts_uitemplate;
    
    DECLARE @sql_ts_uitemplate NVARCHAR(MAX);
    SET @sql_ts_uitemplate = 'ALTER TABLE ts_uitemplate DROP COLUMN ' + QUOTENAME(@name_ts_uitemplate)
    EXEC sp_executesql @sql_ts_uitemplate
    
    
    FETCH NEXT FROM cursor_ts_uitemplate INTO @name_ts_uitemplate
END

CLOSE cursor_ts_uitemplate;
DEALLOCATE cursor_ts_uitemplate;


-- [ts_uitemplate.Name] -------------


-- [ts_uitemplate.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uitemplate') AND name='Name')
    BEGIN
     ALTER TABLE ts_uitemplate ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uitemplate_Name NVARCHAR(MAX);
    SET @sql_add_ts_uitemplate_Name = 'ALTER TABLE ts_uitemplate ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uitemplate_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uitemplateName')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [Constraint_ts_uitemplateName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uitemplateName')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [UniqueNonclustered_ts_uitemplateName]
    END

-- [ts_uitemplate.Caption] -------------


-- [ts_uitemplate.Caption] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uitemplate') AND name='Caption')
    BEGIN
     ALTER TABLE ts_uitemplate ALTER COLUMN [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uitemplate_Caption NVARCHAR(MAX);
    SET @sql_add_ts_uitemplate_Caption = 'ALTER TABLE ts_uitemplate ADD [Caption] NVARCHAR(256) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_uitemplate_Caption
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uitemplateCaption')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [Constraint_ts_uitemplateCaption]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uitemplateCaption')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [UniqueNonclustered_ts_uitemplateCaption]
    END

-- [ts_uitemplate.Project] -------------


-- [ts_uitemplate.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_uitemplate') AND name='Project')
    BEGIN
     ALTER TABLE ts_uitemplate ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_uitemplate_Project NVARCHAR(MAX);
    SET @sql_add_ts_uitemplate_Project = 'ALTER TABLE ts_uitemplate ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_uitemplate_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_uitemplateProject')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [Constraint_ts_uitemplateProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_uitemplateProject')
    BEGIN
    ALTER TABLE ts_uitemplate DROP  CONSTRAINT [UniqueNonclustered_ts_uitemplateProject]
    END
-- [ts_vartype] ----------------------

IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='ts_vartype' AND xtype='U')

BEGIN

    CREATE TABLE ts_vartype ([ID] BIGINT NOT NULL
        ,[Createdat] BIGINT NOT NULL
        ,[Updatedat] BIGINT NOT NULL
        ,[Sort] BIGINT NOT NULL,
        [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
        ,[Val] NVARCHAR(MAX)
        ,[BindType] INT
        ,[Bind] BIGINT
        ,[Project] BIGINT
, CONSTRAINT [PK_ts_vartype] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]
END


-- Dropping obsolete fields -----------
DECLARE @name_ts_vartype NVARCHAR(64)
DECLARE cursor_ts_vartype CURSOR FOR 
    SELECT name FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND (name NOT IN ('ID','Createdat','Updatedat','Sort','Name','Type','Val','BindType','Bind','Project'))

OPEN cursor_ts_vartype
FETCH NEXT FROM cursor_ts_vartype INTO @name_ts_vartype

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Dropping ts_vartype.' + @name_ts_vartype;
    
    DECLARE @sql_ts_vartype NVARCHAR(MAX);
    SET @sql_ts_vartype = 'ALTER TABLE ts_vartype DROP COLUMN ' + QUOTENAME(@name_ts_vartype)
    EXEC sp_executesql @sql_ts_vartype
    
    
    FETCH NEXT FROM cursor_ts_vartype INTO @name_ts_vartype
END

CLOSE cursor_ts_vartype;
DEALLOCATE cursor_ts_vartype;


-- [ts_vartype.Name] -------------


-- [ts_vartype.Name] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='Name')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_Name NVARCHAR(MAX);
    SET @sql_add_ts_vartype_Name = 'ALTER TABLE ts_vartype ADD [Name] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_vartype_Name
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeName')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeName]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeName')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeName]
    END

-- [ts_vartype.Type] -------------


-- [ts_vartype.Type] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='Type')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_Type NVARCHAR(MAX);
    SET @sql_add_ts_vartype_Type = 'ALTER TABLE ts_vartype ADD [Type] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS'
    EXEC sp_executesql @sql_add_ts_vartype_Type
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeType')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeType')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeType]
    END

-- [ts_vartype.Val] -------------


-- [ts_vartype.Val] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='Val')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [Val] NVARCHAR(MAX)
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_Val NVARCHAR(MAX);
    SET @sql_add_ts_vartype_Val = 'ALTER TABLE ts_vartype ADD [Val] NVARCHAR(MAX)'
    EXEC sp_executesql @sql_add_ts_vartype_Val
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeVal')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeVal]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeVal')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeVal]
    END

-- [ts_vartype.BindType] -------------


-- [ts_vartype.BindType] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='BindType')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [BindType] INT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_BindType NVARCHAR(MAX);
    SET @sql_add_ts_vartype_BindType = 'ALTER TABLE ts_vartype ADD [BindType] INT'
    EXEC sp_executesql @sql_add_ts_vartype_BindType
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeBindType')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeBindType]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeBindType')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeBindType]
    END

-- [ts_vartype.Bind] -------------


-- [ts_vartype.Bind] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='Bind')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [Bind] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_Bind NVARCHAR(MAX);
    SET @sql_add_ts_vartype_Bind = 'ALTER TABLE ts_vartype ADD [Bind] BIGINT'
    EXEC sp_executesql @sql_add_ts_vartype_Bind
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeBind')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeBind]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeBind')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeBind]
    END

-- [ts_vartype.Project] -------------


-- [ts_vartype.Project] -------------

IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('ts_vartype') AND name='Project')
    BEGIN
     ALTER TABLE ts_vartype ALTER COLUMN [Project] BIGINT
    END
ELSE
    BEGIN
    DECLARE @sql_add_ts_vartype_Project NVARCHAR(MAX);
    SET @sql_add_ts_vartype_Project = 'ALTER TABLE ts_vartype ADD [Project] BIGINT'
    EXEC sp_executesql @sql_add_ts_vartype_Project
    END


IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_ts_vartypeProject')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [Constraint_ts_vartypeProject]
    END

IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_ts_vartypeProject')
    BEGIN
    ALTER TABLE ts_vartype DROP  CONSTRAINT [UniqueNonclustered_ts_vartypeProject]
    END