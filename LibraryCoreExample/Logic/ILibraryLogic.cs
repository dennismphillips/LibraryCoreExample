using LibraryCoreExample.Models;
using LibraryCoreExample.Models.WebRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Logic
{
    public interface ILibraryLogic
    {
        public List<BookResponse> GetAllBooks();
        int AddNewBook(BookRequest aNewBook);
        int AddNewCustomer(CustomerRequest aNewCustomer);
        bool CheckoutABook(CheckoutRequest checkout);
    }
}
