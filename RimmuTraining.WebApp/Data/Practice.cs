using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Data
{
    public class Practice
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid StartingMemberId { get; set; }
        public Member StartingMember { get; set; }
        public string Type { get; set; }

        public ICollection<Attendance> Attendances{ get; set; }
    }
}
