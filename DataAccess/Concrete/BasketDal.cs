using Core.DataAccess.Concrete;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class BasketDal : BaseRepository<Basket, ApplicationDbContext>, IBasketDal
    {
        private readonly ApplicationDbContext _context;

        public BasketDal(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Basket> GetAllWithBasket()
        {
            var data = _context.Baskets
                        .Where(b => b.Deleted == 0)
                        .Include(b => b.User).ToList();

            return data;
        }

        public Basket GetByUserId(int userId)
        {
            var data= _context.Baskets
                   .Include(b => b.Items.Where(i => i.Deleted == 0))
                    .ThenInclude(i => i.Book)
                   .FirstOrDefault(b => b.UserId == userId && b.Deleted == 0);
            return data;
        }
    }
}
