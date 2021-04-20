using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowBookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDBContext _db;

        public BorrowBookController(IMapper mapper, AppDBContext db)
        {
            this._mapper = mapper;
            this._db = db;
        }

        [HttpPost("AddBorrowBook")]
        public IActionResult AddBorrowBook(BorrowBookDTO_ToCreate AddBorrowBook)
        {
            var newBorrow = new BorrowBook();
            var book = _db.Books.FirstOrDefault(x => x.Id == AddBorrowBook.BookId);
            newBorrow.BookId = AddBorrowBook.BookId;
            newBorrow.CustomerId = AddBorrowBook.CustomerId;
            newBorrow.AdminId = AddBorrowBook.AdminId;
            newBorrow.RecieveDate = AddBorrowBook.RecieveDate;
            newBorrow.DueDate = AddBorrowBook.RecieveDate.AddDays(book.BorrowDay);// newBorrow.RecieveDate.AddDays(book.BorrowDay); ;

            // ใช้ตอน Edit Update newBorrow.ReturnDate = AddBorrowBook.ReturnDate;
            newBorrow.TotalLatePrice = 0;
            newBorrow.TotalPrice = book.BorrowPrice * book.BorrowDay;
            newBorrow.TotalAmount = newBorrow.TotalPrice;

            _db.Add(newBorrow);
            _db.SaveChanges();
            var lastData = _db.BorrowBooks.Max(x => x.Id);
            var result = _db.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == lastData).FirstOrDefault();

            return Ok(_mapper.Map<BorrowBookDTO_ToReturn>(result));


        }

        [HttpGet("GetAllBorrowBook")]
        public IActionResult GetAllBorrowBook()
        {
            var getBorrowBook = _db.BorrowBooks
            .Include(x => x.Books)
            .ThenInclude(x => x.CategoryBooks)
            .Include(x => x.Customers)
            .ToList();

            return Ok(_mapper.Map<List<BorrowBookDTO_ToReturn>>(getBorrowBook));
        }

        [HttpGet("SearchBorrowByCustomer/{cusName}")]
        public IActionResult SearchBorrowByCustomer(string cusName)
        {
            return Ok(_mapper.Map<List<BorrowBookDTO_ToReturn>>(_db.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Customers.Name.Contains(cusName)).ToList()));
        }

        [HttpGet("SearchBorrowByBook/{bookName}")]
        public IActionResult SearchBorrowByBook(string bookName)
        {
            return Ok(_mapper.Map<List<BorrowBookDTO_ToReturn>>(_db.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Books.Name.Contains(bookName)).ToList()));
        }

        [HttpPut("UpdateBorrowBook/{id}")]
        public IActionResult UpdateBorrowBook(BorrowBookDTO_ToUpdate UpdateBorrowBook, int id)
        {
            var oldDataBorrowBook = _db.BorrowBooks.FirstOrDefault(x => x.Id == id);
            var book = _db.Books.FirstOrDefault(x => x.Id == oldDataBorrowBook.BookId);

            if (UpdateBorrowBook.ReturnDate > oldDataBorrowBook.DueDate)
            {
                int lateDate = UpdateBorrowBook.ReturnDate.Day - oldDataBorrowBook.DueDate.Day;

                oldDataBorrowBook.TotalLatePrice = lateDate * book.LatePrice;
                oldDataBorrowBook.ReturnDate = UpdateBorrowBook.ReturnDate;
                oldDataBorrowBook.AdminId = UpdateBorrowBook.AdminId;
                oldDataBorrowBook.TotalAmount = oldDataBorrowBook.TotalPrice + oldDataBorrowBook.TotalLatePrice;

            }
            else
            {
                oldDataBorrowBook.ReturnDate = UpdateBorrowBook.ReturnDate;
                oldDataBorrowBook.AdminId = UpdateBorrowBook.AdminId;
                oldDataBorrowBook.TotalAmount = oldDataBorrowBook.TotalPrice + oldDataBorrowBook.TotalLatePrice;

            }
            return Ok(_db.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == oldDataBorrowBook.Id).FirstOrDefault());
        }

        [HttpPut("ReNewBorrowBook/{id}")]
        public IActionResult ReNewBorrowBook(BorrowBookDTO_ToReNew ReNewBorrowBook, int id)
        {
            var oldDataBorrowBook = _db.BorrowBooks.FirstOrDefault(x => x.Id == id);
            var book = _db.Books.FirstOrDefault(x => x.Id == oldDataBorrowBook.BookId);

            int newDate = ReNewBorrowBook.RecieveDate.Day - oldDataBorrowBook.RecieveDate.Day;


            oldDataBorrowBook.AdminId = ReNewBorrowBook.AdminId;
            oldDataBorrowBook.RecieveDate = ReNewBorrowBook.RecieveDate;
            oldDataBorrowBook.DueDate = ReNewBorrowBook.RecieveDate.AddDays(book.BorrowDay);
            oldDataBorrowBook.TotalLatePrice = 0;
            oldDataBorrowBook.TotalPrice = (book.BorrowPrice * book.BorrowDay) + (book.BorrowPrice * newDate);


            oldDataBorrowBook.TotalAmount = oldDataBorrowBook.TotalPrice;
            _db.SaveChanges();

            return Ok(_db.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == oldDataBorrowBook.Id).FirstOrDefault());
        }

    }
}