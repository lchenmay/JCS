module TypeSys.Orm

open System
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text

open Util.Cat
open Util.Bin
open Util.Text
open Util.Db
open Util.DbQuery
open Util.DbTx


open System.IO

type FK = int64
type Caption = string
type Chars = string
type Link = string
type Text = string
type Bin = byte[]
type RichText = string
type Integer = int64
type Money = float
type Float = float
type Boolean = bool
type Pwd = string
type SelectLines = int
type Timestamp = DateTime
type TimeSeries = List<DateTime * byte[]>

let FK__bin(f:FK,bb:BytesBuilder) = int64__bin
let bin__FK(bin:byte[],index:Ref<int>):FK = (bin,index)|>bin__int64

let Caption__bin(f:Caption,bb:BytesBuilder) = str__bin
let bin__Caption(bin:byte[],index:Ref<int>):Caption = (bin,index)|>bin__str

let Chars__bin(f:Chars,bb:BytesBuilder) = str__bin
let bin__Chars(bin:byte[],index:Ref<int>):Chars = (bin,index)|>bin__str

let Link__bin(f:Link,bb:BytesBuilder) = str__bin
let bin__Link(bin:byte[],index:Ref<int>):Link = (bin,index)|>bin__str

let Text__bin(f:Text,bb:BytesBuilder) = str__bin
let bin__Text(bin:byte[],index:Ref<int>):Text = (bin,index)|>bin__str

let RichText__bin(f:RichText,bb:BytesBuilder) = str__bin
let bin__RichText(bin:byte[],index:Ref<int>):RichText = (bin,index)|>bin__str

let Integer__bin(f:Integer,bb:BytesBuilder) = int64__bin
let bin__Integer(bin:byte[],index:Ref<int>):Integer = (bin,index)|>bin__int64

let Money__bin(f:Money,bb:BytesBuilder) = float__bin
let bin__Money(bin:byte[],index:Ref<int>):Money = (bin,index)|>bin__float

let Float__bin(bb:BytesBuilder)(f:Float) = float__bin
let bin__Float(bin:byte[],index:Ref<int>):Float = (bin,index)|>bin__float

let Boolean__bin(f:Boolean,bb:BytesBuilder) = bool__bin
let bin__Boolean(bin:byte[],index:Ref<int>):Boolean = (bin,index)|>bin__bool

let Pwd__bin(f:Pwd,bb:BytesBuilder) = str__bin
let bin__Pwd(bin:byte[],index:Ref<int>):Pwd = (bin,index)|>bin__str

let SelectLines__bin(f:SelectLines,bb:BytesBuilder) = int32__bin
let bin__SelectLines(bin:byte[],index:Ref<int>):SelectLines = (bin,index)|>bin__int32

let Timestamp__bin(f:Timestamp,bb:BytesBuilder) = DateTime__bin
let bin__Timestamp(bin:byte[],index:Ref<int>):Timestamp = (bin,index)|>bin__DateTime

let TimeSeries__bin(f:TimeSeries,bb:BytesBuilder) = 
    f.Count |> System.BitConverter.GetBytes |> bb.append
    f.ToArray()
    |> Array.iter(fun item -> 
        let dt,data = item
        dt.Ticks |> System.BitConverter.GetBytes |> bb.append
        data.Length |> System.BitConverter.GetBytes |> bb.append
        data |> bb.append)

let bin__TimeSeries(bin:byte[],index:Ref<int>):TimeSeries = 

    let count = System.BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4

    let res = new List<DateTime * byte[]>()
    [| 0..count - 1|]
    |> Array.map(fun i ->
        let dt = System.BitConverter.ToInt64(bin,index.Value)
        index.Value <- index.Value + 8
        let dst = System.BitConverter.ToInt32(bin,index.Value) |> Array.zeroCreate
        index.Value <- index.Value + 4
        Array.Copy(bin,index.Value,dst,0,dst.Length)
        index.Value <- index.Value + dst.Length
        DateTime.FromBinary(dt),dst)
    |> res.AddRange

    res
        
let TimeSeries__bytes(f:TimeSeries) = 
    let bb = new BytesBuilder()
    TimeSeries__bin(f,bb)
    bb.bytes()

type FieldDef =
| FK of string
| Caption of string*int
| Chars of string*int
| Link of string*int
| Text of string
| Bin of string
| RichText of string
| Integer of string
| Money of string
| Float of string
| Boolean of string
| Pwd of string
| SelectLines of string*(string*string)[]
| Timestamp of string
| TimeSeries of string
| Unknown of string

let int__fieldname i =
    match i with
    | 0 -> "FK"
    | 1 -> "Caption"
    | 2 -> "Chars"
    | 3 -> "Link"
    | 4 -> "Text"
    | 6 -> "Bin"
    | 7 -> "RichText"
    | 8 -> "Integer"
    | 9 -> "Money"
    | 10 -> "Float"
    | 11 -> "Boolean"
    | 12 -> "Pwd"
    | 13 -> "SelectLines"
    | 14 -> "Timestamp"
    | 15 -> "TimeSeries"
    | _ -> ""

type Rcd<'p> = {
    ID:int64;
    Createdat:DateTime;
    mutable Updatedat:DateTime;
    mutable Sort:int64;
    mutable p:'p }

let db__Rcd db__p (line:Object[]) = {
    ID = line.[0] :?> int64;
    Createdat = DateTime.FromBinary(line.[1] :?> int64);
    Updatedat = DateTime.FromBinary(line.[2] :?> int64);
    Sort = line.[3] :?> int64;
    p = db__p line }

let refin(conn,fieldorders,table,db__rcd,key)(id:int64) =

    match 
        {
            text = "SELECT " + fieldorders + " FROM [" + table + "] WHERE [" + key + "]=@" + key;
            ps = [|new SqlParameter(key,id)|] }
        |> multiline_query conn with
    | Suc(x) ->
        x.lines.ToArray() 
        |> Array.map(fun line ->
            let id = line.[0] :?> int64
            let createdat = DateTime.FromBinary(line.[1] :?> int64)
            let updatedat = DateTime.FromBinary(line.[2] :?> int64)
            let sort = line.[3] :?> int64
            (id,createdat,updatedat,sort),db__rcd line)
        |> Some
    | Fail(error,ex) -> None

let id__rcd(conn, fieldorders, table, db__rcd)(id:int64):'T option =
    match { text = "SELECT " + fieldorders + " FROM [" + table + "] WHERE [ID]=@ID"; ps = [|new SqlParameter("ID", id)|]}
        |> singleline_query conn with
        | Suc x -> x.line.Value |> db__rcd |> Some
        | _ -> None

let create_sql(table,sps_loader)(id:int64,createdat:DateTime,updatedat:DateTime,sort:int64,p) =
    let sps:SqlParameter[] = p |> sps_loader
    let text =
        let sb = new List<string>()
        sb.Add("INSERT INTO [" + table + "] (")
        sb.Add("[ID],[Createdat],[Updatedat],[Sort]")
        [0..sps.Length - 1] |> Seq.iter(fun i ->
            sb.Add(",[" + sps.[i].ParameterName + "]"))
        sb.Add(") VALUES (") |> ignore
        sb.Add("@ID,@Createdat,@Updatedat,@Sort")
        [0..sps.Length - 1] |> Seq.iter(fun i ->
            sb.Add(",@" + sps.[i].ParameterName))
        sb.Add(")")
        sb |> String.Concat
    let ps =
        let array = new ResizeArray<SqlParameter>()
        array.Add(SqlParameter("ID", id))
        array.Add(SqlParameter("Createdat", createdat.Ticks))
        array.Add(SqlParameter("Updatedat", updatedat.Ticks))
        array.Add(SqlParameter("Sort", sort))
        array.AddRange sps
        array.ToArray()
    {text=text;ps=ps}

let create(conn,output,table, sps_loader)(id:int64, timestamp:DateTime, p) =
    tx conn output ([|create_sql(table,sps_loader)(id,timestamp,timestamp,id,p)|])

let create_incremental(conn,output,table,sp_loader)(id:Ref<int64>,p) =
    let currentid = System.Threading.Interlocked.Increment id
    let currenttime = DateTime.UtcNow
    currentid,currenttime,create(conn,output,table,sp_loader)(currentid,currenttime,p)

let update_sql(table,fieldassigns,m__ps)(id:int64,ctime:DateTime,p)=
    let sps = m__ps(p)
    let ps = [|new SqlParameter("ID",id);new SqlParameter("Updatedat",ctime.Ticks)|]
    {
        text = "UPDATE ["+table+"] SET "+fieldassigns+" WHERE ID=@ID";
        ps = [|sps;ps|] |> Array.concat }

let update
    (conn,output,table,fieldassigns,m__ps,suc,fail)
    (id:int64,p,rollback_p,rollback_updatedat) =
    let currenttime = DateTime.UtcNow
    let sql = update_sql(table,fieldassigns,m__ps)(id,currenttime,p)
    currenttime, tx conn output [|sql|]

type LoadOrCreateRes<'p> = 
| Loaded of Rcd<'p>
| Created of Rcd<'p>
| ExLoC of DbTxError
| OverOneLoc

let loadorcreate(conn,output,table, id:Ref<int64>, sps_loader, db__p, wrapper)(sql,p) =
    match singleline_query conn sql with
    | Suc x ->
        let line = x.line.Value
        let id = line.[0] :?> int64
        let createdat = DateTime.FromBinary(line.[1] :?> int64)
        let updatedat = DateTime.FromBinary(line.[2] :?> int64)
        let sort = line.[3] :?> int64
        let p = line |> db__p
        ((id,createdat,updatedat,sort),p) |> wrapper |> Loaded
    | Fail(error,x) ->
        match error with
        | DbQueryError.Zero ->
            let cid,ctime,res = create_incremental(conn,output,table,sps_loader)(id,p)
            match res with
            | Suc x -> ((cid,ctime,ctime,cid),p) |> wrapper |> Created
            | Fail(x,y) -> ExLoC x
        | DbQueryError.Ex exn -> ExLoC{ exno = Some exn; sqlo = None }
        | DbQueryError.OverOne -> OverOneLoc

let loadall
    (conn:string)
    (table,fieldorders,db__rcd)
    where =

    match 
        [| "SELECT ";
            fieldorders;
            " FROM [" + table + "] ";
            where |]
        |> String.Concat
        |> str__sql
        |> multiline_query conn with
    | Suc x ->
        x.lines.ToArray()
        |> Array.map  db__rcd
        |> Some
    | Fail(error,x) -> None

let swap_sort(conn,output,table,error)(a:Rcd<'p>,b:Rcd<'p>) = 
    let sql = 
        [|
            "UPDATE " + table;
            " SET Sort=@Sort";
            " WHERE ID=@ID" |]
        |> String.Concat

    let sql1 = 
        sql
        |> build([|
                    new SqlParameter("Sort",b.Sort);
                    new SqlParameter("ID",a.ID)|])
    let sql2 = 
        sql
        |> build([|
                    new SqlParameter("Sort",a.Sort);
                    new SqlParameter("ID",b.ID)|])

    match tx conn output [| sql1; sql2 |] with
    | Suc v ->
        let asort,bsort = a.Sort, b.Sort
        a.Sort <- bsort
        b.Sort <- asort
    | Fail(eso,v) -> error(eso,v)

let data__ps(creator, assignor) data =
    data
    |> Array.map(fun item ->
        let p = creator()
        assignor(item,p)
        p)

type MetadataTypes<'p> = 
    {
        fieldorders:string;
        db__rcd:Object[]->Rcd<'p>;
        wrapper:((int64*DateTime*DateTime*int64)*'p)->Rcd<'p>;
        sps:'p->SqlParameter[];
        id:int64 ref;
        id__rcdo:(int64 -> Rcd<'p> option);
        clone:('p -> 'p);
        empty__p:(unit -> 'p);
        rcd__bin:BytesBuilder -> Rcd<'p> -> unit;
        bin__rcd:(byte[] * Ref<int>) -> Rcd<'p>;
        sql_update:string;
        rcd_update: (string -> unit) -> Rcd<'p> -> unit;
        table:string;
        shorthand:string }

let str__metadata = Dictionary<string, string*string>()

let id__tablename = Dictionary<int64, string>()
let tablename__ledger = Dictionary<string, int64>()

let str__enums = Dictionary<string*string, string[]>()
    
let build_create_sql metadata = create_sql(metadata.table,metadata.sps)
let build_update_sql metadata = update_sql(metadata.table,metadata.sql_update,metadata.sps)


let batch output processor (conn,top,where,order,metadata) =

    let count = 
        match
            [| "SELECT COUNT(ID)";
                " FROM [" + metadata.table + "]";
                " " + where |]
            |> Util.Text.linesConcat
            |> str__sql
            |> singlevalue_query conn with
        | Some v -> v :?> int32
        | None -> 0

    output("Count = " + count.ToString())

    let mutable processed = 0

    let mutable keep = true

    while keep do
        let threads =
            match
                [| "SELECT TOP " + top.ToString();
                    metadata.fieldorders;
                    " FROM [" + metadata.table + "]";
                    " " + where;
                    " " + order |]
                |> Util.Text.linesConcat
                |> str__sql
                |> multiline_query conn with
            | Suc ctx -> 
                ctx.lines.ToArray()
                |> Array.map metadata.db__rcd
            | Fail(exn,ctx) -> 
                [| |]

        if threads.Length = 0 then
            keep <- false

        threads
        |> Array.iter(fun t -> 
            processor t
            metadata.rcd_update output t)

        processed <- processed + threads.Length

        output("Processed = " + processed.ToString() + " / " + count.ToString())

let merge pretx conn output metadata dstID srcID incomings = 

    ("DELETE FROM " + metadata.table + " WHERE ID=" + srcID.ToString())
    |> str__sql
    |> pretx.sqls.Add

    incomings
    |> Array.map(fun incoming ->

        let table,fieldorders,field,suc = incoming

        let affected = 
            match 
                [| "SELECT " + fieldorders;
                    " FROM [" + table + "]";
                    " WHERE " + field + "=" + srcID.ToString() |]
                |> linesConcat
                |> str__sql
                |> multiline_query conn with
            | Suc ctx -> ctx.lines.ToArray()
            | Fail(exn,ctx) -> 
                output (exn.ToString())
                Util.Runtime.halt output "HALT merge" ""
                [| |]

        [| "UPDATE [" + table + "]";
            " SET " + field + "=" + dstID.ToString();
            " WHERE " + field + "=" + srcID.ToString() |]
        |> linesConcat
        |> str__sql
        |> pretx.sqls.Add
                    
        (fun ctx -> affected |> Array.iter suc)
        |> pretx.sucs.Add
                    
        affected.Length)

let loadDuplication conn output metadata fields where keying = 

    let dict = new Dictionary<string,List<int64>>()

    match 
        [| "SELECT ID," + fields;
            " FROM [" + metadata.table + "] ";
            where;
            " ORDER BY ID" |]
        |> linesConcat
        |> str__sql
        |> multiline_query conn with
    | Suc ctx -> 
        ctx.lines.ToArray() 
        |> Array.iter(fun line -> 
            let id = line.[0] :?> int64
            let key = line |> keying
            if dict.ContainsKey key = false then
                dict.Add(key,new List<int64>())
            dict.[key].Add id)
    | Fail(exn,ctx) -> 
        exn.ToString()
        |> Util.Runtime.halt output "HALT merge" 

    let hits = 
        dict.Keys
        |> Seq.toArray
        |> Array.map(fun k -> k,dict.[k])
        |> Array.filter(fun i -> snd(i).Count > 1)

    hits
    |> Array.map(fun hit -> 

        let key,items = hit

        let s = items.ToArray() |> Array.map(fun i -> i.ToString()) |> String.concat ","

        let dst = items.[0]
        let srcs = items.GetRange(1,items.Count - 1).ToArray()

        key,dst,srcs)


let mergeAll conn output metadata fields where keying incomings = 

    let pretx = None |> opctx__pretx

    let hits = loadDuplication conn output metadata fields where keying

    output("Merging " + hits.Length.ToString())

    hits
    |> Array.iter(fun hit -> 

        let key,dst,srcs = hit

        srcs
        |> Array.map(fun i -> 
            let src = (metadata.id__rcdo i).Value
            merge pretx conn output metadata dst src incomings)
            |> ignore
        ())

    match 
        pretx
        |> pipeline conn output with
    | Suc ctx ->
        ()
    | Fail(dte,ctx) -> 
        ()        


let checkBrokenFK pretx dom fk cod zeroOrRemove where = 

    [|  Util.Runtime.ptf zeroOrRemove "UPDATE" "DELETE FROM";
        " [" + dom.table + "]";
        Util.Runtime.ptf zeroOrRemove (" SET " + fk + "=0") "";
        " where [" + fk + "] NOT IN";
        " (SELECT ID FROM " + cod.table + ") ";
        where |]
    |> linesConcat
    |> str__sql
    |> pretx.sqls.Add

let commitCheckBrokenFK conn output dom fk cod zeroOrRemove where = 

    let pretx = None |> opctx__pretx

    checkBrokenFK pretx dom fk cod zeroOrRemove where

    pretx.sqls.ToArray()
    |> Array.iter(fun i -> i.text |> output)
    output("checkBrokenFK = " + pretx.sqls.Count.ToString() + " Tx")

    match 
        pretx
        |> pipeline conn output with
    | Suc(ctx) ->
        ()
    | Fail(dte,ctx) -> 
        ()        


//====================== [BIN] =======================================


let rcds__bin conn (top,where) metadata = 

    match
        [| "SELECT " + top;
            metadata.fieldorders;
            " FROM [" + metadata.table + "] " + where |]
        |> String.Concat
        |> str__sql
        |> multiline_query conn with
    | Suc(x) ->

        let items = 
            x.lines.ToArray()
            |> Array.map metadata.db__rcd
            |> Array.map(fun rcd -> 
                let bb = new Util.Bin.BytesBuilder()
                metadata.rcd__bin bb rcd
                let bin = bb.bytes()
                [| BitConverter.GetBytes(bin.Length); bin |]
                |> Array.concat)
                
        let bb = new Util.Bin.BytesBuilder()
        BitConverter.GetBytes(items.Length) |> bb.append
        items |> Array.iter bb.append

        bb.bytes()
        |> Some
            
    | Fail(exn,ctx) -> None

let bin__rcds metadata (bin:byte[]) = 

    if bin.Length >= 4 then
        let index = ref 0
        let count = BitConverter.ToInt32(bin,index.Value)
        index.Value <- index.Value + 4
        [| 0 .. count - 1 |]
        |> Array.map(fun item -> 
            let length = BitConverter.ToInt32(bin,index.Value)
            index.Value <- index.Value + 4
            let rcd = metadata.bin__rcd(bin,index)
            rcd)
    else
        [||]

let loaderByID(key,metadata,conn,output,exnLogger) id = 

    match 
        [|"SELECT ";
            metadata.fieldorders;
            " FROM [" + metadata.table + "] ";
            " WHERE ID=" + id.ToString() |]
        |> String.Concat
        |> str__sql
        |> singleline_query conn with
    | Suc x ->
        x.line.Value 
        |> metadata.db__rcd
        |> Some
    | Fail(error,x) ->
        match error with
        | DbQueryError.Zero -> ()
        | DbQueryError.Ex exn -> 
            (exn,x) 
            |> exnLogger("Orm.loaderByID/" + key) 
            |> ignore
        | DbQueryError.OverOne -> 
            Util.Runtime.halt output ("Orm.loaderByID/" + key) ""
        None

let loadIDs(conn,exnLogger,table,top,where) = 
    match
        [| "SELECT " + top;
            " [ID]";
            " FROM [" + table + "] ";
            where |]
        |> Util.Text.linesConcat
        |> str__sql
        |> multiline_query conn with
        | Suc ctx -> ctx.lines.ToArray()
        | Fail(exn,ctx) -> 
            (exn,ctx) |> exnLogger
            [| |]
