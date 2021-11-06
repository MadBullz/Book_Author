using System;
using System.Collections.Generic;

#nullable disable

namespace Q3_PE.Models
{
    public partial class Book
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
