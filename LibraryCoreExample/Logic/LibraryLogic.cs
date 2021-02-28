using LibraryCoreExample.Models;
using LibraryCoreExample.Models.DataModels;
using LibraryCoreExample.Models.WebRequest;
using LibraryCoreExample.Queries;
using System;
using System.Collections.Generic;

namespace LibraryCoreExample.Logic
{
    public class LibraryLogic : ILibraryLogic
    {
        private readonly ILibraryQuery _libraryQuery;

        public LibraryLogic(ILibraryQuery libraryQuery)
        {
            _libraryQuery = libraryQuery;
        }

        public int AddNewBook(BookRequest aNewBook)
        {
            BookDataModel dataModel = new BookDataModel()
            {
                BOOK_AUTHOR = aNewBook.Author,
                BOOK_TITLE = aNewBook.Title,
                NUM_PAGES = aNewBook.NumPages,
                PUBLISHED_DATE = aNewBook.PublishedDate
            };

            int Identifier = _libraryQuery.InsertNewBook(dataModel);

            return Identifier;
        }

        public int AddNewCustomer(CustomerRequest aNewCustomer)
        {
            CustomerDataModel dataModel = new CustomerDataModel()
            {
                CUSTOMER_NAME = aNewCustomer.Name,
                CUSTOMER_EMAIL = aNewCustomer.Email,
                CUSTOMER_PHONE = aNewCustomer.Phone
            };

            int Identifier = _libraryQuery.InsertNewCustomer(dataModel);

            return Identifier;
        }

        public bool CheckoutABook(CheckoutRequest checkout)
        {
            //Check if the customer exists
            if(_libraryQuery.GetCustomerByIdentifier(checkout.CustomerIdentifier) == null)
            {
                throw new Exception("The customer does not exist in this system");
            }

            //Check if book exists
            if(_libraryQuery.GetBookByIdentifier(checkout.BookIdentifier) == null)
            {
                throw new Exception("The book does not exist in this system");
            }


            //Check if Book is available for checkout
            if(_libraryQuery.IsBookCheckedOut(checkout.BookIdentifier))
            {
                throw new Exception("The book is already checked out and is unavailable");
            }

            //Check if user has too many books checked out
            List<BookDataModel> dataModels = _libraryQuery.GetBooksByUser(checkout.CustomerIdentifier);
            if(dataModels == null || dataModels.Count >= 5)
            {
                throw new Exception("The customer has too many books checked out");
            }

            //Perform checkout
            CheckoutDataModel datamodel = new CheckoutDataModel()
            {
                BOOK_ID = checkout.BookIdentifier,
                CUSTOMER_ID = checkout.CustomerIdentifier
            };

            bool result = _libraryQuery.CheckoutBook(datamodel);

            return result;
        }

        public List<BookResponse> GetAllBooks()
        {
            List<BookDataModel> dbBooks = _libraryQuery.GetAllBooks();

            List<BookResponse> booksResponses = new List<BookResponse>();

            foreach(BookDataModel dataModel in dbBooks)
            {
                BookResponse response = new BookResponse()
                {
                    Author = dataModel.BOOK_AUTHOR,
                    Identifier = dataModel.BOOK_ID,
                    NumPages = dataModel.NUM_PAGES,
                    PublishedDate = dataModel.PUBLISHED_DATE,
                    Title = dataModel.BOOK_TITLE
                };

                booksResponses.Add(response);
            }
            return booksResponses;
        }
    }
}
