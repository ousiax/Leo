// MIT License

using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using Leo.UI.Services;
using Leo.UI.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Leo.Windows.Forms
{
    public partial class EchoForm : Form
    {
        private const string DEFAULT_WS_PATH = "/ws/echo";
        private readonly IAuthenticationService _authenticationService;
        private ClientWebSocket? _ws;

        public EchoForm(IOptions<WebOptions> webOptions, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            InitializeComponent();

            txtAddress.Text = new UriBuilder(webOptions.Value.BaseAddress!)
            {
                Scheme = Uri.UriSchemeWs,
                Path = DEFAULT_WS_PATH
            }.Uri.ToString();

            FormClosed += async (s, a) =>
            {
                await DisconnectAsync();
            };
        }

        private Uri WsUri => new(txtAddress.Text);

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_ws?.State == WebSocketState.Open)
            {
                await DisconnectAsync();
                btnConnect.Text = "Connect";
                string direction = "<-";
                string message = $"Disconnected from {WsUri}";
                AppendListView(direction, message);
                btnSend.Enabled = false;
            }
            else
            {
                _ws = new ClientWebSocket();
                Microsoft.Identity.Client.AuthenticationResult result = await _authenticationService.ExecuteAsync();
                _ws.Options.SetRequestHeader(HeaderNames.Authorization, $"Bearer {result.IdToken}");
                _ws.Options.KeepAliveInterval = TimeSpan.FromMinutes(5);

                await _ws.ConnectAsync(WsUri, CancellationToken.None);
                btnConnect.Text = "Disconnect";
                btnSend.Enabled = true;
                lstvResponse.Items.Clear();
                string message = $"Connected to {WsUri}";
                string direction = "->";
                AppendListView(direction, message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            CancellationToken cancellationToken = CancellationToken.None;

            await _ws!.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(txtMessage.Text)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                cancellationToken);
            AppendListView("->", txtMessage.Text);

            int BUFFER_SIZE = 1024 * 4;
            byte[] buffer = new byte[BUFFER_SIZE];
            var msg = new List<byte>(BUFFER_SIZE);
            WebSocketReceiveResult recv = await _ws!.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
            msg.AddRange(new ArraySegment<byte>(buffer, 0, recv.Count));
            while (!recv.EndOfMessage)
            {
                recv = await _ws!.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                msg.AddRange(new ArraySegment<byte>(buffer, 0, recv.Count));
            }
            AppendListView("<-", Encoding.UTF8.GetString(msg.ToArray()));
        }

        private void AppendListView(string direction, string message)
        {
            lstvResponse.Items.Insert(
                0,
                new ListViewItem([
                    string.Empty,
                    direction, message,
                    DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture) ])
                );
        }

        private async Task DisconnectAsync()
        {
            if (_ws?.State == WebSocketState.Open)
            {
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "NormalClosure", CancellationToken.None);
                _ws.Dispose();
                _ws = null;
            }
        }
    }
}
