using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.Data.Domain.Dtos;
using Leo.UI;
using Leo.Wpf.App.Messages;

namespace Leo.Wpf.App.ViewModels
{
    public partial class NewCustomerDetailViewModel(
        ICustomerDetailService _detailService,
        IMapper _mapper,
        IMessenger _messenger) : ObservableObject
    {
        [ObservableProperty]
        private Guid _id;

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
            var dto = _mapper.Map<CustomerDetailDto>(this);
            var detailId = await _detailService.CreateAsync(dto);
            _messenger.Send(new CustomerDetailCreatedMessage(CustomerId, detailId));
            _messenger.Send(new CloseWindowMessage());
        }

        [RelayCommand]
        private void Close() { }

        public record CloseWindowMessage();
    }
}
