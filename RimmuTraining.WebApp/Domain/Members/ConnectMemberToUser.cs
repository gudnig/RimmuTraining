using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class ConnectMemberToUser
    {
        public Guid UserId { get; set; }
        public Guid MemberId { get; set; }
    }
    public class ConnectMemberToUserCommandHandler : ICommandHandler<ConnectMemberToUser>
    {
        private readonly RimmuDbContext dbContext;
        public ConnectMemberToUserCommandHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result> HandleAsync(ConnectMemberToUser command)
        {
            var member = await dbContext.Members.FindAsync(command.MemberId);
            if(member == null)
            {
                return new Result("Member not found.");
            }
            member.UserId = command.UserId;
            dbContext.SaveChanges();
            return new Result();
        }
    }
}
