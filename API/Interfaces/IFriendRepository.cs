using API.Entities;

namespace API.Interfaces
{
    public interface IFriendRepository
    {
        Task<Friend> GetFriend(int userId, int friendUserId);
        Task<bool> AreFriends(int userId, int friendUserId);
        Task AddFriend(int userId, int friendUserId);
        Task RemoveFriend(int userId, int friendUserId);
    }
}
