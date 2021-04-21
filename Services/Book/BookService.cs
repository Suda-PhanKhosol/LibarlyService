using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.Models;
using mBook = NetCoreAPI_Template_v3_with_auth.Models.Book;


namespace NetCoreAPI_Template_v3_with_auth.Services.Book
{
      public class BookService : ServiceBase, IBook
      {
            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<BookService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public BookService(AppDBContext dbContext, IMapper mapper, ILogger<BookService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  _dbContext = dbContext;
                  _mapper = mapper;
                  _log = log;
                  _httpContext = httpContext;
            }

            public async Task<ServiceResponse<BookDTO_ToReturn>> CreateCustomer(BookDTO_ToCreate AddBook)
            {
                  var newBook = new mBook();

                  newBook.Name = AddBook.Name;
                  newBook.Writter = AddBook.Writter;
                  newBook.BorrowDay = AddBook.BorrowDay;
                  newBook.BorrowPrice = AddBook.BorrowPrice;
                  newBook.LatePrice = AddBook.LatePrice;
                  newBook.CategoryBookId = AddBook.CategoryBookId;

                  _dbContext.Add(newBook);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<BookDTO_ToReturn>(newBook));
            }

            public async Task<ServiceResponse<List<BookDTO_ToReturn>>> GetAllBook()
            {
                  var book = await _dbContext.Books.Include(x => x.CategoryBooks).AsNoTracking().ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<BookDTO_ToReturn>>(book));

            }

            public async Task<ServiceResponse<BookDTO_ToReturn>> GetBookById(int Id)
            {
                  var book = await _dbContext.Books.Include(x => x.CategoryBooks).Where(x => x.Id == Id).FirstOrDefaultAsync();
                  return ResponseResult.Success(_mapper.Map<BookDTO_ToReturn>(book));

            }

            public async Task<ServiceResponse<BookDTO_ToReturn>> UpdateBook(BookDTO_ToUpdate UpdateBook, int id)
            {
                  var oldBookData = _dbContext.Books.FirstOrDefault(x => x.Id == id);
                  var cateGoryBookCheck = _dbContext.CategoryBooks.FirstOrDefault(x => x.Id == UpdateBook.CategoryBookId);
                  if (cateGoryBookCheck == null)
                  {
                        return ResponseResult.Failure<BookDTO_ToReturn>("CategoryBook id not found");

                  }
                  else
                  {
                        if (oldBookData.Name != UpdateBook.Name ||
                        oldBookData.Writter != UpdateBook.Writter ||
                        oldBookData.BorrowDay != UpdateBook.BorrowDay ||
                        oldBookData.BorrowPrice != UpdateBook.BorrowPrice ||
                        oldBookData.LatePrice != UpdateBook.LatePrice ||
                        oldBookData.CategoryBookId != UpdateBook.CategoryBookId)
                        {
                              oldBookData.Name = UpdateBook.Name;
                              oldBookData.Writter = UpdateBook.Writter;
                              oldBookData.BorrowDay = UpdateBook.BorrowDay;
                              oldBookData.BorrowPrice = UpdateBook.BorrowPrice;
                              oldBookData.LatePrice = UpdateBook.LatePrice;
                              //check categoryBook
                              oldBookData.CategoryBookId = UpdateBook.CategoryBookId;
                              await _dbContext.SaveChangesAsync();
                        }
                        return ResponseResult.Success(_mapper.Map<BookDTO_ToReturn>(oldBookData));
                  }
            }

            public async Task<ServiceResponse<List<BookDTO_ToReturn>>> DeleteBookrById(int Id)
            {
                  var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == Id);
                  _dbContext.Remove(book);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<List<BookDTO_ToReturn>>(_dbContext.Books.ToList()));

            }
      }
}