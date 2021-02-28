using LibraryCoreExample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LibraryCoreExample.Models.DataModels;

namespace LibraryCoreExample.Queries
{
    public class LibraryQuery : ILibraryQuery
    {
        private readonly IDbConnection _dbConnection;

        public LibraryQuery (IDbConnection connection)
        {
            _dbConnection = connection;
        }

        public bool CheckoutBook(CheckoutDataModel dataModel)
        {
            string sql = "insert into customer_book_checkout (customer_id, book_id, checkout_date) values (@customer_id, @book_id, CURDATE());";
            
            var affectedRows = _dbConnection.Execute(sql, dataModel);

            if(affectedRows == 1)
            {
                return true;
            }

            return false;
        }

        public List<BookDataModel> GetAllBooks()
        {
            string sql = "SELECT book_id, book_title, book_author, num_pages, published_date from books";

            var result = _dbConnection.Query<BookDataModel>(sql).ToList();

            return result;
        }

        public List<BookDataModel> GetBooksByUser(int customerIdentifier)
        {
            string sql = "select book_id, book_title, book_author, num_pages, published_date from books where book_id in (select book_id from customer_book_checkout where customer_id = @customerIdentifier);";

            var result = _dbConnection.Query<BookDataModel>(sql, customerIdentifier).ToList();

            return result;
        }

        public BookDataModel GetBookByIdentifier(int bookId)
        {
            string sql = "select book_id, book_title, book_author, num_pages, published_date from books where book_id = @book_id;";

            BookDataModel result = _dbConnection.Query<BookDataModel>(sql, new { book_id = bookId }).FirstOrDefault();

            return result;
        }

        public int InsertNewBook(BookDataModel dataModel)
        {
            //This quey performs an insert and also returns back the identifier for the record that was inserted. We do this so the database can generate a unique id for our new book
            string sql = "INSERT INTO BOOKS (book_title, book_author, num_pages, published_date, available) values (@BOOK_TITLE, @BOOK_AUTHOR, @NUM_PAGES, @PUBLISHED_DATE, @AVAILABLE); select last_insert_id();";

            int result = _dbConnection.Query<int>(sql, dataModel).FirstOrDefault();

            return result;
        }

        public int InsertNewCustomer(CustomerDataModel dataModel)
        {
            string sql = "INSERT INTO CUSTOMERS(customer_name, customer_phone, customer_email) values (@CUSTOMER_NAME, @CUSTOMER_PHONE, @CUSTOMER_EMAIL); select last_insert_id();";

            int result = _dbConnection.Query<int>(sql, dataModel).FirstOrDefault();

            return result;
        }

        public bool IsBookCheckedOut(int bookId)
        {
            string sql = "SELECT BOOK_ID from CUSTOMER_BOOK_CHECKOUT where BOOK_ID = @book_Id;";

            bool result = _dbConnection.Query(sql, new { book_Id = bookId }).Any();

            return result;
        }

        public CustomerDataModel GetCustomerByIdentifier(int customerId)
        {
            string sql = "select customer_id, customer_name, customer_phone, customer_email from customers where customer_id = @customer_id";

            CustomerDataModel result = _dbConnection.Query<CustomerDataModel>(sql, new { customer_id = customerId }).FirstOrDefault();

            return result;
        }
    }
}
