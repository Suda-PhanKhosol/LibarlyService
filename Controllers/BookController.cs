using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GitLibary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.Models;
using NetCoreAPI_Template_v3_with_auth.Services.Book;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class BookController : ControllerBase
      {
            private readonly IBook _bookService;

            public BookController(IBook bookService)
            {
                  this._bookService = bookService;
            }

            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] BookDTO_Filter filter)
            {
                  return Ok(await _bookService.SearchPagination(filter));
            }

            [HttpPost("CreateBook")]
            public async Task<IActionResult> CreateBook(BookDTO_ToCreate book)
            {
                  return Ok(await _bookService.CreateCustomer(book));
            }

            [HttpGet("GetAllBook")]
            public async Task<IActionResult> GetAllBook()
            {
                  return Ok(await _bookService.GetAllBook());
            }

            [HttpGet("GetBookById")]
            public async Task<IActionResult> GetBookById(int id)
            {
                  return Ok(await _bookService.GetBookById(id));
            }

            [HttpPut("UpdateBook/{id}")]
            public async Task<IActionResult> UpdateBook(BookDTO_ToUpdate book, int id)
            {
                  return Ok(await _bookService.UpdateBook(book, id));
            }

            [HttpDelete("DeleteBookById/{id}")]
            public async Task<IActionResult> DeleteBookById(int id)
            {
                  return Ok(await _bookService.DeleteBookrById(id));
            }
      }
}