
// [Stroke] Structure

export const Stroke__bin = (bb:BytesBuilder) => (v:Stroke) {

    
    List__bin (((bb:BytesBuilder) => (v:any) => {
        
        let v0 = v.v0
        float32__bin (bb) (v0)
        let v1 = v.v1
        float32__bin (bb) (v1)})) (bb) (v.points)
    float32__bin (bb) (v.strokeSize)
    uint32__bin (bb) (v.color)
}

export const bin__Stroke:Stroke = (bi:BinIndexed) => {

    return {
        points: bin__List (((bi:BinIndexed) => {
                        let v0 = bin__float32(bi)
                        let v1 = bin__float32(bi)
                
                        return {v0:v0,v1:v1}})) (bi),
        strokeSize: bin__float32 (bi),
        color: bin__uint32 (bi),
    }
}

// [ActionWhiteboard] Structure

export const ActionWhiteboard__bin = (bb:BytesBuilder) => (v:ActionWhiteboard) {

    match v with
    | ActionWhiteboard.Stroke v ->
        int32__bin (bb) (0)
        Stroke__bin (bb) (v)
    | ActionWhiteboard.Clear v ->
        int32__bin (bb) (1)
        uint32__bin (bb) (v)
    | ActionWhiteboard.Msg v ->
        int32__bin (bb) (2)
        str__bin (bb) (v)
}

export const bin__ActionWhiteboard:ActionWhiteboard = (bi:BinIndexed) => {

    match bin__int32 bi with
    | 2 -> bin__str bi |> ActionWhiteboard.Msg
    | 1 -> bin__uint32 bi |> ActionWhiteboard.Clear
    | _ -> bin__Stroke bi |> ActionWhiteboard.Stroke
}