-- [Ts_Api] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_api'));

    IF not condition THEN
    CREATE TABLE ts_api (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_Api.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_api' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_api ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_Api.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_api' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_api ADD "project" bigint;
    END IF;
END $$;
-- [Ts_Field] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_field'));

    IF not condition THEN
    CREATE TABLE ts_field (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"desc" TEXT
        ,"fieldtype" INT
        ,"length" BIGINT
        ,"selectlines" TEXT
        ,"project" BIGINT
        ,"table" BIGINT);

   END IF;
END $$;


-- [Ts_Field.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_Field.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "desc" text;
    END IF;
END $$;

-- [Ts_Field.FieldType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='fieldtype'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "fieldtype" int;
    END IF;
END $$;

-- [Ts_Field.Length] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='length'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "length" bigint;
    END IF;
END $$;

-- [Ts_Field.SelectLines] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='selectlines'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "selectlines" text;
    END IF;
END $$;

-- [Ts_Field.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "project" bigint;
    END IF;
END $$;

-- [Ts_Field.Table] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_field' AND column_name='table'));

    IF not condition THEN
        ALTER TABLE ts_field ADD "table" bigint;
    END IF;
END $$;
-- [Ts_HostConfig] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_hostconfig'));

    IF not condition THEN
    CREATE TABLE ts_hostconfig (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"hostname" VARCHAR(64)
        ,"database" INT
        ,"databasename" VARCHAR(64)
        ,"databaseconn" VARCHAR(64)
        ,"dirvsshared" VARCHAR(64)
        ,"dirvscodeweb" VARCHAR(64)
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_HostConfig.Hostname] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='hostname'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "hostname" varchar(64);
    END IF;
END $$;

-- [Ts_HostConfig.Database] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='database'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "database" int;
    END IF;
END $$;

-- [Ts_HostConfig.DatabaseName] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='databasename'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "databasename" varchar(64);
    END IF;
END $$;

-- [Ts_HostConfig.DatabaseConn] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='databaseconn'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "databaseconn" varchar(64);
    END IF;
END $$;

-- [Ts_HostConfig.DirVsShared] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='dirvsshared'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "dirvsshared" varchar(64);
    END IF;
END $$;

-- [Ts_HostConfig.DirVsCodeWeb] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='dirvscodeweb'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "dirvscodeweb" varchar(64);
    END IF;
END $$;

-- [Ts_HostConfig.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "project" bigint;
    END IF;
END $$;
-- [Ts_Project] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_project'));

    IF not condition THEN
    CREATE TABLE ts_project (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"code" VARCHAR(64)
        ,"caption" VARCHAR(256));

   END IF;
END $$;


-- [Ts_Project.Code] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='code'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "code" varchar(64);
    END IF;
END $$;

-- [Ts_Project.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "caption" varchar(256);
    END IF;
END $$;
-- [Ts_Table] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_table'));

    IF not condition THEN
    CREATE TABLE ts_table (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"desc" TEXT
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_Table.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_Table.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='desc'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "desc" text;
    END IF;
END $$;

-- [Ts_Table.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_table' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_table ADD "project" bigint;
    END IF;
END $$;
-- [Ts_UiComponent] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_uicomponent'));

    IF not condition THEN
    CREATE TABLE ts_uicomponent (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"caption" VARCHAR(256)
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_UiComponent.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_UiComponent.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "caption" varchar(256);
    END IF;
END $$;

-- [Ts_UiComponent.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uicomponent' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uicomponent ADD "project" bigint;
    END IF;
END $$;
-- [Ts_UiPage] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_uipage'));

    IF not condition THEN
    CREATE TABLE ts_uipage (id BIGINT NOT NULL
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
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_UiPage.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_UiPage.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "caption" varchar(256);
    END IF;
END $$;

-- [Ts_UiPage.Route] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='route'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "route" text;
    END IF;
END $$;

-- [Ts_UiPage.OgTitle] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogtitle'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogtitle" text;
    END IF;
END $$;

-- [Ts_UiPage.OgDesc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogdesc'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogdesc" text;
    END IF;
END $$;

-- [Ts_UiPage.OgImage] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='ogimage'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "ogimage" text;
    END IF;
END $$;

-- [Ts_UiPage.Template] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='template'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "template" bigint;
    END IF;
END $$;

-- [Ts_UiPage.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uipage' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uipage ADD "project" bigint;
    END IF;
END $$;
-- [Ts_UiTemplate] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_uitemplate'));

    IF not condition THEN
    CREATE TABLE ts_uitemplate (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"caption" VARCHAR(256)
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_UiTemplate.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_UiTemplate.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "caption" varchar(256);
    END IF;
END $$;

-- [Ts_UiTemplate.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_uitemplate' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_uitemplate ADD "project" bigint;
    END IF;
END $$;
-- [Ts_VarType] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ts_vartype'));

    IF not condition THEN
    CREATE TABLE ts_vartype (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"name" VARCHAR(64)
        ,"type" VARCHAR(64)
        ,"bindtype" INT
        ,"bind" BIGINT
        ,"project" BIGINT);

   END IF;
END $$;


-- [Ts_VarType.Name] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='name'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "name" varchar(64);
    END IF;
END $$;

-- [Ts_VarType.Type] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='type'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "type" varchar(64);
    END IF;
END $$;

-- [Ts_VarType.BindType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='bindtype'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "bindtype" int;
    END IF;
END $$;

-- [Ts_VarType.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='bind'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "bind" bigint;
    END IF;
END $$;

-- [Ts_VarType.Project] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='project'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "project" bigint;
    END IF;
END $$;