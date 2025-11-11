using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Concrete
{
    public class UserDal : BaseRepository<User, ApplicationDbContext>, IUserDal
    {
        private readonly ApplicationDbContext _context;

        public UserDal(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllWithBasket()
        {
            var data = _context.Users
               .Where(x => x.Deleted == 0)
               .Include(u => u.Basket)
               .ThenInclude(b => b.Items).ToList();

            return data;
        }
    }
}
