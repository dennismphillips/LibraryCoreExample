using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Models
{
    public class BookResponse
    {
        public int Identifier { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int NumPages { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool Available { get; set; }
    }
}
