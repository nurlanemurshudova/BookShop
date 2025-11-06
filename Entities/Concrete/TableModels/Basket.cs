using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Basket : BaseEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
