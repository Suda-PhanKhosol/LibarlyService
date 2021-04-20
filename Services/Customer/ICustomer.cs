using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Services.Customer
{
      public interface ICustomer
      {
            Task<ServiceResponse<CustomerDTO_ToRetun>> CreateCustomer(CustomerDTO_ToCreate NewCustomer);
            Task<ServiceResponse<List<CustomerDTO_ToRetun>>> GetAllCustomer();
            Task<ServiceResponse<CustomerDTO_ToRetun>> GetCustomerById(int Id);
            Task<ServiceResponse<CustomerDTO_ToRetun>> UpdateCustomer(CustomerDTO_ToUpdate UpdateACustomer, int id);
            Task<ServiceResponse<List<CustomerDTO_ToRetun>>> DeleteCustomerById(int Id);



      }
}