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

// [ApiComplex]
export type ApiComplex = {
reqs:{[key:string]: VARTYPE},

reps:{[key:string]: VARTYPE},

api:API
}

// [ProjectComplex]
export type ProjectComplex = {
hostconfigs:{[key:string]: HOSTCONFIG},

tables:{[key:string]: TableComplex},

comps:{[key:string]: CompComplex},

templates:{[key:string]: TEMPLATE},

pages:{[key:string]: PageComplex},

apis:{[key:string]: ApiComplex},

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
