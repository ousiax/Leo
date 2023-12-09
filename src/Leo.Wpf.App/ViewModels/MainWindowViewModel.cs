using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using Leo.Wpf.App.Messages;

namespace Leo.Wpf.App.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private CustomerViewModel? _currentCustomer;

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly INewCustomerWindowService _newCustomerWindow;
        private readonly IFindWindowService _findWindow;

        public MainWindowViewModel(
            ICustomerService customerService,
            IMapper mapper,
            INewCustomerWindowService newCustomerWindowService,
            IFindWindowService findWindowService,
            IMessenger messenger) : base(messenger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _newCustomerWindow = newCustomerWindowService;
            _findWindow = findWindowService;

            Messenger.Register<CustomerCreatedMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.Id));
            });

            Messenger.Register<CustomerFoundMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.Id));
            });
        }

        [RelayCommand]
        private void NewCustomer()
        {
            _newCustomerWindow.ShowDialog();
        }

        [RelayCommand]
        private void FindCustomer()
        {
            _findWindow.ShowDialog();
        }

        private async Task ReloadCurrentCustomerAsync(string? id)
        {
            if (string.IsNullOrEmpty(id)) return;

            var dto = await _customerService.GetAsync(Guid.Parse(id));
            if (dto != null)
            {
                this.CurrentCustomer = _mapper.Map<CustomerViewModel>(dto);
            }
        }
    }
}
