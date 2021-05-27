using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finals.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        public string CatName { get; set; }

        public ICollection<Query> Queries { get; set; }
    }
}
