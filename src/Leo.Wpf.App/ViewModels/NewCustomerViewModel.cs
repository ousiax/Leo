using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Entities;
using Leo.UI;
using Leo.Wpf.App.Messages;
using System.ComponentModel.DataAnnotations;

namespace Leo.Wpf.App.ViewModels
{
    public partial class NewCustomerViewModel(
        ICustomerService _customerService,
        IMapper _mapper,
        IMessenger _messenger) : ObservableValidator
    {
        [Required(AllowEmptyStrings = false)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _name;

        [Required]
        [Phone]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _phone;

        [Required]
        [EnumDataType(typeof(Gender))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _gender;

        [Required]
        [DataType(DataType.Date)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private DateTime? _birthday; // DateOnly

        [Required(AllowEmptyStrings = false)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _cardNo;

        [RelayCommand]
        private void Close() => CloseAction?.Invoke();

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            var dto = _mapper.Map<CustomerDto>(this);
            var id = await _customerService.CreateAsync(dto);
            _messenger.Send(new CustomerCreatedMessage(id));
            Close();
        }

        public event Action? CloseAction;

        private bool CanSave()
        {
            return !HasErrors && Validate();
        }

        private bool Validate()
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(this);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}