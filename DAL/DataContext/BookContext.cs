using DAL.ModelInfo;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;

namespace DAL.DataContext
{
    public class BookContext : DbContext
    {
        public BookContext() : base("name=BookContext"){}
        public virtual DbSet<UserInfo> Users { get; set; }
        public virtual DbSet<BookInfo> Books { get; set; }
        public virtual DbSet<PublishingInfo> Publishings { get; set; }
    }
}