using Business.Abstract;
using Business.BaseMessages;
using Core.Extension;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookAuthorDal _bookAuthorDal;
        private readonly IBookDal _bookDal;

        public BookManager(IBookAuthorDal bookAuthorDal, IBookDal bookDal)
        {
            _bookAuthorDal = bookAuthorDal;
            _bookDal = bookDal;
        }

        //BookAuthorDal _bookAuthorDal = new();
        //BookDal _bookDal = new();
        public IResult Add(Book entity, List<int> authorIds, IFormFile photoUrl, string webRootPath)
        {
            entity.PhotoUrl = PictureHelper.UploadImage(photoUrl, webRootPath);
            _bookDal.Add(entity);
            entity.BookAuthors = new List<BookAuthor>();

            if (authorIds != null && authorIds.Any())
            {
                foreach (var authorId in authorIds)
                {
                    BookAuthor bookAuthor = new()
                    {
                        BookId = entity.Id,
                        AuthorId = authorId
                    };
                    _bookAuthorDal.Add(bookAuthor);
                }
            }

            return new SuccessResult(UIMessages.ADDED_MESSAGE);
        }
        public IResult Update(Book entity, List<int> authorIds, IFormFile photoUrl, string webRootPath)
        {
            var existData = GetById(entity.Id).Data;
            if (photoUrl == null)
            {
                entity.PhotoUrl = existData.PhotoUrl;
            }
            else
            {
                entity.PhotoUrl = PictureHelper.UploadImage(photoUrl, webRootPath);
            }
            entity.LastUpdatedDate = DateTime.Now;
            _bookDal.Update(entity);

            var oldAuthors = _bookAuthorDal.GetAll(x => x.BookId == entity.Id);

            foreach (var old in oldAuthors)
            {
                _bookAuthorDal.Delete(old);
            }

            if (authorIds != null && authorIds.Any())
            {
                foreach (var authorId in authorIds)
                {
                    BookAuthor newRel = new()
                    {
                        BookId = entity.Id,
                        AuthorId = authorId
                    };
                    _bookAuthorDal.Add(newRel);
                }
            }
            return new SuccessResult(UIMessages.UPDATE_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var data = GetById(id).Data;
            data.Deleted = id;

            _bookDal.Update(data);
            return new SuccessResult(UIMessages.Deleted_MESSAGE);
        }

        public IDataResult<List<Book>> GetAll()
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAllWithAuthors());
        }

        public IDataResult<Book> GetById(int id)
        {
            return new SuccessDataResult<Book>(_bookDal.GetById(id));
        }


    }
}
