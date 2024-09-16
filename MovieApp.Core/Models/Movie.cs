using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class Movie :BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }

        //relational
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MovieImage> MovieImages { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
