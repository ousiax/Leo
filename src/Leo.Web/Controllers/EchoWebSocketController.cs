using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Leo.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("ws")]
    [ApiController]
    public class EchoWebSocketController : ControllerBase
    {
        [Route("echo")]
        public async Task GetEchoAsync()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var ws = await HttpContext.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);
                await EchoAsync(ws, HttpContext.RequestAborted);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        public async Task EchoAsync(WebSocket ws, CancellationToken cancellationToken = default)
        {
            var buffer = new byte[4096];
            var recv = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

            while (!recv.CloseStatus.HasValue)
            {
                await ws.SendAsync(
                    new ArraySegment<byte>(buffer, 0, recv.Count),
                    recv.MessageType,
                    recv.EndOfMessage,
                    cancellationToken).ConfigureAwait(false);

                recv = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);
            }

            await ws.CloseAsync(
                recv.CloseStatus.Value,
                recv.CloseStatusDescription,
                cancellationToken).ConfigureAwait(false);
        }
    }
}
