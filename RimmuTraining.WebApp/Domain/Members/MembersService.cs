using Microsoft.AspNetCore.Identity;
using RimmuTraining.WebApp.Data;
using RimmuTraining.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain.Members
{
    public class MembersService 
    {
        private readonly ICommandHandler<ConnectMemberToUser> connectMemberCommandHandler;
        private readonly ICommandHandler<CreateMember> createMemberCommandHandler;
        private readonly ICommandHandler<MakeTrainer> makeTrainerCommandHandler;
        private readonly IQueryHandler<GetUnconnectedUsers, List<User>> getUnconnectedUsersQueryHandler;
        private readonly IQueryHandler<GetMembers, List<Member>> getMembersQueryHandler;
        private readonly IQueryHandler<GetMemberById, Member> getMemberByIdQueryHandler;
        private readonly ICommandHandler<AddToRole> addToRoleCommandHandler;
        public MembersService(
            ICommandHandler<ConnectMemberToUser> connectMemberCommandHandler, 
            ICommandHandler<CreateMember> createMemberCommandHandler, 
            ICommandHandler<MakeTrainer> makeTrainerCommandHandler,
            IQueryHandler<GetUnconnectedUsers, List<User>> getUnconnectedUsersQueryHandler,
            IQueryHandler<GetMembers, List<Member>> getMembersQueryHandler, 
            IQueryHandler<GetMemberById, Member> getMemberByIdQueryHandler,
            ICommandHandler<AddToRole> addToRoleCommandHandler)
        {
            this.connectMemberCommandHandler = connectMemberCommandHandler;
            this.createMemberCommandHandler = createMemberCommandHandler;
            this.makeTrainerCommandHandler = makeTrainerCommandHandler;
            this.getUnconnectedUsersQueryHandler = getUnconnectedUsersQueryHandler;
            this.getMembersQueryHandler = getMembersQueryHandler;
            this.getMemberByIdQueryHandler = getMemberByIdQueryHandler;
            this.addToRoleCommandHandler = addToRoleCommandHandler;
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

        public async Task<Member> GetMember(Guid id)
        {
            var result = await getMemberByIdQueryHandler.HandleAsync(new GetMemberById
            {
                MemberId = id
            });
            return result;
        }

        public async Task<Result> AddToRole(Guid memberId, string role)
        {
            return await addToRoleCommandHandler.HandleAsync(new AddToRole
            {
                MemberId = memberId,
                Role = role
            });
        }
    }
}
