
-- [ca_address] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_address'));

    IF condition THEN
        RAISE NOTICE 'ca_address exists.';
    ELSE

    CREATE TABLE ca_address (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Caption VARCHAR(256)
        ,Bind BIGINT
        ,Type INT
        ,Line1 VARCHAR(300)
        ,Line2 VARCHAR(300)
        ,State VARCHAR(16)
        ,County VARCHAR(16)
        ,Town VARCHAR(16)
        ,Contact VARCHAR(64)
        ,Tel VARCHAR(20)
        ,Email VARCHAR(256)
        ,Zip VARCHAR(16)
        ,City BIGINT
        ,Country BIGINT
        ,Remarks TEXT);

   END IF;
END $$;


-- [ca_address.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_address ADD Caption VARCHAR(256);
    END IF;
END $$;

-- [ca_address.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Bind'));

    IF condition THEN
        RAISE NOTICE 'Bind exists.';
    ELSE
        ALTER TABLE ca_address ADD Bind BIGINT;
    END IF;
END $$;

-- [ca_address.Type] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Type'));

    IF condition THEN
        RAISE NOTICE 'Type exists.';
    ELSE
        ALTER TABLE ca_address ADD Type INT;
    END IF;
END $$;

-- [ca_address.Line1] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Line1'));

    IF condition THEN
        RAISE NOTICE 'Line1 exists.';
    ELSE
        ALTER TABLE ca_address ADD Line1 VARCHAR(300);
    END IF;
END $$;

-- [ca_address.Line2] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Line2'));

    IF condition THEN
        RAISE NOTICE 'Line2 exists.';
    ELSE
        ALTER TABLE ca_address ADD Line2 VARCHAR(300);
    END IF;
END $$;

-- [ca_address.State] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='State'));

    IF condition THEN
        RAISE NOTICE 'State exists.';
    ELSE
        ALTER TABLE ca_address ADD State VARCHAR(16);
    END IF;
END $$;

-- [ca_address.County] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='County'));

    IF condition THEN
        RAISE NOTICE 'County exists.';
    ELSE
        ALTER TABLE ca_address ADD County VARCHAR(16);
    END IF;
END $$;

-- [ca_address.Town] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Town'));

    IF condition THEN
        RAISE NOTICE 'Town exists.';
    ELSE
        ALTER TABLE ca_address ADD Town VARCHAR(16);
    END IF;
END $$;

-- [ca_address.Contact] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Contact'));

    IF condition THEN
        RAISE NOTICE 'Contact exists.';
    ELSE
        ALTER TABLE ca_address ADD Contact VARCHAR(64);
    END IF;
END $$;

-- [ca_address.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Tel'));

    IF condition THEN
        RAISE NOTICE 'Tel exists.';
    ELSE
        ALTER TABLE ca_address ADD Tel VARCHAR(20);
    END IF;
END $$;

-- [ca_address.Email] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Email'));

    IF condition THEN
        RAISE NOTICE 'Email exists.';
    ELSE
        ALTER TABLE ca_address ADD Email VARCHAR(256);
    END IF;
END $$;

-- [ca_address.Zip] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Zip'));

    IF condition THEN
        RAISE NOTICE 'Zip exists.';
    ELSE
        ALTER TABLE ca_address ADD Zip VARCHAR(16);
    END IF;
END $$;

-- [ca_address.City] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='City'));

    IF condition THEN
        RAISE NOTICE 'City exists.';
    ELSE
        ALTER TABLE ca_address ADD City BIGINT;
    END IF;
END $$;

-- [ca_address.Country] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Country'));

    IF condition THEN
        RAISE NOTICE 'Country exists.';
    ELSE
        ALTER TABLE ca_address ADD Country BIGINT;
    END IF;
END $$;

-- [ca_address.Remarks] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='Remarks'));

    IF condition THEN
        RAISE NOTICE 'Remarks exists.';
    ELSE
        ALTER TABLE ca_address ADD Remarks TEXT;
    END IF;
END $$;
-- [ca_biz] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_biz'));

    IF condition THEN
        RAISE NOTICE 'ca_biz exists.';
    ELSE

    CREATE TABLE ca_biz (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Code VARCHAR(256)
        ,Caption VARCHAR(256)
        ,Parent BIGINT
        ,BasicAcct BIGINT
        ,Desc TEXT
        ,Website VARCHAR(256)
        ,Icon VARCHAR(256)
        ,City BIGINT
        ,Country BIGINT
        ,Lang BIGINT
        ,IsSocial BIT
        ,IsCmsSource BIT
        ,IsPay BIT
        ,MomentLatest BIGINT
        ,CountFollowers BIGINT
        ,CountFollows BIGINT
        ,CountMoments BIGINT
        ,CountViews BIGINT
        ,CountComments BIGINT
        ,CountThumbUps BIGINT
        ,CountThumbDns BIGINT
        ,IsCrawling BIT
        ,IsGSeries BIT
        ,RemarksCentral TEXT
        ,Agent BIGINT
        ,SiteCats TEXT
        ,ConfiguredCats TEXT);

   END IF;
END $$;


-- [ca_biz.Code] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Code'));

    IF condition THEN
        RAISE NOTICE 'Code exists.';
    ELSE
        ALTER TABLE ca_biz ADD Code VARCHAR(256);
    END IF;
END $$;

-- [ca_biz.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_biz ADD Caption VARCHAR(256);
    END IF;
END $$;

-- [ca_biz.Parent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Parent'));

    IF condition THEN
        RAISE NOTICE 'Parent exists.';
    ELSE
        ALTER TABLE ca_biz ADD Parent BIGINT;
    END IF;
END $$;

-- [ca_biz.BasicAcct] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='BasicAcct'));

    IF condition THEN
        RAISE NOTICE 'BasicAcct exists.';
    ELSE
        ALTER TABLE ca_biz ADD BasicAcct BIGINT;
    END IF;
END $$;

-- [ca_biz.Desc] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Desc'));

    IF condition THEN
        RAISE NOTICE 'Desc exists.';
    ELSE
        ALTER TABLE ca_biz ADD Desc TEXT;
    END IF;
END $$;

-- [ca_biz.Website] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Website'));

    IF condition THEN
        RAISE NOTICE 'Website exists.';
    ELSE
        ALTER TABLE ca_biz ADD Website VARCHAR(256);
    END IF;
END $$;

-- [ca_biz.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_biz ADD Icon VARCHAR(256);
    END IF;
END $$;

-- [ca_biz.City] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='City'));

    IF condition THEN
        RAISE NOTICE 'City exists.';
    ELSE
        ALTER TABLE ca_biz ADD City BIGINT;
    END IF;
END $$;

-- [ca_biz.Country] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Country'));

    IF condition THEN
        RAISE NOTICE 'Country exists.';
    ELSE
        ALTER TABLE ca_biz ADD Country BIGINT;
    END IF;
END $$;

-- [ca_biz.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Lang'));

    IF condition THEN
        RAISE NOTICE 'Lang exists.';
    ELSE
        ALTER TABLE ca_biz ADD Lang BIGINT;
    END IF;
END $$;

-- [ca_biz.IsSocial] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='IsSocial'));

    IF condition THEN
        RAISE NOTICE 'IsSocial exists.';
    ELSE
        ALTER TABLE ca_biz ADD IsSocial BIT;
    END IF;
END $$;

-- [ca_biz.IsCmsSource] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='IsCmsSource'));

    IF condition THEN
        RAISE NOTICE 'IsCmsSource exists.';
    ELSE
        ALTER TABLE ca_biz ADD IsCmsSource BIT;
    END IF;
END $$;

-- [ca_biz.IsPay] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='IsPay'));

    IF condition THEN
        RAISE NOTICE 'IsPay exists.';
    ELSE
        ALTER TABLE ca_biz ADD IsPay BIT;
    END IF;
END $$;

-- [ca_biz.MomentLatest] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='MomentLatest'));

    IF condition THEN
        RAISE NOTICE 'MomentLatest exists.';
    ELSE
        ALTER TABLE ca_biz ADD MomentLatest BIGINT;
    END IF;
END $$;

-- [ca_biz.CountFollowers] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountFollowers'));

    IF condition THEN
        RAISE NOTICE 'CountFollowers exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountFollowers BIGINT;
    END IF;
END $$;

-- [ca_biz.CountFollows] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountFollows'));

    IF condition THEN
        RAISE NOTICE 'CountFollows exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountFollows BIGINT;
    END IF;
END $$;

-- [ca_biz.CountMoments] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountMoments'));

    IF condition THEN
        RAISE NOTICE 'CountMoments exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountMoments BIGINT;
    END IF;
END $$;

-- [ca_biz.CountViews] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountViews'));

    IF condition THEN
        RAISE NOTICE 'CountViews exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountViews BIGINT;
    END IF;
END $$;

-- [ca_biz.CountComments] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountComments'));

    IF condition THEN
        RAISE NOTICE 'CountComments exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountComments BIGINT;
    END IF;
END $$;

-- [ca_biz.CountThumbUps] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountThumbUps'));

    IF condition THEN
        RAISE NOTICE 'CountThumbUps exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountThumbUps BIGINT;
    END IF;
END $$;

-- [ca_biz.CountThumbDns] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='CountThumbDns'));

    IF condition THEN
        RAISE NOTICE 'CountThumbDns exists.';
    ELSE
        ALTER TABLE ca_biz ADD CountThumbDns BIGINT;
    END IF;
END $$;

-- [ca_biz.IsCrawling] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='IsCrawling'));

    IF condition THEN
        RAISE NOTICE 'IsCrawling exists.';
    ELSE
        ALTER TABLE ca_biz ADD IsCrawling BIT;
    END IF;
END $$;

-- [ca_biz.IsGSeries] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='IsGSeries'));

    IF condition THEN
        RAISE NOTICE 'IsGSeries exists.';
    ELSE
        ALTER TABLE ca_biz ADD IsGSeries BIT;
    END IF;
END $$;

-- [ca_biz.RemarksCentral] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='RemarksCentral'));

    IF condition THEN
        RAISE NOTICE 'RemarksCentral exists.';
    ELSE
        ALTER TABLE ca_biz ADD RemarksCentral TEXT;
    END IF;
END $$;

-- [ca_biz.Agent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='Agent'));

    IF condition THEN
        RAISE NOTICE 'Agent exists.';
    ELSE
        ALTER TABLE ca_biz ADD Agent BIGINT;
    END IF;
END $$;

-- [ca_biz.SiteCats] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='SiteCats'));

    IF condition THEN
        RAISE NOTICE 'SiteCats exists.';
    ELSE
        ALTER TABLE ca_biz ADD SiteCats TEXT;
    END IF;
END $$;

-- [ca_biz.ConfiguredCats] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='ConfiguredCats'));

    IF condition THEN
        RAISE NOTICE 'ConfiguredCats exists.';
    ELSE
        ALTER TABLE ca_biz ADD ConfiguredCats TEXT;
    END IF;
END $$;
-- [ca_country] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_country'));

    IF condition THEN
        RAISE NOTICE 'ca_country exists.';
    ELSE

    CREATE TABLE ca_country (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Code2 VARCHAR(2)
        ,Caption VARCHAR(64)
        ,Fullname VARCHAR(256)
        ,Icon VARCHAR(256)
        ,Tel VARCHAR(4)
        ,Cur BIGINT
        ,Capital BIGINT
        ,Place BIGINT
        ,Lang BIGINT);

   END IF;
END $$;


-- [ca_country.Code2] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Code2'));

    IF condition THEN
        RAISE NOTICE 'Code2 exists.';
    ELSE
        ALTER TABLE ca_country ADD Code2 VARCHAR(2);
    END IF;
END $$;

-- [ca_country.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_country ADD Caption VARCHAR(64);
    END IF;
END $$;

-- [ca_country.Fullname] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Fullname'));

    IF condition THEN
        RAISE NOTICE 'Fullname exists.';
    ELSE
        ALTER TABLE ca_country ADD Fullname VARCHAR(256);
    END IF;
END $$;

-- [ca_country.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_country ADD Icon VARCHAR(256);
    END IF;
END $$;

-- [ca_country.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Tel'));

    IF condition THEN
        RAISE NOTICE 'Tel exists.';
    ELSE
        ALTER TABLE ca_country ADD Tel VARCHAR(4);
    END IF;
END $$;

-- [ca_country.Cur] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Cur'));

    IF condition THEN
        RAISE NOTICE 'Cur exists.';
    ELSE
        ALTER TABLE ca_country ADD Cur BIGINT;
    END IF;
END $$;

-- [ca_country.Capital] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Capital'));

    IF condition THEN
        RAISE NOTICE 'Capital exists.';
    ELSE
        ALTER TABLE ca_country ADD Capital BIGINT;
    END IF;
END $$;

-- [ca_country.Place] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Place'));

    IF condition THEN
        RAISE NOTICE 'Place exists.';
    ELSE
        ALTER TABLE ca_country ADD Place BIGINT;
    END IF;
END $$;

-- [ca_country.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='Lang'));

    IF condition THEN
        RAISE NOTICE 'Lang exists.';
    ELSE
        ALTER TABLE ca_country ADD Lang BIGINT;
    END IF;
END $$;
-- [ca_cur] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_cur'));

    IF condition THEN
        RAISE NOTICE 'ca_cur exists.';
    ELSE

    CREATE TABLE ca_cur (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Code VARCHAR(16)
        ,Caption VARCHAR(64)
        ,Hidden BIT
        ,IsSac BIT
        ,IsTransfer BIT
        ,IsCash BIT
        ,EnableReward BIT
        ,EnableOTC BIT
        ,Icon VARCHAR(512)
        ,CurType INT
        ,Dec BIGINT
        ,AnchorRate FLOAT
        ,Freezable BIT
        ,Authorizable BIT
        ,ChaninID VARCHAR(256)
        ,ChaninName VARCHAR(256)
        ,ContractAddress VARCHAR(256)
        ,WalletAddress VARCHAR(256)
        ,BaseRate FLOAT);

   END IF;
END $$;


-- [ca_cur.Code] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Code'));

    IF condition THEN
        RAISE NOTICE 'Code exists.';
    ELSE
        ALTER TABLE ca_cur ADD Code VARCHAR(16);
    END IF;
END $$;

-- [ca_cur.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_cur ADD Caption VARCHAR(64);
    END IF;
END $$;

-- [ca_cur.Hidden] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Hidden'));

    IF condition THEN
        RAISE NOTICE 'Hidden exists.';
    ELSE
        ALTER TABLE ca_cur ADD Hidden BIT;
    END IF;
END $$;

-- [ca_cur.IsSac] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='IsSac'));

    IF condition THEN
        RAISE NOTICE 'IsSac exists.';
    ELSE
        ALTER TABLE ca_cur ADD IsSac BIT;
    END IF;
END $$;

-- [ca_cur.IsTransfer] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='IsTransfer'));

    IF condition THEN
        RAISE NOTICE 'IsTransfer exists.';
    ELSE
        ALTER TABLE ca_cur ADD IsTransfer BIT;
    END IF;
END $$;

-- [ca_cur.IsCash] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='IsCash'));

    IF condition THEN
        RAISE NOTICE 'IsCash exists.';
    ELSE
        ALTER TABLE ca_cur ADD IsCash BIT;
    END IF;
END $$;

-- [ca_cur.EnableReward] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='EnableReward'));

    IF condition THEN
        RAISE NOTICE 'EnableReward exists.';
    ELSE
        ALTER TABLE ca_cur ADD EnableReward BIT;
    END IF;
END $$;

-- [ca_cur.EnableOTC] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='EnableOTC'));

    IF condition THEN
        RAISE NOTICE 'EnableOTC exists.';
    ELSE
        ALTER TABLE ca_cur ADD EnableOTC BIT;
    END IF;
END $$;

-- [ca_cur.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_cur ADD Icon VARCHAR(512);
    END IF;
END $$;

-- [ca_cur.CurType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='CurType'));

    IF condition THEN
        RAISE NOTICE 'CurType exists.';
    ELSE
        ALTER TABLE ca_cur ADD CurType INT;
    END IF;
END $$;

-- [ca_cur.Dec] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Dec'));

    IF condition THEN
        RAISE NOTICE 'Dec exists.';
    ELSE
        ALTER TABLE ca_cur ADD Dec BIGINT;
    END IF;
END $$;

-- [ca_cur.AnchorRate] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='AnchorRate'));

    IF condition THEN
        RAISE NOTICE 'AnchorRate exists.';
    ELSE
        ALTER TABLE ca_cur ADD AnchorRate FLOAT;
    END IF;
END $$;

-- [ca_cur.Freezable] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Freezable'));

    IF condition THEN
        RAISE NOTICE 'Freezable exists.';
    ELSE
        ALTER TABLE ca_cur ADD Freezable BIT;
    END IF;
END $$;

-- [ca_cur.Authorizable] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='Authorizable'));

    IF condition THEN
        RAISE NOTICE 'Authorizable exists.';
    ELSE
        ALTER TABLE ca_cur ADD Authorizable BIT;
    END IF;
END $$;

-- [ca_cur.ChaninID] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='ChaninID'));

    IF condition THEN
        RAISE NOTICE 'ChaninID exists.';
    ELSE
        ALTER TABLE ca_cur ADD ChaninID VARCHAR(256);
    END IF;
END $$;

-- [ca_cur.ChaninName] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='ChaninName'));

    IF condition THEN
        RAISE NOTICE 'ChaninName exists.';
    ELSE
        ALTER TABLE ca_cur ADD ChaninName VARCHAR(256);
    END IF;
END $$;

-- [ca_cur.ContractAddress] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='ContractAddress'));

    IF condition THEN
        RAISE NOTICE 'ContractAddress exists.';
    ELSE
        ALTER TABLE ca_cur ADD ContractAddress VARCHAR(256);
    END IF;
END $$;

-- [ca_cur.WalletAddress] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='WalletAddress'));

    IF condition THEN
        RAISE NOTICE 'WalletAddress exists.';
    ELSE
        ALTER TABLE ca_cur ADD WalletAddress VARCHAR(256);
    END IF;
END $$;

-- [ca_cur.BaseRate] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cur' AND column_name='BaseRate'));

    IF condition THEN
        RAISE NOTICE 'BaseRate exists.';
    ELSE
        ALTER TABLE ca_cur ADD BaseRate FLOAT;
    END IF;
END $$;
-- [ca_enduser] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_enduser'));

    IF condition THEN
        RAISE NOTICE 'ca_enduser exists.';
    ELSE

    CREATE TABLE ca_enduser (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Caption VARCHAR(64)
        ,Username VARCHAR(64)
        ,SocialAuthBiz BIGINT
        ,SocialAuthId TEXT
        ,SocialAuthAvatar TEXT
        ,Email VARCHAR(256)
        ,Tel VARCHAR(32)
        ,Gender INT
        ,Status INT
        ,Admin INT
        ,BizPartner INT
        ,Privilege BIGINT
        ,Verify INT
        ,Pwd VARCHAR(16)
        ,Online BIT
        ,Icon VARCHAR(256)
        ,Background VARCHAR(256)
        ,BasicAcct BIGINT
        ,Refer VARCHAR(7)
        ,Referer BIGINT
        ,Url TEXT
        ,About TEXT);

   END IF;
END $$;


-- [ca_enduser.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Caption VARCHAR(64);
    END IF;
END $$;

-- [ca_enduser.Username] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Username'));

    IF condition THEN
        RAISE NOTICE 'Username exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Username VARCHAR(64);
    END IF;
END $$;

-- [ca_enduser.SocialAuthBiz] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='SocialAuthBiz'));

    IF condition THEN
        RAISE NOTICE 'SocialAuthBiz exists.';
    ELSE
        ALTER TABLE ca_enduser ADD SocialAuthBiz BIGINT;
    END IF;
END $$;

-- [ca_enduser.SocialAuthId] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='SocialAuthId'));

    IF condition THEN
        RAISE NOTICE 'SocialAuthId exists.';
    ELSE
        ALTER TABLE ca_enduser ADD SocialAuthId TEXT;
    END IF;
END $$;

-- [ca_enduser.SocialAuthAvatar] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='SocialAuthAvatar'));

    IF condition THEN
        RAISE NOTICE 'SocialAuthAvatar exists.';
    ELSE
        ALTER TABLE ca_enduser ADD SocialAuthAvatar TEXT;
    END IF;
END $$;

-- [ca_enduser.Email] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Email'));

    IF condition THEN
        RAISE NOTICE 'Email exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Email VARCHAR(256);
    END IF;
END $$;

-- [ca_enduser.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Tel'));

    IF condition THEN
        RAISE NOTICE 'Tel exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Tel VARCHAR(32);
    END IF;
END $$;

-- [ca_enduser.Gender] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Gender'));

    IF condition THEN
        RAISE NOTICE 'Gender exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Gender INT;
    END IF;
END $$;

-- [ca_enduser.Status] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Status'));

    IF condition THEN
        RAISE NOTICE 'Status exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Status INT;
    END IF;
END $$;

-- [ca_enduser.Admin] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Admin'));

    IF condition THEN
        RAISE NOTICE 'Admin exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Admin INT;
    END IF;
END $$;

-- [ca_enduser.BizPartner] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='BizPartner'));

    IF condition THEN
        RAISE NOTICE 'BizPartner exists.';
    ELSE
        ALTER TABLE ca_enduser ADD BizPartner INT;
    END IF;
END $$;

-- [ca_enduser.Privilege] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Privilege'));

    IF condition THEN
        RAISE NOTICE 'Privilege exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Privilege BIGINT;
    END IF;
END $$;

-- [ca_enduser.Verify] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Verify'));

    IF condition THEN
        RAISE NOTICE 'Verify exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Verify INT;
    END IF;
END $$;

-- [ca_enduser.Pwd] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Pwd'));

    IF condition THEN
        RAISE NOTICE 'Pwd exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Pwd VARCHAR(16);
    END IF;
END $$;

-- [ca_enduser.Online] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Online'));

    IF condition THEN
        RAISE NOTICE 'Online exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Online BIT;
    END IF;
END $$;

-- [ca_enduser.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Icon VARCHAR(256);
    END IF;
END $$;

-- [ca_enduser.Background] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Background'));

    IF condition THEN
        RAISE NOTICE 'Background exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Background VARCHAR(256);
    END IF;
END $$;

-- [ca_enduser.BasicAcct] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='BasicAcct'));

    IF condition THEN
        RAISE NOTICE 'BasicAcct exists.';
    ELSE
        ALTER TABLE ca_enduser ADD BasicAcct BIGINT;
    END IF;
END $$;

-- [ca_enduser.Refer] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Refer'));

    IF condition THEN
        RAISE NOTICE 'Refer exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Refer VARCHAR(7);
    END IF;
END $$;

-- [ca_enduser.Referer] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Referer'));

    IF condition THEN
        RAISE NOTICE 'Referer exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Referer BIGINT;
    END IF;
END $$;

-- [ca_enduser.Url] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='Url'));

    IF condition THEN
        RAISE NOTICE 'Url exists.';
    ELSE
        ALTER TABLE ca_enduser ADD Url TEXT;
    END IF;
END $$;

-- [ca_enduser.About] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='About'));

    IF condition THEN
        RAISE NOTICE 'About exists.';
    ELSE
        ALTER TABLE ca_enduser ADD About TEXT;
    END IF;
END $$;
-- [ca_file] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_file'));

    IF condition THEN
        RAISE NOTICE 'ca_file exists.';
    ELSE

    CREATE TABLE ca_file (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Caption VARCHAR(256)
        ,Encrypt INT
        ,SHA256 TEXT
        ,Size BIGINT
        ,Bind BIGINT
        ,BindType INT
        ,State INT
        ,Folder BIGINT
        ,FileType INT
        ,JSON TEXT);

   END IF;
END $$;


-- [ca_file.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_file ADD Caption VARCHAR(256);
    END IF;
END $$;

-- [ca_file.Encrypt] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='Encrypt'));

    IF condition THEN
        RAISE NOTICE 'Encrypt exists.';
    ELSE
        ALTER TABLE ca_file ADD Encrypt INT;
    END IF;
END $$;

-- [ca_file.SHA256] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='SHA256'));

    IF condition THEN
        RAISE NOTICE 'SHA256 exists.';
    ELSE
        ALTER TABLE ca_file ADD SHA256 TEXT;
    END IF;
END $$;

-- [ca_file.Size] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='Size'));

    IF condition THEN
        RAISE NOTICE 'Size exists.';
    ELSE
        ALTER TABLE ca_file ADD Size BIGINT;
    END IF;
END $$;

-- [ca_file.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='Bind'));

    IF condition THEN
        RAISE NOTICE 'Bind exists.';
    ELSE
        ALTER TABLE ca_file ADD Bind BIGINT;
    END IF;
END $$;

-- [ca_file.BindType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='BindType'));

    IF condition THEN
        RAISE NOTICE 'BindType exists.';
    ELSE
        ALTER TABLE ca_file ADD BindType INT;
    END IF;
END $$;

-- [ca_file.State] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='State'));

    IF condition THEN
        RAISE NOTICE 'State exists.';
    ELSE
        ALTER TABLE ca_file ADD State INT;
    END IF;
END $$;

-- [ca_file.Folder] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='Folder'));

    IF condition THEN
        RAISE NOTICE 'Folder exists.';
    ELSE
        ALTER TABLE ca_file ADD Folder BIGINT;
    END IF;
END $$;

-- [ca_file.FileType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='FileType'));

    IF condition THEN
        RAISE NOTICE 'FileType exists.';
    ELSE
        ALTER TABLE ca_file ADD FileType INT;
    END IF;
END $$;

-- [ca_file.JSON] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_file' AND column_name='JSON'));

    IF condition THEN
        RAISE NOTICE 'JSON exists.';
    ELSE
        ALTER TABLE ca_file ADD JSON TEXT;
    END IF;
END $$;
-- [ca_folder] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_folder'));

    IF condition THEN
        RAISE NOTICE 'ca_folder exists.';
    ELSE

    CREATE TABLE ca_folder (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Caption VARCHAR(256)
        ,Encrypt INT
        ,Bind BIGINT
        ,BindType INT
        ,Parent BIGINT);

   END IF;
END $$;


-- [ca_folder.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_folder' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_folder ADD Caption VARCHAR(256);
    END IF;
END $$;

-- [ca_folder.Encrypt] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_folder' AND column_name='Encrypt'));

    IF condition THEN
        RAISE NOTICE 'Encrypt exists.';
    ELSE
        ALTER TABLE ca_folder ADD Encrypt INT;
    END IF;
END $$;

-- [ca_folder.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_folder' AND column_name='Bind'));

    IF condition THEN
        RAISE NOTICE 'Bind exists.';
    ELSE
        ALTER TABLE ca_folder ADD Bind BIGINT;
    END IF;
END $$;

-- [ca_folder.BindType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_folder' AND column_name='BindType'));

    IF condition THEN
        RAISE NOTICE 'BindType exists.';
    ELSE
        ALTER TABLE ca_folder ADD BindType INT;
    END IF;
END $$;

-- [ca_folder.Parent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_folder' AND column_name='Parent'));

    IF condition THEN
        RAISE NOTICE 'Parent exists.';
    ELSE
        ALTER TABLE ca_folder ADD Parent BIGINT;
    END IF;
END $$;
-- [ca_lang] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_lang'));

    IF condition THEN
        RAISE NOTICE 'ca_lang exists.';
    ELSE

    CREATE TABLE ca_lang (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Code2 VARCHAR(2)
        ,Caption VARCHAR(64)
        ,Native VARCHAR(64)
        ,Icon VARCHAR(256)
        ,IsBlank BIT
        ,IsLocale BIT
        ,IsContent BIT
        ,IsAutoTranslate BIT
        ,TextDirection INT);

   END IF;
END $$;


-- [ca_lang.Code2] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='Code2'));

    IF condition THEN
        RAISE NOTICE 'Code2 exists.';
    ELSE
        ALTER TABLE ca_lang ADD Code2 VARCHAR(2);
    END IF;
END $$;

-- [ca_lang.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_lang ADD Caption VARCHAR(64);
    END IF;
END $$;

-- [ca_lang.Native] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='Native'));

    IF condition THEN
        RAISE NOTICE 'Native exists.';
    ELSE
        ALTER TABLE ca_lang ADD Native VARCHAR(64);
    END IF;
END $$;

-- [ca_lang.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_lang ADD Icon VARCHAR(256);
    END IF;
END $$;

-- [ca_lang.IsBlank] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='IsBlank'));

    IF condition THEN
        RAISE NOTICE 'IsBlank exists.';
    ELSE
        ALTER TABLE ca_lang ADD IsBlank BIT;
    END IF;
END $$;

-- [ca_lang.IsLocale] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='IsLocale'));

    IF condition THEN
        RAISE NOTICE 'IsLocale exists.';
    ELSE
        ALTER TABLE ca_lang ADD IsLocale BIT;
    END IF;
END $$;

-- [ca_lang.IsContent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='IsContent'));

    IF condition THEN
        RAISE NOTICE 'IsContent exists.';
    ELSE
        ALTER TABLE ca_lang ADD IsContent BIT;
    END IF;
END $$;

-- [ca_lang.IsAutoTranslate] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='IsAutoTranslate'));

    IF condition THEN
        RAISE NOTICE 'IsAutoTranslate exists.';
    ELSE
        ALTER TABLE ca_lang ADD IsAutoTranslate BIT;
    END IF;
END $$;

-- [ca_lang.TextDirection] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_lang' AND column_name='TextDirection'));

    IF condition THEN
        RAISE NOTICE 'TextDirection exists.';
    ELSE
        ALTER TABLE ca_lang ADD TextDirection INT;
    END IF;
END $$;
-- [ca_webcredential] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_webcredential'));

    IF condition THEN
        RAISE NOTICE 'ca_webcredential exists.';
    ELSE

    CREATE TABLE ca_webcredential (ID BIGINT NOT NULL
        ,Createdat BIGINT NOT NULL
        ,Updatedat BIGINT NOT NULL
        ,Sort BIGINT NOT NULL
        ,Caption VARCHAR(64)
        ,ExternalId BIGINT
        ,Icon VARCHAR(256)
        ,EU BIGINT
        ,Biz BIGINT
        ,Json TEXT);

   END IF;
END $$;


-- [ca_webcredential.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='Caption'));

    IF condition THEN
        RAISE NOTICE 'Caption exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD Caption VARCHAR(64);
    END IF;
END $$;

-- [ca_webcredential.ExternalId] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='ExternalId'));

    IF condition THEN
        RAISE NOTICE 'ExternalId exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD ExternalId BIGINT;
    END IF;
END $$;

-- [ca_webcredential.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='Icon'));

    IF condition THEN
        RAISE NOTICE 'Icon exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD Icon VARCHAR(256);
    END IF;
END $$;

-- [ca_webcredential.EU] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='EU'));

    IF condition THEN
        RAISE NOTICE 'EU exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD EU BIGINT;
    END IF;
END $$;

-- [ca_webcredential.Biz] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='Biz'));

    IF condition THEN
        RAISE NOTICE 'Biz exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD Biz BIGINT;
    END IF;
END $$;

-- [ca_webcredential.Json] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='Json'));

    IF condition THEN
        RAISE NOTICE 'Json exists.';
    ELSE
        ALTER TABLE ca_webcredential ADD Json TEXT;
    END IF;
END $$;