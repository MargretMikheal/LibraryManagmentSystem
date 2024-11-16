using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Core.Interfaces.Repositories;


namespace LibraryManagmentSystem.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Book> Books { get; }
        public IGenericRepository<ApplicationUser> Users { get; }
        public IGenericRepository<Borrowing> Borrowings { get; }
        public IGenericRepository<Fine> Fines { get; }
        public IGenericRepository<Genre> Genres { get; }
        public IGenericRepository<Inventory> Inventory { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(context);
            Users = new GenericRepository<ApplicationUser>(context);
            Borrowings = new GenericRepository<Borrowing>(context);
            Fines = new GenericRepository<Fine>(context);
            Genres = new GenericRepository<Genre>(context);
            Inventory = new GenericRepository<Inventory>(context);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
