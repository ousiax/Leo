using Leo.UI.Options;
using Leo.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;

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

            this.txtAddress.Text = new UriBuilder(webOptions.Value.BaseAddress!)
            {
                Scheme = Uri.UriSchemeWs,
                Path = DEFAULT_WS_PATH
            }.Uri.ToString();

            this.FormClosed += async (s, a) =>
            {
                await DisconnectAsync();
            };
        }

        private Uri WsUri => new(this.txtAddress.Text);

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_ws?.State == WebSocketState.Open)
            {
                await DisconnectAsync();
                btnConnect.Text = "Connect";
                var direction = "<-";
                var message = $"Disconnected from {WsUri}";
                AppendListView(direction, message);
                btnSend.Enabled = false;
            }
            else
            {
                _ws = new ClientWebSocket();
                var result = await _authenticationService.ExecuteAsync();
                _ws.Options.SetRequestHeader(HeaderNames.Authorization, $"Bearer {result.IdToken}");
                _ws.Options.KeepAliveInterval = TimeSpan.FromMinutes(5);

                await _ws.ConnectAsync(WsUri, CancellationToken.None);
                btnConnect.Text = "Disconnect";
                btnSend.Enabled = true;
                lstvResponse.Items.Clear();
                var message = $"Connected to {WsUri}";
                var direction = "->";
                AppendListView(direction, message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var cancellationToken = CancellationToken.None;

            await _ws!.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(txtMessage.Text)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                cancellationToken);
            AppendListView("->", txtMessage.Text);

            var BUFFER_SIZE = 1024 * 4;
            var buffer = new byte[BUFFER_SIZE];
            var msg = new List<byte>(BUFFER_SIZE);
            var recv = await _ws!.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
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
                new ListViewItem(new string[] {
                    string.Empty,
                    direction, message,
                    DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture) })
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
