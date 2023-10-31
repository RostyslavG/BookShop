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
    public class UserServise : IServise<User>
    {
        private List<string> adminUsernames;
        private List<User> usersList;
        private UserRepository _usersDB;
        public UserServise()
        {
            adminUsernames = new List<string>();
            _usersDB = new UserRepository();
            usersList = new List<User>();
            adminUsernames.Add("Murka");
        }

        public bool IsAdmin(string username)
        {
            return adminUsernames.Contains(username);
        }

        private UserInfo TranslateUserToUserInfo(User user)
        {
            UserInfo userInfo = new UserInfo()
            {
                Id = user.Id,
                Login = user.Login,
                Passworld = user.Passworld,
                UserBooks = user.UserBooks.Select(book => new BookInfo()
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
                }).ToList()
            };

            return userInfo;
        }

        private User TranslateUserInfoToUser(UserInfo userInfo)
        {
            User user = new User()
            {
                Id = userInfo.Id,
                Login = userInfo.Login,
                Passworld = userInfo.Passworld,
                UserBooks = userInfo.UserBooks.Select(book => new Book()
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
                    PublishingBook = new Publishing()
                    {
                        Id = book.PublishingBook.Id,
                        Name = book.PublishingBook.Name,
                        Year = book.PublishingBook.Year
                    }
                }).ToList()
            };
            return user;
        }

        public void Add(User value)
        {
            if (_usersDB.GetAll().Any(u => u.Login == value.Login))
                throw new InvalidOperationException("Користувач з таким логіном вже існує.");
            _usersDB.Add(TranslateUserToUserInfo(value));
        }

        public void Delete(User value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(User value, FindMode findMode)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            usersList.Clear();
            foreach (var item in _usersDB.GetAll())
                usersList.Add(TranslateUserInfoToUser(item));
            return usersList;
        }

        public void Update(User value)
        {
            _usersDB.Update(TranslateUserToUserInfo(value));
        }

        public bool TryLogin(string login, string password, out User loggedInUser)
        {
            UserInfo userInfo = _usersDB.GetAll().FirstOrDefault(u => u.Login == login && u.Passworld == password);

            if (userInfo != null)
            {
                loggedInUser = TranslateUserInfoToUser(userInfo);
                return true;
            }
            else
            {
                loggedInUser = null;
                return false;
            }
        }
        public IEnumerable<User> GetBooksByCriteria(BookCriteria criteria, int count)
        {
            throw new NotImplementedException();
        }
    }
}
