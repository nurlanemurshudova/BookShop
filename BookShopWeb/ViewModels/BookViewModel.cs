using Entities.Concrete.TableModels;

namespace BookShopWeb.ViewModels
{
    public class BookViewModel
    {

        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}


