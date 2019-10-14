using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Data
{
    public class Attendance    
    {
        public Guid Id { get; set; }
        public Guid PracticeId { get; set; }
        public Guid MemberId { get; set; }
        public Practice Practice { get; set; }
        public Member Member { get; set; }
    }
}
