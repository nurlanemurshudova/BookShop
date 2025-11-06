using Business.Abstract;
using Business.BaseMessages;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Concrete;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookAuthorManager : IBookAuthorService
    {
        BookAuthorDal _bookAuthorDal = new();

        public IResult Add(BookAuthor entity)
        {
            _bookAuthorDal.Add(entity);
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BookAuthor>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<BookAuthor> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(BookAuthor entity)
        {
            throw new NotImplementedException();
        }
    }
}
