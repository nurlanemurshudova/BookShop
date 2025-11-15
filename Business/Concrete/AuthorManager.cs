using Business.Abstract;
using Business.BaseMessages;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorDal _authorDal;
        private readonly IBookAuthorDal _bookAuthorDal;

        public AuthorManager(IAuthorDal authorDal, IBookAuthorDal bookAuthorDal)
        {
            _authorDal = authorDal;
            _bookAuthorDal = bookAuthorDal;
        }
        //AuthorDal _authorDal = new();
        //BookAuthorDal _bookAuthorDal = new();



        public IResult AddAuthorWithBooks(Author author, List<int> bookIds)
        {

            _authorDal.Add(author);
            author.BookAuthors = new List<BookAuthor>();

            if (bookIds != null && bookIds.Any())
            {
                foreach (var bookId in bookIds)
                {
                    BookAuthor bookAuthor = new()
                    {
                        BookId = bookId,
                        AuthorId = author.Id
                    };
                    _bookAuthorDal.Add(bookAuthor);
                }
            }

            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }


        public IResult Add(Author entity)
        {
            _authorDal.Add(entity);
            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }
        public IResult Update(Author entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            _authorDal.Update(entity);

            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }
        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _authorDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<Author>> GetAll()
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetAll(x => x.Deleted == 0));
        }

        public IDataResult<Author> GetById(int id)
        {
            return new SuccessDataResult<Author>(_authorDal.GetById(id));
        }

    }
}
