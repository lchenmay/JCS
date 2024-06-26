﻿module WebLogics.Whiteboard

open System
open System.Collections.Generic

open Blazor.Extensions
open Blazor.Extensions.Canvas
open Blazor.Extensions.Canvas.Canvas2D

open Util.Bin
open Util.Json
open Util.Text
open Util.IA

open UtilBlazor.Graphics

open BizShared.OrmTypes
open BizShared.Types
open BizShared.CustomMor

open WebLogics.Common
open WebLogics.DataService


let empty__Stroke (strokeSize,color) = {
    points = new List<float32 * float32>()
    strokeSize = strokeSize
    color = color }

//let facts__strokes facts = 

//    let buffer = new List<Stroke>()

//    facts
//    |> List.iter(fun f -> 
//        match f.action with
//        | ActionWhiteboard.Stroke s -> buffer.Add s
//        | _ -> ())

//    buffer.ToArray()


type ColorInPalette = {
mutable rect: Rect
color: string }

let cips = 
    palette
    |> Array.map(fun c -> {
        rect = empty__Rect()
        color = c })

//type Whiteboard =

//    struct
//        val layout: Grid
//        val currentCIP: ColorInPalette ref
//        val currentStroke: Stroke ref
//        val drawing: bool ref
//        val facts: SortedDictionary<DateTime,FactWhiteboard>
//        val mainCanvas:SKCanvasView
//        val bitmap:SKBitmap ref
//        val paletteCanvas:SKCanvasView
//        val strokeSize:Stepper
//        val cips:ColorInPalette[]
//        val prompt: Label
//        val msg: Editor
//        val buttonSend:Button
//        val chatSession: Label
//        val senderCaption: Label

//        new(w:int,h:int) = 

//            let currentCIP = cips[0]
//            let currentStrokeSize = 3f
            
//            { 
//                layout = new Grid()
//                currentCIP = ref currentCIP
//                currentStroke = ref (empty__Stroke(currentStrokeSize,currentCIP.color))
//                drawing = ref false
//                facts = new SortedDictionary<DateTime,FactWhiteboard>()
//                mainCanvas = new SKCanvasView(
//                    WidthRequest = float w,
//                    HeightRequest = float h)
//                bitmap = ref(new SKBitmap(w,h))
//                paletteCanvas = new SKCanvasView(
//                    WidthRequest = float w,
//                    HeightRequest = 200)
//                strokeSize = new Stepper(0.5,5.0,3.0,0.5)
//                cips = cips
//                prompt = new Label()
//                msg = new Editor()
//                buttonSend = new Button(Text = "发送")
//                chatSession = new Label(Text = "聊天记录")
//                senderCaption= new Label(Text = Util.Prob.array__rand senderCaptionCandidates ) }
//    end

//    member this.incomingFacts (whiteboard:Whiteboard) facts = 

//        let mutable redraw = false

//        facts
//        |> Array.iter(fun f -> 
        
//            match f with
//            | FactBroadcast.Whiteboard f ->

//                redraw <- true

//                if whiteboard.facts.ContainsKey f.clientTimestamp then
//                    let i = whiteboard.facts[f.clientTimestamp]
//                    if i.clientId = f.clientId then
//                        whiteboard.facts.Remove f.clientTimestamp |> ignore

//                if whiteboard.facts.ContainsKey f.serverTimestamp then
//                    whiteboard.facts[f.serverTimestamp] <- f
//                else
//                    whiteboard.facts.Add(f.serverTimestamp,f)

//            | _ -> ())
                
//        if redraw then
//            whiteboard.mainCanvas.InvalidateSurface()

//    interface IDisposable with
//        member this.Dispose() = 
//            this.incomingFacts this
//            |> client.incomingFacts.Remove
//            |> ignore

//let factId = ref 0L

//let action__fact (whiteboard:Whiteboard) action = {
//    action = action
//    actor = whiteboard.senderCaption.Text
//    clientId = System.Threading.Interlocked.Increment factId
//    serverId = 0L
//    clientTimestamp = DateTime.UtcNow
//    serverTimestamp = DateTime.UtcNow }

//let pushFactWhiteboard (whiteboard:Whiteboard) (fact:FactWhiteboard) = 
//    whiteboard.facts.Add(fact.serverTimestamp,fact)
    
//    fact
//    |> FactBroadcast.Whiteboard
//    |> clientPushFact__server client

//let initWhiteboard(whiteboard:Whiteboard,children:IList<IView>) = 

//    whiteboard.incomingFacts whiteboard
//    |> client.incomingFacts.Add

//    let layout = whiteboard.layout
//    let mainCanvas = whiteboard.mainCanvas
//    let paletteCanvas = whiteboard.paletteCanvas
//    let strokeSize = whiteboard.strokeSize
//    let prompt = whiteboard.prompt
//    let msg = whiteboard.msg
//    let buttonSend = whiteboard.buttonSend
//    let chatSession = whiteboard.chatSession
//    let senderCaption = whiteboard.senderCaption

//    absLayout mainCanvas (0.0, 0.0, 1.0, 0.48)
//    absLayout paletteCanvas (0.0, 0.65, 1.0, 0.05)
//    absLayout strokeSize (0.0, 0.7, 1.0, 0.05)
//    absLayout prompt (0.0, 0.75, 1.0, 0.05)
//    absLayout msg (0.0, 0.8, 1.0, 0.05)
//    absLayout buttonSend (0.0, 0.85, 1.0, 0.05)
//    absLayout chatSession (0.0, 0.9, 1.0, 0.05)
//    absLayout senderCaption (0.0, 0.95, 1.0, 0.05)

//    layout |> appendControl children |> ignore

//    mainCanvas |> appendControl children |> ignore
//    paletteCanvas |> appendControl children |> ignore
//    strokeSize |> appendControl children |> ignore
//    prompt |> appendControl children |> ignore
//    msg |> appendControl children |> ignore
//    buttonSend |> appendControl children |> ignore
//    chatSession |> appendControl children |> ignore
//    senderCaption |> appendControl children |> ignore

//    // Event

//    mainCanvas.EnableTouchEvents <- true
//    paletteCanvas.EnableTouchEvents <- true

//    mainCanvas.SizeChanged.Add(fun e -> 
//        whiteboard.bitmap.Value <- new SKBitmap(int mainCanvas.Width,int mainCanvas.Height)
//        use bitmapCanvas = new SKCanvas(whiteboard.bitmap.Value)
//        bitmapCanvas.Clear SK"AliceBlue)

//    mainCanvas.PaintSurface.Add(fun e -> 
//        let canvas,w,h = e |> skPaintSurfaceE__info

//        //e.Surface.Canvas.DrawBitmap(whiteboard.bitmap,0.0f,0.0f)

//        let paint = new SKPaint(Color = SK"Magenta,Style = SKPaintStyle.Stroke)
//        canvas.DrawRect(new SKRect(0.0f,0.0f,float32 w - 1.0f,float32 h - 1.0f),paint)
 
//        lock whiteboard.facts (fun _ -> 

//            whiteboard.facts.Values
//            |> Seq.toArray
//            |> Array.map(fun f -> 
//                match f.action with
//                | ActionWhiteboard.Stroke s -> Some s
//                | _ -> None)
//            |> Array.filter(fun o -> o.IsSome)
//            |> Array.map(fun o -> o.Value)
//            |> Array.iter(drawStroke canvas)

//            ())
        
//        drawStroke canvas whiteboard.currentStroke.Value)

//    mainCanvas.Touch.Add(fun e -> 
//        match e.ActionType with
//        | SKTouchAction.Pressed -> 
//            whiteboard.drawing.Value <- true

//        | SKTouchAction.Released -> 
//            whiteboard.drawing.Value <- false
            
//            whiteboard.currentStroke.Value 
//            |> ActionWhiteboard.Stroke 
//            |> action__fact whiteboard
//            |> pushFactWhiteboard whiteboard

//            whiteboard.currentStroke.Value <- 
//                empty__Stroke(float32 whiteboard.strokeSize.Value,whiteboard.currentCIP.Value.color)

//        | SKTouchAction.Moved ->
//            if whiteboard.drawing.Value then
//                let s = whiteboard.currentStroke.Value
//                let x,y = e.Location.X,e.Location.Y
//                s.points.AddRange [| (x,y) |]

//        | _ -> ()

//        mainCanvas.InvalidateSurface()

//        prompt.Text <- 
//            " pts = " + whiteboard.currentStroke.Value.points.Count.ToString() 
//            + " " + e.Location.ToString()

//        e.Handled <- true)

//    paletteCanvas.PaintSurface.Add(fun e -> 
//        let canvas,w,h = e |> skPaintSurfaceE__info

//        let m = 21
//        let grid = float w / float m

//        [| 0 .. whiteboard.cips.Length - 1 |]
//        |> Array.iter(fun i ->
//            let cip = whiteboard.cips[i]

//            let x = float(i % m) * grid
//            let y = float(i / m) * grid
//            whiteboard.cips[i].rect <- new Rect(x,y,grid - 5.0,grid - 5.0)
            
//            let paint = new SKPaint(Color = new SKColor(cip.color),Style = SKPaintStyle.Fill)
//            let r = Rect__skRectRound(whiteboard.cips[i].rect,15.0f)
//            canvas.DrawRoundRect(r,paint)))

//    paletteCanvas.Touch.Add(fun e -> 
//        match e.ActionType with
//        | SKTouchAction.Pressed -> 
//            match 
//                whiteboard.cips
//                |> Array.tryFind(fun i -> 
//                    e.Location
//                    |> skPoint__xyFloat
//                    |> i.rect.Contains) with
//            | Some cip -> whiteboard.currentCIP.Value <- cip
//            | None -> ()
//        | _ -> ()
        
//        e.Handled <- true)

//    msg.TextChanged.Add(fun e -> ())
//    msg.Completed.Add(fun e -> chatSession.Text <- chatSession.Text + System.Environment.NewLine + ".." + msg.Text)
    
