using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class BasketItemDal : BaseRepository<BasketItem, ApplicationDbContext>, IBasketItemDal
    {
        private readonly ApplicationDbContext _context;

        public BasketItemDal(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<BasketItem> GetBasketItemWithBasket()
        {
            var data = _context.BasketItems
                .Where(x => x.Deleted == 0)
                .Include(x => x.Book)
                .Include(x => x.Basket)
                .ThenInclude(basket => basket.User).ToList();

            return data;
        }
    }
}
