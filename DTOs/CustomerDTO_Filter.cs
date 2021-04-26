using NetCoreAPI_Template_v3_with_auth.DTOs;

namespace GitLibary.DTOs
{
      public class CustomerDTO_Filter : PaginationDto
      {
            public string Name { get; set; }
            public string Email { get; set; }

      }
}