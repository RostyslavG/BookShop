using BLL.Interfases;
using BLL.Models;
using DAL.ModelInfo;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servises
{
    public class BookServise : IServise<Book>
    {
        private List<Book> booksList;
        private BookRepository _booksDB;
        public BookServise()
        {
            _booksDB = new BookRepository();
            booksList = new List<Book>();
        }
        private BookInfo TranslateBookToBookInfo(Book book)
        {
            BookInfo bookInfo = new BookInfo()
            {
                Id = book.Id,
                Image = book.Image,
                Name = book.Name,
                AutorName = book.AutorName,
                AutorLastName = book.AutorLastName,
                Genre = book.Genre,
                CountPage = book.CountPage,
                BookCount = book.BookCount,
                Cost = book.Cost,
                PublishingBook = new PublishingInfo()
                {
                    Id = book.PublishingBook.Id,
                    Name = book.PublishingBook.Name,
                    Year = book.PublishingBook.Year
                }
            };
            return bookInfo;
        }

        private Book TranslateBookInfoToBook(BookInfo bookInfo)
        {
            Book book = new Book()
            {
                Id = bookInfo.Id,
                Image = bookInfo.Image,
                Name = bookInfo.Name,
                AutorName = bookInfo.AutorName,
                AutorLastName = bookInfo.AutorLastName,
                Genre = bookInfo.Genre,
                CountPage = bookInfo.CountPage,
                BookCount = bookInfo.BookCount,
                Cost = bookInfo.Cost,
                PublishingBook = new Publishing()
                {
                    Id = bookInfo.PublishingBook.Id,
                    Name = bookInfo.PublishingBook.Name,
                    Year = bookInfo.PublishingBook.Year
                }
            };
            return book;
        }
        public void Add(Book value)
        {
            _booksDB.Add(TranslateBookToBookInfo(value));
        }

        public void Delete(Book value)
        {
            _booksDB.Delete(TranslateBookToBookInfo(value));
        }

        public IEnumerable<Book> Find(Book value, FindMode findMode)
        {
            List<Book> foundBooks = new List<Book>();
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;

            switch (findMode)
            {
                case FindMode.NameFinding:
                    foundBooks = booksList.Where(book => book.Name.IndexOf(value.Name, comparison) >= 0).ToList();
                    break;

                case FindMode.AutorFinding:
                    foundBooks = booksList.Where(book => book.AutorLastName.IndexOf(value.AutorLastName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    break;

                case FindMode.GenreFinding:
                    foundBooks = booksList.Where(book => book.Genre.IndexOf(value.Genre, comparison) >= 0).ToList();
                    break;
            }
            return foundBooks;
        }

        public List<Book> GetAll()
        {
            booksList.Clear();
            foreach (var item in _booksDB.GetAll())
                booksList.Add(TranslateBookInfoToBook(item));
            return booksList;
        }

        public void Update(Book value)
        {
            _booksDB.Update(TranslateBookToBookInfo(value));
        }

        public bool TryLogin(string login, string password, out User loggedInUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooksByCriteria(BookCriteria criteria, int count = 10)
        {
            switch (criteria)
            {
                case BookCriteria.NewArrivals:
                    return booksList.Where(book => book.PublishingBook.Year >= DateTime.Now.Year - 1).OrderByDescending(book => book.PublishingBook.Year).Take(count).ToList();

                case BookCriteria.BestSellers:
                    return booksList.OrderByDescending(book => book.BookCount).Take(count).ToList();

                case BookCriteria.TopAuthors:
                    var authorBooks = booksList.GroupBy(book => $"{book.AutorName} {book.AutorLastName}").OrderByDescending(group => group.Sum(book => book.BookCount))
                .Take(count).SelectMany(group => group.ToList()).ToList();
                    return authorBooks;
                default:
                    return new List<Book>();
            }
        }
    }
}
