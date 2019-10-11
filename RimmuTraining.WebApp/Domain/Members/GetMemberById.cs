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
    public class GetMemberById : IQuery<Member>
    {
        public Guid MemberId { get; set; }
    }

    public class GetMemberByIdQueryHandler : IQueryHandler<GetMemberById, Member>
    {
        private readonly RimmuDbContext dbContext;
        private readonly UserManager<RimmuUser> userManager;
        public GetMemberByIdQueryHandler(RimmuDbContext dbContext, UserManager<RimmuUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<Member> HandleAsync(GetMemberById query)
        {
            var member = await dbContext.Members.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == query.MemberId);
            var result = new Member
            {
                Id = member.Id,
                Name = member.Name,

            };
            if (member.User != null)
            {
                result.User = new User
                {
                    Id = member.User.Id,
                    UserName = member.User.UserName,
                    Roles = (await userManager.GetRolesAsync(member.User)).ToList(),
                };

            }
            // look up event history here later
            return result;
        }
    }
}
