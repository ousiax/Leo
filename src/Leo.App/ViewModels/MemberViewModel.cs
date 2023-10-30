using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Leo.App.ViewModels
{
    public class MemberViewModel : INotifyPropertyChanged
    {
        private Guid _id;
        private string? _name;
        private string? _phone;
        private string? _gender;
        private DateTime? _birthday;
        private string? _cardNo;
        private readonly List<MemberDetailViewModel> _details = new();

        public Guid Id
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
                    var num = Math.Round(DateTime.Now.Subtract(Birthday.Value).TotalDays / 365, 1);
                    return string.Format("{0} 岁", num);
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

        public List<MemberDetailViewModel> Details
        {
            get { return _details; }
        }
    }
}
