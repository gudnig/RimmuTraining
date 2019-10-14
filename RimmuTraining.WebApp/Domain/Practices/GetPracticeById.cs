using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;  
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class GetPracticeById : IQuery<PracticeDetailItem>
    {
        public Guid Id { get; set; }
    }
    public class GetPracticeByIdQueryHandler : IQueryHandler<GetPracticeById, PracticeDetailItem>
    {
        private readonly RimmuDbContext dbContext;
        public GetPracticeByIdQueryHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PracticeDetailItem> HandleAsync(GetPracticeById query)
        {
            var practice = await dbContext.Practices.Include(p => p.StartingMember).Include(p => p.Attendances).FirstOrDefaultAsync(p => p.Id == query.Id);
            var members = await dbContext.Members.Include(m => m.Attendances).ThenInclude(a => a.Practice).ToListAsync();
            var nonAttendingMembers = members.Where(m => !m.Attendances.Any(a => a.Practice.Id == practice.Id)).ToList();

                return new PracticeDetailItem
            {
                AttendingMembers = practice.Attendances.Select(a => new Member
                {
                    Id = a.Member.Id,
                    Name = a.Member.Name
                }).OrderBy(m => m.Name).ToList(),
                NonAttendingMembers = nonAttendingMembers.OrderBy(m => m.Name).Select(m => new Member
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                Id = practice.Id,
                End = practice.EndTime,
                Start = practice.StartTime,
                TrainerName = practice.StartingMember.Name,
                Type = practice.Type                
            };
            
        }
    }
}
