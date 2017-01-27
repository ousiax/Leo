using System;

namespace HoneyLovely.Models
{
    public class MemberDetail
    {
        public Guid MemberId { get; set; }

        public DateTime Date { get; set; }

        public string Item { get; set; }

        public int Count { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }
    }
}
