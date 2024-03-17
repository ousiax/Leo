// MIT License

using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI.Services;
using Leo.UI.Services.Models;
using Leo.Wpf.App.Messages;

namespace Leo.Wpf.App.ViewModels
{
    public partial class CustomerEditorViewModel : ObservableObject
    {
        private CustomerViewModel? _selectedCustomer;

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IMessenger _messenger;

        public CustomerEditorViewModel(
            ICustomerService customerService,
            IMapper mapper,
            IMessenger messenger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _messenger = messenger;
            SaveCommand = new AsyncRelayCommand(SaveAsync, () => SelectedCustomer != null);
        }

        public CustomerViewModel? SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    SetProperty(ref _selectedCustomer, value);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand SaveCommand { get; init; }

        public event Action? CloseAction;

        public async Task LoadSelectedCustomerAsync(string customerId)
        {
            ArgumentException.ThrowIfNullOrEmpty(customerId, nameof(customerId));

            var dto = await _customerService.GetAsync(customerId);
            var customerViewModel = _mapper.Map<CustomerViewModel>(dto);
            if (customerViewModel != null)
            {
                SelectedCustomer = customerViewModel;
            }
        }

        private async Task SaveAsync()
        {
            if (SelectedCustomer != null)
            {
                var dto = _mapper.Map<CustomerDto>(this.SelectedCustomer);
                await _customerService.UpdateAsync(dto);
                _messenger.Send(new CustomerCreatedMessage(SelectedCustomer.Id));
            }
            Close();
        }

        [RelayCommand]
        private void Close() => CloseAction?.Invoke();
    }
}
