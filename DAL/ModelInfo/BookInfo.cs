using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelInfo
{
    [Table("Book")]
    public class BookInfo
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string AutorName { get; set; }
        [Required]
        [MaxLength(50)]
        public string AutorLastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }
        [Required]
        public int CountPage { get; set; }
        [Required]
        public int BookCount { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public virtual PublishingInfo PublishingBook { get; set; }
    }
}
