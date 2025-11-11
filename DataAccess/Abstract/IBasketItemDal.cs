using Core.DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete.TableModels;

namespace DataAccess.Abstract
{
    public interface IBasketItemDal : IBaseRepository<BasketItem> 
    {
        List<BasketItem> GetBasketItemWithBasket();

    }
}
