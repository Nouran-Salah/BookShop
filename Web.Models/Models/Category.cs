using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Web.Models.Models
{
    [Table("Categories" ,Schema = "MasterSchema")]
    public class Category
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(50)]
        public string catName { get; set; }

        //[Required]
        public int catOrder { get; set; }

        //[NotMapped]
        public DateTime createdDate { get; set; } = DateTime.Now;

        //[Column("is deleted")]
        public bool markedAsDeleted { get; set; }
    }
}
