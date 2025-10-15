using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InvitationsController : BaseApiController
    {
       
        private readonly IUnitOfWork _uow;
        
        public InvitationsController(IUnitOfWork uow)
        {
            _uow = uow;
           
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddInvitation(string username)
        {
            var sourceUserId = User.GetUserId();
            var invidedUser = await _uow.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _uow.InvitationsRepository.GetUserWithInvitations(sourceUserId);

            if(invidedUser == null) return NotFound();

            if(sourceUser.UserName == username) return BadRequest("You cannot invide to friends yourself");

            var userInvitation = await _uow.InvitationsRepository.GetUserInvitation(sourceUserId, invidedUser.Id);

            if(userInvitation != null) return BadRequest("You already invided this user");

            // Sprawdź czy użytkownicy już są znajomymi
            var areFriends = await _uow.FriendRepository.AreFriends(sourceUserId, invidedUser.Id);
            if(areFriends) return BadRequest("You are already friends with this user");

            userInvitation = new UserInvitation
            {
                SourceUserId = sourceUserId,
                TargetUserId = invidedUser.Id
            };

            sourceUser.InvideUsers.Add(userInvitation);

            if(await _uow.Complete()) return Ok();

            return BadRequest("Failed to invide user");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<InvitationDto>>> GetUserInvitations([FromQuery]InvitationsParams invitationsParams)
        {
            invitationsParams.UserId = User.GetUserId();

            var users = await _uow.InvitationsRepository.GetUserInvitations(invitationsParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }

        [HttpGet("friends")]
        public async Task<ActionResult<PagedList<InvitationDto>>> GetUserFriends([FromQuery]InvitationsParams invitationsParams)
        {
            invitationsParams.UserId = User.GetUserId();

            var friends = await _uow.InvitationsRepository.GetUserFriends(invitationsParams);

            Response.AddPaginationHeader(new PaginationHeader(friends.CurrentPage, friends.PageSize, friends.TotalCount, friends.TotalPages));

            return Ok(friends);
        }

        [HttpPost("accept/{username}")]
        public async Task<ActionResult> AcceptInvitation(string username)
        {
            var targetUserId = User.GetUserId();
            var sourceUser = await _uow.UserRepository.GetUserByUsernameAsync(username);

            if (sourceUser == null) return NotFound();

            var success = await _uow.InvitationsRepository.AcceptInvitation(sourceUser.Id, targetUserId);

            if (success) return Ok();

            return BadRequest("Failed to accept invitation");
        }

        [HttpPost("reject/{username}")]
        public async Task<ActionResult> RejectInvitation(string username)
        {
            var targetUserId = User.GetUserId();
            var sourceUser = await _uow.UserRepository.GetUserByUsernameAsync(username);

            if (sourceUser == null) return NotFound();

            var success = await _uow.InvitationsRepository.RejectInvitation(sourceUser.Id, targetUserId);

            if (success) return Ok();

            return BadRequest("Failed to reject invitation");
        }

        [HttpGet("check-friend/{username}")]
        public async Task<ActionResult<bool>> CheckIfFriend(string username)
        {
            var currentUserId = User.GetUserId();
            var targetUser = await _uow.UserRepository.GetUserByUsernameAsync(username);

            if (targetUser == null) return NotFound();

            var isFriend = await _uow.FriendRepository.AreFriends(currentUserId, targetUser.Id);

            return Ok(isFriend);
        }
    }
}