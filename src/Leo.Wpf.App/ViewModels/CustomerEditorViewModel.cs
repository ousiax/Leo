// MIT License

using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using Leo.UI.Services.Models;
using Leo.Wpf.App.Messages;

namespace Leo.Wpf.App.ViewModels
{
    public partial class CustomerEditorViewModel : ObservableObject
    {
        private CustomerViewModel? _seletedCustomer;

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
            SaveCommand = new AsyncRelayCommand(SaveAsync, () => SeletedCustomer != null);
        }

        public CustomerViewModel? SeletedCustomer
        {
            get { return _seletedCustomer; }
            set
            {
                if (_seletedCustomer != value)
                {
                    SetProperty(ref _seletedCustomer, value);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand SaveCommand { get; init; }

        public event Action? CloseAction;

        public async Task LoadSeletedCustomerAsync(string customerId)
        {
            ArgumentException.ThrowIfNullOrEmpty(customerId, nameof(customerId));

            var dto = await _customerService.GetAsync(customerId);
            var customerViewModel = _mapper.Map<CustomerViewModel>(dto);
            if (customerViewModel != null)
            {
                SeletedCustomer = customerViewModel;
            }
        }

        private async Task SaveAsync()
        {
            if (SeletedCustomer != null)
            {
                var dto = _mapper.Map<CustomerDto>(this.SeletedCustomer);
                await _customerService.UpdateAsync(dto);
                _messenger.Send(new CustomerCreatedMessage(SeletedCustomer.Id));
            }
            Close();
        }

        [RelayCommand]
        private void Close() => CloseAction?.Invoke();
    }
}
