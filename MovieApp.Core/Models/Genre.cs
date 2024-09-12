using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        //relational
        public ICollection<Movie> Movies { get; set; }
    }
}
