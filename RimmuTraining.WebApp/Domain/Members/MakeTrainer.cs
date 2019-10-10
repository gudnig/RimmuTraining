using Microsoft.AspNetCore.Identity;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class MakeTrainer
    {
        public Guid MemberId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
    public class MakeTrainerCommandHandler : ICommandHandler<MakeTrainer>
    {
        private RimmuDbContext dbContext;
        private UserManager<RimmuUser> userManager;
        public MakeTrainerCommandHandler(RimmuDbContext dbContext, UserManager<RimmuUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Result> HandleAsync(MakeTrainer command)
        {
            if (!TrainingTypes.Get().Contains(command.Type))
            {
                return new Result("Invalid training type.");
            }
            var member = await dbContext.Members.FindAsync(command.MemberId);
            if (member != null)
            {
                if (member.User != null)
                {
                    member.Events.Add(new MemberEvents
                    {
                        Date = command.Date,
                        Title = string.Format("Became {0} Trainer", command.Type)
                    });
                    var roleResult = await userManager.AddToRoleAsync(member.User, string.Format("{0} Trainer", command.Type));
                    if (roleResult.Succeeded)
                    {
                        await dbContext.SaveChangesAsync();
                        return new Result();
                    }
                    return new Result(roleResult.Errors.Select(e => e.Description).ToString());
                }
                else
                {
                    return new Result("Member does not have a user. Register a user before making him/her a trainer.");
                }
            }

            return new Result("Member not found.");
        }
    }
}
