using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<UserInvitation> GetUserInvitation(int sourceUserId, int targetUserId);
        Task<User> GetUserWithInvitations(int userId);
        Task<IEnumerable<InvitationDto>> GetUserInvitations(string predicate, int userId);
    }
}