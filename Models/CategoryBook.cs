using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    [Table("CategoryBook")]

    public class CategoryBook
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }


        [Required(ErrorMessage = "CategoryBook Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 50 character")]
        public string Type { get; set; }

        [Required(ErrorMessage = "CodeType Name is required")]
        [StringLength(4, ErrorMessage = "The Name must be 4 character")]
        public string CodeType { get; set; }
    }
}