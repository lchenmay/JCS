import * as Graphics from '~/lib/util/graphics'
import * as GraphicsImpl from '~/lib/util/graphicsH5'
import * as types from '~/lib/bizLogics/types'
import { drawLine, fillRect } from '../util/graphicsPixi'


export const darkmode__palette = (darkmode:boolean):types.Palette => {

    if(darkmode)
        return {
        background: '#001133',
        text: '#666666',
        grid: '#666666',
        bar: '#ffffff',
        mark: '#ffffff',
        up: '#00ff00',
        dn: '#ff0000',
        ma1: '#ffff00',
        ma2: '#0000ff',
        spot: '#ffffff',
        mean: '#ffffff',
        min: '#ffffff',
        max: '#ffffff',
        median: '#ff0000',
        mediana: '#0000ff',
        medianb: '#0000ff',
        medianc: '#0000ff',
        mediand: '#0000ff' }
    else
        return {
        background: '#fcfcff',
        text: '#999999',
        grid: '#999999',
        bar: '#999999',
        mark: '#000000',
        up: '#006600',
        dn: '#660000',
        ma1: '#999900',
        ma2: '#9999ff',
        spot: '#ffffff',
        mean: '#000000',
        min: '#666666',
        max: '#666666',
        median: '#0000ff',
        mediana: '#0000ff',
        medianb: '#0000ff',
        medianc: '#0000ff',
        mediand: '#0000ff'}
}



export const bars__coords =
  (c:Graphics.Chart,bars:any[],tpb:j7.TpbItem[]): Graphics.CoordXY => {

  let coordx = { 
    pinf: bars[0].index - 1,
    psup: 5 + bars[bars.length - 1].index + tpb.length + 1,
    dinf: c.l, 
    dsup: c.l + c.w } as Graphics.Coord
    
  let coordy = { 
    pinf: bars[0].lb,
    psup: bars[0].ha,
    dinf: c.t + c.h, 
    dsup: c.t } as Graphics.Coord

  bars.forEach(bar => {
    Graphics.checkPRange(coordy)(bar.lb)
    Graphics.checkPRange(coordy)(bar.ha)
  });        

  return {
    x: coordx,
    y: coordy
  } as Graphics.CoordXY
}

export const drawBars = 
  (ctx:GraphicsImpl.H5Ctx,coords:Graphics.CoordXY,palette:types.Palette) => 
  (bars:any[]) => {

  let coordx = coords.x
  let coordy = coords.y

  let semiwidth = (coordx.dsup - coordx.dinf) / (10 + 1 + 2 * bars.length) - 3
  for(let i = 0; i < bars.length; i++){
    let bar = bars[i]
    let x = Graphics.p__d(coordx)(bar.index)
    let yha = Graphics.p__d(coordy)(bar.ha)
    let yhb = Graphics.p__d(coordy)(bar.hb)
    let yla = Graphics.p__d(coordy)(bar.la)
    let ylb = Graphics.p__d(coordy)(bar.lb)

    if(semiwidth > 1){
      let yoa = Graphics.p__d(coordy)(bar.oa)
      let yob = Graphics.p__d(coordy)(bar.ob)
      let yca = Graphics.p__d(coordy)(bar.ca)
      let ycb = Graphics.p__d(coordy)(bar.cb)

      if(bar.c > bar.o){
        GraphicsImpl.drawRect(ctx.g)(palette.bar)(x - semiwidth,yoa,2*semiwidth,yca - yoa)
        GraphicsImpl.drawRect(ctx.g)(palette.bar)(x - semiwidth,yob,2*semiwidth,ycb - yob)
        GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,yha,x,yca)
        GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,ylb,x,yoa)
      }
      else{
        GraphicsImpl.drawRect(ctx.g)(palette.bar)(x - semiwidth,yoa,2*semiwidth,yca - yoa)
        GraphicsImpl.drawRect(ctx.g)(palette.bar)(x - semiwidth,yob,2*semiwidth,ycb - yob)
        GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,yha,x,yoa)
        GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,ylb,x,yca)
      }
    }
    else{
      GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,yha,x,yhb)
      GraphicsImpl.drawLine(ctx.g)(palette.bar)(x,yla,x,ylb)
    }
  }

  if(bars.length > 0){
    let timestamp = new Date(Number(bars[0].timestamp))
    let txt = timestamp.getFullYear() + "/" + timestamp.getMonth() + "/" + timestamp.getDay()
    ctx.g.fillStyle = palette.text
    ctx.g.fillText(txt,coordx.dinf,coordy.dinf)
  }
}

export const drawMa = 
  (ctx:GraphicsImpl.H5Ctx,coords:Graphics.CoordXY) => 
  (bars:any[],ma:any[],color:string) => {

  let coordx = coords.x
  let coordy = coords.y

  let pts = []
  for(let i = 0; i < ma.length; i ++)
    pts.push(Graphics.pp__dd(coordx,coordy)(bars[i].index,ma[i]))
    
    GraphicsImpl.drawPath(ctx.g)(color)(pts)
}

export const drawZen =
  (ctx:GraphicsImpl.H5Ctx,coords:Graphics.CoordXY) => 
  (moves:j7.ZenMove[],palette:types.Palette) => {

  let coordx = coords.x
  let coordy = coords.y

  moves.forEach((zm:j7.ZenMove) => {
    if(zm.ud){
      let pStart = Graphics.pp__dd(coordx,coordy)(zm.bar1.index,zm.bar1.la)
      let pEnd = Graphics.pp__dd(coordx,coordy)(zm.bar2.index,zm.bar2.hb)
      let pConfirm = Graphics.pp__dd(coordx,coordy)(zm.confirm.index,zm.confirm.cb)

      ctx.g.lineWidth = 1.5
      GraphicsImpl.drawLineVct2(ctx.g)('#333333')(pStart,pEnd)

      ctx.g.setLineDash([5, 2])

      ctx.g.lineWidth = 1
      GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pStart,pConfirm)

      if(zm.retreato){
        let pRetreat = Graphics.pp__dd(coordx,coordy)(zm.retreato.index,zm.retreato.la)
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pConfirm,pRetreat)

        ctx.g.lineWidth = 1.5
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pRetreat,pEnd)
      }
      else{
        ctx.g.lineWidth = 1.5
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pConfirm,pEnd)
      }

      ctx.g.setLineDash([])
    }
    else{
      let pStart = Graphics.pp__dd(coordx,coordy)(zm.bar1.index,zm.bar1.hb)
      let pEnd = Graphics.pp__dd(coordx,coordy)(zm.bar2.index,zm.bar2.la)
      let pConfirm = Graphics.pp__dd(coordx,coordy)(zm.confirm.index,zm.confirm.ca)

      //ctx.g.lineWidth = 1.5
      GraphicsImpl.drawLineVct2(ctx.g)('#333333')(pStart,pEnd)

      ctx.g.setLineDash([5, 2])

      //ctx.g.lineWidth = 1
      GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pStart,pConfirm)

      if(zm.retreato){
        let pRetreat = Graphics.pp__dd(coordx,coordy)(zm.retreato.index,zm.retreato.hb)
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pConfirm,pRetreat)

        //ctx.g.lineWidth = 1.5
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pRetreat,pEnd)
      }
      else{
        //ctx.g.lineWidth = 1.5
        GraphicsImpl.drawLineVct2(ctx.g)('#6666ff')(pConfirm,pEnd)
      }

      ctx.g.setLineDash([])
    }
  })
}

export const drawTPB = 
  (ctx:GraphicsImpl.H5Ctx,coords:Graphics.CoordXY) => 
  (spot:number,bars:any[],tpb:j7.TpbItem[]) => {

  let coordx = coords.x
  let coordy = coords.y

  let bar = bars[bars.length - 1]
  let index = bar.index

  let upMax = []
  let upd = []
  let upb = []
  let upMedian = []
  let upa = []
  let dna = []
  let dnMedian = []
  let dnb = []
  let dnd = []
  let dnMax = []
  for(let i = 0; i < tpb.length; i ++){
    let item = tpb[i]
    let v = { x: index + i, y: spot } 
     
    upMax.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (item.up.max)))
    upd.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (item.up.md)))
    upb.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (item.up.mb)))
    upMedian.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (item.up.median)))
    upa.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (item.up.ma)))

    dna.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (-item.dn.ma)))
    dnMedian.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (-item.dn.median)))
    dnb.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (-item.dn.mb)))
    dnd.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (-item.dn.md)))
    dnMax.push(Graphics.pxy__dxy(coords)(Graphics.Vct2_yOffset (v) (-item.dn.max)))
  }

  GraphicsImpl.drawPath(ctx.g)('#ffff00')(upMax)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(upd)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(upb)
  GraphicsImpl.drawPath(ctx.g)('#00ff00')(upMedian)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(upa)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(dna)
  GraphicsImpl.drawPath(ctx.g)('#ff0000')(dnMedian)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(dnb)
  GraphicsImpl.drawPath(ctx.g)('#0000ff')(dnd)
  GraphicsImpl.drawPath(ctx.g)('#ffff00')(dnMax)
}

export const ud__color = (palette:types.Palette,ud:number) => {
  let color = palette.bar
  if(ud > 0)
    color = palette.up
  if(ud < 0)
    color = palette.dn
  return color
}

export const checkTicketRange = 
  (cxy:Graphics.CoordXY) => 
  (tobo: j7.TicketOnBars | null) => {
  if(tobo){
    let t = tobo.pTicket as j7.pTICKET
    Graphics.checkPRange(cxy.y)(t.OpenPrice)
    Graphics.checkPRange(cxy.y)(t.SL)
    Graphics.checkPRange(cxy.y)(t.TP)
  }
}

export const drawTicket =
  (ctx:GraphicsImpl.H5Ctx) =>
  (cxy:Graphics.CoordXY,palette:types.Palette) => 
  (tobo: j7.TicketOnBars) => {

    let t = tobo.pTicket as j7.pTICKET

    let cup = palette.up
    let cdn = palette.dn

    let yopen = Graphics.p__d(cxy.y)(t.OpenPrice)
    let yclose = Graphics.p__d(cxy.y)(t.ClosePrice)
    let ysl = Graphics.p__d(cxy.y)(t.SL)
    let ytp = Graphics.p__d(cxy.y)(t.TP)

    let x1 = cxy.x.dinf
    let x2 = cxy.x.dinf

    ctx.g.lineWidth = 3
    switch(t.State){

      case 0: // Pending
        if(tobo.pendingBaro){
          x1 = Graphics.p__d(cxy.x)(tobo.pendingBaro.index)
          x2 = cxy.x.dsup
        }
      break

      case 1: // Open
        if(tobo.openBaro){
          x1 = Graphics.p__d(cxy.x)(tobo.openBaro.index)
          x2 = cxy.x.dsup + 20
        }
        GraphicsImpl.drawLine(ctx.g)('#0000ff')(x1,yopen,cxy.x.dsup + 20,yopen)

        break

      case 2: // Closed
        if(tobo.openBaro && tobo.closeBaro){
          x1 = Graphics.p__d(cxy.x)(tobo.openBaro.index)
          x2 = Graphics.p__d(cxy.x)(tobo.closeBaro.index)
        }

        let color = cdn
        if(t.Lot > 0 && t.ClosePrice > t.OpenPrice)
          color = cup
        if(t.Lot < 0 && t.ClosePrice < t.OpenPrice)
          color = cup

        GraphicsImpl.drawLine(ctx.g)(color)(x1,yopen,x2,yclose)

        break
    }

    ctx.g.lineWidth = 1
  
    GraphicsImpl.drawLine(ctx.g)('#0000ff')(x1,yopen,x2,yopen)
    GraphicsImpl.drawLine(ctx.g)(cdn)(x1,ysl,x2,ysl)
    GraphicsImpl.drawLine(ctx.g)(cup)(x1,ytp,x2,ytp)

    ctx.g.fillStyle = palette.text
    ctx.g.fillText(t.TicketNum,x1 - 80,yopen)

}