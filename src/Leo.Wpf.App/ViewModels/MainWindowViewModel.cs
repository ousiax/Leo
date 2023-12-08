using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using Leo.Wpf.App.Messages;
using Leo.Wpf.App.Services;

namespace Leo.Wpf.App.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private CustomerViewModel? _currentCustomer;

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly INewCustomerWindowService _newCustomerWindow;

        public MainWindowViewModel(
            ICustomerService customerService,
            IMapper mapper,
            INewCustomerWindowService newCustomerWindowService,
            IMessenger messenger) : base(messenger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _newCustomerWindow = newCustomerWindowService;

            Messenger.Register<CustomerCreatedMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.Id));
            });
        }

        [RelayCommand]
        private void NewCustomer()
        {
            _newCustomerWindow.ShowDialog();
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
