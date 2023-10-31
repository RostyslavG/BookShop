using DAL.ModelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string AutorName { get; set; }
        public string AutorLastName { get; set; }
        public string Genre { get; set; }
        public int CountPage { get; set; }
        public int BookCount { get; set; }
        public int Cost { get; set; }
        public Publishing PublishingBook { get; set; }
    }
}
