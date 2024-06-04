import { BytesBuilder } from 'Bin.ts'
import { BinIndexed } from 'Bin.ts'
import { FactBroadcast } from 'Types.d.ts'

// [Stroke] Structure

export const Stroke__bin = (bb:BytesBuilder) => (v:Stroke) => {

    
    List__bin (((bb:BytesBuilder) => (v:any) => {
        
        let v0 = v.v0
        float32__bin (bb) (v0)
        let v1 = v.v1
        float32__bin (bb) (v1)})) (bb) (v.points)
    float32__bin (bb) (v.strokeSize)
    str__bin (bb) (v.color)
}

export const bin__Stroke:Stroke = (bi:BinIndexed) => {

    return {
        points: bin__List (((bi:BinIndexed) => {
                        let v0 = bin__float32(bi)
                        let v1 = bin__float32(bi)
                
                        return {v0:v0,v1:v1}})) (bi),
        strokeSize: bin__float32 (bi),
        color: bin__str (bi),
    }
}

// [ActionWhiteboard] Structure

export const ActionWhiteboard__bin = (bb:BytesBuilder) => (v:ActionWhiteboard) => {

    int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            Stroke__bin (bb) (v)
            break
        case 1:
            uint32__bin (bb) (v)
            break
        case 2:
            str__bin (bb) (v)
            break
    }
}

export const bin__ActionWhiteboard:ActionWhiteboard = (bi:BinIndexed) => {

    let v:ActionWhiteboard = {}
    v.e = bin__int32 (bi)
    switch (v.e) {
        case 2:
            v.val = bin__str (bi) 
            break
        case 1:
            v.val = bin__uint32 (bi) 
            break
        case 0:
            v.val = bin__Stroke (bi) 
            break
    }
    return v
}

// [FactWhiteboard] Structure

export const FactWhiteboard__bin = (bb:BytesBuilder) => (v:FactWhiteboard) => {

    ActionWhiteboard__bin (bb) (v.action)
    str__bin (bb) (v.actor)
    int64__bin (bb) (v.clientId)
    int64__bin (bb) (v.serverId)
    DateTime__bin (bb) (v.clientTimestamp)
    DateTime__bin (bb) (v.serverTimestamp)
}

export const bin__FactWhiteboard:FactWhiteboard = (bi:BinIndexed) => {

    return {
        action: bin__ActionWhiteboard (bi),
        actor: bin__str (bi),
        clientId: bin__int64 (bi),
        serverId: bin__int64 (bi),
        clientTimestamp: bin__DateTime (bi),
        serverTimestamp: bin__DateTime (bi),
    }
}

// [FactBroadcast] Structure

export const FactBroadcast__bin = (bb:BytesBuilder) => (v:FactBroadcast) => {

    int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            FactWhiteboard__bin (bb) (v)
            break
        case 1:
            break
    }
}

export const bin__FactBroadcast:FactBroadcast = (bi:BinIndexed) => {

    let v:FactBroadcast = {}
    v.e = bin__int32 (bi)
    switch (v.e) {
        case 1:
            break
        case 0:
            v.val = bin__FactWhiteboard (bi) 
            break
    }
    return v
}