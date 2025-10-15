namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository{get;}
        IInvitationsRepository InvitationsRepository {get;}
        IFriendRepository FriendRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IPostRepository PostRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}