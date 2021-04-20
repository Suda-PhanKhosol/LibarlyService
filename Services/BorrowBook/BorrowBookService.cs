using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook;
using NetCoreAPI_Template_v3_with_auth.Models;
using mBorrowBook = NetCoreAPI_Template_v3_with_auth.Models.BorrowBook;



namespace NetCoreAPI_Template_v3_with_auth.Services.BorrowBook
{
      public class BorrowBookService : ServiceBase, IBorrowBook
      {

            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<BorrowBookService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public BorrowBookService(AppDBContext dbContext, IMapper mapper, ILogger<BorrowBookService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }

            public async Task<ServiceResponse<BorrowBookDTO_ToReturn>> CreateBorrowBook(BorrowBookDTO_ToCreate AddBorrowBook)
            {
                  var newBorrow = new mBorrowBook();
                  var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == AddBorrowBook.BookId);
                  newBorrow.BookId = AddBorrowBook.BookId;
                  newBorrow.CustomerId = AddBorrowBook.CustomerId;
                  newBorrow.AdminId = AddBorrowBook.AdminId;
                  newBorrow.RecieveDate = AddBorrowBook.RecieveDate;
                  newBorrow.DueDate = AddBorrowBook.RecieveDate.AddDays(book.BorrowDay);// newBorrow.RecieveDate.AddDays(book.BorrowDay); ;

                  // ใช้ตอน Edit Update newBorrow.ReturnDate = AddBorrowBook.ReturnDate;
                  newBorrow.TotalLatePrice = 0;
                  newBorrow.TotalPrice = book.BorrowPrice * book.BorrowDay;
                  newBorrow.TotalAmount = newBorrow.TotalPrice;

                  await _dbContext.AddAsync(newBorrow);
                  await _dbContext.SaveChangesAsync();
                  var lastData = await _dbContext.BorrowBooks.MaxAsync(x => x.Id);
                  var result = await _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == lastData).FirstOrDefaultAsync();

                  return ResponseResult.Success(_mapper.Map<BorrowBookDTO_ToReturn>(result));
            }



            public async Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> GetAllBorrowBook()
            {
                  var borrowBook = await _dbContext.BorrowBooks
                  .Include(x => x.Books).ThenInclude(x => x.CategoryBooks)
                  .Include(x => x.Customers).AsNoTracking().ToListAsync();

                  return ResponseResult.Success(_mapper.Map<List<BorrowBookDTO_ToReturn>>(borrowBook));
            }

            public async Task<ServiceResponse<BorrowBookDTO_ToReturn>> GetBorrowBookById(int Id)
            {
                  var borrowBook = await _dbContext.BorrowBooks.FirstOrDefaultAsync(x => x.Id == Id);
                  return ResponseResult.Success(_mapper.Map<BorrowBookDTO_ToReturn>(borrowBook));
            }

            public async Task<ServiceResponse<BorrowBookDTO_ToReturn>> UpdateBorrowBook(BorrowBookDTO_ToUpdate UpdateBorrowBook, int id)
            {
                  var oldDataBorrowBook = await _dbContext.BorrowBooks.FirstOrDefaultAsync(x => x.Id == id);
                  var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == oldDataBorrowBook.BookId);

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
                  var borrowBook = await _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == oldDataBorrowBook.Id).FirstOrDefaultAsync();

                  return ResponseResult.Success(_mapper.Map<BorrowBookDTO_ToReturn>(borrowBook));

            }

            public async Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> DeleteBorrowBookById(int Id)
            {
                  var borrowBook = await _dbContext.BorrowBooks.FirstOrDefaultAsync(x => x.Id == Id);
                  _dbContext.Remove(borrowBook);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<List<BorrowBookDTO_ToReturn>>(_dbContext.BorrowBooks.ToList()));

            }

            public async Task<ServiceResponse<BorrowBookDTO_ToReturn>> ReNewBorrowBook(BorrowBookDTO_ToReNew ReNewBorrowBook, int id)
            {
                  var oldDataBorrowBook = await _dbContext.BorrowBooks.FirstOrDefaultAsync(x => x.Id == id);
                  var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == oldDataBorrowBook.BookId);

                  int newDate = ReNewBorrowBook.RecieveDate.Day - oldDataBorrowBook.RecieveDate.Day;


                  oldDataBorrowBook.AdminId = ReNewBorrowBook.AdminId;
                  oldDataBorrowBook.RecieveDate = ReNewBorrowBook.RecieveDate;
                  oldDataBorrowBook.DueDate = ReNewBorrowBook.RecieveDate.AddDays(book.BorrowDay);
                  oldDataBorrowBook.TotalLatePrice = 0;
                  oldDataBorrowBook.TotalPrice = (book.BorrowPrice * book.BorrowDay) + (book.BorrowPrice * newDate);


                  oldDataBorrowBook.TotalAmount = oldDataBorrowBook.TotalPrice;
                  await _dbContext.SaveChangesAsync();

                  return ResponseResult.Success(
                        _mapper.Map<BorrowBookDTO_ToReturn>
                        (
                         _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks)
                        .Include(x => x.Customers)
                        .Where(x => x.Id == oldDataBorrowBook.Id)
                        .FirstOrDefault()
                        )
                  );

            }

            public async Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> SearchBorrowByCustomer(string cusName)
            {
                  var borrowBook = await _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks)
                       .Include(x => x.Customers)
                       .Where(x => x.Customers.Name.ToUpper().Contains(cusName.ToUpper())).ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<BorrowBookDTO_ToReturn>>(borrowBook));

            }

            public async Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> SearchBorrowByBook(string bookName)
            {
                  var borrowBook = await _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks)
                      .Include(x => x.Customers)
                      .Where(x => x.Books.Name.ToUpper().Contains(bookName.ToUpper())).ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<BorrowBookDTO_ToReturn>>(borrowBook));
            }

      }
}