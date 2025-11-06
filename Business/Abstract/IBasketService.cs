using Core.Results.Abstract;
using Entities.Concrete.TableModels;

namespace Business.Abstract
{
    public interface IBasketService
    {
        IResult Add(Basket entity);
        IResult Update(Basket entity);
        IResult Delete(int id);
        IDataResult<List<Basket>> GetAll();
        IDataResult<Basket> GetById(int id);
    }
}
