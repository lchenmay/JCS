declare global {

namespace jcs {


// [EuComplex]
export type EuComplex = {
eu:EU
}

// [FBindComplex]
export type FBindComplex = {
file:FILE,

fbind:FBIND
}

// [MomentComplex]
export type MomentComplex = {
fbxs:FBindComplex[],

m:MOMENT
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

tablexs:{[key:string]: TableComplex},

compxs:{[key:string]: CompComplex},

templatexs:{[key:string]: TEMPLATE},

pagexs:{[key:string]: PageComplex},

apixs:{[key:string]: ApiComplex},

project:PROJECT
}

// [Fact]
export type Fact = {
e: number,

val: any
}

// [RuntimeData]
export type RuntimeData = {
facts:Fact[],

projectxs:{[key:number]: ProjectComplex},

files:{[key:number]: FILE},

mxs:{[key:number]: MomentComplex},

books:BOOK[]
}

// [ClientRuntime]
export type ClientRuntime = {
version:number
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
