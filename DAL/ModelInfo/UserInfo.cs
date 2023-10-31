using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelInfo
{
    [Table("User")]
    public class UserInfo
    {
        public UserInfo() { UserBooks = new List<BookInfo>(); }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        [Required]
        public string Passworld { get; set; }
        [Required]
        public virtual IList<BookInfo> UserBooks { get; set; }
    }
}
