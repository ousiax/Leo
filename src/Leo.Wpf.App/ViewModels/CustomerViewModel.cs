using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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

        public ObservableCollection<CustomerDetailViewModel> Details { get; } = [];

        public partial class CustomerDetailViewModel : ObservableObject
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
        }
    }
}
