using Microsoft.AspNetCore.Identity;
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
        private readonly RimmuDbContext dbContext;
        private readonly UserManager<RimmuUser> userManager;
        public GetMembersQueryHandler(RimmuDbContext dbContext, UserManager<RimmuUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
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
                        UserName = member.User.UserName,
                        Roles = (await userManager.GetRolesAsync(member.User)).ToList()
                    };
                }
                
                result.Add(memberItem);
            }
            return result;
        }
    }
}
