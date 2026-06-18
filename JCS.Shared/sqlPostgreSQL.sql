-- [ca_book] ----------------------

-- [ca_book] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ca_book' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ca_book" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"caption" VARCHAR(64)
            ,"email" VARCHAR(64)
            ,"message" TEXT
            ,CONSTRAINT "pk_ca_book" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ca_book' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'caption', 'email', 'message'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ca_book', fn);
    END LOOP;
END $$;


-- [ca_book.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_book' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_book ADD "caption" varchar(64);
    END IF;
END $$;

-- [ca_book.Email] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_book' AND column_name='email'));

    IF not condition THEN
        ALTER TABLE ca_book ADD "email" varchar(64);
    END IF;
END $$;

-- [ca_book.Message] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_book' AND column_name='message'));

    IF not condition THEN
        ALTER TABLE ca_book ADD "message" text;
    END IF;
END $$;
-- [ca_enduser] ----------------------

-- [ca_enduser] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ca_enduser' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ca_enduser" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"caption" VARCHAR(64)
            ,"authtype" INT
            ,CONSTRAINT "pk_ca_enduser" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ca_enduser' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'caption', 'authtype'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ca_enduser', fn);
    END LOOP;
END $$;


-- [ca_enduser.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "caption" varchar(64);
    END IF;
END $$;

-- [ca_enduser.AuthType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='authtype'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "authtype" int;
    END IF;
END $$;
-- [ca_file] ----------------------

-- [ca_file] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ca_file' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ca_file" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"caption" TEXT
            ,"desc" TEXT
            ,"suffix" VARCHAR(4)
            ,"size" BIGINT
            ,"thumbnail" BYTEA
            ,"owner" BIGINT
            ,CONSTRAINT "pk_ca_file" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ca_file' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'caption', 'desc', 'suffix', 'size', 'thumbnail', 'owner'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ca_file', fn);
    END LOOP;
END $$;


-- [ca_file.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "caption" text;
    END IF;
END $$;

-- [ca_file.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "desc" text;
    END IF;
END $$;

-- [ca_file.Suffix] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='suffix'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "suffix" varchar(4);
    END IF;
END $$;

-- [ca_file.Size] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='size'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "size" bigint;
    END IF;
END $$;

-- [ca_file.Thumbnail] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='thumbnail'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "thumbnail" bytea;
    END IF;
END $$;

-- [ca_file.Owner] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='owner'));

    IF not condition THEN
        ALTER TABLE ca_file ADD "owner" bigint;
    END IF;
END $$;
-- [social_filebind] ----------------------

-- [social_filebind] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'social_filebind' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "social_filebind" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"file" BIGINT
            ,"moment" BIGINT
            ,"desc" TEXT
            ,CONSTRAINT "pk_social_filebind" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'social_filebind' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'file', 'moment', 'desc'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'social_filebind', fn);
    END LOOP;
END $$;


-- [social_filebind.File] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_filebind' AND column_name='file'));

    IF not condition THEN
        ALTER TABLE social_filebind ADD "file" bigint;
    END IF;
END $$;

-- [social_filebind.Moment] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_filebind' AND column_name='moment'));

    IF not condition THEN
        ALTER TABLE social_filebind ADD "moment" bigint;
    END IF;
END $$;

-- [social_filebind.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_filebind' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE social_filebind ADD "desc" text;
    END IF;
END $$;
-- [social_moment] ----------------------

-- [social_moment] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'social_moment' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "social_moment" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"title" TEXT
            ,"summary" TEXT
            ,"fulltext" TEXT
            ,"tags" TEXT
            ,"previewimgurl" TEXT
            ,"link" TEXT
            ,"type" INT
            ,"state" INT
            ,"mediatype" INT
            ,CONSTRAINT "pk_social_moment" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'social_moment' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'title', 'summary', 'fulltext', 'tags', 'previewimgurl', 'link', 'type', 'state', 'mediatype'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'social_moment', fn);
    END LOOP;
END $$;


-- [social_moment.Title] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='title'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "title" text;
    END IF;
END $$;

-- [social_moment.Summary] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='summary'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "summary" text;
    END IF;
END $$;

-- [social_moment.FullText] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='fulltext'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "fulltext" text;
    END IF;
END $$;

-- [social_moment.Tags] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='tags'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "tags" text;
    END IF;
END $$;

-- [social_moment.PreviewImgUrl] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='previewimgurl'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "previewimgurl" text;
    END IF;
END $$;

-- [social_moment.Link] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='link'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "link" text;
    END IF;
END $$;

-- [social_moment.Type] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='type'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "type" int;
    END IF;
END $$;

-- [social_moment.State] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='state'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "state" int;
    END IF;
END $$;

-- [social_moment.MediaType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='social_moment' AND column_name='mediatype'));

    IF not condition THEN
        ALTER TABLE social_moment ADD "mediatype" int;
    END IF;
END $$;
-- [sys_log] ----------------------

-- [sys_log] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'sys_log' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "sys_log" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"location" TEXT
            ,"content" TEXT
            ,"sql" TEXT
            ,CONSTRAINT "pk_sys_log" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'sys_log' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'location', 'content', 'sql'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'sys_log', fn);
    END LOOP;
END $$;


-- [sys_log.Location] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='sys_log' AND column_name='location'));

    IF not condition THEN
        ALTER TABLE sys_log ADD "location" text;
    END IF;
END $$;

-- [sys_log.Content] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='sys_log' AND column_name='content'));

    IF not condition THEN
        ALTER TABLE sys_log ADD "content" text;
    END IF;
END $$;

-- [sys_log.Sql] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='sys_log' AND column_name='sql'));

    IF not condition THEN
        ALTER TABLE sys_log ADD "sql" text;
    END IF;
END $$;
-- [sys_pagelog] ----------------------

-- [sys_pagelog] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'sys_pagelog' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "sys_pagelog" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"ip" VARCHAR(64)
            ,"request" TEXT
            ,CONSTRAINT "pk_sys_pagelog" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'sys_pagelog' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'ip', 'request'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'sys_pagelog', fn);
    END LOOP;
END $$;


-- [sys_pagelog.Ip] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='sys_pagelog' AND column_name='ip'));

    IF not condition THEN
        ALTER TABLE sys_pagelog ADD "ip" varchar(64);
    END IF;
END $$;

-- [sys_pagelog.Request] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='sys_pagelog' AND column_name='request'));

    IF not condition THEN
        ALTER TABLE sys_pagelog ADD "request" text;
    END IF;
END $$;
-- [ts_api] ----------------------

-- [ts_api] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_api' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_api" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_api" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_api' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_api', fn);
    END LOOP;
END $$;


-- [ts_api.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_api' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_api ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_api.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_api' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_api ADD "project" bigint;
    END IF;
END $$;
-- [ts_field] ----------------------

-- [ts_field] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_field' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_field" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"desc" TEXT
            ,"fieldtype" INT
            ,"length" BIGINT
            ,"selectlines" TEXT
            ,"project" BIGINT
            ,"table" BIGINT
            ,CONSTRAINT "pk_ts_field" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_field' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'desc', 'fieldtype', 'length', 'selectlines', 'project', 'table'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_field', fn);
    END LOOP;
END $$;


-- [ts_field.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_field.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "desc" text;
    END IF;
END $$;

-- [ts_field.FieldType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='fieldtype'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "fieldtype" int;
    END IF;
END $$;

-- [ts_field.Length] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='length'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "length" bigint;
    END IF;
END $$;

-- [ts_field.SelectLines] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='selectlines'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "selectlines" text;
    END IF;
END $$;

-- [ts_field.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "project" bigint;
    END IF;
END $$;

-- [ts_field.Table] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='table'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "table" bigint;
    END IF;
END $$;
-- [ts_hostconfig] ----------------------

-- [ts_hostconfig] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_hostconfig' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_hostconfig" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"hostname" VARCHAR(64)
            ,"database" INT
            ,"databasename" VARCHAR(64)
            ,"databaseconn" VARCHAR(64)
            ,"dirvs" VARCHAR(64)
            ,"dirvscodeweb" VARCHAR(64)
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_hostconfig" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_hostconfig' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'hostname', 'database', 'databasename', 'databaseconn', 'dirvs', 'dirvscodeweb', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_hostconfig', fn);
    END LOOP;
END $$;


-- [ts_hostconfig.Hostname] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='hostname'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "hostname" varchar(64);
    END IF;
END $$;

-- [ts_hostconfig.Database] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='database'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "database" int;
    END IF;
END $$;

-- [ts_hostconfig.DatabaseName] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='databasename'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "databasename" varchar(64);
    END IF;
END $$;

-- [ts_hostconfig.DatabaseConn] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='databaseconn'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "databaseconn" varchar(64);
    END IF;
END $$;

-- [ts_hostconfig.DirVs] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='dirvs'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "dirvs" varchar(64);
    END IF;
END $$;

-- [ts_hostconfig.DirVsCodeWeb] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='dirvscodeweb'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "dirvscodeweb" varchar(64);
    END IF;
END $$;

-- [ts_hostconfig.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "project" bigint;
    END IF;
END $$;
-- [ts_project] ----------------------

-- [ts_project] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_project' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_project" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"code" VARCHAR(64)
            ,"caption" VARCHAR(256)
            ,"typesessionuser" VARCHAR(64)
            ,CONSTRAINT "pk_ts_project" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_project' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'code', 'caption', 'typesessionuser'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_project', fn);
    END LOOP;
END $$;


-- [ts_project.Code] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='code'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "code" varchar(64);
    END IF;
END $$;

-- [ts_project.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "caption" varchar(256);
    END IF;
END $$;

-- [ts_project.TypeSessionUser] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='typesessionuser'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "typesessionuser" varchar(64);
    END IF;
END $$;
-- [ts_table] ----------------------

-- [ts_table] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_table' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_table" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"desc" TEXT
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_table" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_table' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'desc', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_table', fn);
    END LOOP;
END $$;


-- [ts_table.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_table.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "desc" text;
    END IF;
END $$;

-- [ts_table.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "project" bigint;
    END IF;
END $$;
-- [ts_uicomponent] ----------------------

-- [ts_uicomponent] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_uicomponent' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_uicomponent" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"caption" VARCHAR(256)
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_uicomponent" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_uicomponent' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'caption', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_uicomponent', fn);
    END LOOP;
END $$;


-- [ts_uicomponent.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_uicomponent.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "caption" varchar(256);
    END IF;
END $$;

-- [ts_uicomponent.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "project" bigint;
    END IF;
END $$;
-- [ts_uipage] ----------------------

-- [ts_uipage] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_uipage' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_uipage" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"caption" VARCHAR(256)
            ,"route" TEXT
            ,"ogtitle" TEXT
            ,"ogdesc" TEXT
            ,"ogimage" TEXT
            ,"template" BIGINT
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_uipage" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_uipage' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'caption', 'route', 'ogtitle', 'ogdesc', 'ogimage', 'template', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_uipage', fn);
    END LOOP;
END $$;


-- [ts_uipage.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_uipage.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "caption" varchar(256);
    END IF;
END $$;

-- [ts_uipage.Route] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='route'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "route" text;
    END IF;
END $$;

-- [ts_uipage.OgTitle] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogtitle'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogtitle" text;
    END IF;
END $$;

-- [ts_uipage.OgDesc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogdesc'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogdesc" text;
    END IF;
END $$;

-- [ts_uipage.OgImage] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogimage'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogimage" text;
    END IF;
END $$;

-- [ts_uipage.Template] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='template'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "template" bigint;
    END IF;
END $$;

-- [ts_uipage.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "project" bigint;
    END IF;
END $$;
-- [ts_uitemplate] ----------------------

-- [ts_uitemplate] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_uitemplate' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_uitemplate" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"caption" VARCHAR(256)
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_uitemplate" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_uitemplate' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'caption', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_uitemplate', fn);
    END LOOP;
END $$;


-- [ts_uitemplate.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_uitemplate.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "caption" varchar(256);
    END IF;
END $$;

-- [ts_uitemplate.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "project" bigint;
    END IF;
END $$;
-- [ts_vartype] ----------------------

-- [ts_vartype] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables 
        WHERE table_name = 'ts_vartype' 
          AND table_schema = 'public'
    ));

    IF not condition THEN
        CREATE TABLE "ts_vartype" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"name" VARCHAR(64)
            ,"type" VARCHAR(64)
            ,"val" TEXT
            ,"bindtype" INT
            ,"bind" BIGINT
            ,"project" BIGINT
            ,CONSTRAINT "pk_ts_vartype" PRIMARY KEY (id)
        );
    END IF;
END $$;


-- PostgreSQL: Dropping obsolete fields -----------
DO $$ 
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN 
        SELECT column_name 
        FROM information_schema.columns 
        WHERE table_name = 'ts_vartype' 
          AND table_schema = 'public' 
          AND column_name <> ALL(ARRAY['id', 'createdat', 'updatedat', 'sort', 'name', 'type', 'val', 'bindtype', 'bind', 'project'])
    LOOP
        -- 对应 PRINT 'Dropping ' + @tname + '.' + @fn
        
        -- 对应 EXEC sp_executesql @sql (format %I 对应 QUOTENAME)
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ts_vartype', fn);
    END LOOP;
END $$;


-- [ts_vartype.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "name" varchar(64);
    END IF;
END $$;

-- [ts_vartype.Type] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='type'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "type" varchar(64);
    END IF;
END $$;

-- [ts_vartype.Val] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='val'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "val" text;
    END IF;
END $$;

-- [ts_vartype.BindType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='bindtype'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "bindtype" int;
    END IF;
END $$;

-- [ts_vartype.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='bind'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "bind" bigint;
    END IF;
END $$;

-- [ts_vartype.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "project" bigint;
    END IF;
END $$;