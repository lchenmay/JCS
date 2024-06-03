module BizShared.Types

open System
open System.Collections.Generic

//[TypeManaged]{

type Stroke = {
points: List<float32 * float32>
strokeSize: float32
color: uint32 }

type ActionWhiteboard =
| Stroke of Stroke
| Clear of uint32
| Msg of string

type FactWhiteboard = {
action: ActionWhiteboard
actor: string
clientId: int64
mutable serverId: int64
clientTimestamp: DateTime
mutable serverTimestamp: DateTime }

type FactBroadcast =
| Whiteboard of FactWhiteboard
| Undefined


//}
    
