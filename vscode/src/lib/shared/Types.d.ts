declare global {

namespace jcs {


// [EuComplex]
export type EuComplex = {
eu:number
}

// [ProjectComplex]
export type ProjectComplex = {
pages:{[key:number]: PAGE},

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
