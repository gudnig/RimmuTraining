using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class GetMembers : IQuery<List<Member>>
    {
    }
    public class GetMembersQueryHandler : IQueryHandler<GetMembers, List<Member>>
    {
        private RimmuDbContext dbContext;
        public GetMembersQueryHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Member>> HandleAsync(GetMembers query)
        {
            var result = new List<Member>();
            var members = await dbContext.Members.Include(m => m.Events).Include(m => m.User).ToListAsync();
            foreach (var member in members)
            {
                var memberItem = new Member
                {
                    Id = member.Id,
                    Name = member.Name,

                };
                if (member.User != null)
                {
                    memberItem.User = new User
                    {
                        Id = member.User.Id,
                        UserName = member.User.UserName
                    };
                }
                if (member.Events.Any(e => e.Title == "Became Archery Trainer"))
                {
                    memberItem.Activities.Add("Archery Trainer");
                }
                if (member.Events.Any(e => e.Title == "Became Fighting Trainer"))
                {
                    memberItem.Activities.Add("Fighting Trainer");
                }
                if (member.Events.Any(e => e.Title == "Started Fighting"))
                {
                    memberItem.Activities.Add("Fighter");
                }
                if (member.Events.Any(e => e.Title == "Started Archery"))
                {
                    memberItem.Activities.Add("Archery");
                }
                result.Add(memberItem);
            }
            return result;
        }
    }
}
