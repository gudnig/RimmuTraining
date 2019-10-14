using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class PracticeDetailItem
    {
        public Guid Id { get; set; }
        public string TrainerName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public List<Member> AttendingMembers { get; set; }
        public List<Member> NonAttendingMembers { get; set; }
    }

    public class PracticeListItem
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NumberOfAttendees { get; set; }
    }
}
