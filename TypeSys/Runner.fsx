// Runner.fsx — JCS TypeSys 生成入口（AI / 其它项目的唯一入口）
//
// 目的：其它应用直接调用本脚本触发 TypeSys 生成，不必各自复制一份生成脚本。
//
// 设计：只暴露 shortcut —— 全部落地逻辑（暂存隔离 → 生成 → Native 归位 → 行尾规范化
//       → 额外 open 注入 → 写回）委托 TypeSys.Deploy（TypeSys.dll，与各项目
//       Run-TypeSys-*.fsx 是同一实现），本脚本只负责按 Code 推导 Profile 并调用，
//       不重复实现任何环节。
//
// 生成物（OrmTypes.fs / OrmMor.fs / CustomMor.fs / OrmMor.Native.fs / *.sql / *.ts）
// 禁止手工改动，改 <Code>.Shared/Design-*.json 后重跑即可。
//
// 前置：dotnet build JCS/TypeSys/TypeSys.fsproj
// 用法：
//   dotnet fsi Runner.fsx --proj=WYI
//   或 #load 本脚本后 shortcut "WYI"
//
// ⚠️ 适用边界：Profile 按 CodeRobot.short 的 PG 约定推导，故仅适用 PostgreSQL 项目
//    （WYI / Aiarwa / jCopilot / AIO…）。SQL Server 项目（J7 / Game / JCS）
//    仍需 Program.fs 的 target__config。

#I "c:/Dev/JCS/TypeSys/bin/Debug/net10.0"
#r "UtilCrossPlatform.dll"
#r "Util.dll"
#r "TypeSys.dll"

open System
open System.IO

open TypeSys.Deploy

let output (s: string) = Console.WriteLine s

let typeSysDir = Path.GetFullPath __SOURCE_DIRECTORY__
let exeDir = Path.Combine(typeSysDir, "bin", "Debug", "net10.0")
let devRoot = Path.GetDirectoryName (Path.GetDirectoryName typeSysDir)

let shortcut code = run output exeDir (profile__project devRoot code) Write

match fsi.CommandLineArgs |> Array.skip 1 with
| [| a |] when a.StartsWith "--proj=" -> shortcut (a.Substring 7) |> ignore
| _ -> output "Usage: dotnet fsi Runner.fsx --proj=<Code>"
