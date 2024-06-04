
export type BinIndexed = {
    bin: ArrayBuffer,
    index: number
}

export class BytesBuilder {

    count: number
    buffer: ArrayBuffer[]

    constructor() {
        this.buffer = []
        this.count = 0
    }

    append(bin: ArrayBuffer) {
        this.buffer.push(bin)
        this.count += bin.byteLength
    }

    bytes() {
        let res = new ArrayBuffer(this.count)
        let i = 0
        this.buffer.forEach((bin: ArrayBuffer) => {
            new Uint8Array(res).set(new Uint8Array(bin), i)
            i += bin.byteLength
        });
        return res
    }
}

// Primitive types

export const int32__binL = (bb: BytesBuilder) => (v: number) => {
    const bin = new Int32Array([v]).buffer
    bb.append(bin)
}
export const int32__bin = (bb: BytesBuilder) => (v: number) => {
    const buffer = new ArrayBuffer(4)
    const view = new DataView(buffer)
    view.setInt32(0, v, false)
    bb.append(buffer)
}

export const bin__int32 = (bi: BinIndexed, littleEndian = true) => {
    const v = new DataView(bi.bin)
    return v.getInt32(bi.index, littleEndian)
}

export const int64__bin = (bb: BytesBuilder) => (v: bigint) => {
    const buffer = new ArrayBuffer(8)
    const view = new DataView(buffer)
    view.setBigInt64(0, v, false)
    bb.append(buffer)
}

export const bin__int64 = (bi: BinIndexed, littleEndian = true) => {
    const v = new DataView(bi.bin)
    return Number(v.getBigInt64(bi.index, littleEndian))
}

export const float__bin = (bb: BytesBuilder) => (v: number) => {
    const bin = new Float64Array([v]).buffer
    bb.append(bin)
}

export const bin__float = (bi: BinIndexed) => {
    const v = new DataView(bi.bin)
    return v.getFloat64(bi.index)
}

export const bool__bin = (bb: BytesBuilder) => (v: boolean) => {
    const bin = new Uint8Array([v ? 1 : 0]).buffer
    bb.append(bin)
}

export const bin__bool = (bi: BinIndexed) => {
    const v = new DataView(bi.bin)
    return v.getUint8(bi.index) === 1
}

export const str__bin = (bb: BytesBuilder) => (str: string) => {
    const encoder = new TextEncoder();
    const encoded = encoder.encode(str);
    int32__bin(bb)(encoded.buffer.byteLength)
    bb.append(encoded.buffer);
}

export const bin__str = (bi: BinIndexed) => {
    const length = bin__int32(bi)
    const decoder = new TextDecoder('utf-8');
    const bytes = new Uint8Array(bi.bin, bi.index + 4, length);
    bi.index = bi.index + length + 4
    return decoder.decode(bytes);
}

// Generic types

export const array__bin = <T>(item__bin: Function) => (bb: BytesBuilder) => (array: Array<T>) => {
    int32__bin(bb)(array.length)
    array.forEach((i: T) => {
        item__bin(bb)(i)
    })
}

export const bin__array = <T>(bin__item: Function) => (bi: BinIndexed) => {
    let res = []
    if (bi.bin.byteLength >= 4) {
        let length = bin__int32(bi)
        for (let i = 0; i < length; i++) {
            let item = bin__item(bi)
            res.push(item)
        }
    }
    return res;
}

export const List__bin = <T>(item__bin: Function) => (bb: BytesBuilder) => (array: Array<T>) => {
    int32__bin(bb)(array.length)
    array.forEach((i: T) => {
        item__bin(bb)(i)
    })
}

export const bin__List = <T>(bin__item: Function) => (bi: BinIndexed) => {
    let res = []
    if (bi.bin.byteLength >= 4) {
        let length = bin__int32(bi)
        for (let i = 0; i < length; i++) {
            let item = bin__item(bi)
            res.push(item)
        }
    }
    return res;
}

export const bin__dict = (bi: BinIndexed): { [key: string]: any } => {
    const decoder = new TextDecoder('utf-8');
    const bytes = new Uint8Array(bi.bin, bi.index);
    const jsonString = decoder.decode(bytes);
    return JSON.parse(jsonString);
}

export const dict__bin = (bb: BytesBuilder) => (dict: { [key: string]: any }) => {
    const jsonString = JSON.stringify(dict);
    const encoder = new TextEncoder();
    const encoded = encoder.encode(jsonString);
    bb.append(encoded.buffer);
}

export const arrayBuffer__Hex = (buffer: ArrayBuffer): string => {
    const view = new DataView(buffer)
    let hexString = ''
    for (let i = 0; i < view.byteLength; i++) {
        const byte = view.getUint8(i)
        const hex = byte.toString(16).padStart(2, '0')
        hexString += hex;
    }
    return hexString.toUpperCase()
}
