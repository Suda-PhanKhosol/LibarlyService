using NetCoreAPI_Template_v3_with_auth.DTOs;

namespace GitLibary.DTOs
{
      public class BookDTO_Filter : PaginationDto
      {
            public string Name { get; set; }
            // public string Writer { get; set; }
            public string CategoryCode { get; set; }
            public int MinPrice { get; set; }


      }
}