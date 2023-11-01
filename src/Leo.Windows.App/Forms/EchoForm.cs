using Leo.UI.Options;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;

namespace Leo.Windows.Forms
{
    public partial class EchoForm : Form
    {
        private ClientWebSocket? _ws;
        private readonly Uri _baseAddress;

        public EchoForm(IOptions<WebOptions> webOptions)
        {
            _baseAddress = new UriBuilder(webOptions.Value.BaseAddress!) { Scheme = Uri.UriSchemeWs }.Uri;
            InitializeComponent();
            this.FormClosed += async (s, a) =>
            {
                if (_ws?.State == WebSocketState.Open)
                {
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                }
            };
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_ws?.State == WebSocketState.Open)
            {
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                btnConnect.Text = "Connect";
                var direction = "<-";
                var message = $"Disconnected from {WsUri}";
                AppendListView(direction, message);
                btnSend.Enabled = false;
            }
            else
            {
                _ws = new ClientWebSocket();
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
            await _ws!.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(txtMessage.Text)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.None,
                CancellationToken.None);
            AppendListView("->", txtMessage.Text);

            var buffer = new byte[1024 * 4];
            var recv = await _ws!.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            AppendListView("<-", Encoding.UTF8.GetString(buffer, 0, recv.Count)); // read only once
        }

        private Uri WsUri => new UriBuilder(_baseAddress) { Path = txtAddress.Text }.Uri;

        private void AppendListView(string direction, string message)
        {
            lstvResponse.Items.Insert(0, new ListViewItem(new string[] { string.Empty, direction, message, DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentUICulture) }));
        }
    }
}
