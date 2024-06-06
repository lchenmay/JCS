module WebLogics.Game

open System
open System.Collections.Generic
open System.Threading.Tasks

open Blazor.Extensions
open Blazor.Extensions.Canvas
open Blazor.Extensions.Canvas.Canvas2D

open UtilBlazor.Graphics

open BizShared.Types


type Ball = {
mutable color: string
mutable name: string
mutable r: float
mutable hit: bool
mutable x: float
mutable y: float 
mutable vx: float
mutable vy: float }

type Whiteboard = {
strokes: List<Stroke>
mutable currentStrokeWidth: float32
mutable currentColor: string
mutable currentStroke: Stroke }

type Field = {
whiteboard: Whiteboard
mutable colorSrc: float * float * float
mutable colorDst: float * float * float
mutable interpolate: int
mutable width: float
mutable height: float 
mutable hor: float
mutable span: float
mutable thickness: float
mutable mouse: (float * float) option
balls: Ball[]
mutable lastRender: DateTime }

let field__balls n (w,h) = 

    let r = new Random()

    let v() =
        let min,max = 0.25, 1.5
        if r.NextDouble() > 0.5 then
            min + max * r.NextDouble()
        else
            - min - max * r.NextDouble()

    [| 0 .. n - 1 |]
    |> Array.map(fun i -> {
        color = rand__color()
        name = "Nr " + i.ToString() + ""
        r = 5.0 + 15.0 * r.NextDouble()
        hit = false
        x = w * r.NextDouble()
        y = h * r.NextDouble()
        vx = v()
        vy = v() })

let generalColor() = 
    let r = new Random()
    r.NextDouble(),r.NextDouble(),r.NextDouble()

let createField n (w,h) = {
    whiteboard = {
        strokes = new List<Stroke>()
        currentStrokeWidth = 3f
        currentColor = "Red"
        currentStroke = {
            points = new List<float32 * float32>()
            strokeSize = 3f
            color = "Red"}}
    colorSrc = generalColor()
    colorDst = generalColor()
    interpolate = 0
    width = w
    height = h
    hor = 0.5 * w
    span = 0.2
    thickness = 70.0
    mouse = None
    balls = field__balls n (w,h)
    lastRender = DateTime.UtcNow }

let pushStrokePoint field = 
    match field.mouse with
    | Some (x,y) -> 
        (x |> float32,y |> float32) 
        |> field.whiteboard.currentStroke.points.Add
    | None -> ()

let closeStroke field = 
    let whiteboard = field.whiteboard
    if whiteboard.currentStroke.points.Count > 0 then
        whiteboard.currentStroke |> whiteboard.strokes.Add
        whiteboard.currentStroke <- {
            points = new List<float32 * float32>()
            strokeSize = whiteboard.currentStrokeWidth
            color = whiteboard.currentColor }

let collisionCheck field ball = 

    let mutable xhit,yhit = false,false

    let w,h = field.width,field.height
    let x,y,r = ball.x,ball.y,ball.r

    if x < r || x + r > w then
        xhit <- true
    if y < r || y + r > h then
        yhit <- true

    let yy = 0.5 * w
    let d1 = y - 0.5 * w
    let d2 = 0.5 * field.thickness + r
    if d1 * d1 < d2 * d2 then
        yhit <- true

    xhit,yhit
    

let move field elapse = 

    let w,h = field.width,field.height

    field.balls
    |> Seq.iter(fun ball -> 
        ball.x <- ball.x + ball.vx * elapse * 10.1
        ball.y <- ball.y + ball.vy * elapse * 10.1

        let xhit,yhit = collisionCheck field ball

        if xhit then
            ball.vx <- - ball.vx
            if ball.x < 0 then
                ball.x <- 0
            if ball.x > w then
                ball.x <- w

        if yhit then
            ball.vy <- - ball.vy
            if ball.y < 0 then
                ball.y <- 0
            if ball.y > h then
                ball.y <- h)

let cycle = 500

let renderCaption (ctx:Canvas2DContext) field (elapse:float) = 

    let backColor = 

        if field.interpolate >= cycle then
            field.interpolate <- 0
            field.colorSrc <- field.colorDst
            field.colorDst <- generalColor()

        field.interpolate <- field.interpolate + 1

        let s1,s2,s3 = field.colorSrc
        let d1,d2,d3 = field.colorDst

        let c(s,d) = 
            let v = s + (d-s) * (float field.interpolate)/(float cycle)
            v * 0.3 * 256.0
            |> uint8

        let s = 
            [|  c(s1,d1)
                c(s2,d2)
                c(s3,d3) |]
            |> Array.map(fun b -> 
                b.ToString("X").PadLeft(2,'0'))
            |> String.Concat

        "#" + s
    
    //let textColor = "#aaaa33"
    let textColor = "white"

    task{
        do! ctx.ClearRectAsync(0, 0, field.width, field.height)
        do! ctx.SetFillStyleAsync backColor
        do! ctx.FillRectAsync(0, 0, field.width, field.height)

        do! ctx.SetFillStyleAsync textColor
        do! ctx.SetStrokeStyleAsync textColor

        let cursor = ref 30

        do! ctx.SetFontAsync("26px Segoe UI")
        do! ctx.FillTextAsync("IA: DB + Server <<== WebSocket ==>> Blazaor WebAssembly + HTML Canvas", 10, cursor.Value)
        cursor.Value <- cursor.Value + 20

        do! ctx.SetFontAsync("16px consolas")

        do! [|  "Transiting %" + (100.0 * (float field.interpolate)/(float cycle)).ToString("00.00")
                " " + backColor
                " Elapse = " + elapse.ToString("0.00") + "s"   |]
            |> String.Concat
            |> drawText ctx (10, cursor.Value)
        cursor.Value <- cursor.Value + 20

        do! [|  "w = " + field.width.ToString("0")
                ", h = " + field.height.ToString("0")  
                ", hor = " + field.hor.ToString("0")   |]
            |> String.Concat
            |> drawText ctx (10, cursor.Value)
        cursor.Value <- cursor.Value + 20


        match field.mouse with
        | Some (x,y) ->
            do! [|  "x = " + x.ToString("0")
                    ", y = " + y.ToString("0")    |]
                |> String.Concat
                |> drawText ctx (10, cursor.Value)
            cursor.Value <- cursor.Value + 20

            do! ctx.SetStrokeStyleAsync "#cccccc"
            do! ctx.SetLineWidthAsync 1.0f
            do! (x,0.0,x,field.height) 
                |> drawLine ctx
            do! (0.0,y,field.width,y) 
                |> drawLine ctx
            
        | None -> ()


        do! ctx.SetFontAsync("32px Segoe UI")
        do! "======   MOVE ME, x = " + field.hor.ToString("0") + "   ======"
            |> drawText ctx (field.hor,0.5 * field.height)

        //do! ctx.SetFillStyleAsync "White"
        //do! ctx.FillRectAsync(
        //    field.hor - field.span * field.width,
        //    0.5 * field.height - field.thickness,
        //    2.0 * field.span * field.width,
        //    2.0 * field.span)
    }

let renderBall (ctx:Canvas2DContext) ball = 
    let color = 
        if ball.hit then
            "#ffffff"
        else
            ball.color

    task{
        do! drawCircle ctx color ball.r (ball.x, ball.y)
        do! [|  
                ball.name
                ", r = " + ball.r.ToString("0.0")
                ", vx = " + ball.vx.ToString("0.0")
                ", vy = " + ball.vy.ToString("0.0") |]
            |> String.Concat
            |> drawText ctx (ball.x + ball.r,ball.y + ball.r)
    }


let render (ctx:Canvas2DContext) field = 

    let elapse = (DateTime.UtcNow - field.lastRender).TotalSeconds
    field.lastRender <- DateTime.UtcNow

    move field elapse

    let t = 
        task{


            do! ctx.BeginBatchAsync()

            do! renderCaption ctx field elapse

            do! ctx.SetFontAsync("16px consolas")

            let! tasks = 
                field.balls
                |> Seq.map(renderBall ctx)
                |> Task.WhenAll

            do! ctx.EndBatchAsync()
        }

    //t.RunSynchronously()
    t
