namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository{get;}
        IInvitationsRepository InvitationsRepository {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}