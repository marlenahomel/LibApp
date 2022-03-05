using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Humanizer.DateTimeHumanizeStrategy;
using LibApp.Models;

namespace LibApp.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int id);

        IEnumerable<Book> GetAllBooks();

        Book GetBookById(int id);

        void Save();
    }
}