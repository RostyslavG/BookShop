using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class User
    {
        public User() { UserBooks = new List<Book>(); }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passworld { get; set; }
        public virtual IList<Book> UserBooks { get; set; }
    }
}
