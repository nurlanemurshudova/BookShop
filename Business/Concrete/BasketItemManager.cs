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
    public class BasketItemManager : IBasketItemService
    {
        private readonly IBasketItemDal _basketItemDal;

        public BasketItemManager(IBasketItemDal basketItemDal)
        {
            _basketItemDal = basketItemDal;
        }

        //BasketItemDal _basketItemDal = new();
        public IResult Add(BasketItem entity)
        {
            _basketItemDal.Add(entity);
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _basketItemDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<BasketItem>> GetAll()
        {
            return new SuccessDataResult<List<BasketItem>>(_basketItemDal.GetBasketItemWithBasket());
        }

        public IDataResult<BasketItem> GetById(int id)
        {
            return new SuccessDataResult<BasketItem>(_basketItemDal.GetById(id));
        }

        public IResult Update(BasketItem entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            _basketItemDal.Update(entity);

            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }
    }
}
