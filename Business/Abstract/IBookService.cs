using Core.Results.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService 
    {
        IResult Add(Book entity, List<int> authorIds, IFormFile photoUrl, string webRootPath);
        IResult Update(Book entity, List<int> authorIds, IFormFile photoUrl, string webRootPath);
        IResult Delete(int id);
        IDataResult<List<Book>> GetAll();
        IDataResult<Book> GetById(int id);
    }
}
