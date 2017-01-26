using System;
using System.Collections.Generic;

namespace HoneyLovely.Models
{
    public class Member
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Age
        {
            get
            {
                var num = Math.Round(DateTime.Now.Subtract(Birthday).TotalDays / 365, 1);
                return string.Format("{0} 岁", num);
            }
        }

        public string CardNo { get; set; }

        public IList<MemberDetail> Detail { get; set; }
    }
}
