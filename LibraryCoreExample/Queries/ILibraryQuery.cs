using LibraryCoreExample.Models;
using LibraryCoreExample.Models.DataModels;
using System.Collections.Generic;
namespace LibraryCoreExample.Queries
{
    public interface ILibraryQuery
    {
        public List<BookDataModel> GetAllBooks();

        public BookDataModel GetBookByIdentifier(int bookId);

        public List<BookDataModel> GetBooksByUser(int customerIdentifier);
        public int InsertNewBook(BookDataModel dataModel);

        public int InsertNewCustomer(CustomerDataModel dataModel);

        public bool IsBookCheckedOut(int bookId);

        public bool CheckoutBook(CheckoutDataModel dataModel);

        public CustomerDataModel GetCustomerByIdentifier(int customerId);
    }
}
