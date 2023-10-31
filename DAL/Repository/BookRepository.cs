using DAL.DataContext;
using DAL.Interfases;
using DAL.ModelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DAL.Repository
{
    public class BookRepository : IRepository<BookInfo>
    {
        private BookContext _bookContext;
        public BookRepository()
        {
            _bookContext = new BookContext();
        }
        public void Add(BookInfo value)
        {
            _bookContext.Books.Add(value);
            _bookContext.SaveChanges();
        }

        public void Delete(BookInfo value)
        {
            var bookToDelete = _bookContext.Books.FirstOrDefault(b => b.Id == value.Id);
            if (bookToDelete != null)
            {
                _bookContext.Books.Remove(bookToDelete);
                _bookContext.SaveChanges();
            }
        }

        public List<BookInfo> GetAll()
        {
            return _bookContext.Books.ToList();
        }

        public void Update(BookInfo value)
        {
            if (value != null)
            {
                var tempBook = _bookContext.Books.Find(value.Id);
                if (tempBook != null)
                {
                    tempBook.Image = value.Image;
                    tempBook.Name = value.Name;
                    tempBook.AutorName = value.AutorName;
                    tempBook.AutorLastName = value.AutorLastName;
                    tempBook.Genre = value.Genre;
                    tempBook.CountPage = value.CountPage;
                    tempBook.BookCount = value.BookCount;
                    tempBook.Cost = value.Cost;
                    tempBook.PublishingBook.Name = value.PublishingBook.Name;
                    tempBook.PublishingBook.Year = value.PublishingBook.Year;
                    _bookContext.SaveChanges();
                }
            }
        }
    }
}
