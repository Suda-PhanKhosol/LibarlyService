using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.Book
{
    public class BookDTO_ToCreate
    {

        [Required(ErrorMessage = "Book Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 50 character")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Writter Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 50 character")]
        public string Writter { get; set; }


        [Range(1, 20)]
        public int BorrowDay { get; set; }
        public int BorrowPrice { get; set; }
        public int LatePrice { get; set; }

        public int CategoryBookId { get; set; }
    }
}