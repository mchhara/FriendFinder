using API.Entities;

namespace API.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task CreatePost(Post post);
    }
}