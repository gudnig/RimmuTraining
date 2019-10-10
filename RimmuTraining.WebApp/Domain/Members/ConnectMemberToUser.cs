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
        public Task<Result> HandleAsync(ConnectMemberToUser command)
        {
            throw new NotImplementedException();
        }
    }
}
