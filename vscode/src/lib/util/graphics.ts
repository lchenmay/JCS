export type GraphicsContext = {
    g: object,// Environment-free context
    w: number,
    h: number
}

export type Coord = {
    pinf: number,
    psup: number,
    dinf: number,
    dsup: number
}

export type Chart = {
    l: number,
    t: number,
    w: number,
    h: number
}

export const canvas__g = (element:string):GraphicsContext => {

    
    return {
        g: null,
        w: 0.0,
        h: 0.0
    }
}