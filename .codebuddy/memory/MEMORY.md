# JCS (J-Category Systems) - 持久记忆

> 核心理念: RPC as Functor — 范畴论驱动的跨平台类型共享全栈系统
> 论文: "RPC as a Functor: Cross-Platform Type Sharing" (Chen Siduo, 2024, DOI: 10.5281/zenodo.11398320)

---

## 一、项目架构总览

```
JCS (c:/Dev/JCS)
├── Common (外部依赖, c:/Dev/Common)
│   ├── Util/Util.fsproj       — 核心工具库 (Bin, Json, Db, Orm, Cat, Perf, Collection)
│   ├── UtilKestrel/           — Kestrel Web 服务器封装
│   ├── UtilBlazor/            — Blazor 工具
│   ├── UtilWebServer/         — Web 服务器工具
│   └── UtilZmq/               — ZeroMQ 封装
│
├── JCS.Shared (Library, net10.0)
│   └── 共享类型 + ORM 层 (16个实体, 由 TypeSys 自动生成)
│
├── TypeSys (Library, net10.0) — 离线代码生成工具
│   └── JSON 设计文件 → 自动生成 F#/TS ORM 代码
│
├── JCS.BizLogics (Library, net10.0) — 运行时业务逻辑
│   └── Session/Runtime/Db/Init/Branch/SSR/CodeRobot
│
├── WebLogics (Library, net8.0) — Web 前端逻辑
├── BizShared (Library) — 业务共享类型 (白板)
├── AioServer (Exe, net8.0) — All-In-One 服务器 (HTTP+WS, 端口 5045)
├── Server (Exe, net8.0) — 服务器启动入口
├── portal (Vue 3, 端口 5045) — 公开门户
├── vscode (Vue 3, 端口 5046) — 管理后台
└── VsCodeTemplate (Vue 3, 端口 5173) — 项目模板
```

---

## 二、运行时调用链

```
Server (Exe, net8.0)
  → JCS.BizLogics.Launcher.launch()
    → Init.init runtime (加载16个表)
    → Branch.branch (API路由)
  → Util.Runtime.halt

AioServer (Exe, net8.0)
  → ZmqWeb 实例 (HTTP + WebSocket + 静态文件)
  → branch 函数处理 public/admin API
  → wsHandler 处理 WebSocket 消息
```

**依赖关系**:
- Util.fsproj: 被 JCS.Shared, JCS.BizLogics, TypeSys, WebLogics, BizShared, AioServer, Server 引用
- UtilKestrel.fsproj: 被 JCS.BizLogics 引用
- UtilBlazor.fsproj: 被 WebLogics 引用
- JCS.Shared: 被 JCS.BizLogics, TypeSys 引用（所有业务模块的类型基础）

---

## 三、JCS.Shared — 类型系统

### 16个数据库实体
| 模块 | 表名 | 说明 |
|------|------|------|
| Ca | BOOK, EU, FILE | 内容/认证（用户AuthType: Normal/Authorized/Admin） |
| Social | FBIND, MOMENT | 文件绑定, 文章（Type有12种, MediaType: None/Video/Audio） |
| Sys | LOG, PLOG | 日志, 页面日志 |
| Ts | API, FIELD, HOSTCONFIG, PROJECT, TABLE, COMP, PAGE, TEMPLATE, VARTYPE | TypeSys 元数据 |

### 复合类型 (Types.fs)
- EuComplex, FBindComplex, MomentComplex, TableComplex
- CompComplex, PageComplex, ApiComplex, ProjectComplex
- Fact, RuntimeData, ClientRuntime
- Msg: Heartbeat | ApiRequest | ApiResponse | SingleFact | MultiFact
- Er: ApiNotExists | InvalideParameter | Unauthorized | NotAvailable | Internal

### 编译顺序
PreOrm.fs → OrmTypes.fs → Types.fs → OrmMor.fs → CustomMor.fs → Project.fs

---

## 四、JCS.BizLogics — 业务逻辑层

| 文件 | 功能 |
|------|------|
| Common.fs | Session={token,eu,key,isAdmin,json}, Sessions=Dict, HostData, Runtime, EchoCtx(X), WrapX |
| Db.fs | 数据库操作 (tryCU 泛型模式, dbLoggero 日志) |
| Init.fs | 启动初始化: 加载16表 → 创建默认JCS项目 → 注册Comp/Page Props和States |
| Branch.fs | API路由分支 (branching/branch, 多数路由已注释) |
| SSR.fs | 服务端渲染配置 |
| CodeRobot.fs | Vue代码生成 (VueFile, buildVueFile, buildComps, buildPages, runProject) |
| Launcher.fs | 启动入口 |

**编译顺序**: Common.fs → Db.fs → Init.fs → Branch.fs → SSR.fs → CodeRobot.fs → Launcher.fs

---

## 五、TypeSys — 代码生成工具

### 核心模块
| 文件 | 功能 |
|------|------|
| MetaType.fs | 类型AST (FieldDef 13种, TypeEnum 含 Primitive/Product/Sum/OrmRcd/Ary/List 等) |
| RDBMS.fs | SQL Server/PostgreSQL DDL生成 |
| CodeRobotI.fs | 字段到类型映射, S/D代码生成 |
| CodeRobotIIFs.fs | F# 递归S/D生成 (t__bin/bin__t/t__json/json__t/empty/clone) |
| CodeRobotIITs.fs | TypeScript 递归S/D生成 |
| LangPackTypeScript.fs | TS 类型注解 |
| CodeRobot.fs | 主生成器 (load→buildTypeCat→buildTables→buildCustomTypes→save) |
| Program.fs | 入口, 12项目配置 (J7/Game/GNexts/J/Studio/FA/JA/CTC/GCHAIN/Personal/GenVI/BizShared) |

### 编译顺序
FSharp.fs → MetaType.fs → Common.fs → RDBMS.fs → CodeRobotI.fs → LangPackTypeScript.fs → CodeRobotIIFs.fs → CodeRobotIITs.fs → FrontendPackVue.fs → CodeRobot.fs → Loadcfg.fs → Program.fs

### 代码生成流水线
```
Design-*.json → TypeSys(Program.fs) → CodeRobot
  → OrmTypes.fs (实体Record定义)
  → OrmMor.fs (ORM层: bin/json序列化, DB CRUD, CREATE TABLE)
  → CustomMor.fs (复合类型序列化)
  → Types.fs (高级复合类型)
  → sqlPostgreSQL.sql / sqlSQLServer.sql (迁移脚本)
  → CustomMor.ts / OrmMor.ts (TypeScript端)
  → Types.d.ts / OrmTypes.d.ts (TS类型声明)
```

### 命名约定 (T__U 双下划线格式)
- 扁平化存储: `p-Type` (如 pBOOK)
- 完整记录: `Rcd Type` (如 BOOK)
- 序列化: `pBOOK__bin`, `bin__pBOOK`, `BOOK__json`, `json__BOOKo`
- 工厂: `empty__BOOK`, `empty__ProjectComplex`
- 数据库: `db__pBOOK`, `pBOOK__sps`, `BOOK_update`, `BOOK_create`

---

## 六、前端架构 (Vue 3 + TypeScript)

### 通信方式 (双重)
1. **HTTP REST (JSON)**: fetch.ts 的 post/get, 后端动态拼接 URL
2. **WebSocket**: ws://localhost:5045, MsgEnum: Heartbeat=0, ApiRequest=1, ApiResponse=2, SingleFact=3, MultiFact=4

### 序列化体系 (三层)
- 二进制: Xxx__bin / bin__Xxx (BytesBuilder/BinIndexed)
- JSON: Xxx__json / json__Xxx
- 空值: Xxx_empty / empty__Xxx
- TS端 shared/ 目录由 TypeSys 从 F# 定义自动生成

### 状态管理
- Vue 3 reactive() + 全局变量 (globalThis.runtime, globalThis.host, globalThis.clientRuntime)
- 不使用 Vuex/Pinia
- runtime: session, user(EuComplex), router, data(RuntimeData), wsctx

### 认证
- Key 授权 + Discord OAuth

---

## 七、理论基础 — RPC as a Functor

### 范畴论映射
| 范畴概念 | 编程对应 |
|----------|----------|
| 对象 (Object) | 类型 (Type) |
| 态射 (Arrow/Morphism) | 纯函数 f: X → Y |
| 态射复合 (g ∘ f) | 函数管道 (g >> f) |
| 范畴 L | 编程语言的类型系统 |
| Functor F: L₁ → L₂ | RPC 代码生成器 |

### 核心定理
- **RPC Functor**: F(f(x)) = f'(F(x)) (交换图)
- **Type Sharing**: 序列化 s: T→W, 反序列化 d: W→T 互为逆映射, W 为共享类型 (byte[] 或 string)
- **自动 S/D**: 积类型 (Record) 和和类型 (Union) 可从子类型自动生成 S/D 函数

### 与 JCS 源码对应
| 论文概念 | JCS 实现 |
|----------|----------|
| Functor F | TypeSys 代码生成器 |
| 积类型 X×Y | F# Record |
| 和类型 X+Y | F# Discriminated Union |
| S/D 函数对 | pBOOK__bin / bin__BOOK 等 |
| 泛型 S/D | CodeRobotIIFs/CodeRobotIITs 递归生成 |
| 共享类型 W | byte[] (二进制) / JSON string |
| 范畴 L̂ | F# + TypeScript 跨语言类型系统 |

---

## 八、代码风格规范 (Common 代码库)

1. **T__U 命名**: 转换函数双下划线, 如 `json__str`, `str__bin`
2. **构造器**: `empty__Type()` 工厂模式
3. **管道化**: 大量 `|>` 串联
4. **计算表达式**: 自定义 CE (maybe/or_else/result)
5. **Railway Oriented**: CtxWrapper (Suc/Fail), `>>=`, `>=>`, `<||>` 运算符
6. **性能监控**: CodeWrapper/ConcurrencyWrapper/MemAlloc + IDisposable + use
7. **自包含**: 优先手写而非第三方库
8. **自定义集合**: ModDict 分片锁字典
9. **异步循环器**: asyncCycler 系列 (while true + Async.Start)
10. **泛型插件**: RuntimeTemplate 编译时安全
11. **极简主义**: 零注释、单字母变量、mutable、不忌讳null/全局状态
12. **类型推断优先**: 避免显式类型标注

---

---

## 九、审计日志体系

**全局日志位置**: `c:\Dev\.codebuddy\log\`（18个审计日志 + 6个分析文件 + 35个 memory-detail 归档）

**命名规范**: `YYYY-MM-DD_HHMM_<项目>_<任务简述>.md`

**关键教训（影响所有项目）**:
- **P0**: AI 越权操作（生产 SSH 前必须确认授权）、develop 白名单误匹配 hostname → 生产 502、PID 误杀 → IDE 崩溃
- **P1**: 端口硬编码泄露 16h、子 agent 虚假信息须二次验证、SSH passphrase 死锁
- **流程**: 用户视角定律（前端调试从浏览器开始）、部署验证管两头、Silent failure 反模式

**Memory 优化方向**: C 类迁移到 log/memory-detail/，目标节省 56% tokens

**Credits 成本**: Pro ¥58/2000C，加量包 ¥0.05/C，43,583 tokens≈3.78 Credits

---

## 十、跨项目关联

**项目清单** (健康度 2026-06-29):
- 🟢 WYI (whatsyourideal.com) | Aiarwa (kayhuaoil.com) | J7 (j7.ai) | CQT (jCQT.ai)
- 🟡 Game (jBet.us) | AIO
- 🔴 JCS（重构中，net8.0→net10.0 P0）

**公共基础设施**:
- Hetzner VPS (WYI+Aiarwa): Cloudflare Tunnel 穿透
- npm: @lchenmay/jcs-common（FileSysTree 等 Vue 组件）
- MCP: 微信公众号 v2.2.0 + 飞书
- 凭据唯一源: `c:\Dev\AI-archive\sys.md`

---

*最后更新: 2026-06-29*
