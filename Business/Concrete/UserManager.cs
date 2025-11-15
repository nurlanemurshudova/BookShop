using Business.Abstract;
using Business.BaseMessages;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IBasketDal _basketDal;

        public UserManager(IUserDal userDal, IBasketDal basketDal)
        {
            _userDal = userDal;
            _basketDal = basketDal;
        }

        //UserDal _userDal = new();
        //BasketDal _basketDal = new();
        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            if (entity.Basket == null)
            {
                Basket basket = new()
                {
                    UserId = entity.Id,
                    User = entity
                };
                _basketDal.Add(basket);
            }
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _userDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAllWithBasket());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.GetById(id));
        }

        public IResult Update(User entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            _userDal.Update(entity);

            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }
    }
}
