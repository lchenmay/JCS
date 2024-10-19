declare global {

namespace jcs {


// [EuComplex]
export type EuComplex = {
eu:number
}

// [ProjectComplex]
export type ProjectComplex = {
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

pcs:{[key:string]: ProjectComplex}
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
