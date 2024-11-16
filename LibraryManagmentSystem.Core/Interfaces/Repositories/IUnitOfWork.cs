using LibraryManagementSystem.Domain.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<ApplicationUser> Users { get; }
        IGenericRepository<Borrowing> Borrowings { get; }
        IGenericRepository<Fine> Fines { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<Inventory> Inventory { get; }

        Task<bool> SaveChangesAsync();
    }

}
