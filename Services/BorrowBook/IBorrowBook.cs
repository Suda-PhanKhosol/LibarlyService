using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Services.BorrowBook
{
      public interface IBorrowBook
      {
            Task<ServiceResponse<BorrowBookDTO_ToReturn>> CreateBorrowBook(BorrowBookDTO_ToCreate AddBorrowBook);
            Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> GetAllBorrowBook();
            Task<ServiceResponse<BorrowBookDTO_ToReturn>> GetBorrowBookById(int Id);
            Task<ServiceResponse<BorrowBookDTO_ToReturn>> UpdateBorrowBook(BorrowBookDTO_ToUpdate UpdateBorrowBook, int id);
            Task<ServiceResponse<List<BorrowBookDTO_ToReturn>>> DeleteBorrowBookById(int Id);
      }
}