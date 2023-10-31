using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfases
{
    public interface IRepository<T>
    {
        void Add(T value);
        void Delete(T value);
        void Update(T value);
        List<T> GetAll();
    }
}
