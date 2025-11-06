using Core.Results.Abstract;
using Entities.Concrete.TableModels;

namespace Business.Abstract
{
    public interface IBasketItemService
    {
        IResult Add(BasketItem entity);
        IResult Update(BasketItem entity);
        IResult Delete(int id);
        IDataResult<List<BasketItem>> GetAll();
        IDataResult<BasketItem> GetById(int id);
    }
}
