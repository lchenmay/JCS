import * as Graphics from '~/lib/util/graphics'

export type H5Ctx = {
    canvas: any,
    w: number,
    h: number,
    g: CanvasRenderingContext2D
}

export const init = (e:any,w:number,h:number):H5Ctx => {

    let res = {
        canvas: {},
        w:w,
        h:h,
        g: {}} as H5Ctx

    res.canvas = e as HTMLCanvasElement
    res.g = res.canvas.getContext('2d') as CanvasRenderingContext2D

    return res
}
  
export const fillRect = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (x:number,y:number,w:number,h:number) => {
    g.fillStyle = color
    g.fillRect(x,y,w,h)
}

export const drawLine = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (x1:number,y1:number,x2:number,y2:number) => {

    g.strokeStyle = color
    g.beginPath()
    g.moveTo(x1,y1)
    g.lineTo(x2,y2)
    g.stroke()
}

export const drawPath = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (points:Array<any>) => {

  if(points.length > 1){
    g.strokeStyle = color
 
    let pt = points[0]

    g.beginPath()
    g.moveTo(pt.x,pt.y)

    for(let i = 1; i < points.length; i++){
        pt = points[i]
        g.lineTo(pt.x,pt.y)
    }

    g.stroke()
  }
}

export const drawRect = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (x:number,y:number,w:number,h:number) => {
    g.strokeStyle = color
    g.strokeRect(x,y,w,h)
}

export const drawChartLayout = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (chart:Graphics.Chart) => {
    drawRect(g)(color)(chart.l,chart.t,chart.w,chart.h)
    //drawLine(g)(color)(chart.l,chart.t,chart.l + chart.w,chart.t + chart.h)
    //drawLine(g)(color)(chart.l,chart.t + chart.h,chart.l + chart.w,chart.t)
}

export const drawLineHor = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (coords:Graphics.CoordXY) =>
    (v:number) => {
    if (coords.y.pinf <= v && coords.y.psup >= v){
        let y = Graphics.p__d(coords.y)(v)
        drawLine(g)(color)(coords.x.dinf,y,coords.x.dsup,y)  
    }
}

export const drawLineVer = 
    (g:CanvasRenderingContext2D) => 
    (color:string) => 
    (coords:Graphics.CoordXY) =>
    (v:number) => {
    if (coords.x.pinf <= v && coords.x.psup >= v){
        let x = Graphics.p__d(coords.x)(v)
        drawLine(g)(color)(x,coords.y.dinf,x,coords.y.dsup)  
    }
}

export function getMousePosition(canvas:any,event: MouseEvent): Graphics.Vct2 {
    const rect = canvas.getBoundingClientRect()
    const x = event.clientX - rect.left
    const y = event.clientY - rect.top
    return { x, y }
}
