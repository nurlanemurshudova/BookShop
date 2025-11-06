using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class BasketItem : BaseEntity
    {
        public int BasketId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtTimeOfAddition { get; set; } 
        public virtual Basket Basket { get; set; }
        public virtual Book Book { get; set; }
    }
}
