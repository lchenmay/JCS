﻿module WebLogics.Game

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
r: float
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
        //color = "#ffffcc"
        color = rand__color()
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

let move field = 

    let w,h = field.width,field.height

    field.balls
    |> Array.iter(fun ball -> 
        ball.x <- ball.x + ball.vx
        ball.y <- ball.y + ball.vy

        //match field.mouse with
        //| Some (x,y) -> 

        //    let disx = ball.x - x
        //    let disy = ball.y - y

        //    if disx * disx + disy * disy < 10.0 * 10.0 then
        //        ball.hit <- true
        //    else
        //        ball.hit <- false

        //| None -> ()

        if ball.x < 0 || ball.x > w then
            ball.vx <- - ball.vx
            if ball.x < 0 then
                ball.x <- 0
            if ball.x > w then
                ball.x <- w

        if ball.y < 0 || ball.y > h then
            ball.vy <- - ball.vy
            if ball.y < 0 then
                ball.y <- 0
            if ball.y > h then
                ball.y <- h)

let cycle = 500

let renderCaption (ctx:Canvas2DContext) field = 

    let elapse = (DateTime.UtcNow - field.lastRender).TotalSeconds
    field.lastRender <- DateTime.UtcNow

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
        do! ctx.FillTextAsync("Blazaor WebAssembly + HTML Canvas", 10, cursor.Value)
        cursor.Value <- cursor.Value + 20

        do! ctx.SetFontAsync("16px consolas")

        do! [|  "Transiting %" + (100.0 * (float field.interpolate)/(float cycle)).ToString("00.00")
                " " + backColor
                " Elapse = " + elapse.ToString("0.00") + "s"   |]
            |> String.Concat
            |> drawText ctx (10, cursor.Value)
        cursor.Value <- cursor.Value + 20

        do! [|  "w = " + field.width.ToString("0")
                ", h = " + field.height.ToString("0")    |]
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

        do! [|  "Current stroke points = " + field.whiteboard.currentStroke.points.Count.ToString()  |]
            |> String.Concat
            |> drawText ctx (10, cursor.Value)
        cursor.Value <- cursor.Value + 20
    }

let renderBall (ctx:Canvas2DContext) ball = 
    let color = 
        if ball.hit then
            "#ffffff"
        else
            ball.color
    drawCircle ctx color ball.r (ball.x, ball.y)


let drawStroke (ctx:Canvas2DContext) (s:Stroke) = 

    task{
        do! s.color 
            |> ctx.SetStrokeStyleAsync
        do! ctx.SetLineWidthAsync s.strokeSize
        do! s.points.ToArray() 
            |> Array.map(fun (x,y) -> float x,float y)
            |>  drawPath ctx
    }

let renderWhiteboard (ctx:Canvas2DContext) whiteboard = 
    task{
        let! tasks = 
            whiteboard.strokes.ToArray()
            |> Array.map(drawStroke ctx)
            |> Task.WhenAll
    
        do! drawStroke ctx whiteboard.currentStroke
    }

let render (ctx:Canvas2DContext) field = 

    move field

    let t = 
        task{
            do! ctx.BeginBatchAsync()

            do! renderCaption ctx field

            let! tasks = 
                field.balls
                |> Array.map(renderBall ctx)
                |> Task.WhenAll

            do! renderWhiteboard ctx field.whiteboard

            do! ctx.EndBatchAsync()
        }

    //t.RunSynchronously()
    t
