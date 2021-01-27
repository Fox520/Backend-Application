using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innoloft.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String Category { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }
}
