using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class StartPractice
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public string StartingUser { get; set; }
    }

    public class StartPracticeCommandHandler : ICommandHandler<StartPractice>
    {
        private readonly RimmuDbContext dbContext;
        public StartPracticeCommandHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> HandleAsync(StartPractice command)
        {
            var user = await dbContext.Users.Include(m => m.Member).FirstOrDefaultAsync(u => u.UserName == command.StartingUser);
            var practice = new Practice
            {
                EndTime = command.End,
                Id = command.Id,
                StartingMemberId = user.Member.Id,
                StartTime = command.Start,
                Type = command.Type

            };
            await dbContext.Practices.AddAsync(practice);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
            
            return new Result();
        }
    }
}