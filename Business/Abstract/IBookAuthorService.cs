using Core.Results.Abstract;
using Entities.Concrete.TableModels;

namespace Business.Abstract
{
    public interface IBookAuthorService
    {
        IResult Add(BookAuthor entity);
        IResult Update(BookAuthor entity);
        IResult Delete(int id);
        IDataResult<List<BookAuthor>> GetAll();
        IDataResult<BookAuthor> GetById(int id);
    }
}
