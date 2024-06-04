using Microsoft.FSharp.Core;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using BizShared;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

using WebLogics;

namespace BlazorWebAssembly.Pages
{
    public partial class Home
    {
        private Canvas2DContext ctx;
        protected BECanvasComponent CanvasRef;

        private Game.Field field = Game.createField(10, 400.0, 300.0);

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this.ctx = await this.CanvasRef.CreateCanvas2DAsync();
            await JsRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            await base.OnInitializedAsync();
        }

        protected void MouseMove(MouseEventArgs e) {
            field.mouse = new Tuple<double, double>(e.OffsetX, e.OffsetY);
            if (e.Buttons == 1) {
                Game.pushStrokePoint(field);
            }
        }
        protected void MouseUp(MouseEventArgs e)
        {
            Game.closeStroke(field);
        }

        protected void MouseLeave(MouseEventArgs e)
        {
            field.mouse = null;
        }

        [JSInvokable]
        public void ResizeInBlazor(double width, double height)
        {
            this.field.width = width;
            this.field.height = height;
        }

        [JSInvokable]
        public async ValueTask RenderInBlazor(float timeStamp)
        {
            await Game.render(this.ctx, this.field);
        }

    }
}
