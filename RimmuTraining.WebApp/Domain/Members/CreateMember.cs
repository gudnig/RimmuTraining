using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class CreateMember
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateMemberCommandHandler : ICommandHandler<CreateMember>
    {
        private RimmuDbContext dbContext;
        public CreateMemberCommandHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<Result> HandleAsync(CreateMember command)
        {
            var member = new Data.Member
            {
                Id = command.Id,
                Name = command.Name
            };
            try
            {
                await dbContext.Members.AddAsync(member);
                await dbContext.SaveChangesAsync();
                return new Result();
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
    }
}
