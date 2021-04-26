using NetCoreAPI_Template_v3_with_auth.DTOs;

namespace GitLibary.DTOs
{
      public class BorrowBookDTO_Filter : PaginationDto
      {
            public string BookName { get; set; }
            public string CusName { get; set; }
            // public int BorrowId { get; set; }
            // public int AdminId { get; set; }

      }
}