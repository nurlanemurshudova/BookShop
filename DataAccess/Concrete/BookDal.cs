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

            var data = _context.Books
                               .Where(b => b.Deleted == 0)
                               .Include(b => b.BookAuthors)
                               .ThenInclude(ba => ba.Author)
                               .ToList();
            return data;

        }
    }
}
