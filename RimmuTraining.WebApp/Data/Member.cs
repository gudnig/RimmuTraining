using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Data
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public   RimmuUser User { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<MemberEvents> Events { get; set; }
    }
}
