using Core.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        IResult Add(Author entity);
        IResult Update(Author entity);
        IResult Delete(int id);
        IDataResult<List<Author>> GetAll();
        IDataResult<Author> GetById(int id);


        IResult AddAuthorWithBooks(Author author, List<int> bookIds);
    }
}
