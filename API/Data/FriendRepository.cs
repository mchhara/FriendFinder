using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _dataContext;

        public FriendRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Friend> GetFriend(int userId, int friendUserId)
        {
            return await _dataContext.Friends
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendUserId == friendUserId);
        }

        public async Task<bool> AreFriends(int userId, int friendUserId)
        {
            return await _dataContext.Friends
                .AnyAsync(f => f.UserId == userId && f.FriendUserId == friendUserId);
        }

        public async Task AddFriend(int userId, int friendUserId)
        {
            // Sprawdź czy już nie są znajomymi
            if (await AreFriends(userId, friendUserId))
                return;

            // Dodaj relację w obu kierunkach (symetryczna relacja znajomych)
            var friend1 = new Friend
            {
                UserId = userId,
                FriendUserId = friendUserId
            };

            var friend2 = new Friend
            {
                UserId = friendUserId,
                FriendUserId = userId
            };

            _dataContext.Friends.Add(friend1);
            _dataContext.Friends.Add(friend2);

            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveFriend(int userId, int friendUserId)
        {
            var friend1 = await GetFriend(userId, friendUserId);
            var friend2 = await GetFriend(friendUserId, userId);

            if (friend1 != null)
                _dataContext.Friends.Remove(friend1);

            if (friend2 != null)
                _dataContext.Friends.Remove(friend2);

            await _dataContext.SaveChangesAsync();
        }
    }
}
