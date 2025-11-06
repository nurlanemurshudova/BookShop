using Core.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService 
    {
        IResult Add(Book entity);
        IResult Update(Book entity);
        IResult Delete(int id);
        IDataResult<List<Book>> GetAll();
        IDataResult<Book> GetById(int id);
    }
}
