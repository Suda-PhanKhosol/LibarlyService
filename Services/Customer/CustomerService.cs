using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;
using NetCoreAPI_Template_v3_with_auth.Models;
using mCustomer = NetCoreAPI_Template_v3_with_auth.Models.Customer;

namespace NetCoreAPI_Template_v3_with_auth.Services.Customer
{
      public class CustomerService : ServiceBase, ICustomer
      {

            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<CustomerService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public CustomerService(AppDBContext dbContext, IMapper mapper, ILogger<CustomerService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  this._dbContext = dbContext;
                  this._mapper = mapper;
                  this._log = log;
                  this._httpContext = httpContext;
            }

            public async Task<ServiceResponse<CustomerDTO_ToRetun>> CreateCustomer(CustomerDTO_ToCreate NewCustomer)
            {
                  var newCustomer = new mCustomer();
                  newCustomer.Name = NewCustomer.Name;
                  newCustomer.MobilePhone = NewCustomer.MobilePhone;
                  newCustomer.Email = NewCustomer.Email;
                  newCustomer.IdCardNumber = NewCustomer.IdCardNumber;
                  _dbContext.Add(newCustomer);
                  await _dbContext.SaveChangesAsync();
                  return ResponseResult.Success(_mapper.Map<CustomerDTO_ToRetun>(newCustomer));
            }


            public async Task<ServiceResponse<List<CustomerDTO_ToRetun>>> GetAllCustomer()
            {
                  var customer = await _dbContext.Customers.AsNoTracking().ToListAsync();
                  return ResponseResult.Success(_mapper.Map<List<CustomerDTO_ToRetun>>(customer));

            }

            public async Task<ServiceResponse<CustomerDTO_ToRetun>> GetCustomerById(int Id)
            {
                  var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == Id);
                  return ResponseResult.Success(_mapper.Map<CustomerDTO_ToRetun>(customer));


            }

            public async Task<ServiceResponse<CustomerDTO_ToRetun>> UpdateCustomer(CustomerDTO_ToUpdate UpdateACustomer, int id)
            {
                  var oldCustomerData = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                  if (oldCustomerData.Name != UpdateACustomer.Name || oldCustomerData.MobilePhone != UpdateACustomer.MobilePhone || oldCustomerData.Email != UpdateACustomer.Email)
                  {
                        oldCustomerData.Name = UpdateACustomer.Name;
                        oldCustomerData.MobilePhone = UpdateACustomer.MobilePhone;
                        oldCustomerData.Email = UpdateACustomer.Email;
                        oldCustomerData.IdCardNumber = UpdateACustomer.IdCardNumber;

                        await _dbContext.SaveChangesAsync();
                  }
                  return ResponseResult.Success(_mapper.Map<CustomerDTO_ToRetun>(oldCustomerData));

            }
            public async Task<ServiceResponse<List<CustomerDTO_ToRetun>>> DeleteCustomerById(int Id)
            {
                  var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == Id);
                  _dbContext.Remove(customer);
                  await _dbContext.SaveChangesAsync();
                  // ใช้แบบนี้ไม่ได้ return ResponseResult.Success(_mapper.Map<List<CustomerDTO_ToRetun>>(_dbContext.Customers.AsNoTracking().ToListAsync()));
                  return ResponseResult.Success(_mapper.Map<List<CustomerDTO_ToRetun>>(_dbContext.Customers.ToList()));

            }

      }
}