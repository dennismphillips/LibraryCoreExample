using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Models
{
    public class BookDataModel
    {
        public int BOOK_ID { get; set; }
        public string BOOK_TITLE { get; set; }
        public string BOOK_AUTHOR { get; set; }
        public int NUM_PAGES { get; set; }
        public DateTime PUBLISHED_DATE { get; set; }
    }
}
