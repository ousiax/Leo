using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.Data.Domain.Dtos;
using Leo.UI;
using Leo.Wpf.App.Messages;

namespace Leo.Wpf.App.ViewModels
{
    public partial class NewCustomerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _gender;

        [ObservableProperty]
        private DateTime? _birthday;

        [ObservableProperty]
        private string? _cardNo;

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IMessenger _messenger;

        public NewCustomerViewModel(
            ICustomerService customerService,
            IMapper mapper,
            IMessenger messenger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _messenger = messenger;
        }

        [RelayCommand]
        private void Close() => CloseAction?.Invoke();

        [RelayCommand]
        private async Task SaveAsync()
        {
            var dto = _mapper.Map<CustomerDto>(this);
            var id = await _customerService.CreateAsync(dto);
            _messenger.Send(new CustomerCreatedMessage { Id = id });
            Close();
        }

        public event Action? CloseAction;
    }
}