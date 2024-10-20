declare global {

namespace jcs {


// [EuComplex]
export type EuComplex = {
eu:number
}

// [TableComplex]
export type TableComplex = {
fields:{[key:string]: FIELD},

table:TABLE
}

// [CompComplex]
export type CompComplex = {
states:{[key:string]: VARTYPE},

props:{[key:string]: VARTYPE},

comp:COMP
}

// [PageComplex]
export type PageComplex = {
states:{[key:string]: VARTYPE},

props:{[key:string]: VARTYPE},

page:PAGE
}

// [ProjectComplex]
export type ProjectComplex = {
hostconfigs:{[key:string]: HOSTCONFIG},

tables:{[key:string]: TableComplex},

comps:{[key:number]: CompComplex},

templates:{[key:number]: TEMPLATE},

pages:{[key:number]: PageComplex},

project:PROJECT
}

// [Fact]
export type Fact = {
e: number,

val: any
}

// [RuntimeData]
export type RuntimeData = {
facts:Array<Fact>,

pcs:{[key:number]: ProjectComplex}
}

// [Msg]
export type Msg = {
e: number,

val: any
}

// [Er]
export type Er = {
e: number,

val: any
}

}

}

export {}
