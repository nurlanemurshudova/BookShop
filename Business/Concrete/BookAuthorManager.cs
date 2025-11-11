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
    public class BookAuthorManager : IBookAuthorService
    {
        private readonly IBookAuthorDal _bookAuthorDal;

        public BookAuthorManager(IBookAuthorDal bookAuthorDal)
        {
            _bookAuthorDal = bookAuthorDal;
        }

        //BookAuthorDal _bookAuthorDal = new();

        public IResult Add(BookAuthor entity)
        {
            _bookAuthorDal.Add(entity);
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _bookAuthorDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<BookAuthor>> GetAll()
        {
            return new SuccessDataResult<List<BookAuthor>>(_bookAuthorDal.GetAll(x => x.Deleted == 0));
        }

        public IDataResult<BookAuthor> GetById(int id)
        {
            return new SuccessDataResult<BookAuthor>(_bookAuthorDal.GetById(id));
        }

        public IResult Update(BookAuthor entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            _bookAuthorDal.Update(entity);

            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }
    }
}
