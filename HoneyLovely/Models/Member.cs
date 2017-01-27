using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HoneyLovely.Models
{
    public class Member : INotifyPropertyChanged
    {
        private Guid _id;
        private string _name;
        private string _phone;
        private string _gender;
        private DateTime _birthday;
        private string _cardNo;
        private readonly List<MemberDetail> _detail = new List<MemberDetail>();

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return DateTime.MinValue.Equals(_birthday) ? DateTime.Now : _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get
            {
                if (Birthday == null)
                {
                    return null;
                }
                else
                {
                    var num = Math.Round(DateTime.Now.Subtract(Birthday).TotalDays / 365, 1);
                    return string.Format("{0} 岁", num);
                }
            }
        }

        public string CardNo
        {
            get { return _cardNo; }
            set
            {
                _cardNo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<MemberDetail> Detail
        {
            get { return _detail; }
        }

        public void Dump(Member mem)
        {
            this.Name = mem.Name;
            this.Gender = mem.Gender;
            this.Birthday = mem.Birthday;
            this.Id = mem.Id;
            this.Phone = mem.Phone;
            this.CardNo = mem.CardNo;
            this._detail.Clear();
            this.Detail.AddRange(mem.Detail);
        }
    }
}
