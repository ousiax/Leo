using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI;
using Leo.Wpf.App.Messages;
using static Leo.Wpf.App.ViewModels.CustomerViewModel;

namespace Leo.Wpf.App.ViewModels
{
    public sealed partial class MainWindowViewModel : ObservableRecipient, IDisposable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditCustomerCommand))]
        [NotifyCanExecuteChangedFor(nameof(NewCustomerDetailCommand))]
        private CustomerViewModel? _currentCustomer;

        private bool disposedValue;
        private readonly ICustomerService _customerService;
        private readonly ICustomerDetailService _detailService;
        private readonly IMapper _mapper;
        private readonly INewCustomerWindowService _newCustomerWindow;
        private readonly ICustomerEditorWindowService _customerEditorWindow;
        private readonly INewCustomerDetailWindowService _newCustomerDetailWindow;
        private readonly IFindWindowService _findWindow;

        public MainWindowViewModel(
            ICustomerService customerService,
            ICustomerDetailService detailService,
            IMapper mapper,
            INewCustomerWindowService newCustomerWindowService,
            ICustomerEditorWindowService customerEditorWindow,
            INewCustomerDetailWindowService newCustomerDetailWindowService,
            IFindWindowService findWindowService,
            IMessenger messenger) : base(messenger)
        {
            _customerService = customerService;
            _detailService = detailService;
            _mapper = mapper;
            _newCustomerWindow = newCustomerWindowService;
            _customerEditorWindow = customerEditorWindow;
            _newCustomerDetailWindow = newCustomerDetailWindowService;
            _findWindow = findWindowService;

            Messenger.Register<CustomerCreatedMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.Id));
            });

            Messenger.Register<CustomerFoundMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.Id));
            });

            Messenger.Register<CustomerDetailCreatedMessage>(this, (rcpt, msg) =>
            {
                Task.Run(() => ReloadCurrentCustomerAsync(msg.customerId));
            });
        }

        [RelayCommand]
        private void NewCustomer()
        {
            _newCustomerWindow.ShowDialog();
        }

        [RelayCommand(CanExecute = nameof(CanEditCustomer))]
        private void EditCustomer(CustomerViewModel? customer)
        {
            if (customer != null)
            {
                _customerEditorWindow.ShowDialog(customer.Id!);
            }
        }

        [RelayCommand(CanExecute = nameof(CanNewCustomerDetail))]
        private void NewCustomerDetail(string? customerId)
        {
            if (customerId != null)
            {
                _newCustomerDetailWindow.ShowDialog(customerId);
            }
        }

        [RelayCommand]
        private void FindCustomer()
        {
            _findWindow.ShowDialog();
        }

        private bool CanEditCustomer(CustomerViewModel? customer)
        {
            return customer != null;
        }

        private bool CanNewCustomerDetail(string? customerId)
        {
            return customerId != null;
        }

        private async Task ReloadCurrentCustomerAsync(string? id)
        {
            if (string.IsNullOrEmpty(id)) return;

            var dto = await _customerService.GetAsync(id);
            if (dto != null)
            {
                var viewModel = _mapper.Map<CustomerViewModel>(dto);
                var detailDtos = await _detailService.GetByCustomerIdAsync(id);
                foreach (var detailDto in detailDtos)
                {
                    var detailViewModel = _mapper.Map<CustomerDetailViewModel>(detailDto);
                    viewModel.Details.Add(detailViewModel);
                }
                this.CurrentCustomer = viewModel;
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Messenger.UnregisterAll(this);
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
