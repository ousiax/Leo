using CommunityToolkit.Mvvm.ComponentModel;

namespace Leo.Wpf.App.ViewModels
{
    public partial class CustomerViewModel : ObservableObject
    {
        [ObservableProperty]
        private Guid _id;

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

        private readonly List<CustomerDetailViewModel> _details = new();

        public string? Age
        {
            get
            {
                if (Birthday == null)
                {
                    return null;
                }
                else
                {
                    var num = Math.Round(DateTime.Now.Subtract(Birthday.Value).TotalDays / 365, 1);
                    return string.Format("{0} 岁", num);
                }
            }
        }

        public List<CustomerDetailViewModel> Details
        {
            get { return _details; }
        }
    }
}
