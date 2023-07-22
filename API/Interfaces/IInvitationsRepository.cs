using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<UserInvitation> GetUserInvitation(int sourceUserId, int targetUserId);
        Task<User> GetUserWithInvitations(int userId);
        Task<PagedList<InvitationDto>> GetUserInvitations(InvitationsParams invitationsParams);
    }
}