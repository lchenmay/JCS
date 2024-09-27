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

    base64() {
        return ArrayBuffer__base64(this.bytes());
    }
}

export const Mors__BytesBuilder = <T>(fn: Function) => (data: T) => {
    const bb = new BytesBuilder()
    fn(bb)(data)
    return bb
}
export const ArrayBuffer__base64 = (buffer: ArrayBuffer) => {

    let bytes = new Uint8Array(buffer);
    let bstr = '';

    for (let i = 0; i < bytes.byteLength; i++) {
        bstr += String.fromCharCode(bytes[i]);
    }

    return btoa(bstr);
}

export const data__base64 = <T>(data__bin: Function) => (data: T) => {
    return Mors__BytesBuilder<T>(data__bin)(data).base64()
}

export const ArrayBuffer__hex = (buffer: ArrayBuffer) => {
    const uint8Array = new Uint8Array(buffer);
    let hexString = '';

    for (let i = 0; i < uint8Array.length; i++) {
        const hex = uint8Array[i].toString(16).padStart(2, '0');
        hexString += hex + ' ';
    }

    const hex = hexString.trim()
    return hex
}

export const string__ArrayBuffer = (str: string): ArrayBuffer => {
    const stringLength = str.length
    const buffer = new ArrayBuffer(stringLength * 2)
    const bufferView = new Uint16Array(buffer)
    for (let i = 0; i < stringLength; i++) {
        bufferView[i] = str.charCodeAt(i)
    }
    return buffer
}


// Primitive types

export const int32__binL = (bb: BytesBuilder) => (v: number) => {
    const bin = new Int32Array([v]).buffer
    bb.append(bin)
}
export const int32__bin = (bb: BytesBuilder) => (v: number) => {
    const buffer = new ArrayBuffer(4)
    const view = new DataView(buffer)
    view.setInt32(0, v, true)
    bb.append(buffer)
}

export const bin__int32 = (bi: BinIndexed, littleEndian = true) => {
    const v = new DataView(bi.bin)
    const res = v.getInt32(bi.index, littleEndian)
    bi.index += 4
    return res
}

export const int64__bin = (bb: BytesBuilder) => (v: number | bigint) => {
    if (typeof v == "number") { v = BigInt(v) }
    const buffer = new ArrayBuffer(8)
    const view = new DataView(buffer)
    view.setBigInt64(0, v, true)
    bb.append(buffer)
}

export const bin__int64 = (bi: BinIndexed, littleEndian = true) => {
    const v = new DataView(bi.bin)
    const res = v.getBigInt64(bi.index, littleEndian)
    bi.index += 8
    return Number(res)
}


export const DateTime__bin = (bb: BytesBuilder) => (v: Date | number) => {
    const checkDate = (v: any) => {
        if (typeof v == "number") return v
        else {
            try { return v.getTime() }
            catch { console.log(`something wrong with ${v}, return current Date.`); return new Date() }
        }
    }
    console.log("v=" + v)

    const timestamp = BigInt(checkDate(v)); // 将 Date 对象转换为时间戳（毫秒）
    const buffer = new ArrayBuffer(8);
    const view = new DataView(buffer);
    view.setBigInt64(0, timestamp, true); // 将时间戳作为 BigInt64 存储
    bb.append(buffer);
}

export const bin__DateTime = (bi: BinIndexed, littleEndian = true): Date => {
    const view = new DataView(bi.bin);
    const timestamp = view.getBigInt64(bi.index, littleEndian); // 从 DataView 中获取 BigInt64
    const res = new Date(Number(timestamp)); // 将 BigInt 转换为 Number 并创建 Date 对象
    bi.index += 8
    return res
}

export const float__bin = (bb: BytesBuilder) => (v: number) => {
    const bin = new Float64Array([v]).buffer
    bb.append(bin)
}

export const bin__float = (bi: BinIndexed) => {
    const v = new DataView(bi.bin)
    const res = v.getFloat64(bi.index)
    bi.index += 8
    return res
}

export const bool__bin = (bb: BytesBuilder) => (v: boolean) => {
    const bin = new Uint8Array([v ? 1 : 0]).buffer
    bb.append(bin)
}

export const bin__bool = (bi: BinIndexed) => {
    const v = new DataView(bi.bin)
    const res = v.getUint8(bi.index) === 1
    bi.index += 1
    return res
}

export const str__bin = (bb: BytesBuilder) => (str: string) => {
    const encoder = new TextEncoder();
    const encoded = encoder.encode(str)
    const length = encoded.buffer.byteLength
    int32__bin(bb)(length)
    bb.append(encoded.buffer)
}

export const bin__str = (bi: BinIndexed) => {
    const length = Number(bin__int32(bi))
    const decoder = new TextDecoder('utf-8');
    const bytes = new Uint8Array(bi.bin, bi.index + 4, length);
    const res = decoder.decode(bytes);
    bi.index += length + 4
    return res
}

export const bin__Json = (bi: BinIndexed) => {
    return JSON.stringify(bin__str(bi))
}
export const Json__bin = (bb: BytesBuilder) => (obj: Object) => {
    str__bin(bb)(JSON.stringify(obj))
}

// Generic types

export const option__bin = <T>(item__bin: Function) => (bb: BytesBuilder) => (v: T) => {
    if(v){
        bool__bin(bb)(true)
        item__bin(bb)(v)
    }
    else
        bool__bin(bb)(false)
}

// This is a 2-order function with 2 arrows in the signature
export const bin__option = <T>(bin__item: Function) => (bi: BinIndexed) => {
    if(bin__bool(bi))
        return bin__item(bi)
    else
        return null
}

export const array__bin = <T>(item__bin: Function) => (bb: BytesBuilder) => (array: Array<T>) => {
    int32__bin(bb)(array.length)
    array.forEach((i: T) => {
        item__bin(bb)(i)
    })
}

// This is a 2-order function with 2 arrows in the signature
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

// Provided with specific typed marshal functions, reducing 1 order
export const bin__int32array = bin__array(bin__int32)
export const bin__int64array = bin__array(bin__int64)
export const bin__boolarray = bin__array(bin__bool)


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

export type Kvp = {
    k: string,
    v: any
}

export const dict__kvpArray = (dict: Record<string, any>): Kvp[] => {
    return Object.entries(dict).map(([k, v]) => ({ k, v }));
}

export const dict__bin = (k__bin: Function) => (v__bin: Function) => (bb: BytesBuilder) => (dict: any) => {

    // Add:   const array = dict -> array
    const array = dict__kvpArray(dict)

    array__bin(() => {
        k__bin(bb)
        v__bin(bb)
    })(bb)(array)
}

export const bin__dict = (bin__k: Function) => (bin__v: Function) => (bi: BinIndexed) => {

    // build an empty dict
    const dict: Record<string, any> = {}

    if (bi.bin.byteLength >= 4) {
        let length = bin__int32(bi)
        for (let i = 0; i < length; i++) {
            const k = bin__k(bi) as string
            const v = bin__v(bi) as any
            const kvp: Kvp = {
                k: k,
                v: v
            }
            dict[k] = v // with key
        }
    }
    return dict;
}


/*
export const bin__dict = (bi: BinIndexed): { [key: string]: any } => {
    const decoder = new TextDecoder('utf-8');
    const bytes = new Uint8Array(bi.bin, bi.index);
    const jsonString = decoder.decode(bytes); // 将二进制数据解码为 JSON 字符串
    return JSON.parse(jsonString); // 将 JSON 字符串转换为字典类型
}

export const dict__bin = (bb: BytesBuilder) => (dict: { [key: string]: any }) => {
    const jsonString = JSON.stringify(dict); // 将字典类型转换为 JSON 字符串
    const encoder = new TextEncoder();
    const encoded = encoder.encode(jsonString); // 将 JSON 字符串编码为 Uint8Array
    bb.append(encoded.buffer); // 将 Uint8Array 转换为 ArrayBuffer 并添加到 BytesBuilder 中
}
*/

// Tool

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

export function dict__KeyArray<V>(dict: { [key: string]: V }): string[] {
    return Object.entries(dict).map(([k, v]) => k)
}

export function dict__ValArray<V>(dict: { [key: string]: V }): V[] {
    return Object.entries(dict).map(([k, v]) => v)
}


// [Stat]
export type Stat = {
    mean: number,

    middle: number,

    var: number,

    median: number,

    ma: number,

    mb: number,

    mc: number,

    md: number,

    min: number,

    max: number,

    count: number
}

// [SpotInStat]
export type SpotInStat = {
    deviation: number,

    spot: number,

    anchor: number,

    digit: number,

    unit: string,

    stat: Stat
}


// [Stat] Structure

export const Stat_empty = (): Stat => {
    return {
        mean: 0.0,
        middle: 0.0,
        var: 0.0,
        median: 0.0,
        ma: 0.0,
        mb: 0.0,
        mc: 0.0,
        md: 0.0,
        min: 0.0,
        max: 0.0,
        count: 0,
    } as Stat
}

export const Stat__bin = (bb: BytesBuilder) => (v: any) => {

    float__bin(bb)(v.mean)
    float__bin(bb)(v.middle)
    float__bin(bb)(v.var)
    float__bin(bb)(v.median)
    float__bin(bb)(v.ma)
    float__bin(bb)(v.mb)
    float__bin(bb)(v.mc)
    float__bin(bb)(v.md)
    float__bin(bb)(v.min)
    float__bin(bb)(v.max)
    int32__bin(bb)(v.count)
}

export const bin__Stat = (bi: BinIndexed): Stat => {

    return {
        mean: bin__float(bi),
        middle: bin__float(bi),
        var: bin__float(bi),
        median: bin__float(bi),
        ma: bin__float(bi),
        mb: bin__float(bi),
        mc: bin__float(bi),
        md: bin__float(bi),
        min: bin__float(bi),
        max: bin__float(bi),
        count: bin__int32(bi),
    }
}

// [SpotInStat] Structure

export const SpotInStat_empty = (): SpotInStat => {
    return {
        deviation: 0.0,
        spot: 0.0,
        anchor: 0.0,
        digit: 0,
        unit: "",
        stat: Stat_empty(),
    } as SpotInStat
}

export const SpotInStat__bin = (bb: BytesBuilder) => (v: any) => {

    float__bin(bb)(v.deviation)
    float__bin(bb)(v.spot)
    float__bin(bb)(v.anchor)
    int32__bin(bb)(v.digit)
    str__bin(bb)(v.unit)
    Stat__bin(bb)(v.stat)
}

export const bin__SpotInStat = (bi: BinIndexed): SpotInStat => {

    return {
        deviation: bin__float(bi),
        spot: bin__float(bi),
        anchor: bin__float(bi),
        digit: bin__int32(bi),
        unit: bin__str(bi),
        stat: bin__Stat(bi),
    }
}













// 测试



const test = (i: any, tob: Function, toa: Function) => {
    console.log(`Input: ${JSON.stringify(i)}`)
    const bb = new BytesBuilder()

    tob(bb)(i)
    const binn: BinIndexed = { bin: bb.bytes(), index: 0 }

    console.log(`bin.hex: 0x${arrayBuffer__Hex(binn.bin)}`)
    console.log(`Out: ${JSON.stringify(toa(binn))}`)

}

const doo = () => {

    console.log(`===== Test int32 =====`)
    test(7742, int32__bin, bin__int32)

    console.log()
    console.log(`===== Test string =====`)
    test("Hello world", str__bin, bin__str)

    console.log()
    console.log(`===== Test utf8 =====`)
    test("这些是UTF8字符串😀😃😄😁😆😅😂🤣🥲🥹☺️", str__bin, bin__str)

    console.log()
    console.log(`===== Test dict =====`)
    test({ "key1": "value1", "key2": "value2" }, dict__bin, bin__dict)
}


