// MIT License

using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.UI.Services;
using Leo.Wpf.App.Messages;
using Microsoft.Extensions.Logging;
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
        private readonly IEchoWindowService _echoWindow;
        private readonly ILogger<MainWindowViewModel> _logger;

        public MainWindowViewModel(
            ICustomerService customerService,
            ICustomerDetailService detailService,
            IMapper mapper,
            INewCustomerWindowService newCustomerWindowService,
            ICustomerEditorWindowService customerEditorWindow,
            INewCustomerDetailWindowService newCustomerDetailWindowService,
            IFindWindowService findWindowService,
            IEchoWindowService echoWindowService,
            IMessenger messenger,
            ILogger<MainWindowViewModel> logger) : base(messenger)
        {
            _customerService = customerService;
            _detailService = detailService;
            _mapper = mapper;
            _newCustomerWindow = newCustomerWindowService;
            _customerEditorWindow = customerEditorWindow;
            _newCustomerDetailWindow = newCustomerDetailWindowService;
            _findWindow = findWindowService;
            _echoWindow = echoWindowService;
            _logger = logger;

            Messenger.Register<CustomerCreatedMessage>(this, (rcpt, msg) =>
            {
                _ = ReloadCurrentCustomerAsync(msg.Id);
            });

            Messenger.Register<CustomerFoundMessage>(this, (rcpt, msg) =>
            {
                ReloadCurrentCustomerAsync(msg.Id).ContinueWith(t =>
                {
                    if (t.Exception is not null)
                    {
                        _logger.LogError(t.Exception, t.Exception.Message);
                    }
                });
            });

            Messenger.Register<CustomerDetailCreatedMessage>(this, (rcpt, msg) =>
            {
                Task.Factory.StartNew(() => ReloadCurrentCustomerAsync(msg.customerId),
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext());
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

        [RelayCommand]
        private void Echo()
        {
            _echoWindow.Show();
        }

        private static bool CanEditCustomer(CustomerViewModel? customer)
        {
            return customer != null;
        }

        private static bool CanNewCustomerDetail(string? customerId)
        {
            return customerId != null;
        }

        private async Task ReloadCurrentCustomerAsync(string? id)
        {
            if (string.IsNullOrEmpty(id)) return;

            UI.Services.Models.CustomerDto? dto = await _customerService.GetAsync(id);
            if (dto != null)
            {
                CustomerViewModel viewModel = _mapper.Map<CustomerViewModel>(dto);
                List<UI.Services.Models.CustomerDetailDto> detailDtos = await _detailService.GetByCustomerIdAsync(id);
                foreach (UI.Services.Models.CustomerDetailDto detailDto in detailDtos)
                {
                    CustomerDetailViewModel detailViewModel = _mapper.Map<CustomerDetailViewModel>(detailDto);
                    viewModel.Details.Add(detailViewModel);
                }
                CurrentCustomer = viewModel;
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
