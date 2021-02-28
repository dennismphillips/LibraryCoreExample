using LibraryCoreExample.Logic;
using LibraryCoreExample.Models;
using LibraryCoreExample.Models.WebRequest;
using LibraryCoreExample.Models.WebResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryCoreExample.Controllers
{
    [ApiController]
    public class LibraryWebServiceController : ControllerBase
    {
        private readonly ILibraryLogic _libraryLogic;

        public LibraryWebServiceController(ILibraryLogic libraryLogic)
        {
            _libraryLogic = libraryLogic;
        }

        [Route("/api/v1/core/getallbooks")]
        [ProducesResponseType(200, Type = typeof(Response<List<BookResponse>>))]
        [HttpGet]
        public ActionResult GetAllBooks()
        {
            List<BookResponse> books = _libraryLogic.GetAllBooks();
            return Ok(new Response<List<BookResponse>>
            {
                Status = "200",
                ResponseMessage = "Data Retrieved Successfully",
                ResponseData = books
            });
        }

        [Route("/api/v1/core/addnewbook")]
        [ProducesResponseType(200, Type = typeof(Response<int>))]
        [HttpPost]
        public ActionResult AddNewBook([FromBody] BookRequest aNewBook)
        {
            if(ModelState.IsValid) { 
                int response = _libraryLogic.AddNewBook(aNewBook);
                return Ok(new Response<int>
                {
                    Status = "200",
                    ResponseMessage = "Book Added Successfully. Identifier is on the responseData",
                    ResponseData = response
                });
            } else {
                return BadRequest(new Response<BookResponse>
                {
                    Status = "400",
                    ResponseMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)),
                    ResponseData = null
                });
            }
        }

        [Route("/api/v1/core/registercustomer")]
        [ProducesResponseType(200, Type = typeof(Response<int>))]
        [HttpPost]
        public ActionResult AddNewCustomer([FromBody] CustomerRequest aNewCustomer)
        {
            if (ModelState.IsValid)
            {
                int response = _libraryLogic.AddNewCustomer(aNewCustomer);
                return Ok(new Response<int>
                {
                    Status = "200",
                    ResponseMessage = "Customer Added Successfully. Identifier is on the responseData",
                    ResponseData = response
                });
            }
            else
            {
                return BadRequest(new Response<CustomerResponse>
                {
                    Status = "400",
                    ResponseMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)),
                    ResponseData = null
                });
            }
        }

        [Route("/api/v1/core/checkout")]
        [ProducesResponseType(200, Type = typeof(Response<int>))]
        [HttpPost]
        public ActionResult Checkout([FromBody] CheckoutRequest checkoutRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = _libraryLogic.CheckoutABook(checkoutRequest);
                    if (success)
                    {
                        return Ok(new Response<bool>
                        {
                            Status = "200",
                            ResponseMessage = "Book Checked out successfully.",
                            ResponseData = success
                        });
                    }
                    else
                    {
                        return BadRequest(new Response<bool>
                        {
                            Status = "500",
                            ResponseMessage = "Book Checkout did not succeed. Please try again later",
                            ResponseData = success
                        });
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(new Response<string>
                    {
                        Status = "500",
                        ResponseMessage = "Book Checkout did not succeed. Please try again later",
                        ResponseData = e.Message
                    });
                }
            }
            else
            {
                return BadRequest(new Response<bool>
                {
                    Status = "400",
                    ResponseMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)),
                    ResponseData = false
                });
            }
        }
    }
}
