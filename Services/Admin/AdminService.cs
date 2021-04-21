using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAPI_Template_v3_with_auth.Data;
using NetCoreAPI_Template_v3_with_auth.DTOs.Admin;
using NetCoreAPI_Template_v3_with_auth.Models;
using mAdmin = NetCoreAPI_Template_v3_with_auth.Models.Admin;

//Business logic 

namespace NetCoreAPI_Template_v3_with_auth.Services.Admin
{
      //main service , httpContext = ,ServiceBase = 

      public class AdminService : ServiceBase, IAdmin
      {
            //private variable >>> call
            private readonly AppDBContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<AdminService> _log;
            private readonly IHttpContextAccessor _httpContext;

            public AdminService(AppDBContext dbContext, IMapper mapper, ILogger<AdminService> log, IHttpContextAccessor httpContext) : base(dbContext, mapper, httpContext)
            {
                  _dbContext = dbContext;
                  _mapper = mapper;
                  _log = log;
                  _httpContext = httpContext;
            }
            //async no queue ไม่รอผลลัพธ์ตอบกลับมาก่อน ทำอย่างอื่นรอได้
            public async Task<ServiceResponse<AdminDTO_ToReturn>> AddAdmin(AdminDTO_ToCreate CreateAdmin)
            {
                  // try {}catch =need >>  return ResponseResult.error ...
                  var newAdmin = new mAdmin();
                  newAdmin.Name = CreateAdmin.Name;
                  newAdmin.MobilePhone = CreateAdmin.MobilePhone;
                  newAdmin.Email = CreateAdmin.Email;
                  _dbContext.Admins.Add(newAdmin);
                  await _dbContext.SaveChangesAsync();
                  var dto = _mapper.Map<AdminDTO_ToReturn>(newAdmin);
                  //return for 
                  return ResponseResult.Success(dto);
            }



            //response อะไรกลับไปต้อง return กลับไปแบบนั้นด้วย  >>  Task<ServiceResponse<AdminDTO_ToReturn>> = return(_mapper.Map<AdminDTO_ToReturn>(adminById))
            public async Task<ServiceResponse<AdminDTO_ToReturn>> GetAdminById(int id)
            {
                  var adminById = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);
                  var result = _mapper.Map<AdminDTO_ToReturn>(adminById);
                  return ResponseResult.Success(result);
            }

            public async Task<ServiceResponse<List<AdminDTO_ToReturn>>> GetAllAdmin()
            {
                  var getAdmin = await _dbContext.Admins.AsNoTracking().ToListAsync();
                  var admin = _mapper.Map<List<AdminDTO_ToReturn>>(getAdmin);
                  //   var admin = _mapper.Map<List<AdminDTO_ToReturn>>(_dbContext.Admins.AsNoTracking().ToListAsync());
                  //ResponseResult ต้องใช้ async
                  return ResponseResult.Success(admin);
            }

            public async Task<ServiceResponse<AdminDTO_ToReturn>> UpdateAdmin(AdminDTO_ToUpdate UpdateAdmin, int id)
            {
                  var oldAdminData = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);
                  if (oldAdminData.Name != UpdateAdmin.Name || oldAdminData.MobilePhone != UpdateAdmin.MobilePhone || oldAdminData.Email != UpdateAdmin.Email)
                  {
                        oldAdminData.Name = UpdateAdmin.Name;
                        oldAdminData.MobilePhone = UpdateAdmin.MobilePhone;
                        oldAdminData.Email = UpdateAdmin.Email;
                        await _dbContext.SaveChangesAsync();
                  }
                  return ResponseResult.Success(_mapper.Map<AdminDTO_ToReturn>(oldAdminData));
            }
            public async Task<ServiceResponse<List<AdminDTO_ToReturn>>> DeleteAdminById(int id)
            {
                  var findAdmin = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);

                  _dbContext.Remove(findAdmin);
                  await _dbContext.SaveChangesAsync();
                  var admin = _dbContext.Admins.ToList();
                  return ResponseResult.Success(_mapper.Map<List<AdminDTO_ToReturn>>(admin));



            }
      }
}