using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class RegisterAttendance
    {
        public Guid PracticeId { get; set; }
        public Guid MemberId { get; set; }
    }

    public class RegisterAttendanceCommandHandler : ICommandHandler<RegisterAttendance>
    {
        private readonly RimmuDbContext dbContext;
        public RegisterAttendanceCommandHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result> HandleAsync(RegisterAttendance command)
        {
            var attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                MemberId = command.MemberId,
                PracticeId = command.PracticeId
            };
            await dbContext.Attendances.AddAsync(attendance);
            await dbContext.SaveChangesAsync();
            return new Result();
        }
    }
}
