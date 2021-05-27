using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finals.Models
{
    public class Solution2
    {
        [Key]
        public int SolutionID { get; set; }
        public int QueryID { get; set; }


        public string Scontent { get; set; }
        public string Artifact { get; set; }
        public Query Que { get; set; }
    }
}
