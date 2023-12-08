using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using System.Collections.ObjectModel;

namespace Leo.Wpf.App.ViewModels
{
    public partial class FindCustomerViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isLoading;

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IMessenger _messenger;

        public FindCustomerViewModel(
            ICustomerService customerService,
            IMapper mapper,
            IMessenger messenger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _messenger = messenger;
            _ = LoadAllCustomersAsync();
        }

        public ObservableCollection<CustomerViewModel> AllCustomers { get; set; } = [];

        private async Task LoadAllCustomersAsync()
        {
            IsLoading = true;

            var dtos = await _customerService.GetAsync();
            foreach (var dto in dtos)
            {
                AllCustomers.Add(_mapper.Map<CustomerViewModel>(dto));
            }

            IsLoading = false;
        }
    }
}
