# JCS 项目架构文档

> 本文档聚焦 `JCS/TypeSys`——JCS 体系下的**类型驱动跨平台代码生成器**（"设计即真相 / Design-as-Truth"）。
> 它以 `Design-*.json` 领域模型声明为唯一输入，自动生成 F# 后端 ORM、TypeScript 前端类型/序列化、SQL 建表脚本三套产物。
> 本文基于 `TypeSys` 全部源码逐行核实，并以 **Aiarwa 项目**（当前唯一激活的 target）作端到端实例（见 [§8](#8-aiarwa-端到端实例worked-example)）。
> 已知问题汇总见 [§9](#9-已知问题与风险)。

---

## 目录

- [1. 系统定位与总体架构](#1-系统定位与总体架构)
  - [1.1 一句话定位](#11-一句话定位)
  - [1.2 四套产物](#12-四套产物)
  - [1.3 处理链路图](#13-处理链路图)
- [2. 目录与文件清单](#2-目录与文件清单)
- [3. 领域模型：MetaType 类型系统](#3-领域模型metatype-类型系统)
  - [3.1 核心记录：Table / Field / FieldDef](#31-核心记录table--field--fielddef)
  - [3.2 类型系统：Type / TypeEnum](#32-类型系统type--typeenum)
  - [3.3 FieldDef 全变体与语言映射](#33-fielddef-全变体与语言映射)
- [4. 配置与入口](#4-配置与入口)
  - [4.1 RobotConfig 结构](#41-robotconfig-结构)
  - [4.2 Program.fs 入口编排](#42-programfs-入口编排)
  - [4.3 多 target 配置（当前仅激活 20=Aiarwa）](#43-多-target-配置当前仅激活-20aiarwa)
  - [4.4 Loadcfg.fs 与 set.json（当前为孤儿）](#44-loadcfgfs-与-setjson当前为孤儿)
- [5. 加载阶段：Design-*.json → 内存模型](#5-加载阶段design-json--内存模型)
  - [5.1 文件发现与聚合](#51-文件发现与聚合)
  - [5.2 shorthand 推导](#52-shorthand-推导)
  - [5.3 字段解析与 FK 二次解析](#53-字段解析与-fk-二次解析)
  - [5.4 自定义类型解析（Types.fs 的 TypeManaged 区）](#54-自定义类型解析typesfs-的-typemanaged-区)
- [6. 生成器实现](#6-生成器实现)
  - [6.1 分层设计](#61-分层设计)
  - [6.2 CodeRobot.go 总编排](#62-coderobotgo-总编排)
  - [6.3 F# 记录类型生成（OrmTypes.fs）](#63-f-记录类型生成ormtypesfs)
  - [6.4 F# ORM 读写层（OrmMor.fs）](#64-f-orm-读写层ormmorfs)
  - [6.5 二进制序列化（CodeRobotIIFs）](#65-二进制序列化coderototiifs)
  - [6.6 JSON 序列化（CodeRobotI.fs 内 fdef__tjson/fdef__jsont）](#66-json-序列化coderototifs-内-fdef__tjsonfdef__jsont)
  - [6.7 TypeScript 生成（OrmTypes.d.ts / OrmMor.ts / marshall）](#67-typescript-生成ormtypesdts--ormmorts--marshall)
  - [6.8 SQL 生成（RDBMS.fs）](#68-sql-生成rdbmsfs)
  - [6.9 Vue 前端骨架（FrontendPackVue，当前不落盘）](#69-vue-前端骨架frontendpackvue当前不落盘)
  - [6.10 原子操作层（CodeRobotI.fs）](#610-原子操作层coderototifs)
- [7. 序列化二进制协议](#7-序列化二进制协议)
  - [7.1 系统列固定 32 字节](#71-系统列固定-32-字节)
  - [7.2 业务字段拼接](#72-业务字段拼接)
- [8. Aiarwa 端到端实例（worked example）](#8-aiarwa-端到端实例worked-example)
  - [8.1 项目背景与 target 20 配置](#81-项目背景与-target-20-配置)
  - [8.2 输入：Design-*.json 切片](#82-输入design-json-切片)
  - [8.3 产物一：F# OrmTypes.fs（记录类型）](#83-产物一f-ormtypesfs记录类型)
  - [8.4 产物二：F# OrmMor.fs（ORM 读写层）](#84-产物二f-ormmorfsorm-读写层)
  - [8.5 产物三：TS OrmTypes.d.ts](#85-产物三ts-ormtypesdts)
  - [8.6 产物四：SQL 建表脚本](#86-产物四sql-建表脚本)
  - [8.7 产物五：Types.fs 的 TypeManaged 区](#87-产物五typesfs-的-typemanaged-区)
  - [8.8 端到端串联与路径错位提醒](#88-端到端串联与路径错位提醒)
- [9. 已知问题与风险](#9-已知问题与风险)
  - [9.1 P0 — 阻断 / 安全](#91-p0--阻断--安全)
  - [9.2 P1 — 正确性隐患](#92-p1--正确性隐患)
  - [9.3 P2 — 工程 / 可维护性](#93-p2--工程--可维护性)
- [10. 修复路线图](#10-修复路线图)
- [11. 结论与建议](#11-结论与建议)
- [12. 跨平台双产物规范](#12-跨平台双产物规范完整-server-引用--跨平台-mauiapp-引用)

---

## 1. 系统定位与总体架构

### 1.1 一句话定位

`TypeSys` 是一个 **"设计即真相"（Design-as-Truth）跨平台代码生成器**：领域模型只在一处声明（`Design-*.json`），所有前后端数据层代码由工具统一生成，避免手写 ORM 与序列化带来的漂移。

### 1.2 四套产物

| 产物 | 落盘文件 | 内容 |
|---|---|---|
| F# 后端 ORM | `OrmTypes.fs` / `OrmMor.fs` / `Types.fs` | 记录类型 + bin/json 序列化 + DB 读写 S/D + DML 增删改查 + 枚举（见 §6.4：其中 DB 写操作 `sps`/`create`/`update`/`table` 另拆入 `Aiarwa.Shared.Native/Native/OrmMor.Native.fs`，供移动端引用时不间接持有 `Npgsql`/`System.Data.SqlClient`） |
| TypeScript 前端 | `OrmTypes.d.ts` / `marshall.ts` / `OrmMor.ts` | 类型定义 + 二进制/json 序列化 + API 通信 |
| SQL | `sqlSQLServer.sql` / `sqlPostgreSQL.sql` | SqlServer + PostgreSQL 双方言建表 + 增量迁移 |
| Vue（半成品） | （当前不落盘） | 从 `Design.json` 的 `vue.components` 生成 `.vue` 骨架 |

### 1.3 处理链路图

```
Design-*.json
      │  (Directory.GetFiles 过滤 "Design-" + ".json")
      ▼
MetaType 反序列化 + 类型打包（load）
      │  • shorthand 推导（大写首字母/大写缩写）
      │  • 字段解析 items__fieldo
      │  • FK 二次解析（ref → 目标 Table）
      │  • Types.fs 的 //[TypeManaged]{...} 自定义类型
      ▼
CodeRobot.go target
      ├─▶ F#   OrmTypes.fs / OrmMor.fs / Types.fs
      ├─▶ TS   OrmTypes.d.ts / OrmMor.ts
      ├─▶ SQL  sqlSQLServer.sql / sqlPostgreSQL.sql
      └─▶ Vue  骨架（buildComponent 写盘被注释）
```

---

## 2. 目录与文件清单

`JCS/TypeSys/` 下核心源文件（已忽略 `bin/` `obj/` 编译产物）：

| 文件 | 职责 |
|---|---|
| `Program.fs` | 入口。定义 `target__config`（魔术整数→RobotConfig）、`runMultiple`（当前只跑 20）。`[<EntryPoint>]` 等价逻辑：`BaseDirectory → runMultiple → halt`。 |
| `MetaType.fs` | 领域模型类型定义：`Table` / `Field` / `FieldDef` / `Type` / `TypeEnum` / `ProgrammingLang`。 |
| `Config.fs` | **重构后新增（规范模块）**：`RobotConfig`（`domainName`，已修正旧 `donmainName` 拼写）/ `Src` / `src__txt` / `productItems__term` / `table__fieldKeys` / `table__typeName`。活跃模块均 `open TypeSys.Config`。 |
| `CodeRobot.fs` | 总编排：`load` / `go` / `prepareRobot` / `buildTable*` / `buildType*` / `buildCustomTypes`。 |
| `CodeRobotI.fs` | **原子操作层**：`fdef__srcTypes` / `fdef__empty` / `fdef__tbin` / `fdef__bint` / `fdef__tjson` / `fdef__jsont` 等按字段类型分发的纯函数。 |
| `CodeRobotIIFs.fs` | F# 侧递归序列化实现：`t__binImpl` / `bin__tImpl` / `t__jsonImpl` / `json__tImpl` / `clone` / `t__emptyImpl`。 |
| `CodeRobotIITs.fs` | TS 侧递归序列化实现：`t__binImpl` / `bin__tImpl` / `t__emptyImpl` 等。 |
| `LangPackTypeScript.fs` | TS 类型注解：`type__annotation`（大量 `any`）、`type__TypeScript`（自定义类型生成）。 |
| `RDBMS.fs` | SQL 生成：`table__sql`（双方言建表）、`sqlField`、`updateDatabase`（**死代码**）。 |
| `FSharp.fs` | `go`（空壳：解析 `Types.fs` 但返回 `()`，无任何产出；重构后已无调用方）。 |

> ⚠️ **编译图（fsproj 实际 `<Compile Include>`）**：仅上述 10 个 `.fs` 参与构建，顺序为 `FSharp → MetaType → Config → RDBMS → CodeRobotI → LangPackTypeScript → CodeRobotIIFs → CodeRobotIITs → CodeRobot → Program`。
> 以下文件**仍在目录中但已被移出编译图**（不参与构建，改动无副作用），属重构前的残留：
> - `Common.fs`：旧版 `RobotConfig`(`donmainName`)/`Src`/辅助函数的副本，仅被同样未编译的 `FrontendPackVue.fs`/`Loadcfg.fs` 引用。
> - `FrontendPackVue.fs`：Vue 组件骨架生成，`buildComponent` 末尾 `try_write_text` 三行被注释 → 不落盘。
> - `Loadcfg.fs`：从 `set.json` 用 `System.Text.Json` 反序列化配置（**从未被 Program 调用**，且仍 `open TypeSys.Common`）。
> - `set.json`：多项目配置（Game 含明文密码）。**与 Program.fs 内联硬编码不一致，当前是"假配置"**。
> - `mem.md`：模块记忆（作者注记，含若干历史描述，部分已与实际代码不符，本文以实际代码为准）。 |

---

## 3. 领域模型：MetaType 类型系统

### 3.1 核心记录：Table / Field / FieldDef

`Table`（定义在 `MetaType.fs`）：

```fsharp
type Table = {
    tableName: string
    fields: Dictionary<string, Field>      // key = 字段名
    fkins: List<Table * string>            // 入边 FK
    fkouts: List<string * Table>           // 出边 FK
    idstarting: int64                     // ID 起始值（自增种子）
    typeName: string }                    // 由 shorthand 推导，全大写
```

`Field` 是四元组 `(sort:int, name:string, def:FieldDef, items:string[])`，存于 `Table.fields` 字典，key 为字段名。

### 3.2 类型系统：Type / TypeEnum

`Type` 支持四种 `TypeEnum` 变体（驱动不同生成路径）：

| TypeEnum | 含义 | 生成去向 |
|---|---|---|
| `OrmRcd table` | ORM 记录（来自 Design 表） | `OrmTypes.fs` + `OrmMor.fs` |
| `Ormp table` | ORM 记录的 `p` 载荷类型 | `OrmTypes.fs` |
| `Structure items` | 普通结构体 | `Types.fs` / `CustomMor.fs` |
| `Enum items` / `Sum items` / `Product` | 枚举 / 可区分联合 / 积类型 | `Types.fs` / `CustomMor.fs` / `.d.ts` |

`buildTypeCat`（`CodeRobot.fs:64`）把每张表打包成 `OrmRcd` + `Ormp` 两个 `Type`，再并入 `Types.fs` 里 `//[TypeManaged]{...}` 解析出的自定义类型。

### 3.3 FieldDef 全变体与语言映射

`FieldDef` 是字段类型判别联合，`fdef__srcTypes`（`CodeRobotI.fs`）给出三语言映射 `(fsType, csType, tsType)`：

| FieldDef | F# 类型 | TS 类型 | 备注 |
|---|---|---|---|
| `FK table` | `int64` | `number` | 存目标表 ID |
| `Caption n` | `string` | `string` | 定长 n 字符显示名 |
| `Chars n` | `string` | `string` | 定长 n 字符串 |
| `Link n` | `string` | `string` | 链接 |
| `Text` | `string` | `string` | 长文本 |
| `Bin` | `byte[]` | `Uint8Array` | 二进制块 |
| `Integer` | `int64` | `number` | |
| `Float` | `float` | `number` | |
| `Boolean` | `bool` | `boolean` | **默认值为 `true`（见 §9.2）** |
| `SelectLines lines` | 枚举（`int`） | `number` | 下拉枚举，含 `(key,caption)` |
| `Timestamp` | `DateTime` | `Date` | `Ticks` 存储 |
| `TimeSeries` | `TimeSpan`/`Date` | — | **四处序列化被注释（见 §9.2）** |
| `Other` | — | — | 占位/未识别 |

---

## 4. 配置与入口

### 4.1 RobotConfig 结构

`RobotConfig` 记录现定义于 **`Config.fs`（`TypeSys.Config`，重构后从 `Common.fs` 抽出，成为规范模块）**：

```fsharp
type RobotConfig = {
    ns: string            // 命名空间，如 "Aiarwa.Shared"
    rdbms: Rdbms          // SqlServer / PostgreSql
    dbName: string
    domainName: string   // ✅ 原 donmainName 拼写错误已修正
    conn: string          // 数据库连接串（**明文密码**，见 §9.1）
    mainDir: string       // F# 输出目录（含 Design-*.json）
    JsDir: string }       // TS 输出目录
```

> ⚠️ `Common.fs` 仍保留旧版 `RobotConfig`（`donmainName`）与同名辅助函数，但 `Common.fs` 已**不在编译图中**（见 §2）；该副本为死代码，引用它的 `FrontendPackVue.fs`/`Loadcfg.fs` 同样被排除出构建。`Src` 记录（`{ filename:string; w:MultiLineTextWriter }`）与 `productItems__term`/`table__fieldKeys`/`table__typeName` 选择器现也位于 `Config.fs`。

### 4.2 Program.fs 入口编排

```
System.AppContext.BaseDirectory.TrimEnd(...)  ──▶  runMultiple exeDir
        │
        │  [| 20 |]  (其余 target 全注释)
        ▼
Array.map target__config   ──▶  每个 target → RobotConfig
        │
        ▼
Array.iter (CodeRobot.go output exeDir)
        │
        ▼
Util.Runtime.halt output "" ""
```

### 4.3 多 target 配置（当前仅激活 20=Aiarwa）

`target__config`（`Program.fs:15`）是一个 `match target` 的魔术整数分发器，支持约十几个项目：

| target | 项目 | ns | rdbms | 路径 |
|---|---|---|---|---|
| 6 | JCS | `JCS.Shared` | SqlServer | `C:\Dev\JCS\JCS.Shared` |
| 7 | J7 | `J7.Shared` | SqlServer | `C:\Dev\J7\J7.Shared` |
| 9 / 10 | Game | `Shared`/`Game.Shared` | SqlServer/PG | `C:\Dev\Game\...` |
| 11 | GNexts | `Shared` | SqlServer | `C:\Dev\GNexts\...` |
| 15 / 17 | J | `Shared`/`J.Shared` | SqlServer | `C:\Dev\J\...` |
| 16 | Studio | `Studio.Shared` | SqlServer | `C:\Dev\Studio\...` |
| 18 | FA | `FA.Shared` | SqlServer | `C:\Dev\FA\...` |
| 19 | JA | `JA.Shared` | SqlServer | `C:\Dev\JA\...` |
| **20** | **Aiarwa** | `Aiarwa.Shared` | **PostgreSql** | **`C:\Dev\Aiarwa\Shared`** |
| 0/5 | CTC | `Shared` | PG/SqlServer | `C:\Dev\GCHAIN2024\...` |
| 8 | GenVI | `Shared` | SqlServer | `C:\Dev\DevCoop\...` |
| 1 | GCHAIN | `Shared` | SqlServer | `C:\Dev\GCHAIN2024\...` |
| 2 | Personal | `BizType` | SqlServer | `C:\Dev\Personal\...` |
| `_`(默认) | BizShared | `BizShared` | SqlServer | `C:\Dev\JCS\BizShared` |

> ⚠️ **问题**：target 是魔术整数无枚举；15 与 17 都对应 "J"、9 与 10 都对应 "Game"（重复）。`runMultiple` 当前只跑 `20`（其余 target 仅历史残留，见下方说明）。

> **重构现状**：`target__config` 实际**只内联了 target 20**（Aiarwa）的真实配置（见 [§8.1](#81-项目背景与-target-20-配置)），路径已修正为 `C:\Dev\Aiarwa\...`、`domainName` 拼写 `donmainName`→`domainName` 已修正；表中其余 target（6/7/9/10/…）为历史残留、当前并未被 `target__config` 引用，跑 `runMultiple` 只会生成 Aiarwa。`RobotConfig` 现定义于 `Config.fs`（规范模块），`Common.fs` 的旧副本已移出编译图。

### 4.4 Loadcfg.fs 与 set.json（已孤儿化 + 移出编译图）

`Program.fs` 配置走 `target__config` 内联硬编码，`Loadcfg.fs` 从未被调用。它与 `Common.fs`/`FrontendPackVue.fs` 一并**未列入 `TypeSys.fsproj`**，不参与构建——改动对当前运行无任何效果（"假配置"陷阱）。`Loadcfg.fs` 仍 `open TypeSys.Common` 并用 `System.Text.Json`（非项目统一的 `Util.Json`），保留旧 `donmainName` 读取逻辑，与 `Config.fs` 的新 `domainName` 脱节。

---

## 5. 加载阶段：Design-*.json → 内存模型

`load`（`CodeRobot.fs:158`）是核心加载函数，返回 `(modulenames, cTypes, tc, tables)`。

### 5.1 文件发现与聚合

```fsharp
robot.config.mainDir
|> Directory.GetFiles
|> Array.filter(fun i -> i.Contains "Design-" && i.EndsWith ".json")
|> Array.map (Util.FileSys.try_read_string >> snd)
|> Array.map (findBidirectional ("[","]"))   // 取顶层 [ ] 数组块
|> String.concat ","
```

多份 `Design-*.json` 的各表被拼接为单个 `{"tables":[...]}` 再反序列化。Aiarwa 即采用**分域多文件**策略：`Design-Ai.json` / `Design-Bus.json` / `Design-Ca.json` / `Design-IoT.json` / `Design-Mm.json` / `Design-Model.json` / `Design-Oa.json` / `Design-Petro.json` / `Design-Sys.json` / `Design.json` 共 10 份，覆盖 8 个业务域。

### 5.2 shorthand 推导

`dict__shorthand`（`CodeRobot.fs:162`）：若 json 含 `"shorthand"` 字段则用之；否则取 `name` 中**全大写且非下划线**字符组成缩写（如 `Kernel_Unit` → `KU`）。`typeName = shorthand.ToUpper()`。`Config.fs` 的 `table__typeName t = t.shorthand.ToUpper()` 引用了 `Table` 上已不存在的字段（见 §9.2）。

### 5.3 字段解析与 FK 二次解析

1. `items__fieldo` 把每个 field 解析为 `(fname, fdef, items)` 并加入 `t.fields`。
2. `FieldDef.Other` 且 `items["enum"]=="FK"` 时，按 `ref`（缺省取 `fname`）去 `tables` 里查找目标表，回填为 `FK targetTable`；找不到则指向自身（自引用）。
3. 最后删除所有残留的 `FieldDef.Other` 字段（未被识别为 FK 的）。

### 5.4 自定义类型解析（Types.fs 的 TypeManaged 区）

```fsharp
robot.config.mainDir + @"\Types.fs"
|> filename__lines
|> findInLines("//[TypeManaged]{","//}")
|> parseCustomTypes modulenames
```

`Types.fs` 中用 `//[TypeManaged]{ ... }` 包裹的 F# 类型定义（Structure/Enum/Sum）被解析进 `cTypes`，并提取模块名（`modulenames`）供 `OrmMor.fs` / `CustomMor.fs` 的 `open` 语句使用。Aiarwa 的 `Types.fs` 即含 `EuComplex` / `HostEnum` / `RuntimeData` 等自定义类型（见 [§8.7](#87-产物五typesfs-的-typemanaged-区)）。

---

## 6. 生成器实现

### 6.1 分层设计

```
Config.fs              配置/IO 契约层（RobotConfig / Src / 选择器）— 重构后从 Common.fs 抽出，规范模块
CodeRobot.fs           编排层（go / load / buildTable* / buildType*）
   ├─ CodeRobotI.fs      原子操作层（按 FieldDef 分发的纯函数）
   ├─ CodeRobotIIFs.fs   F# 递归序列化实现
   ├─ CodeRobotIITs.fs   TS 递归序列化实现
   ├─ LangPackTypeScript.fs  TS 类型注解/自定义类型
   └─ RDBMS.fs           SQL 生成

（FrontendPackVue.fs / Loadcfg.fs / Common.fs 已移出编译图，见 §2）
```

F# 与 TS 各有一套并行的递归生成器（`t__binImpl`/`bin__tImpl`/`t__jsonImpl`/`json__tImpl`/`t__emptyImpl`），结构对称。

### 6.2 CodeRobot.go 总编排

`go`（`CodeRobot.fs:1102`）流程：

1. `prepareRobot`：按 `config.mainDir` / `config.JsDir` 创建 9 个 `Src`（含文件名）。
2. `load`：得到 `modulenames, cTypes, tc, tables`。
3. 写 SQL 头 `USE [dbName]`，逐表 `table__sql`。
4. 写 TS `OrmMor.ts` / `CustomMor.ts` 的 import 头与 `marshall` 合并。
5. 写 F# header（`fSharpHeader`：module + open 列表）。
6. `buildCustomTypes`：把 `cTypes` 写入 `Types.d.ts`（`declare global { namespace <dbName> }`）与 `CustomMor.ts`。
7. 遍历 `tc__sorted`：
   - `OrmRcd` → `buildType` 写入 `OrmMor.fs` + `OrmMor.ts`
   - `Structure`/`Sum` → `buildType` 写入 `CustomMor.fs` + `CustomMor.ts`
8. `filter<Type,Table> matchOrm |> buildTables`：生成所有 ORM 表的 DML 与 `init()`。
9. `save srcs`：9 个文件落盘。
10. 若 `JsDir` 存在：从 `VsCodeTemplate` 拷贝若干 `util/*.ts` 工具文件；调用 `FrontendPackVue.build`（默认不落盘）。
11. 输出 `"Done"` → `halt`。

### 6.3 F# 记录类型生成（OrmTypes.fs）

`buildTable`（`CodeRobot.fs:788`）按表生成：

- `p<TypeName>` 记录（业务载荷，全 `mutable` 字段）+ `[key:string]: any` 的 TS 对应。
- `buildTableEnums`（`CodeRobot.fs:332`）：为每个 SelectLines 枚举生成 F# 判别联合 + `int__*` / `str__*` / `*__caption` 互转函数。**TS 枚举生成块被注释**（约 40 行 `//export const enum...`）。
- `buildTableType`（`CodeRobot.fs:449`）：生成 `type <TypeName> = Rcd<p<TypeName>>`、字段顺序 `fieldorders()`（双方言）、`sql_update()`、`fields()`、`empty()`、ID/Count/Table 常量。

### 6.4 F# ORM 读写层（双写：跨平台子集 `OrmMor.fs` + native 全量 `OrmMor.Native.fs`）

> **B2 双写（Native 拆分）**：为让移动端（MauiApp）能直接引用 `Aiarwa.Shared` 而不间接持有 `Npgsql`/`System.Data.SqlClient`，单表 `metadata` 被拆为两份，由 **两个写句柄** 分流（命名铁律：不能跨平台用 `Native` 后缀）：
> - `om`（`Robot.om`，`CodeRobot.fs:36`）→ `Aiarwa.Shared/OrmMor.fs`（**跨平台子集**，零 DB 驱动依赖）。
> - `omdb`（`Robot.omdb`，`CodeRobot.fs:37`）→ `Aiarwa.Shared.Native/Native/OrmMor.Native.fs`（**native 全量**，含 SQL 写操作，不能跨平台）。

`buildTableMor`（`CodeRobot.fs:601`，写句柄 `om`）生成**跨平台子集**，每张表产出（注意：无 `sps`/`p_create`/`sql_update`/`rcd_update`/`table` 等 DB 写字段）：

| 函数 | 作用 |
|---|---|
| `db__p<TypeName>` | `Object[]`(DB 行) → `p` 载荷（按列偏移 `i+4` 读取，跳过 4 个系统列；仅用 `Convert.IsDBNull`(BCL) + `:?>` 造型，不引 `System.Data.SqlClient`/`Npgsql`） |
| `db__<TypeName>` | `db__Rcd db__p<TypeName>` |
| `<TypeName>_wrapper` | `(i,c,u,s),p` → 完整 `Rcd` |
| `p<TypeName>_clone` | 浅拷贝 |
| `p<TypeName>_marshall` / `<TypeName>_marshall` | 序列化 marshall 对象（bin/json 函数指针） |
| `<TypeName>_metadata` | `Util.Orm.MetadataTypes<p<TypeName>>`：`db__rcd`/`wrapper`/`id`(`_id`)/`id__rcdo = (fun _ -> None)`/`clone`/`empty__p`/`rcd__bin`/`bin__rcd`/`p__json`/`json__po`/`rcd__json`/`json__rcdo`/`shorthand` —— **不含** `sps`/`p_create`/`sql_update`/`rcd_update`/`table` |

`buildTableMorNative`（`CodeRobot.fs:688`，写句柄 `omdb`）生成**native 全量**，在子集基础上补全 DB 写操作（引 `Util.Db`/`Util.Orm` 的 `kvp__sqlparam`/`create`/`update`/`id__rcd`）：

| 函数 | 作用 |
|---|---|
| `p<TypeName>__sps` | `p` → SQL 参数 `kvp__sqlparam[]`（双方言，SelectLines/Timestamp 特判） |
| `<TypeName>_update_transaction` / `_update` | 带回滚的事务更新 |
| `<TypeName>_create_incremental_transaction` / `_create` | `Interlocked.Increment` 自增 ID 的创建 |
| `id__<TypeName>o` | 按 ID 取记录（`id__rcd(conn, ...)`） |
| `<TypeName>_metadata` | `Util.Orm.MetadataTypesNative<p<TypeName>>`：在子集字段外另含 `sps`/`p_create`/`sql_update`/`rcd_update`/`table` |
| `<TypeName>TxSqlServer` | SqlServer 建表 `IF NOT EXISTS` 内嵌 SQL |

两份 `_metadata` 的聚合构造在各自生成器末尾（子集 L670 `MetadataTypes<pX>`、native L831 `MetadataTypesNative<pX>`）。

`buildTables`（`CodeRobot.fs:816`）再补：两套 `_metadata` 各自的 `conn` 可变、所有表类型 `MetadataEnum` 联合、`tablenames` 数组、`init()`（从 DB 读 MAX(ID)/COUNT 复位内存计数器，仅 native 全量真正连库）。

### 6.5 二进制序列化（CodeRobotIIFs）

递归 `t__binImpl`（写）/ `bin__tImpl`（读）遍历 `Type` 的每个字段，按 `FieldDef` 调用 `CodeRobotI.fs` 的 `fdef__tbin`（写片段）/ `fdef__bint`（读片段）。协议见 [§7](#7-序列化二进制协议)。

### 6.6 JSON 序列化（CodeRobotI.fs 内 fdef__tjson/fdef__jsont）

- `fdef__tjson`：字段 → JSON 键值（`Boolean` 用 `Json.True/False`，正确）。
- `fdef__jsont`：JSON → 字段（读侧，注意 `Boolean` 解析）。
- **`TimeSeries` 在 `fdef__tjson`/`fdef__jsont`/`fdef__tbin`/`fdef__bint` 四处均被注释，落到 `_ -> ()/""`**（见 §9.2）。

### 6.7 TypeScript 生成（OrmTypes.d.ts / OrmMor.ts / marshall）

- `LangPackTypeScript.type__annotation`：对 `Sum`→`any`、`Product`→`any`，大量返回 `"any"`；`OrmTypes.d.ts` 用 `declare global { namespace <dbName> { ... } }` + `[key:string]: any`。生成的 marshall 代码大量 `any` 绕过编译期检查。
- `OrmMor.ts` 头部固定合并 `@lchenmay/jcs-common` 的 `BinIndexed`/`BytesBuilder` 与 `binCommon`，形成 `marshall`。
- TS 侧 JSON 序列化（`t__jsonImpl`/`json__tImpl`）在 `buildType` 中被注释掉（`CodeRobot.fs:926,958`），当前 TS 只生成 bin 序列化。

### 6.8 SQL 生成（RDBMS.fs）

- `table__sql`（`RDBMS.fs`）：双方言建表，列类型由 `sqlField` 映射（`NVARCHAR(n)`/`BIGINT`/`FLOAT` 等）。系统列 `ID/Createdat/Updatedat/Sort` 固定。
- `updateDatabase`（`RDBMS.fs:340`）：**死代码，无任何调用方**。其 SqlServer 分支复制了 `table__sql` 建表 SQL，后半段还把所有字段 `ALTER COLUMN ... NCHAR(64)` 强制定长，与 `sqlField` 冲突。**`go` 实际走 `table__sql`，此函数纯属混淆**。

### 6.9 Vue 前端骨架（FrontendPackVue，当前不落盘）

`buildComponent` 从 `Design.json` 的 `vue.components` 生成 `.vue` 文件内容，但末尾 `try_write_text` 三行被注释（`FrontendPackVue.fs:84-88`）→ 模块产出 `()`，即"生成了但不落盘"。

### 6.10 原子操作层（CodeRobotI.fs）

纯函数集合，按 `FieldDef` 分发给各语言片段：

| 函数 | 作用 |
|---|---|
| `fdef__srcTypes` | 三语言类型名映射 |
| `fdef__empty` | 字段默认值（`Boolean`→`true`，见 §9.2；`CodeRobotI.fs:448`） |
| `fdef__tbin` / `fdef__bint` | bin 写/读片段 |
| `fdef__tjson` / `fdef__jsont` | json 写/读片段 |

均采用非穷尽 `match ... | _ -> ()/""` 兜底（见 §9.2）。

---

## 7. 序列化二进制协议

### 7.1 系统列固定 32 字节

每条记录的二进制布局**前 4 个字段固定为系统列**（共 8×4 = 32 字节）：

| 偏移 | 字段 | 类型 | 字节 |
|---|---|---|---|
| 0 | ID | int64 | 8 |
| 8 | Createdat | int64 (Ticks) | 8 |
| 16 | Updatedat | int64 (Ticks) | 8 |
| 24 | Sort | int64 | 8 |

`db__p<TypeName>` 读取业务字段时从 `line[i+4]`（第 5 列）开始，正是跳过这 4 个系统列。

### 7.2 业务字段拼接

每个 `FieldDef` 按 `fdef__tbin`/`fdef__bint` 片段写入/读出对应字节。`Rcd<T>` 包装 `(ID, Createdat, Updatedat, Sort) * p<T>`，bin 序列化即「系统列 + 业务字段 `p` 顺序拼接」。

---

## 8. Aiarwa 端到端实例（worked example）

Aiarwa 是 `TypeSys` **当前唯一激活的 target（target 20）**，其生成的 `OrmTypes.fs` 约 331KB，覆盖 8 个业务域。下面用 `Ai_Prompt` / `Ca_EndUser` / `Sys_Menu` 三张表串起「设计 → 四套产物」的完整闭环。

### 8.1 项目背景与 target 20 配置

`Program.fs` 的 target 20 配置（重构后实际值）：

```fsharp
| 20 ->
    {   ns = "Aiarwa.Shared"
        rdbms = Rdbms.PostgreSql
        dbName = "Aiarwa"
        domainName = "whatsyourideal.com"   // ✅ 原 donmainName 拼写已修正
        conn = "Host=localhost;Port=5432;Database=aiarwa;Username=aiarwa;Password=e2TpqcaTEYLfkvFMkc"
        mainDir = @"C:\Dev\Aiarwa\Shared"
        JsDir = @"C:\Dev\Aiarwa\Vue\src\lib" }
```

实际源码位于 `c:/Dev/Aiarwa/Aiarwa.Shared/`（跨平台子集）与 `c:/Dev/Aiarwa/Aiarwa.Shared.Native/`（native 全量），输入是 10 份 `Design-*.json`，输出落盘于：
- `Aiarwa.Shared/OrmTypes.fs`、`OrmMor.fs`、`Types.fs`、`CustomMor.fs`、`PreOrm.fs`（跨平台子集，零 DB 驱动依赖）
- `Aiarwa.Shared.Native/Native/OrmMor.Native.fs`（native 全量，含 SQL 写操作，引 `Util.Db`/`Npgsql`）
- `Aiarwa.Shared/sqlPostgreSQL.sql`、`sqlSQLServer.sql`、`OrmTypes.sql`
- `Aiarwa/vscode/src/lib/shared/OrmTypes.d.ts`、`OrmMor.ts`、`CustomMor.ts`、`Types.d.ts`

> ✅ 路径已从旧文档记载的 `E:\DEV\Aiarwa\...` 修正为 `C:\Dev\Aiarwa\...`、`domainName` 拼写也已修正（原 `donmainName`/`wigaoil.com` 错配已解决，见 [§9.1](#91-p0--阻断--安全)）。**但密码仍明文写死源码**（§9.1-P0-①），且 `runMultiple` 仅支持 target 20（魔术整数，§9.1-P0-④）——这些尚未修复。

### 8.2 输入：Design-*.json 切片

**`Design-Ai.json`（节选 Ai_Prompt）**

表名 `Ai_Prompt`、shorthand `PROMPT`，字段 `User`(FK)、`Description`(Chars 200)、`Fulltext`(Text)：

```json
{
  "name": "Ai_Prompt",
  "shorthand": "PROMPT",
  "fields": [
    { "enum": "FK",      "name": "User" },
    { "enum": "Chars",   "length": 200, "name": "Description" },
    { "enum": "Text",    "name": "Fulltext" }
  ]
}
```

**`Design-Ca.json`（节选 Ca_EndUser）**

演示 `id` 起始种子、`SelectLines` 枚举、`FK` 跨域引用（`OA_Division`）：

```json
{
  "name": "Ca_EndUser",
  "shorthand": "eu",
  "id": 1001,
  "fields": [
    { "enum": "Chars", "length": 64,  "name": "Caption" },
    { "enum": "Chars", "length": 255, "name": "Email" },
    { "enum": "Chars", "length": 64,  "name": "Pwd" },
    { "enum": "SelectLines",
      "lines": "Normal//Normal///Authorized//Authorized///Admin//Admin///Frozen//Frozen",
      "name": "AuthType" },
    { "enum": "FK", "ref": "OA_Division", "name": "Division" }
  ]
}
```

**`Design-Sys.json`（节选 Sys_Menu）**

演示 `FK` 自引用（`ref` 指向自身 `Sys_Menu`）：

```json
{
  "name": "Sys_Menu",
  "shorthand": "menu",
  "fields": [
    { "enum": "Text", "name": "route" },
    { "enum": "FK",   "ref": "Sys_Menu", "name": "Parent" },
    { "enum": "Text", "name": "Caption" }
  ]
}
```

### 8.3 产物一：F# OrmTypes.fs（记录类型）

`Ai_Prompt` 生成的 F# 记录与辅助（节选自 `OrmTypes.fs`）：

```fsharp
// [Ai_Prompt] (PROMPT)

type pPROMPT = {
    mutable User: FK
    mutable Description: Chars
    mutable Fulltext: Text }

type PROMPT = Rcd<pPROMPT>

let PROMPT_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[User],[Description],[Fulltext]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "user","description","fulltext" """

let pPROMPT_fields() =
    match rdbms with
    | Rdbms.SqlServer -> [| FK("User"); Chars("Description", 200); Text("Fulltext") |]
    | Rdbms.PostgreSql -> [| FK("user"); Chars("description", 200); Text("fulltext") |]

let pPROMPT_empty(): pPROMPT = { User = 0L; Description = ""; Fulltext = "" }

let PROMPT_id = ref 3658821L     // 自动推导的自增种子（Design 未显式指定 id）
let PROMPT_count = ref 0
let PROMPT_table = "Ai_Prompt"
```

要点：
- `p<TypeName>` 全 `mutable` 业务载荷；`Rcd<p<TypeName>>` 包装 4 个系统列。
- **双方言** `fieldorders()` / `fields()`（SqlServer 大写列、`PostgreSql` 小写列）。
- `SelectLines` 生成判别联合 + 互转函数，如 `Ca_EndUser` 的 `AuthType` →

```fsharp
// [Ca_EndUser] (EU) 内由 SelectLines 生成的枚举
type euAuthTypeEnum =
    | Normal = 0
    | Authorized = 1
    | Admin = 2
    | Frozen = 3
// 以及 int__euAuthTypeEnum / str__euAuthTypeEnum / euAuthTypeEnum__caption 三件套
```

> 注意：若 Design 显式写 `"id": 1001`（如 `Ca_EndUser`），则对应 `EU_id = ref 1001L`；`Ai_Prompt` 未写 id，生成器推导出一个大整数种子 `3658821L`。

### 8.4 产物二：F# OrmMor.fs（ORM 读写层）

`Ai_Prompt` 生成的 DB 读写与 DML（节选自 `OrmMor.fs`）：

```fsharp
let db__pPROMPT(line:Object[]): pPROMPT =
    let p = pPROMPT_empty()
    p.User       <- if Convert.IsDBNull(line[4]) then 0L else line[4] :?> int64   // 跳过 4 个系统列
    p.Description <- string(line[5]).TrimEnd()
    p.Fulltext    <- string(line[6]).TrimEnd()
    p

let pPROMPT__sps (p:pPROMPT) =
    match rdbms with
    | Rdbms.SqlServer ->
        [| ("User", p.User) |> kvp__sqlparam
           ("Description", p.Description) |> kvp__sqlparam
           ("Fulltext", p.Fulltext) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [| ("user", p.User) |> kvp__sqlparam
           ("description", p.Description) |> kvp__sqlparam
           ("fulltext", p.Fulltext) |> kvp__sqlparam |]

let db__PROMPT = db__Rcd db__pPROMPT

let PROMPT_create_incremental_transaction output (suc, fail) p =
    let cid = Interlocked.Increment PROMPT_id            // 自增 ID
    let ctime = DateTime.UtcNow
    match create (conn, output, PROMPT_table, pPROMPT__sps) (cid, ctime, p) with
    | Suc ctx -> ((cid, ctime, ctime, cid), p) |> PROMPT_wrapper |> suc
    | Fail (eso, ctx) -> fail (eso, ctx)

let PROMPT_metadata = {
    fieldorders = PROMPT_fieldorders
    db__rcd = db__PROMPT
    sps = pPROMPT__sps
    id = PROMPT_id
    id__rcdo = id__PROMPTo
    empty__p = pPROMPT_empty
    rcd__bin = PROMPT__bin
    bin__rcd = bin__PROMPT
    p__json = pPROMPT__json
    json__po = json__pPROMPTo
    p_create = PROMPT_create
    sql_update = PROMPT_sql_update
    table = PROMPT_table
    (* ... 其余函数指针 ... *) }
```

要点：
- `db__pPROMPT` 从 `line[4..6]` 读取，**首 4 列是系统列**（对应 [§7.1](#71-系统列固定-32-字节)）。
- `pPROMPT__sps` 双方言 SQL 参数；`PROMPT_create_incremental_transaction` 用 `Interlocked.Increment` 实现线程安全自增 ID。
- 上例 `PROMPT_metadata` 为 **native 全量**（`Util.Orm.MetadataTypesNative<pPROMPT>`，见 [§6.4](#64-f-orm-读写层ormmorfs) 的 `buildTableMorNative`），含 `sps`/`p_create`/`sql_update`/`rcd_update`/`table` 等 DB 写字段，落盘 `Aiarwa.Shared.Native/Native/OrmMor.Native.fs`。
- 跨平台子集 `OrmMor.fs` 另有 `PROMPT_metadata : Util.Orm.MetadataTypes<pPROMPT>`（`buildTableMor` 产出），**仅含** `db__rcd`/`wrapper`/`id`/`clone`/`empty__p`/序列化指针/`shorthand`，`id__rcdo = (fun _ -> None)`，**不含**任意 SQL 写字段——移动端引用 `Aiarwa.Shared` 时不会间接持有 `Npgsql`/`System.Data.SqlClient`。

### 8.5 产物三：TS OrmTypes.d.ts

`vscode/src/lib/shared/OrmTypes.d.ts` 对应 `Ai_Prompt` 与 `Ai_Session`：

```typescript
declare global {

namespace aiarwa {

// [Ai_Prompt] (PROMPT)

export type pPROMPT = {
[key:string]: any
    User: number
    Description: string
    Fulltext: string
}

export type PROMPT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPROMPT
}

// [Ai_Session] (AIS)

export type pAIS = {
[key:string]: any
    Caption: string
    User: number
    Provider: number
    Scenario: number
    (* ... 其余省略 ... *)
}

export type AIS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pAIS
}

}}
```

要点：
- `declare global { namespace aiarwa { ... } }`（`dbName` 转小写作命名空间名）。
- 每个 `p*` 都带 `[key:string]: any` —— 即 [§6.7](#67-typescript-生成ormtypesdts--ormmorts--marshall) 提到的类型安全被削弱点。
- Rcd 包装 `PROMPT` 含 `id/createdat/updatedat/sort/p`，与 F# 的 `Rcd<pPROMPT>` 一一对应。

### 8.6 产物四：SQL 建表脚本

`sqlPostgreSQL.sql` 中 `ai_prompt` 表（PostgreSQL 方言，含增量迁移）：

```sql
-- [ai_prompt] ----------------------

DO $$
DECLARE
    condition boolean;
BEGIN
    condition := (SELECT EXISTS(
        SELECT 1 FROM information_schema.tables
        WHERE table_name = 'ai_prompt' AND table_schema = 'public'
    ));
    IF not condition THEN
        CREATE TABLE "ai_prompt" (
            id BIGINT NOT NULL
            ,createdat BIGINT NOT NULL
            ,updatedat BIGINT NOT NULL
            ,sort BIGINT NOT NULL
            ,"user" BIGINT
            ,"description" VARCHAR(200)
            ,"fulltext" TEXT
            ,CONSTRAINT "pk_ai_prompt" PRIMARY KEY (id)
        );
    END IF;
END $$;

-- PostgreSQL: Dropping obsolete fields -----------
DO $$
DECLARE
    fn TEXT;
BEGIN
    FOR fn IN
        SELECT column_name FROM information_schema.columns
        WHERE table_name = 'ai_prompt' AND table_schema = 'public'
          AND column_name <> ALL(ARRAY['id','createdat','updatedat','sort','user','description','fulltext'])
    LOOP
        EXECUTE format('ALTER TABLE %I DROP COLUMN %I', 'ai_prompt', fn);
    END LOOP;
END $$;
```

要点：
- 表名取 **shorthand 小写**（`PROMPT` → `ai_prompt`）。
- 4 个系统列固定 `BIGINT`（`createdat/updatedat` 以 Ticks 整数存储，故为 `BIGINT` 而非 `TIMESTAMP`）。
- `"description" VARCHAR(200)` 来自 Design 的 `Chars length=200`；`"fulltext" TEXT` 来自 `Text`。
- 自动生成「Dropping obsolete fields」**增量迁移块**：当某字段从 Design 删除后，重跑生成的 SQL 会自动 `ALTER TABLE DROP COLUMN` 清理废弃列——这是"设计即真相"的演进保障。

### 8.7 产物五：Types.fs 的 TypeManaged 区

`Aiarwa.Shared/Types.fs` 用 `//[TypeManaged]{ ... }` 包裹自定义（非 ORM）类型，被 `load` 解析进 `cTypes` 并参与生成：

```fsharp
module Aiarwa.Shared.Types

//[TypeManaged]{

type EuComplex = {
    eu: EU }                       // 引用 ORM 表 Ca_EndUser（EU）

type HostEnum =
    | Prod
    | Dev
    | Demo
    | Unknown

type RuntimeData = {
    mutable hostEnum: HostEnum
    flds: ModDictInt64<FLD>
    resvs: ModDictInt64<RESV>
    (* ... 其余 20+ 字段映射到各 ORM 表 ... *)
    spiders: ModDictInt64<SpiderComplex>
    mutable apiKeyGemini: string
    mutable secretFile: string }

//}
```

要点：
- `//[TypeManaged]` 内的类型（`EuComplex`/`HostEnum`/`RuntimeData` 等）会被解析为 `Structure`/`Enum`/`Sum`，生成进 `CustomMor.fs` + `CustomMor.ts` + `Types.d.ts`。
- 自定义类型可**引用 ORM 表**（如 `eu: EU`，`EU` 即 `Ca_EndUser` 的 `typeName`），形成「ORM 记录 ↔ 自定义结构体」的双向桥接。
- `modulenames` 提取自该区，供生成代码的 `open` 语句使用。

### 8.8 端到端串联与路径错位提醒

完整闭环：

```
Design-Ai.json  (Ai_Prompt, shorthand=PROMPT)
      │ load：shorthand→PROMPT, typeName=PROMPT, 字段解析
      ▼
CodeRobot.go 20
      ├─▶ OrmTypes.fs   type pPROMPT + PROMPT_fieldorders/fields/empty + 枚举 (§8.3)
      ├─▶ OrmMor.fs     跨平台子集：db__pPROMPT / PROMPT_metadata(MetadataTypes) (§8.4)
      ├─▶ OrmMor.Native.fs  native 全量：pPROMPT__sps / PROMPT_create / PROMPT_metadata(MetadataTypesNative) (§8.4)
      ├─▶ OrmTypes.d.ts export type pPROMPT / PROMPT (§8.5)
      └─▶ sqlPostgreSQL.sql  CREATE TABLE "ai_prompt" + 增量迁移 (§8.6)
Types.fs [TypeManaged]  ──▶  CustomMor.fs / CustomMor.ts (§8.7)
      ▼
Aiarwa 运行时：RuntimeData 内存优先加载全量 ORM 表，API 字典分发
```

> ✅ **路径已闭合**：target 20 的 `mainDir`/`JsDir` 现已写向 `C:\Dev\Aiarwa\...`（见 [§8.1](#81-项目背景与-target-20-配置)），与真实仓库 `c:/Dev/Aiarwa/...` 一致，可就地重生成 Aiarwa。但若要真正"设计即真相"闭环，仍需先修 [§9.1](#91-p0--阻断--安全) 的 **P0-① 明文密码** 与 **P0-④ 单 target** 问题（`target__config` 仅内联 20）。

---

## 9. 已知问题与风险

> 以下基于源码逐行核实。当前为 **ask 模式只读**，尚未实际修改或 `dotnet build` 验证。

### 9.1 P0 — 阻断 / 安全

1. **明文数据库密码写死源码**（`Program.fs:199`，**未解决**）：Aiarwa 连接串 `Password=e2TpqcaTEYLfkvFMkc` 直接进仓库 → 密钥泄露。`set.json` 的 `Game.Password` 同为明文（且 `set.json` 已移出编译图，见 §2）。
2. **`target__config` 混入非法副作用**（`Program.fs` 旧代码曾对硬编码 `E:\DEV\JCS\BizShared\Types.fs` 调 `TypeSys.FSharp.go`，**已修复**）：当前 `runMultiple` 仅 `Array.map target__config |> Array.iter(CodeRobot.go ...)`，不再调用 `FSharp.go`；空的 `FSharp.fs` 仍保留在编译图但无调用方。
3. **路径错配**（旧 `E:\DEV\Aiarwa\...`，**已修复**）：target 20 现已写向 `C:\Dev\Aiarwa\Shared` 与 `C:\Dev\Aiarwa\Vue\src\lib`（见 [§8.1](#81-项目背景与-target-20-配置)），与真实仓库一致，可就地重生成。
4. **`runMultiple` 只跑 target 20**（`Program.fs`，**未解决**）：`target__config` 仅内联 20，其余 target 为历史残留（见 §4.3），且 target 仍是魔术整数。

### 9.2 P1 — 正确性隐患

1. **`Boolean` 默认值为 `true`**（`CodeRobotI.fs` `fdef__empty`）：F#/TS 两侧布尔字段默认 `true`。多数 ORM 语义应为 `false`（如 `IsDeleted`），需复核是否设计意图。
2. **`TimeSeries` 被静默丢弃**：`fdef__tbin`/`fdef__bint`/`fdef__tjson`/`fdef__jsont` 四处均注释掉 `TimeSeries` 并落到 `_ -> ()/""`。后果：声明 `TimeSeries` 字段会生成 F# 记录 + ORM DML SQL（DB 有列），但 **bin/json 序列化完全缺失** → 运行时读写跳过该字段、数据丢失。应补齐或显式降级报错。
3. **`table__typeName` 引用已删除字段**（`Config.fs:49`，原 `Common.fs`）：`t.shorthand` 在 `Table` 记录中不存在（仅 Design 属性 + `CodeRobot.fs` 局部变量），且该函数**全仓库无调用点**。由于 `Config.fs` 现处于编译图中，若被调用会触发 FS0039 编译失败——需一次 `dotnet build` 核实（在 `Common.fs` 时期该风险因文件未编译而被掩盖）。
4. **大量非穷尽 `match`**：`db__p`/`p__sps`/`fdef__tbin`/`fdef__bint`/`fdef__tjson`/`fdef__jsont` 仅覆盖已知 `FieldDef` 分支，新增变体只给 warning 不报错（正是 TimeSeries 漏处理的根因）。

### 9.3 P2 — 工程 / 可维护性

- **`set.json` + `Loadcfg.fs` 是孤儿且已移出编译图**：`Program.fs` 配置内联硬编码，`Loadcfg.fs` 用 `System.Text.Json` 而非项目统一的 `Util.Json`；二者均不在 `fsproj` 中，改 `set.json` 无效。
- **`Common.fs` 残留死代码**：旧版 `RobotConfig`(`donmainName`)/`Src`/辅助函数，仅被同样未编译的 `FrontendPackVue.fs`/`Loadcfg.fs` 引用，应整体删除或合并进 `Config.fs` 以消歧义。
- **`FSharp.go` 空壳**（`FSharp.fs`）：`go` 解析 `Types.fs` 却返回 `()`，无任何产出；重构后已**无调用方**（§9.1-P0-② 已修复）。
- **`FrontendPackVue` 写盘被注释 + 已移出编译图**：`buildComponent` 末尾 `try_write_text` 三行注释掉 → 不落盘，且文件不在 `fsproj` 中。
- **`updateDatabase` 死代码 + 损坏路径**（`RDBMS.fs:340`）：内含把所有字段 `ALTER COLUMN ... NCHAR(64)` 的 SQL，与 `sqlField` 冲突，且无调用方。
- **`buildTableEnums` 大段注释 TS 枚举生成**（约 40 行），与 F# 侧不对称。
- **TS 类型安全被削弱**：`LangPackTypeScript.type__annotation` 大量返回 `any`；`OrmTypes.d.ts` 用 `declare global` + `[key:string]: any`。
- **拼写错误（已修复）**：`RobotConfig.donmainName` → `domainName`，修正于 `Config.fs`；`Common.fs` 旧副本仍保留 `donmainName`（未编译，无影响）。

---

## 10. 修复路线图

```
P0 阻断/安全
  ├─ 路径参数化 ✅（已实现：C:\Dev；可进一步泛化为 CLI/相对路径）
  ├─ 密码移出源码（环境变量 / 本地密钥文件，不进仓库）← 仍待做
  ├─ 删除 target__config 内 FSharp.go 副作用 ✅（已完成）
  └─ 用 set.json + Loadcfg 真正驱动多 target ← 仍待做（二者目前已移出编译图）
        │
        ▼
P1 正确性
  ├─ Boolean 默认 true → 复核是否应为 false
  ├─ TimeSeries → 补齐 S/D/bin/json 或解析层显式降级报错
  ├─ 修复 table__typeName shorthand 引用 + dotnet build 核实
  └─ match 加显式分支 / 编译期穷尽检查
        │
        ▼
P2 清理 + TS 类型安全
  ├─ 删 FSharp.go 空壳 / FrontendPackVue 注释写盘 / updateDatabase 废弃块
  ├─ buildTableEnums 注释块清理
  └─ OrmTypes.d.ts 用 export 替代 declare global；减少 any 返回
```

---

## 11. 结论与建议

- **最紧急**：P0-① 明文密码（尤其 `Program.fs:199` 的 Aiarwa 密码）应立刻从源码移走；P0-③ 路径错配、P0-② `FSharp.go` 副作用**已在重构中修复**（路径现为 `C:\Dev`、`domainName` 拼写修正、不再调用 `FSharp.go`）。
- **核心可用**：F# 后端 ORM 生成（记录 + bin/json + S/D + DML + SQL）主链路健全，是真正价值所在；Aiarwa 即其落地实例（331KB `OrmTypes.fs`、10 份 Design、4 套产物闭环）。主要风险在「入口编排/配置/安全」层与若干被注释的死路径。
- **建议第一步**：把 `Program.fs` 的密码抽离源码（环境变量或本地密钥文件），并把 `target__config` 改为真正参数化（支持多 target，消除魔术整数）；同时清理已移出编译图的 `Common.fs`/`FrontendPackVue.fs`/`Loadcfg.fs` 残留，统一到 `Config.fs`。

---

---

## 12. 跨平台双产物规范（完整 Server 引用 + 跨平台 MauiApp 引用）

> TypeSys 2026-08-16 验证通过：跨平台项目（典型如 Aiarwa）同一套 ORM 须同时被 **Server（完整 DB 读写）** 与 **MauiApp / 跨平台 client（零 DB 驱动）** 引用 → 生成器按 `om` / `omdb` 两份产物（实现见 §6.4 B2 双写）。本规范所有 JCS 项目统一遵守。

### 12.1 命名铁律（双产物）
| 产物 | 命名 | 内容 | 引用方 |
|------|------|------|--------|
| 跨平台子集 | `<ns>`（无后缀，如 `Aiarwa.Shared`）| `OrmMor.fs` 纯函数 + `MetadataTypes<pX>`（**不含** `sps`/`p_create`/`sql_update`/`rcd_update`/`table`）；零 DB 驱动依赖 | **MauiApp / 跨平台 client** |
| 完整 Server 引用 | `<ns>.Native`（如 `Aiarwa.Shared.Native`）| `OrmMor.Native.fs` + `MetadataTypesNative<pX>`（含全部 DB 写入：sps/create/update/table，引用 Npgsql）| **Server** |

- Design 根目录命名 `<ns>` 即可，CodeRobot 自动产出两份（§6.4 `om`/`omdb`）；无需手工维护第二份。
- **引用规则**：MauiApp/跨平台 client 只 `open`/引用 `<ns>`（子集）；Server 引用 `<ns>.Native`（全量）。
- 🔴 **红线**：跨平台端**禁止**引用 `Native` 后缀模块 —— 会经 `OrmMor.Native.fs` 间接持有 `Npgsql`/`System.Data.SqlClient`，破坏 MAUI 的 AOT / 单文件 / 移动端发布。

### 12.2 适用边界
- 含 Server + 跨平台 client 的 JCS 项目（Aiarwa：Server + MAUI）→ **必须**拆两份。
- 纯 Web SPA 项目（WYI：Clerk + Vue，无原生 client）→ 只需 `<ns>` 子集即可，但建议仍按此模板落地 `<ns>.Native`，未来若扩展桌面/移动 client 零改造成本。
- 改表结构统一走 `Design-*.json` → 重跑 `TypeSys/Program.fs`，两份同步重生成（铁律③）。

---

*文档更新于 2026-08-16，反映 TypeSys **重构后**架构（B2 Native 双写已验证成功）+ 跨平台双产物规范（完整 Server 引用 `<ns>.Native` / 跨平台 MauiApp 引用 `<ns>` 子集，所有 JCS 项目统一遵守）。基于对 `TypeSys` 编译图内全部 `.fs` 源码、目录残留文件及 `Aiarwa` 项目（Design-*.json 与生成产物）逐行核实。*
