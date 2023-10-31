using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfases
{
    public interface IServise<T>
    {
        void Add(T value);
        void Delete(T value);
        void Update(T value);
        List<T> GetAll();
        IEnumerable<T> Find(T value, FindMode findMode);
        IEnumerable<T> GetBooksByCriteria(BookCriteria criteria, int count);
        bool TryLogin(string login, string password, out User loggedInUser);
    }

    public enum BookCriteria
    {
        NewArrivals,
        BestSellers,
        TopAuthors
    }
    public enum FindMode
    {
        NameFinding,
        AutorFinding,
        GenreFinding
    }
}
