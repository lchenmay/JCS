using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

using WebLogics;

namespace BlazorWebAssembly.Components
{
    public partial class GameField
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

        protected void MouseMove(MouseEventArgs e)
        {
            field.mouse = new Tuple<double, double>(e.OffsetX, e.OffsetY);
            //if (e.Buttons == 1)
            //{
            //    Game.pushStrokePoint(field);
            //}
        }
        protected void MouseUp(MouseEventArgs e)
        {
            //Game.closeStroke(field);
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
            //this.field.hor = 0.5 * this.field.width;
        }

        [JSInvokable]
        public async ValueTask RenderInBlazor(float timeStamp)
        {
            await Game.render(this.ctx, this.field);
        }

        public void Move(MouseEventArgs e,double offset) {

            var hor = this.field.hor + offset;

            if (hor > 0.0 && hor < this.field.width)
            {
                this.field.hor = hor;
            }
        }
    }
}
