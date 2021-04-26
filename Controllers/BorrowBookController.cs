using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GitLibary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook;
using NetCoreAPI_Template_v3_with_auth.Models;
using NetCoreAPI_Template_v3_with_auth.Services.BorrowBook;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class BorrowBookController : ControllerBase
      {
            private readonly IBorrowBook _borrowBookService;

            public BorrowBookController(IBorrowBook borrowBookService)
            {
                  this._borrowBookService = borrowBookService;
            }
            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] BorrowBookDTO_Filter filter)
            {
                  return Ok(await _borrowBookService.SearchPagination(filter));
            }
            [HttpPost("AddBorrowBook")]
            public async Task<IActionResult> AddBorrowBook(BorrowBookDTO_ToCreate AddBorrowBook)
            {
                  return Ok(await _borrowBookService.CreateBorrowBook(AddBorrowBook));
            }

            [HttpGet("GetAllBorrowBook")]
            public async Task<IActionResult> GetAllBorrowBook()
            {
                  return Ok(await _borrowBookService.GetAllBorrowBook());
            }

            [HttpGet("SearchBorrowByCustomer/{cusName}")]
            public async Task<IActionResult> SearchBorrowByCustomer(string cusName)
            {
                  return Ok(await _borrowBookService.SearchBorrowByCustomer(cusName));
            }

            [HttpGet("SearchBorrowByBook/{bookName}")]
            public async Task<IActionResult> SearchBorrowByBook(string bookName)
            {
                  return Ok(await _borrowBookService.SearchBorrowByBook(bookName));
            }

            [HttpPut("UpdateBorrowBook/{id}")]
            public async Task<IActionResult> UpdateBorrowBook(BorrowBookDTO_ToUpdate UpdateBorrowBook, int id)
            {
                  return Ok(await _borrowBookService.UpdateBorrowBook(UpdateBorrowBook, id));
            }

            [HttpPut("ReNewBorrowBook/{id}")]
            public async Task<IActionResult> ReNewBorrowBook(BorrowBookDTO_ToReNew ReNewBorrowBook, int id)
            {
                  return Ok(await _borrowBookService.ReNewBorrowBook(ReNewBorrowBook, id));
            }

            [HttpDelete("DeleteBorrowBookById/{id}")]
            public async Task<IActionResult> DeleteBorrowBookById(int id)
            {
                  return Ok(await _borrowBookService.DeleteBorrowBookById(id));
            }

      }
}