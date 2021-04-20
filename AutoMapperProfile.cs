using AutoMapper;
using NetCoreAPI_Template_v3_with_auth.DTOs;
using NetCoreAPI_Template_v3_with_auth.DTOs.Admin;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook;
using NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;
using NetCoreAPI_Template_v3_with_auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPI_Template_v3_with_auth
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.RoleName, x => x.MapFrom(x => x.Name));
            CreateMap<RoleDtoAdd, Role>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.RoleName)); ;
            CreateMap<UserRole, UserRoleDto>();


            CreateMap<Admin, AdminDTO_ToReturn>().ReverseMap();
            CreateMap<Customer, CustomerDTO_ToRetun>().ReverseMap();
            CreateMap<CategoryBook, CategoryBookDTO_ToReturn>().ReverseMap();
            CreateMap<Book, BookDTO_ToReturn>().ReverseMap();
            CreateMap<Book, BorrowBookDTO_ToReturn>().ReverseMap();
            CreateMap<BorrowBook, BorrowBookDTO_ToReturn>().ReverseMap();







        }
    }
}