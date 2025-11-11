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
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        //BasketDal _basketDal = new();
        public IResult Add(Basket entity)
        {
            _basketDal.Add(entity);
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _basketDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<Basket>> GetAll()
        {
            return new SuccessDataResult<List<Basket>>(_basketDal.GetAllWithBasket());
        }

        public IDataResult<Basket> GetById(int id)
        {
            return new SuccessDataResult<Basket>(_basketDal.GetById(id));
        }


        public IResult Update(Basket entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            _basketDal.Update(entity);

            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }
        public IDataResult<Basket> GetByUserId(int userId)
        {
            var basket = _basketDal.GetByUserId(userId);
            return new SuccessDataResult<Basket>(basket);
        }
    }
}
