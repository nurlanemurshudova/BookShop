using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class BookDal : BaseRepository<Book, ApplicationDbContext>,IBookDal 
    {
        private readonly ApplicationDbContext _context;

        public BookDal(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Book> GetAllWithAuthors()
        {
            // Məlumat bazası kontekstini (DbContext) fərz edirik ki, DI vasitəsilə alırsınız (_context)

            var data = _context.Books
                               .Where(b => b.Deleted == 0)

                               // 1. Book-dan BookAuthor ortaq cədvəlinə keçid et
                               .Include(b => b.BookAuthors)

                               // 2. BookAuthor-dan Author entity-sinə keçid et (ThenInclude)
                               .ThenInclude(ba => ba.Author)

                               .ToList();

            return data;

        }
    }
}
