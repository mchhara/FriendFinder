using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
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

        public async Task<IEnumerable<InvitationDto>> GetUserInvitations(string predicate, int userId)
        {
            var users = _dataContext.Users.OrderBy(u =>u.Username).AsQueryable();
            var invitations = _dataContext.Invitations.AsQueryable();

           if (predicate == "invided") 
           {
                invitations = invitations.Where(invitation => invitation.SourceUserId == userId);
                users = invitations.Select(invitation => invitation.TargetUser);
           }

            if (predicate == "invidedBy") 
           {
                invitations = invitations.Where(invitation => invitation.TargetUserId == userId);
                users = invitations.Select(invitation => invitation.SourceUser);
           }

           return await users.Select(user => new InvitationDto
           {
                Username = user.Username,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id
           }).ToListAsync();
        }

        public async Task<User> GetUserWithInvitations(int userId)
        {
             return await _dataContext.Users
                .Include(x => x.InvideUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}