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


//}
    
