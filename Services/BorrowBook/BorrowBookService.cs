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

            public Task<ServiceResponse<BorrowBookDTO_ToReturn>> CreateBorrowBook(BorrowBookDTO_ToCreate AddBorrowBook)
            {
                  throw new System.NotImplementedException();
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
                  var book = _dbContext.Books.FirstOrDefault(x => x.Id == oldDataBorrowBook.BookId);

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
                  var borrowBook = _dbContext.BorrowBooks.Include(x => x.Books).ThenInclude(x => x.CategoryBooks).Include(x => x.Customers).Where(x => x.Id == oldDataBorrowBook.Id).FirstOrDefault();

                  return ResponseResult.Success(_mapper.Map<BorrowBookDTO_ToReturn>(borrowBook));

            }

            public async Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> DeleteBorrowBookById(int Id)
            {
                  var borrowBook = await _dbContext.BorrowBooks.FirstOrDefaultAsync(x => x.Id == Id);
                  _dbContext.Remove(borrowBook);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<List<BorrowBookDTO_ToReturn>>(_dbContext.BorrowBooks.ToList()));

            }
      }
}