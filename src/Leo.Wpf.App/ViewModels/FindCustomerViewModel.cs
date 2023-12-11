using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using Leo.Wpf.App.Messages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Leo.Wpf.App.ViewModels
{
    public partial class FindCustomerViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string? _searchField = "name";

        [ObservableProperty]
        private string? _searchText;

        [ObservableProperty]
        private CustomerViewModel? _selectedCustomer;

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

            AllCustomersView = CollectionViewSource.GetDefaultView(AllCustomers);

            _ = LoadAllCustomersAsync();

            // TODO i18n
            if (CultureInfo.CurrentUICulture.Name.Contains("zh"))
            {
                SearchFields = new() {
                    { "name", "姓名" },
                    { "phone", "手机" },
                    { "card", "卡号" },
                };
            }
            else
            {
                SearchFields = new() {
                    { "name", "Name" },
                    { "phone", "Phone" },
                    { "card", "CardNo" },
                };
            }
        }

        public ObservableCollection<CustomerViewModel> AllCustomers { get; set; } = [];

        public ICollectionView AllCustomersView { get; }

        public Dictionary<string, string> SearchFields { get; } = new()
        {
            { "name", "姓名" },
            { "phone", "手机" },
            { "card", "卡号" },
        };

        [RelayCommand]
        public void Find()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;

            Predicate<object> filter = o => true;
            switch (SearchField)
            {
                case "name":
                    filter = item =>
                    {
                        var viewModel = (CustomerViewModel)item;
                        var filter = viewModel.Name?.Contains(SearchText);
                        return filter ?? true;
                    };
                    break;
                case "card":
                    filter = item =>
                    {
                        var viewModel = (CustomerViewModel)item;
                        var filter = viewModel.CardNo?.Contains(SearchText);
                        return filter ?? true;
                    };
                    break;
                case "phone":
                    filter = item =>
                    {
                        var viewModel = (CustomerViewModel)item;
                        var filter = viewModel.Phone?.Contains(SearchText);
                        return filter ?? true;
                    };
                    break;
            }
            AllCustomersView.Filter = filter;
        }

        [RelayCommand]
        public void Reset()
        {
            AllCustomersView.Filter = null;
        }

        [RelayCommand]
        public void Confirm()
        {
            _messenger.Send(new CustomerFoundMessage(SelectedCustomer!.Id));
            _messenger.Send(new CloseWindowMessage());
        }

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

        public record CloseWindowMessage();
    }
}
