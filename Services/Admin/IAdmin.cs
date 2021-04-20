using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreAPI_Template_v3_with_auth.DTOs.Admin;
using NetCoreAPI_Template_v3_with_auth.Models;

namespace NetCoreAPI_Template_v3_with_auth.Services.Admin
{
      // to call and connect between API-Service
      public interface IAdmin
      {
            Task<ServiceResponse<AdminDTO_ToReturn>> AddAdmin(AdminDTO_ToCreate CreateAdmin);
            //Task คือการทำแบบ async await
            Task<ServiceResponse<List<AdminDTO_ToReturn>>> GetAllAdmin();
            Task<ServiceResponse<AdminDTO_ToReturn>> GetAdminById(int id);

            //response อะไรกลับไปต้อง return กลับไปแบบนั้นด้วย  >>  Task<ServiceResponse<AdminDTO_ToReturn>> = return(_mapper.Map<AdminDTO_ToReturn>(adminById))
            Task<ServiceResponse<AdminDTO_ToReturn>> UpdateAdmin(AdminDTO_ToUpdate UpdateAdmin, int id);

            Task<ServiceResponse<List<AdminDTO_ToReturn>>> DeleteAdminById(int id);

      }
}