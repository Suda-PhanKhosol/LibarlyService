using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Services.Book
{
      public interface IBook
      {
            Task<ServiceResponse<BookDTO_ToReturn>> CreateCustomer(BookDTO_ToCreate AddBook);
            Task<ServiceResponse<List<BookDTO_ToReturn>>> GetAllBook();
            Task<ServiceResponse<BookDTO_ToReturn>> GetBookById(int Id);
            Task<ServiceResponse<BookDTO_ToReturn>> UpdateBook(BookDTO_ToUpdate UpdateBook, int id);
            Task<ServiceResponse<List<BookDTO_ToReturn>>> DeleteBookrById(int Id);
      }
}