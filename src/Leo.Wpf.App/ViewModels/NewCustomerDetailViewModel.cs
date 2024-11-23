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
    public partial class NewCustomerDetailViewModel(
        ICustomerDetailService detailService,
        IMapper mapper,
        IMessenger messenger) : ObservableObject
    {
        [ObservableProperty]
        private string? _id;

        [ObservableProperty]
        private string? _customerId;

        [ObservableProperty]
        private DateTime? _date = DateTime.Now;

        [ObservableProperty]
        private string? _item;

        [ObservableProperty]
        private int _count;

        [ObservableProperty]
        private double _height;

        [ObservableProperty]
        private double _weight;

        [RelayCommand]
        private async Task SaveAsync()
        {
            CustomerDetailDto dto = mapper.Map<CustomerDetailDto>(this);
            string? detailId = await detailService.CreateAsync(dto);
            messenger.Send(new CustomerDetailCreatedMessage(CustomerId, detailId));
            messenger.Send(new CloseWindowMessage());
        }

        [RelayCommand]
        private void Close() { CloseAction?.Invoke(); }

        public event Action? CloseAction;

        public record CloseWindowMessage();
    }
}
