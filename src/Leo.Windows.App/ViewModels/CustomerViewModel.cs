// MIT License

using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Leo.Windows.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private string? _id;
        private string? _name;
        private string? _phone;
        private string? _gender;
        private DateTime? _birthday;
        private string? _cardNo;
        private readonly List<CustomerDetailViewModel> _details = [];

        public string? Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string? Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string? Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string? GenderText
        {
            get
            {
                return new Dictionary<string, string> {
                    {"Unknown", "未知" },
                    {"Male","男" },
                    {"Female","女" },
                }
                .FirstOrDefault(d => d.Key == Gender).Value;
            }
        }

        public DateTime? Birthday
        {
            get { return _birthday == null ? DateTime.MinValue : _birthday.Value; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

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
                    double num = Math.Round(DateTime.Now.Subtract(Birthday.Value).TotalDays / 365, 1);
                    return string.Format(CultureInfo.InvariantCulture, "{0} 岁", num);
                }
            }
        }

        public string? CardNo
        {
            get { return _cardNo; }
            set
            {
                _cardNo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<CustomerDetailViewModel> Details
        {
            get { return _details; }
        }
    }
}
