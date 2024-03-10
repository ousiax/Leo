using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Leo.UI.Options;
using Leo.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;

namespace Leo.Wpf.App.ViewModels
{
    public partial class EchoViewModel : ObservableValidator, IDisposable
    {
        private const string DEFAULT_WS_PATH = "/ws/echo";
        private readonly IAuthenticationService _authenticationService;
        private ClientWebSocket? _ws;

        [ObservableProperty]
        private Uri? _address;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendCommand))]
        private string? _message;

        [ObservableProperty]
        private ObservableCollection<PingPong> _pingPongs = [];

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendCommand))]
        private string? _connectButtonSate = "Connect";

        private WebSocketState? State { get { return _ws?.State; } }

        public EchoViewModel(IOptions<WebOptions> webOptions, IAuthenticationService authenticationService)
        {
            Address = new UriBuilder(webOptions.Value.BaseAddress!)
            {
                Scheme = Uri.UriSchemeWs,
                Path = DEFAULT_WS_PATH
            }.Uri;
            _authenticationService = authenticationService;
        }

        [RelayCommand]
        private async Task ConnectAsync()
        {
            if (_ws?.State == WebSocketState.Open)
            {
                ConnectButtonSate = "Connect";
                await DisconnectAsync();
                var direction = "<-";
                var message = $"Disconnected from {Address}";
                AppendListView(direction, message);
            }
            else
            {
                _ws = new ClientWebSocket();
                var result = await _authenticationService.ExecuteAsync();
                _ws.Options.SetRequestHeader(HeaderNames.Authorization, $"Bearer {result.IdToken}");
                _ws.Options.KeepAliveInterval = TimeSpan.FromMinutes(5);

                await _ws.ConnectAsync(Address!, CancellationToken.None);
                ConnectButtonSate = "Disconnect";

                PingPongs.Clear();
                var message = $"Connected to {Address}";
                var direction = "->";
                AppendListView(direction, message);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSend))]
        private async Task SendAsync()
        {
            var cancellationToken = CancellationToken.None;
            await _ws!.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(Message!)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                cancellationToken);
            AppendListView("->", Message!);

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

        private bool CanSend()
        {
            return !string.IsNullOrEmpty(Message) &&
                string.Equals("Disconnect", ConnectButtonSate, StringComparison.OrdinalIgnoreCase) &&
                State == WebSocketState.Open;
        }

        private void AppendListView(string direction, string message)
        {
            PingPongs.Insert(0, new PingPong { Icon = direction, Message = message, Time = DateTime.Now });
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

        public void Dispose()
        {
            _ws?.Dispose();
            GC.SuppressFinalize(this);
        }

        public partial class PingPong : ObservableObject
        {
            [ObservableProperty]
            private string? _icon;

            [ObservableProperty]
            private string? _message;

            [ObservableProperty]
            private DateTime? _time;
        }
    }
}
