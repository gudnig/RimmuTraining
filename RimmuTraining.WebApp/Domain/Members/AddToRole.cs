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
    public class AddToRole
    {
        public Guid MemberId { get; set; }
        public string Role { get; set; }
    }

    public class AddToRoleCommandHandler : ICommandHandler<AddToRole>
    {
        private readonly RimmuDbContext dbContext;
        private readonly UserManager<RimmuUser> userManager;
        public AddToRoleCommandHandler(RimmuDbContext dbContext, UserManager<RimmuUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<Result> HandleAsync(AddToRole command)
        {

            var member = await dbContext.Members.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == command.MemberId);
            if (member.User == null)
            {
                return new Result("Member is not connected to a user. Connect to user first.");
            }

            var roleResult = await userManager.AddToRoleAsync(member.User, command.Role);

            if (!roleResult.Succeeded)
            {
                return new Result(roleResult.Errors.Select(e => e.Description).ToList());
            }
            return new Result();
        }


    }

}
