using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Practices
{
    public class PracticesService
    {
        private readonly ICommandHandler<StartPractice> startPracticeCommandHandler;
        private readonly IQueryHandler<GetPracticeById, PracticeDetailItem> practiceDetailQueryHandler;
        private readonly IQueryHandler<GetPractices, List<PracticeListItem>> practiceListQueryHandler;
        private readonly ICommandHandler<RegisterAttendance> registerAttendanceCommandHandler;
        private readonly ICommandHandler<RemoveAttendance> removeAttendanceCommandHandler;

        public PracticesService(ICommandHandler<StartPractice> startPracticeCommandHandler,
            IQueryHandler<GetPracticeById, PracticeDetailItem> practiceDetailQueryHandler,
            IQueryHandler<GetPractices, List<PracticeListItem>> practiceListQueryHandler,
            ICommandHandler<RegisterAttendance> registerAttendanceCommandHandler,
            ICommandHandler<RemoveAttendance> removeAttendanceCommandHandler
            )
        {
            this.startPracticeCommandHandler = startPracticeCommandHandler;
            this.practiceDetailQueryHandler = practiceDetailQueryHandler;
            this.practiceListQueryHandler = practiceListQueryHandler;
            this.registerAttendanceCommandHandler = registerAttendanceCommandHandler;
            this.removeAttendanceCommandHandler = removeAttendanceCommandHandler;
        }

        public async Task<Result> StartPracticeAsync(Guid id, DateTime start, DateTime end, string userName, string type)
        {
            return await startPracticeCommandHandler.HandleAsync(new StartPractice
            {
                Id = id,
                End = end,
                Start = start,
                StartingUser = userName,
                Type = type
            });
        }

        public async Task<PracticeDetailItem> GetPracticeAsync(Guid id)
        {
            return await practiceDetailQueryHandler.HandleAsync(new GetPracticeById
            {
                Id = id
            });
        }
        public async Task<List<PracticeListItem>> GetPracticesAsync()
        {
            return await practiceListQueryHandler.HandleAsync(new GetPractices
            {

            });
        }

        public async Task<Result> RegisterAttendanceAsync(Guid memberId, Guid practiceId)
        {
            return await registerAttendanceCommandHandler.HandleAsync(new RegisterAttendance
            {
                MemberId = memberId,
                PracticeId = practiceId
            });
        }
        public async Task<Result> RemoveAttendanceAsync(Guid memberId, Guid practiceId)
        {
            return await removeAttendanceCommandHandler.HandleAsync(new RemoveAttendance
            {
                MemberId = memberId,
                PracticeId = practiceId
            });
        }
    }
}
