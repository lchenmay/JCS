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
        eu: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pEU_empty() },
    } as jcs.EuComplex
}

export const EuComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.EU__bin (bb) (v.eu)
}

export const bin__EuComplex = (bi:BinIndexed):jcs.EuComplex => {

    return {
        eu: marshall.bin__EU (bi),
    }
}

// [FBindComplex] Structure

export const FBindComplex_empty = (): jcs.FBindComplex => { 
    return {
        file: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pFILE_empty() },
        fbind: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pFBIND_empty() },
    } as jcs.FBindComplex
}

export const FBindComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.FILE__bin (bb) (v.file)
    marshall.FBIND__bin (bb) (v.fbind)
}

export const bin__FBindComplex = (bi:BinIndexed):jcs.FBindComplex => {

    return {
        file: marshall.bin__FILE (bi),
        fbind: marshall.bin__FBIND (bi),
    }
}

// [MomentComplex] Structure

export const MomentComplex_empty = (): jcs.MomentComplex => { 
    return {
        fbxs: [],
        m: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pMOMENT_empty() },
    } as jcs.MomentComplex
}

export const MomentComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (FBindComplex__bin) (bb) (v.fbxs)
    marshall.MOMENT__bin (bb) (v.m)
}

export const bin__MomentComplex = (bi:BinIndexed):jcs.MomentComplex => {

    return {
        fbxs: marshall.bin__array (bin__FBindComplex) (bi),
        m: marshall.bin__MOMENT (bi),
    }
}

// [TableComplex] Structure

export const TableComplex_empty = (): jcs.TableComplex => { 
    return {
        fields: {},
        table: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pTABLE_empty() },
    } as jcs.TableComplex
}

export const TableComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.dict__bin (marshall.str__bin)(marshall.FIELD__bin) (bb) (v.fields)
    marshall.TABLE__bin (bb) (v.table)
}

export const bin__TableComplex = (bi:BinIndexed):jcs.TableComplex => {

    return {
        fields: marshall.bin__dict(marshall.bin__str)(marshall.bin__FIELD) (bi),
        table: marshall.bin__TABLE (bi),
    }
}

// [CompComplex] Structure

export const CompComplex_empty = (): jcs.CompComplex => { 
    return {
        states: {},
        props: {},
        comp: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pCOMP_empty() },
    } as jcs.CompComplex
}

export const CompComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.states)
    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.props)
    marshall.COMP__bin (bb) (v.comp)
}

export const bin__CompComplex = (bi:BinIndexed):jcs.CompComplex => {

    return {
        states: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        props: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        comp: marshall.bin__COMP (bi),
    }
}

// [PageComplex] Structure

export const PageComplex_empty = (): jcs.PageComplex => { 
    return {
        states: {},
        props: {},
        page: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pPAGE_empty() },
    } as jcs.PageComplex
}

export const PageComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.states)
    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.props)
    marshall.PAGE__bin (bb) (v.page)
}

export const bin__PageComplex = (bi:BinIndexed):jcs.PageComplex => {

    return {
        states: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        props: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        page: marshall.bin__PAGE (bi),
    }
}

// [ApiComplex] Structure

export const ApiComplex_empty = (): jcs.ApiComplex => { 
    return {
        reqs: {},
        reps: {},
        api: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pAPI_empty() },
    } as jcs.ApiComplex
}

export const ApiComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.reqs)
    
    marshall.dict__bin (marshall.str__bin)(marshall.VARTYPE__bin) (bb) (v.reps)
    marshall.API__bin (bb) (v.api)
}

export const bin__ApiComplex = (bi:BinIndexed):jcs.ApiComplex => {

    return {
        reqs: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        reps: marshall.bin__dict(marshall.bin__str)(marshall.bin__VARTYPE) (bi),
        api: marshall.bin__API (bi),
    }
}

// [ProjectComplex] Structure

export const ProjectComplex_empty = (): jcs.ProjectComplex => { 
    return {
        hostconfigs: {},
        tablexs: {},
        compxs: {},
        templatexs: {},
        pagexs: {},
        apixs: {},
        project: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pPROJECT_empty() },
    } as jcs.ProjectComplex
}

export const ProjectComplex__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.dict__bin (marshall.str__bin)(marshall.HOSTCONFIG__bin) (bb) (v.hostconfigs)
    
    marshall.dict__bin (marshall.str__bin)(TableComplex__bin) (bb) (v.tablexs)
    
    marshall.dict__bin (marshall.str__bin)(CompComplex__bin) (bb) (v.compxs)
    
    marshall.dict__bin (marshall.str__bin)(marshall.TEMPLATE__bin) (bb) (v.templatexs)
    
    marshall.dict__bin (marshall.str__bin)(PageComplex__bin) (bb) (v.pagexs)
    
    marshall.dict__bin (marshall.str__bin)(ApiComplex__bin) (bb) (v.apixs)
    marshall.PROJECT__bin (bb) (v.project)
}

export const bin__ProjectComplex = (bi:BinIndexed):jcs.ProjectComplex => {

    return {
        hostconfigs: marshall.bin__dict(marshall.bin__str)(marshall.bin__HOSTCONFIG) (bi),
        tablexs: marshall.bin__dict(marshall.bin__str)(bin__TableComplex) (bi),
        compxs: marshall.bin__dict(marshall.bin__str)(bin__CompComplex) (bi),
        templatexs: marshall.bin__dict(marshall.bin__str)(marshall.bin__TEMPLATE) (bi),
        pagexs: marshall.bin__dict(marshall.bin__str)(bin__PageComplex) (bi),
        apixs: marshall.bin__dict(marshall.bin__str)(bin__ApiComplex) (bi),
        project: marshall.bin__PROJECT (bi),
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
        projectxs: {},
        files: {},
        mxs: {},
        books: [],
    } as jcs.RuntimeData
}

export const RuntimeData__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (Fact__bin) (bb) (v.facts)
    
    marshall.dict__bin (marshall.int64__bin)(ProjectComplex__bin) (bb) (v.projectxs)
    
    marshall.dict__bin (marshall.int64__bin)(marshall.FILE__bin) (bb) (v.files)
    
    marshall.dict__bin (marshall.int64__bin)(MomentComplex__bin) (bb) (v.mxs)
    
    marshall.array__bin (marshall.BOOK__bin) (bb) (v.books)
}

export const bin__RuntimeData = (bi:BinIndexed):jcs.RuntimeData => {

    return {
        facts: marshall.bin__array (bin__Fact) (bi),
        projectxs: marshall.bin__dict(marshall.bin__int64)(bin__ProjectComplex) (bi),
        files: marshall.bin__dict(marshall.bin__int64)(marshall.bin__FILE) (bi),
        mxs: marshall.bin__dict(marshall.bin__int64)(bin__MomentComplex) (bi),
        books: marshall.bin__array (marshall.bin__BOOK) (bi),
    }
}

// [ClientRuntime] Structure

export const ClientRuntime_empty = (): jcs.ClientRuntime => { 
    return {
        version: 0,
    } as jcs.ClientRuntime
}

export const ClientRuntime__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.version)
}

export const bin__ClientRuntime = (bi:BinIndexed):jcs.ClientRuntime => {

    return {
        version: marshall.bin__int32 (bi),
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