using System.Collections.Generic;
using System.Threading.Tasks;
using GitLibary.DTOs;
using NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Services.CategoryBook
{
      public interface ICategoryBook
      {
            Task<ServiceResponse<CategoryBookDTO_ToReturn>> CreateCategoryBook(CategoryBookDTO_ToCreate AddCategoryBook);
            Task<ServiceResponse<List<CategoryBookDTO_ToReturn>>> GetAllCategoryBook();
            Task<ServiceResponse<CategoryBookDTO_ToReturn>> GetCategoryBookById(int id);

            Task<ServiceResponse<CategoryBookDTO_ToReturn>> UpdateCategoryBook(CategoryBookDTO_ToUpdate UpdateCategoryBook, int id);
            Task<ServiceResponse<List<CategoryBookDTO_ToReturn>>> DeleteCategoryBookById(int id);
            Task<ServiceResponseWithPagination<List<CategoryBookDTO_ToReturn>>> SearchPaginate(CategoryBookDTO_Filter filter);





      }
}