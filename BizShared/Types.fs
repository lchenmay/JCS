module BizShared.Types

open System

//[TypeManaged]{

type User = {
caption: string
id: int64 }

type DirectMsg = User * string

type Notification = 
| DirectMsg of DirectMsg
| SysMsg of string
| Ping

type Ball = {
mutable color: string
r: float
mutable hit: bool
mutable x: float
mutable y: float 
mutable vx: float
mutable vy: float }

type Field = {
mutable width: float
mutable height: float 
mutable mouse: (float * float) option
balls: Ball[]
mutable lastRender: DateTime }

//}
    

let creatRobot() = {
    caption = "Robot"
    id = DateTime.UtcNow.Ticks }