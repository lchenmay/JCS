
export type Vct2 = {
    x: number,
    y: number
}

export const Vct2_xOffset = (v:Vct2) => (x:number) => {
    return {
        x: v.x + x,
        y: v.y
    } as Vct2
}

export const Vct2_yOffset = (v:Vct2) => (y:number) => {
    return {
        x: v.x,
        y: v.y + y
    } as Vct2
}

export const xy__Vct2 = (x:number,y:number) => {
  return { x:x, y: y } as Vct2
}

export type Coord = {
    pinf: number,
    psup: number,
    dinf: number,
    dsup: number
}

export const checkPRange = 
    (coord:Coord) =>
    (p:number) => {
    if(coord.pinf > p)
        coord.pinf = p
    if(coord.psup < p)
        coord.psup = p
}

export const strechPRange = 
    (coord:Coord) =>
    (rate:number) => {
    let inf = coord.pinf
    let sup = coord.psup
    coord.pinf = inf - rate * (sup - inf)
    coord.psup = sup + rate * (sup - inf)
}

export const p__d = 
    (coord:Coord) => 
    (p:number) => {
    return coord.dinf + (coord.dsup - coord.dinf) * (p - coord.pinf) / (coord.psup - coord.pinf);
}

export const pp__dd = 
    (coordx:Coord,coordy:Coord) => 
    (px:number,py:number) => {
    return {
        x: p__d(coordx)(px),
        y: p__d(coordy)(py)
    }
}

export type CoordXY = {
    x: Coord,
    y: Coord
}

export const pxy__dxy = 
    (coordxy:CoordXY) => 
    (v:Vct2) => {
    return {
        x: p__d(coordxy.x)(v.x),
        y: p__d(coordxy.y)(v.y)
    }
}

export type Chart = {
    l: number,
    t: number,
    w: number,
    h: number
}

export type GridLayoutCell = {
    col: number,
    row: number,
    colspan: number,
    rowspan: number
}

export const wh__Chart = (w:number,h:number) => {
  return {
    l: 0,
    t: 0,
    w: w,
    h: h } as Chart
}

export const colrowSpan__gridLayoutCell = 
    (col:number,row:number,colspan:number,rowspan:number):GridLayoutCell => {
    return {
        col: col,
        row: row,
        colspan: colspan,
        rowspan: rowspan
    }
}

export const colrow__gridLayoutCell = (col:number,row:number):GridLayoutCell => {
    return {
        col: col,
        row: row,
        colspan: 1,
        rowspan: 1
    }
}

export const percentage__controlPoints = 
    (inf:number,sup:number) =>
    (vs:Array<number>) => {

    let sum = 0
    vs.forEach(v => { sum += v })

    let res = new Array<number>(vs.length + 1)
    res[0] = inf
    for(let i = 0; i < vs.length; i ++)
        res[i + 1] = res[i] + (sup - inf) * vs[i]/sum

    return res
}

export const chart__layout =
    (chart:Chart) =>
    (xs:Array<number>,ys:Array<number>) => 
    (grids:Array<GridLayoutCell>) => {

    let layoutx = percentage__controlPoints(chart.l,chart.l + chart.w)(xs)
    let layouty = percentage__controlPoints(chart.t,chart.t + chart.h)(ys)

    let res = new Array<Chart>(grids.length)
    for(let i = 0; i < grids.length; i ++){
        let grid = grids[i]
        res[i] = {
            l: layoutx[grid.col],
            t: layouty[grid.row],
            w: layoutx[grid.col + grid.colspan] - layoutx[grid.col],
            h: layouty[grid.row + grid.rowspan] - layouty[grid.row]
        } as Chart
    }

    return res
}

export const chart__cellLayout =
  (chart:Chart) =>
  (xs: number[], ys: number[]) => 
  (cells:Vct2[]) => {

  let grids = 
    cells.map((i) => colrow__gridLayoutCell(i.x,i.y))

  return chart__layout(chart)(xs,ys)(grids)
}

export const RowCol__Cells = (row:number,col:number) => {

  let res = new Array<Vct2>()    

  for(let i = 0; i < row; i ++){
    for(let j = 0; j < col; j ++){
      res.push(xy__Vct2(j,i))
    }
  }

  return res
}

  