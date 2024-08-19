import * as PIXI from 'pixi.js';
import { Application, Assets, Sprite } from 'pixi.js';
import * as Graphics from '~/lib/util/graphics'

export type PixiCtx = {
    canvas: HTMLElement,
    w:number,
    h:number,
    app: PIXI.Application
}

export const init = async (id:string,w:number,h:number):Promise<PixiCtx> => {

    let canvas = document.getElementById(id) as HTMLElement
    let app = new PIXI.Application()
  
    await app.init({ background: '#1099bb', width: w, height: h })
  
    canvas.appendChild(app.canvas)
  
    return {
        canvas: canvas,
        w:w,
        h:h,
        app: app
    }
}
  
export const renderRect = 
    (g:PIXI.Graphics) => 
    (edgeStroke:number,edgeColor:number,fillColor:number) => 
    (x:number,y:number,w:number,h:number) => {
    g.setStrokeStyle({ color: edgeColor, width: edgeStroke})
    g.fill(fillColor)
}

export const fillRect = 
    (g:PIXI.Graphics) => 
    (fillColor:number) => 
    (x:number,y:number,w:number,h:number) => {
        renderRect(g)(0,0,fillColor)(x,y,w,h)
}

export const drawLine = 
    (g:PIXI.Graphics) => 
    (edgeStroke:number,edgeColor:number) => 
    (x1:number,y1:number,x2:number,y2:number) => {

    g.setStrokeStyle({ color: edgeColor, width: edgeStroke})
    g.moveTo(x1,y1)
    g.lineTo(x2,y2)
}

export const drawPath = 
    (g:PIXI.Graphics) => 
    (edgeStroke:number,edgeColor:number) => 
    (points:Array<any>) => {

    g.setStrokeStyle({ color: edgeColor, width: edgeStroke})

    let pt = points[0]

    g.moveTo(pt.x,pt.y)

    for(let i = 1; i < points.length; i++){
        pt = points[i]
        g.lineTo(pt.x,pt.y)
    }
}

export const drawRect = 
    (g:PIXI.Graphics) => 
    (edgeStroke:number,edgeColor:number) => 
    (x:number,y:number,w:number,h:number) => {
    g.setStrokeStyle({ color: edgeColor, width: edgeStroke})
    //g.drawRect(x,y,w,h)
    g.rect(x,y,w,h)
}

export const drawChartLayout = 
    (g:PIXI.Graphics) => 
    (edgeStroke:number,edgeColor:number) => 
    (chart:Graphics.Chart) => {
    drawRect(g)(edgeStroke,edgeColor)(chart.l,chart.t,chart.w,chart.h)
    //drawLine(g)(edgeStroke,edgeColor)(chart.l,chart.t,chart.l + chart.w,chart.t + chart.h)
    //drawLine(g)(edgeStroke,edgeColor)(chart.l,chart.t + chart.h,chart.l + chart.w,chart.t)
}

