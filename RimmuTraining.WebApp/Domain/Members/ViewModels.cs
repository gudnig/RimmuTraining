using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }

    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Activities { get; set; }
        public User User { get; set; }

    }
}
