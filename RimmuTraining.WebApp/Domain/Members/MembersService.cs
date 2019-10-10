using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class MembersService 
    {
        private ICommandHandler<ConnectMemberToUser> connectMemberCommandHandler;
        private ICommandHandler<CreateMember> createMemberCommandHandler;
        private ICommandHandler<MakeTrainer> makeTrainerCommandHandler;
        private IQueryHandler<GetUnconnectedUsers, List<User>> getUnconnectedUsersQueryHandler;
        private IQueryHandler<GetMembers, List<Member>> getMembersQueryHandler;
        public MembersService(
            ICommandHandler<ConnectMemberToUser> connectMemberCommandHandler, 
            ICommandHandler<CreateMember> createMemberCommandHandler, 
            ICommandHandler<MakeTrainer> makeTrainerCommandHandler,
            IQueryHandler<GetUnconnectedUsers, List<User>> getUnconnectedUsersQueryHandler,
            IQueryHandler<GetMembers, List<Member>> getMembersQueryHandler)
        {
            this.connectMemberCommandHandler = connectMemberCommandHandler;
            this.createMemberCommandHandler = createMemberCommandHandler;
            this.makeTrainerCommandHandler = makeTrainerCommandHandler;
            this.getUnconnectedUsersQueryHandler = getUnconnectedUsersQueryHandler;
            this.getMembersQueryHandler = getMembersQueryHandler;
        }

        public async Task<Result> ConnectMemberAndUserAsync(Guid memberId, Guid userId)
        {
            return await connectMemberCommandHandler.HandleAsync(new ConnectMemberToUser
            {
                MemberId = memberId,
                UserId = userId
            });
        }

        public async Task<Result> CreateMemberAsync(string name, Guid id)
        {
            return await createMemberCommandHandler.HandleAsync(new Members.CreateMember
            {
                Id = id,
                Name = name
            });
        }

        public async Task<Result> MakeTrainerAsync(Guid memberId, DateTime date, string type)
        {
            return await makeTrainerCommandHandler.HandleAsync(new MakeTrainer
            {
                Date = date,
                MemberId = memberId,
                Type = type
            });
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await getUnconnectedUsersQueryHandler.HandleAsync(new GetUnconnectedUsers
            {

            });
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            var result = await getMembersQueryHandler.HandleAsync(new GetMembers
            {

            });
            return result;
        }
    }
}
