module BlazorLogics.Graphics

open System

open Blazor.Extensions
open Blazor.Extensions.Canvas
open Blazor.Extensions.Canvas.Canvas2D

open UtilBlazor.Graphics

open BizShared.Types


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
        color = String.Format("#{0:X6}", r.Next(0xFFFFFF))
        r = 5.0 + 5.0 * r.NextDouble()
        x = w * r.NextDouble()
        y = h * r.NextDouble()
        a = 1.0
        vx = v()
        vy = v() })

let createField n (w,h) = {
    width = w
    height = h
    mouse = None
    balls = field__balls n (w,h)
    lastRender = DateTime.UtcNow }

let move field = 

    let w,h = field.width,field.height

    field.balls
    |> Array.iter(fun ball -> 
        ball.x <- ball.x + ball.vx * ball.a
        ball.y <- ball.y + ball.vy * ball.a

        match field.mouse with
        | Some (x,y) -> 

            let disx = ball.x - x
            let disy = ball.y - y

            if disx * disx + disy * disy < 10.0 * 10.0 then
                ball.a <- ball.a
            else
                ball.a <- ball.a

        | None -> ()

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

let renderCaption (ctx:Canvas2DContext) field = 

    let fps = 1.0 / (DateTime.UtcNow - field.lastRender).TotalSeconds
    field.lastRender <- DateTime.UtcNow

    let backColor = "#006633"
    let textColor = "#aaaa33"

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
        do! ctx.FillTextAsync("fps = " + fps.ToString("0.00"), 10, cursor.Value)
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
            do! (x,0.0,x,field.height) 
                |> drawLine ctx
            do! (0.0,y,field.width,y) 
                |> drawLine ctx
            
        | None -> ()
    }

let renderBall (ctx:Canvas2DContext) ball = 
    task{
        do! ctx.BeginPathAsync()
        do! ctx.ArcAsync(ball.x, ball.y, ball.r, 0, 2.0 * System.Math.PI, false)
        do! ctx.SetFillStyleAsync("#33FF33")
        do! ctx.FillAsync()
        do! ctx.StrokeAsync()
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
                |> System.Threading.Tasks.Task.WhenAll

            do! ctx.EndBatchAsync()
        }

    t.RunSynchronously()