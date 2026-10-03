// Runner.fsx — JCS TypeSys 生成入口（AI / 其它项目的唯一入口）
//
// 目的：其它应用直接调用本脚本触发 TypeSys 生成，不必各自复制一份生成脚本。
//
// 设计：只暴露 shortcut，直接转发到 TypeSys.CodeRobot.short —— 与应用本体
//       jCopilot/Runner.fsx 的 fullInit（TypeSys.CodeRobot.short output "jCopilot"）
//       是同一入口，不重复实现任何环节。
//
// 生成物（OrmTypes.fs / OrmMor.fs / CustomMor.fs / OrmMor.Native.fs / *.sql / *.ts）
// 禁止手工改动，改 <Code>.Shared/Design-*.json 后重跑即可。
//
// rdbms / 连接串由 CodeRobot.short 按 Code 自行决定：J7 / Game / JCS 走 SqlServer，
// 其余走 PostgreSQL（C:/Dev 下按 <Code>/<Code>.Shared 约定定位工程根）。
//
// 前置：dotnet build JCS/TypeSys/TypeSys.fsproj
// 用法：
//   dotnet fsi Runner.fsx --proj=J7
//   或 #load 本脚本后 shortcut "J7"

#I "c:/Dev/JCS/TypeSys/bin/Debug/net10.0"
#r "UtilCrossPlatform.dll"
#r "Util.dll"
#r "TypeSys.dll"

open System
open System.IO

open TypeSys.CodeRobot

let output (s: string) = Console.WriteLine s

let shortcut code = short output code

match fsi.CommandLineArgs |> Array.skip 1 with
| [| a |] when a.StartsWith "--proj=" -> shortcut (a.Substring 7) |> ignore
| _ -> output "Usage: dotnet fsi Runner.fsx --proj=<Code>"
