using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;
using NetCoreAPI_Template_v3_with_auth.Models;
using NetCoreAPI_Template_v3_with_auth.Services.Customer;

namespace NetCoreAPI_Template_v3_with_auth.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class CustomerController : ControllerBase
      {
            private readonly ICustomer _customerService;

            public CustomerController(ICustomer customerService)
            {
                  this._customerService = customerService;
            }

            [HttpPost("CreateCustomer")]
            public async Task<IActionResult> CreateCustomer(CustomerDTO_ToCreate customer)
            {
                  return Ok(await _customerService.CreateCustomer(customer));
            }

            [HttpGet("GetAllCustomer")]
            public async Task<IActionResult> GetAllCustomer()
            {
                  return Ok(await _customerService.GetAllCustomer());
            }

            [HttpGet("GetCustomerById/{id}")]
            public async Task<IActionResult> GetCustomerById(int id)
            {
                  return Ok(await _customerService.GetCustomerById(id));
            }

            [HttpPut("GetCustomerById/{id}")]
            public async Task<IActionResult> GetCustomerById(CustomerDTO_ToUpdate customer, int id)
            {
                  return Ok(await _customerService.UpdateCustomer(customer, id));
            }

            [HttpDelete("DeleteCustomerById/{id}")]
            public async Task<IActionResult> DeleteCustomerById(int id)
            {
                  return Ok(await _customerService.DeleteCustomerById(id));
            }
      }
}