using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class GetUnconnectedUsers : IQuery<List<User>>
    {
    }

    public class GetUnconnectedUsersQueryHandler : IQueryHandler<GetUnconnectedUsers, List<User>>
    {
        private RimmuDbContext dbContext;
        public GetUnconnectedUsersQueryHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<User>> HandleAsync(GetUnconnectedUsers query)
        {
            return await dbContext.Users.Where(u => u.Member == null).Select(u => new User { Id = u.Id, UserName = u.UserName }).ToListAsync();            
        }
    }
}
