using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public byte DiscountRate { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
