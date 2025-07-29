using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Models.Models
{
    [Table("Product",Schema = "MasterSchema")]
    public class Product
    {
        //[Key]
        public int Id { get; set; }
        //[Required ,MaxLength(50)]
        public string Title { get; set; }
        //[MaxLength(250)]
        public string Description { get; set; }
        //[Required, MaxLength(50)]
        public string Author { get; set; }

        [Required, Range(1, 1000)]

        //[Column("BookPrice")]
        public decimal price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
    }
