// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
import * as binOrm from './OrmMor'
const marshall = {...binCommon, ...binOrm }

export const enum FactEnum {
    Undefined = 0,//Undefined
}

export const enum MsgEnum {
    Heartbeat = 0,//Heartbeat
    ApiRequest = 1,//ApiRequest
    ApiResponse = 2,//ApiResponse
    SingleFact = 3,//SingleFact
    MultiFact = 4,//MultiFact
}

export const enum ErEnum {
    ApiNotExists = 0,//ApiNotExists
    InvalideParameter = 1,//InvalideParameter
    Unauthorized = 2,//Unauthorized
    NotAvailable = 3,//NotAvailable
    Internal = 4,//Internal
}


// [EuComplex] Structure

export const EuComplex_empty = (): jcs.EuComplex => { 
    return {
        eu: 0,
    } as jcs.EuComplex
}

export const EuComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int64__bin (bb) (v.eu)
}

export const bin__EuComplex = (bi:BinIndexed):jcs.EuComplex => {

    return {
        eu: marshall.bin__int64 (bi),
    }
}

// [Fact] Structure

export const Fact_empty = (): jcs.Fact => { 
    return {
    e:0, val:{}
    } as jcs.Fact
}

export const Fact__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
    }
}

export const bin__Fact = (bi:BinIndexed):jcs.Fact => {

    let v:jcs.Fact = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 0:
            break
    }
    return v
}

// [RuntimeData] Structure

export const RuntimeData_empty = (): jcs.RuntimeData => { 
    return {
        facts: [],
    } as jcs.RuntimeData
}

export const RuntimeData__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (Fact__bin) (bb) (v.facts)
}

export const bin__RuntimeData = (bi:BinIndexed):jcs.RuntimeData => {

    return {
        facts: marshall.bin__array (bin__Fact) (bi),
    }
}

// [Msg] Structure

export const Msg_empty = (): jcs.Msg => { 
    return {
    e:0, val:{}
    } as jcs.Msg
}

export const Msg__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
        case 1:
            marshall.Json__bin (bb) (v.val)
            break
        case 2:
            marshall.Json__bin (bb) (v.val)
            break
        case 3:
            Fact__bin (bb) (v.val)
            break
        case 4:
            
            marshall.array__bin (Fact__bin) (bb) (v.val)
            break
    }
}

export const bin__Msg = (bi:BinIndexed):jcs.Msg => {

    let v:jcs.Msg = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 4:
            v.val = marshall.bin__array (bin__Fact) (bi) 
            break
        case 3:
            v.val = bin__Fact (bi) 
            break
        case 2:
            v.val = marshall.bin__Json (bi) 
            break
        case 1:
            v.val = marshall.bin__Json (bi) 
            break
        case 0:
            break
    }
    return v
}

// [Er] Structure

export const Er_empty = (): jcs.Er => { 
    return {
    e:0, val:{}
    } as jcs.Er
}

export const Er__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
        case 1:
            break
        case 2:
            break
        case 3:
            break
        case 4:
            break
    }
}

export const bin__Er = (bi:BinIndexed):jcs.Er => {

    let v:jcs.Er = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 4:
            break
        case 3:
            break
        case 2:
            break
        case 1:
            break
        case 0:
            break
    }
    return v
}