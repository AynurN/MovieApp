using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class Comment :BaseEntity
    {
        public string Content { get; set; }

        //relations
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
