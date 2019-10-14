using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class RemoveAttendance
    {
        public Guid MemberId { get; set; }
        public Guid PracticeId { get; set; }
    }

    public class RemoveAttendanceCommandHandler : ICommandHandler<RemoveAttendance>
    {
        private readonly RimmuDbContext dbContext;

        public RemoveAttendanceCommandHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> HandleAsync(RemoveAttendance command)
        {
            var attendance = await dbContext.Attendances.FirstOrDefaultAsync(a => a.MemberId == command.MemberId && a.PracticeId == command.PracticeId);
            dbContext.Attendances.Remove(attendance);
            await dbContext.SaveChangesAsync();
            return new Result();
        }
    }
}
