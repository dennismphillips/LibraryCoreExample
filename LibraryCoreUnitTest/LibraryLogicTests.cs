using LibraryCoreExample.Logic;
using LibraryCoreExample.Models;
using LibraryCoreExample.Models.DataModels;
using LibraryCoreExample.Models.WebRequest;
using LibraryCoreExample.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace LibraryCoreUnitTest
{
    [TestClass]
    public class LibraryLogicTests
    {
        ILibraryLogic myLogic;

        [TestInitialize]
        public void Setup()
        {
            var libraryBookValidationMock = new Mock<ILibraryQuery>();

            //Sets up a Book database table
            libraryBookValidationMock.Setup(p => p.GetAllBooks()).Returns(new List<BookDataModel>() {
                new BookDataModel() {
                    BOOK_AUTHOR = "Bob",
                    BOOK_ID = 1,
                    BOOK_TITLE = "A Way Home",
                    NUM_PAGES = 412,
                    PUBLISHED_DATE = DateTime.Now
                },
                new BookDataModel() {
                    BOOK_AUTHOR = "Sam",
                    BOOK_ID = 2,
                    BOOK_TITLE = "A Way Around",
                    NUM_PAGES = 600,
                    PUBLISHED_DATE = DateTime.Now
                }
            });

            //Sets up a user database table
            libraryBookValidationMock.Setup(p => p.GetCustomerByIdentifier(3)).Returns(new CustomerDataModel()
            {
                CUSTOMER_EMAIL = "abc@123.com",
                CUSTOMER_ID = 3,
                CUSTOMER_NAME = "Sally",
                CUSTOMER_PHONE = "555-555-5555"
            });

            //Add the mock database to the library logic
            myLogic = new LibraryLogic(libraryBookValidationMock.Object);
        }
        [TestMethod]
        public void TestGetAllBooks()
        {
            List<BookResponse> bookResponses =  myLogic.GetAllBooks();

            Assert.IsTrue(bookResponses.Count == 2);

            Assert.IsTrue(bookResponses[0].Author == "Bob");

            Assert.IsTrue(bookResponses[1].Author == "Sam");

            Assert.IsTrue(bookResponses[1].NumPages == 600);
        }

        [TestMethod]
        public void TestCheckoutCustomerDoesntExist()
        {
            CheckoutRequest request = new CheckoutRequest()
            {
                BookIdentifier = 1,
                CustomerIdentifier = 1
            };
            

            try { 
                myLogic.CheckoutABook(request);
            } catch (Exception e)
            {
                Assert.IsTrue(e != null);
                Assert.IsTrue(e.Message.Contains("The customer does not exist in this system"));
            }
        }

        [TestMethod]
        public void TestCheckoutBookDoesntExist()
        {
            CheckoutRequest request = new CheckoutRequest()
            {
                BookIdentifier = 1,
                CustomerIdentifier = 3
            };


            try
            {
                myLogic.CheckoutABook(request);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e != null);
                Assert.IsTrue(e.Message.Contains("The book does not exist in this system"));
            }
        }
    }
}
