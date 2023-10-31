using DAL.DataContext;
using DAL.Interfases;
using DAL.ModelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : IRepository<UserInfo>
    {
        private BookContext _bookContext;
        public UserRepository()
        {
            _bookContext = new BookContext();
        }
        public void Add(UserInfo value)
        {
            _bookContext.Users.Add(value);
            _bookContext.SaveChanges();
        }

        public void Delete(UserInfo value)
        {
            _bookContext.Users.Remove(value);
            _bookContext.SaveChanges();
        }

        public List<UserInfo> GetAll()
        {
            return _bookContext.Users.ToList();
        }

        public void Update(UserInfo value)
        {
            if (value != null)
            {
                UserInfo tempUser = _bookContext.Users.Where(e => e.Id == value.Id).First();
                if (tempUser != null)
                {
                    tempUser.Login = value.Login;
                    tempUser.Passworld= value.Passworld;
                    tempUser.UserBooks = value.UserBooks;
                    _bookContext.SaveChanges();
                }
            }
        }
    }
}
