using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Leo.App.Models
{
    public class MemberDetail : INotifyPropertyChanged
    {
        private Guid _id;
        private DateTime _date;
        private string _item;
        private int _count;
        private double _height;
        private double _weight;

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null && propertyName != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
