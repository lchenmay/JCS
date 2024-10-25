-- [Ca_Address] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_address'));

    IF not condition THEN
    CREATE TABLE ca_address (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"caption" VARCHAR(256)
        ,"bind" BIGINT
        ,"addresstype" INT
        ,"line1" VARCHAR(300)
        ,"line2" VARCHAR(300)
        ,"state" VARCHAR(16)
        ,"county" VARCHAR(16)
        ,"town" VARCHAR(16)
        ,"contact" VARCHAR(64)
        ,"tel" VARCHAR(20)
        ,"email" VARCHAR(256)
        ,"zip" VARCHAR(16)
        ,"city" BIGINT
        ,"country" BIGINT
        ,"remarks" TEXT);

   END IF;
END $$;


-- [Ca_Address.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "caption" varchar(256);
    END IF;
END $$;

-- [Ca_Address.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='bind'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "bind" bigint;
    END IF;
END $$;

-- [Ca_Address.AddressType] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='addresstype'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "addresstype" int;
    END IF;
END $$;

-- [Ca_Address.Line1] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='line1'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "line1" varchar(300);
    END IF;
END $$;

-- [Ca_Address.Line2] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='line2'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "line2" varchar(300);
    END IF;
END $$;

-- [Ca_Address.State] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='state'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "state" varchar(16);
    END IF;
END $$;

-- [Ca_Address.County] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='county'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "county" varchar(16);
    END IF;
END $$;

-- [Ca_Address.Town] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='town'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "town" varchar(16);
    END IF;
END $$;

-- [Ca_Address.Contact] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='contact'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "contact" varchar(64);
    END IF;
END $$;

-- [Ca_Address.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='tel'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "tel" varchar(20);
    END IF;
END $$;

-- [Ca_Address.Email] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='email'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "email" varchar(256);
    END IF;
END $$;

-- [Ca_Address.Zip] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='zip'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "zip" varchar(16);
    END IF;
END $$;

-- [Ca_Address.City] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='city'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "city" bigint;
    END IF;
END $$;

-- [Ca_Address.Country] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='country'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "country" bigint;
    END IF;
END $$;

-- [Ca_Address.Remarks] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_address' AND column_name='remarks'));

    IF not condition THEN
        ALTER TABLE ca_address ADD "remarks" text;
    END IF;
END $$;
-- [Ca_Biz] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_biz'));

    IF not condition THEN
    CREATE TABLE ca_biz (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"code" VARCHAR(64)
        ,"caption" VARCHAR(256)
        ,"parent" BIGINT
        ,"basicacct" BIGINT
        ,"desctxt" TEXT
        ,"website" VARCHAR(256)
        ,"icon" VARCHAR(256)
        ,"city" BIGINT
        ,"country" BIGINT
        ,"lang" BIGINT
        ,"issocialplatform" BOOLEAN
        ,"iscmssource" BOOLEAN
        ,"ispaygateway" BOOLEAN);

   END IF;
END $$;


-- [Ca_Biz.Code] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='code'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "code" varchar(64);
    END IF;
END $$;

-- [Ca_Biz.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "caption" varchar(256);
    END IF;
END $$;

-- [Ca_Biz.Parent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='parent'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "parent" bigint;
    END IF;
END $$;

-- [Ca_Biz.BasicAcct] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='basicacct'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "basicacct" bigint;
    END IF;
END $$;

-- [Ca_Biz.DescTxt] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='desctxt'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "desctxt" text;
    END IF;
END $$;

-- [Ca_Biz.Website] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='website'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "website" varchar(256);
    END IF;
END $$;

-- [Ca_Biz.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='icon'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "icon" varchar(256);
    END IF;
END $$;

-- [Ca_Biz.City] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='city'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "city" bigint;
    END IF;
END $$;

-- [Ca_Biz.Country] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='country'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "country" bigint;
    END IF;
END $$;

-- [Ca_Biz.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='lang'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "lang" bigint;
    END IF;
END $$;

-- [Ca_Biz.IsSocialPlatform] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='issocialplatform'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "issocialplatform" boolean;
    END IF;
END $$;

-- [Ca_Biz.IsCmsSource] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='iscmssource'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "iscmssource" boolean;
    END IF;
END $$;

-- [Ca_Biz.IsPayGateway] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_biz' AND column_name='ispaygateway'));

    IF not condition THEN
        ALTER TABLE ca_biz ADD "ispaygateway" boolean;
    END IF;
END $$;
-- [Ca_Cat] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_cat'));

    IF not condition THEN
    CREATE TABLE ca_cat (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"caption" VARCHAR(64)
        ,"lang" BIGINT
        ,"zh" BIGINT
        ,"parent" BIGINT
        ,"catstate" INT);

   END IF;
END $$;


-- [Ca_Cat.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cat' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_cat ADD "caption" varchar(64);
    END IF;
END $$;

-- [Ca_Cat.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cat' AND column_name='lang'));

    IF not condition THEN
        ALTER TABLE ca_cat ADD "lang" bigint;
    END IF;
END $$;

-- [Ca_Cat.Zh] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cat' AND column_name='zh'));

    IF not condition THEN
        ALTER TABLE ca_cat ADD "zh" bigint;
    END IF;
END $$;

-- [Ca_Cat.Parent] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cat' AND column_name='parent'));

    IF not condition THEN
        ALTER TABLE ca_cat ADD "parent" bigint;
    END IF;
END $$;

-- [Ca_Cat.CatState] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_cat' AND column_name='catstate'));

    IF not condition THEN
        ALTER TABLE ca_cat ADD "catstate" int;
    END IF;
END $$;
-- [Ca_City] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_city'));

    IF not condition THEN
    CREATE TABLE ca_city (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"fullname" VARCHAR(64)
        ,"metropolitancode3iata" VARCHAR(3)
        ,"nameen" VARCHAR(64)
        ,"country" BIGINT
        ,"place" BIGINT
        ,"icon" VARCHAR(256)
        ,"tel" VARCHAR(4));

   END IF;
END $$;


-- [Ca_City.Fullname] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='fullname'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "fullname" varchar(64);
    END IF;
END $$;

-- [Ca_City.MetropolitanCode3IATA] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='metropolitancode3iata'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "metropolitancode3iata" varchar(3);
    END IF;
END $$;

-- [Ca_City.NameEn] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='nameen'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "nameen" varchar(64);
    END IF;
END $$;

-- [Ca_City.Country] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='country'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "country" bigint;
    END IF;
END $$;

-- [Ca_City.Place] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='place'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "place" bigint;
    END IF;
END $$;

-- [Ca_City.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='icon'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "icon" varchar(256);
    END IF;
END $$;

-- [Ca_City.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_city' AND column_name='tel'));

    IF not condition THEN
        ALTER TABLE ca_city ADD "tel" varchar(4);
    END IF;
END $$;
-- [Ca_Country] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_country'));

    IF not condition THEN
    CREATE TABLE ca_country (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"code2" VARCHAR(2)
        ,"caption" VARCHAR(64)
        ,"fullname" VARCHAR(256)
        ,"icon" VARCHAR(256)
        ,"tel" VARCHAR(4)
        ,"cur" BIGINT
        ,"capital" BIGINT
        ,"place" BIGINT
        ,"lang" BIGINT);

   END IF;
END $$;


-- [Ca_Country.Code2] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='code2'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "code2" varchar(2);
    END IF;
END $$;

-- [Ca_Country.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "caption" varchar(64);
    END IF;
END $$;

-- [Ca_Country.Fullname] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='fullname'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "fullname" varchar(256);
    END IF;
END $$;

-- [Ca_Country.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='icon'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "icon" varchar(256);
    END IF;
END $$;

-- [Ca_Country.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='tel'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "tel" varchar(4);
    END IF;
END $$;

-- [Ca_Country.Cur] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='cur'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "cur" bigint;
    END IF;
END $$;

-- [Ca_Country.Capital] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='capital'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "capital" bigint;
    END IF;
END $$;

-- [Ca_Country.Place] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='place'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "place" bigint;
    END IF;
END $$;

-- [Ca_Country.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_country' AND column_name='lang'));

    IF not condition THEN
        ALTER TABLE ca_country ADD "lang" bigint;
    END IF;
END $$;
-- [Ca_EndUser] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_enduser'));

    IF not condition THEN
    CREATE TABLE ca_enduser (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"caption" VARCHAR(64)
        ,"username" VARCHAR(64)
        ,"socialauthbiz" BIGINT
        ,"socialauthid" TEXT
        ,"socialauthavatar" TEXT
        ,"email" VARCHAR(256)
        ,"tel" VARCHAR(32)
        ,"gender" INT
        ,"status" INT
        ,"admin" INT
        ,"bizpartner" INT
        ,"privilege" BIGINT
        ,"verify" INT
        ,"pwd" VARCHAR(16)
        ,"online" BOOLEAN
        ,"icon" VARCHAR(256)
        ,"background" VARCHAR(256)
        ,"basicacct" BIGINT
        ,"citizen" BIGINT
        ,"refer" VARCHAR(9)
        ,"referer" BIGINT
        ,"url" TEXT
        ,"about" TEXT);

   END IF;
END $$;


-- [Ca_EndUser.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "caption" varchar(64);
    END IF;
END $$;

-- [Ca_EndUser.Username] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='username'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "username" varchar(64);
    END IF;
END $$;

-- [Ca_EndUser.SocialAuthBiz] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='socialauthbiz'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "socialauthbiz" bigint;
    END IF;
END $$;

-- [Ca_EndUser.SocialAuthId] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='socialauthid'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "socialauthid" text;
    END IF;
END $$;

-- [Ca_EndUser.SocialAuthAvatar] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='socialauthavatar'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "socialauthavatar" text;
    END IF;
END $$;

-- [Ca_EndUser.Email] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='email'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "email" varchar(256);
    END IF;
END $$;

-- [Ca_EndUser.Tel] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='tel'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "tel" varchar(32);
    END IF;
END $$;

-- [Ca_EndUser.Gender] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='gender'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "gender" int;
    END IF;
END $$;

-- [Ca_EndUser.Status] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='status'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "status" int;
    END IF;
END $$;

-- [Ca_EndUser.Admin] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='admin'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "admin" int;
    END IF;
END $$;

-- [Ca_EndUser.BizPartner] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='bizpartner'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "bizpartner" int;
    END IF;
END $$;

-- [Ca_EndUser.Privilege] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='privilege'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "privilege" bigint;
    END IF;
END $$;

-- [Ca_EndUser.Verify] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='verify'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "verify" int;
    END IF;
END $$;

-- [Ca_EndUser.Pwd] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='pwd'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "pwd" varchar(16);
    END IF;
END $$;

-- [Ca_EndUser.Online] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='online'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "online" boolean;
    END IF;
END $$;

-- [Ca_EndUser.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='icon'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "icon" varchar(256);
    END IF;
END $$;

-- [Ca_EndUser.Background] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='background'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "background" varchar(256);
    END IF;
END $$;

-- [Ca_EndUser.BasicAcct] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='basicacct'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "basicacct" bigint;
    END IF;
END $$;

-- [Ca_EndUser.Citizen] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='citizen'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "citizen" bigint;
    END IF;
END $$;

-- [Ca_EndUser.Refer] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='refer'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "refer" varchar(9);
    END IF;
END $$;

-- [Ca_EndUser.Referer] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='referer'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "referer" bigint;
    END IF;
END $$;

-- [Ca_EndUser.Url] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='url'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "url" text;
    END IF;
END $$;

-- [Ca_EndUser.About] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_enduser' AND column_name='about'));

    IF not condition THEN
        ALTER TABLE ca_enduser ADD "about" text;
    END IF;
END $$;
-- [Ca_SpecialItem] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_specialitem'));

    IF not condition THEN
    CREATE TABLE ca_specialitem (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"type" INT
        ,"lang" BIGINT
        ,"bind" BIGINT);

   END IF;
END $$;


-- [Ca_SpecialItem.Type] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_specialitem' AND column_name='type'));

    IF not condition THEN
        ALTER TABLE ca_specialitem ADD "type" int;
    END IF;
END $$;

-- [Ca_SpecialItem.Lang] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_specialitem' AND column_name='lang'));

    IF not condition THEN
        ALTER TABLE ca_specialitem ADD "lang" bigint;
    END IF;
END $$;

-- [Ca_SpecialItem.Bind] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_specialitem' AND column_name='bind'));

    IF not condition THEN
        ALTER TABLE ca_specialitem ADD "bind" bigint;
    END IF;
END $$;
-- [Ca_WebCredential] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT * FROM information_schema.tables WHERE table_name = 'ca_webcredential'));

    IF not condition THEN
    CREATE TABLE ca_webcredential (id BIGINT NOT NULL
        ,createdat BIGINT NOT NULL
        ,updatedat BIGINT NOT NULL
        ,sort BIGINT NOT NULL
        ,"caption" VARCHAR(64)
        ,"externalid" BIGINT
        ,"icon" VARCHAR(256)
        ,"eu" BIGINT
        ,"biz" BIGINT
        ,"json" TEXT);

   END IF;
END $$;


-- [Ca_WebCredential.Caption] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='caption'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "caption" varchar(64);
    END IF;
END $$;

-- [Ca_WebCredential.ExternalId] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='externalid'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "externalid" bigint;
    END IF;
END $$;

-- [Ca_WebCredential.Icon] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='icon'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "icon" varchar(256);
    END IF;
END $$;

-- [Ca_WebCredential.EU] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='eu'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "eu" bigint;
    END IF;
END $$;

-- [Ca_WebCredential.Biz] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='biz'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "biz" bigint;
    END IF;
END $$;

-- [Ca_WebCredential.Json] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ca_webcredential' AND column_name='json'));

    IF not condition THEN
        ALTER TABLE ca_webcredential ADD "json" text;
    END IF;
END $$;
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
        ,"dirvs" VARCHAR(64)
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

-- [Ts_HostConfig.DirVs] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_hostconfig' AND column_name='dirvs'));

    IF not condition THEN
        ALTER TABLE ts_hostconfig ADD "dirvs" varchar(64);
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
        ,"caption" VARCHAR(256)
        ,"typesessionuser" VARCHAR(64));

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

-- [Ts_Project.TypeSessionUser] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_project' AND column_name='typesessionuser'));

    IF not condition THEN
        ALTER TABLE ts_project ADD "typesessionuser" varchar(64);
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
        ,"val" TEXT
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

-- [Ts_VarType.Val] -------------


DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(SELECT column_name FROM information_schema.columns WHERE table_name='ts_vartype' AND column_name='val'));

    IF not condition THEN
        ALTER TABLE ts_vartype ADD "val" text;
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