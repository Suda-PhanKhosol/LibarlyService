using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    [Table("Book")]

    public class Book
    {

        [Key]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }




        [Required(ErrorMessage = "Book Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 2 - 50 character")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Writter Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 2 - 50 character")]
        public string Writter { get; set; }


        [Range(1, 20)]
        public int BorrowDay { get; set; }
        public int BorrowPrice { get; set; }
        public int LatePrice { get; set; }

        public int CategoryBookId { get; set; }
        public CategoryBook CategoryBooks { get; set; }


    }
}