using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Template_v3_with_auth.Models;

using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Admin;
using System.Linq;
using System.Collections.Generic;
using NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook;
using NetCoreAPI_Template_v3_with_auth.Services.CategoryBook;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GitLibary.DTOs;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
      [ApiController]
      [Route("api/[controller]")]

      // [Authorize]
      public class CategoryBookController : ControllerBase
      {
            private readonly ICategoryBook _categoryBookService;

            public CategoryBookController(ICategoryBook categoryBookService)
            {
                  this._categoryBookService = categoryBookService;
            }
            [HttpGet("SearchPaginate")]
            public async Task<IActionResult> SearchPaginate([FromQuery] CategoryBookDTO_Filter filter)
            {

                  return Ok(await _categoryBookService.SearchPaginate(filter));
            }

            [HttpPost("AddCategoryBook")]
            public async Task<IActionResult> AddACustomer(CategoryBookDTO_ToCreate AddCategoryBook)
            {

                  return Ok(await _categoryBookService.CreateCategoryBook(AddCategoryBook));
            }

            [HttpGet("GetAllCategoryBook")]
            // [Authorize(Roles = "Manager")]

            public async Task<IActionResult> GetAllCategoryBook()
            {
                  return Ok(await _categoryBookService.GetAllCategoryBook());
            }



            [HttpGet("GetCategoryBookById")]
            public async Task<IActionResult> GetCategoryBookById(int Id)
            {
                  return Ok(await _categoryBookService.GetCategoryBookById(Id));
            }

            [HttpPut("UpdateCategoryBook/{id}")]
            public async Task<IActionResult> UpdateCategoryBook(CategoryBookDTO_ToUpdate UpdateCategoryBook, int id)
            {

                  return Ok(await _categoryBookService.UpdateCategoryBook(UpdateCategoryBook, id));
            }

            [HttpDelete("DeleteCategoryBookById/{id}")]
            public async Task<IActionResult> DeleteCategoryBookById(int id)
            {

                  return Ok(await _categoryBookService.DeleteCategoryBookById(id));
            }
      }
}