using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Data
{
    public class RimmuUser : IdentityUser<Guid>
    {
        public Member Member { get; set; }
    }
}
