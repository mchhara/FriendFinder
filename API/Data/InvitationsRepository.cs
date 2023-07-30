using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InvitationsRepository : IInvitationsRepository
    {
        private readonly DataContext _dataContext;

        public InvitationsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserInvitation> GetUserInvitation(int sourceUserId, int targetUserId)
        {
           return await _dataContext.Invitations.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<InvitationDto>> GetUserInvitations(InvitationsParams invitationsParams)
        {
            var users = _dataContext.Users.OrderBy(u =>u.UserName).AsQueryable();
            var invitations = _dataContext.Invitations.AsQueryable();

           if (invitationsParams.Predicate == "invided") 
           {
                invitations = invitations.Where(invitation => invitation.SourceUserId == invitationsParams.UserId);
                users = invitations.Select(invitation => invitation.TargetUser);
           }

            if (invitationsParams.Predicate == "invidedBy") 
           {
                invitations = invitations.Where(invitation => invitation.TargetUserId == invitationsParams.UserId);
                users = invitations.Select(invitation => invitation.SourceUser);
           }

           var invidedUsers = users.Select(user => new InvitationDto
           {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id
           });

           return await PagedList<InvitationDto>.CreateAsync(invidedUsers, invitationsParams.PageNumber, invitationsParams.PageSize);
        }

        public async Task<User> GetUserWithInvitations(int userId)
        {
             return await _dataContext.Users
                .Include(x => x.InvideUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}