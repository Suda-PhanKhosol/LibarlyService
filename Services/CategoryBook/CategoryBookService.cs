using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook;
using NetCoreAPI_Template_v3_with_auth.Models;
using mCategoryBook = NetCoreAPI_Template_v3_with_auth.Models.CategoryBook;
namespace NetCoreAPI_Template_v3_with_auth.Services.CategoryBook
{
      public class CategoryBookService : ServiceBase, ICategoryBook
      {
            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<CategoryBookService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public CategoryBookService(AppDBContext dbContext, IMapper mapper, ILogger<CategoryBookService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }

            public async Task<ServiceResponse<CategoryBookDTO_ToReturn>> CreateCategoryBook(CategoryBookDTO_ToCreate AddCategoryBook)
            {
                  var newCategoryBook = new mCategoryBook();
                  newCategoryBook.Type = AddCategoryBook.Type;
                  newCategoryBook.CodeType = AddCategoryBook.CodeType;
                  _dbContext.Add(newCategoryBook);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<CategoryBookDTO_ToReturn>(newCategoryBook));
            }



            public async Task<ServiceResponse<List<CategoryBookDTO_ToReturn>>> GetAllCategoryBook()
            {
                  var categoryBook = await _dbContext.CategoryBooks.AsNoTracking().ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<CategoryBookDTO_ToReturn>>(categoryBook));
            }

            public async Task<ServiceResponse<CategoryBookDTO_ToReturn>> GetCategoryBookById(int id)
            {
                  return ResponseResult.Success(_mapper.Map<CategoryBookDTO_ToReturn>(_dbContext.CategoryBooks.FirstOrDefault(x => x.Id == id)));
            }

            public async Task<ServiceResponse<CategoryBookDTO_ToReturn>> UpdateCategoryBook(CategoryBookDTO_ToUpdate UpdateCategoryBook, int id)
            {
                  var oldCategoryBook = _dbContext.CategoryBooks.FirstOrDefault(x => x.Id == id);
                  if (oldCategoryBook.Type != UpdateCategoryBook.Type || oldCategoryBook.CodeType != UpdateCategoryBook.CodeType)
                  {
                        oldCategoryBook.Type = UpdateCategoryBook.Type;
                        oldCategoryBook.CodeType = UpdateCategoryBook.CodeType;

                        _dbContext.SaveChanges();
                  }
                  return ResponseResult.Success(_mapper.Map<CategoryBookDTO_ToReturn>(oldCategoryBook));
            }
            public async Task<ServiceResponse<List<CategoryBookDTO_ToReturn>>> DeleteCategoryBookById(int id)
            {
                  _dbContext.Remove(_dbContext.CategoryBooks.FirstOrDefault(x => x.Id == id));
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<List<CategoryBookDTO_ToReturn>>(_dbContext.CategoryBooks.ToList()));
            }
      }
}