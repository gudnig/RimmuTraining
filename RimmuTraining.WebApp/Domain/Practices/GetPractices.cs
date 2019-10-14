using Microsoft.EntityFrameworkCore;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class GetPractices : IQuery<List<PracticeListItem>>
    {
    }

    public class GetPracticesQueryHandler : IQueryHandler<GetPractices, List<PracticeListItem>>
    {
        private readonly RimmuDbContext dbContext;
        public GetPracticesQueryHandler(RimmuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<PracticeListItem>> HandleAsync(GetPractices query)
        {
            var result = await dbContext.Practices.Include(p => p.Attendances).Select(p => new PracticeListItem
            {
                Id = p.Id,
                End = p.EndTime,
                NumberOfAttendees = p.Attendances.Count,
                Start = p.StartTime,
                Type = p.Type
            }).ToListAsync();
            return result;
        }
    }
}
