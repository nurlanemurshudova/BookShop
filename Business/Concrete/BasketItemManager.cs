using Business.Abstract;
using Core.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketItemManager : IBasketItemService
    {
        public IResult Add(BasketItem entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BasketItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<BasketItem> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(BasketItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
