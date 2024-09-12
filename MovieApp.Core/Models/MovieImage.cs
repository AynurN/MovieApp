using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class MovieImage :BaseEntity
    {
        public string ImageUrl { get; set; }


        //relational
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
