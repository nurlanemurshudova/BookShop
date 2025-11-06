using Core.Results.Abstract;
using Entities.Concrete.TableModels;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User entity);
        IResult Update(User entity);
        IResult Delete(int id);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
    }
}
