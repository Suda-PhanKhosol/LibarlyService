using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Template_v3_with_auth.Models;

using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Admin;
using System.Linq;
using System.Collections.Generic;
using NetCoreAPI_Template_v3_with_auth.Services.Admin;
using System.Threading.Tasks;
using GitLibary.DTOs;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class AddminController : ControllerBase
      {
            private readonly IAdmin _adminService;

            //call services  >> all inject in service
            public AddminController(IAdmin adminService)
            {
                  _adminService = adminService;
            }


            [HttpGet("SearchPagination")]
            public async Task<IActionResult> SearchPagination([FromQuery] AdminDTO_Filter filter)
            {
                  return Ok(await _adminService.SearchPagination(filter));
            }

            [HttpPost("AddAdmin")]
            public async Task<IActionResult> AddAdmin(AdminDTO_ToCreate Admin)
            {
                  return Ok(await _adminService.AddAdmin(Admin));
            }


            [HttpGet("GetAllAdmin")]
            public async Task<IActionResult> GetAllAdmin()
            {
                  return Ok(await _adminService.GetAllAdmin());
            }

            [HttpGet("GetAdminById")]
            public async Task<IActionResult> GetAllAdmin(int Id)
            {
                  return Ok(await _adminService.GetAdminById(Id));
            }

            [HttpPut("UpdateAdmin/{id}")]
            public async Task<IActionResult> UpdateAdmin(AdminDTO_ToUpdate UpdateAdmin, int id)
            {

                  return Ok(await _adminService.UpdateAdmin(UpdateAdmin, id));
            }


            [HttpDelete("DeleteAdminById/{id}")]
            public async Task<IActionResult> DeleteAdminById(int id)
            {
                  return Ok(_adminService.DeleteAdminById(id));
            }

      }
}