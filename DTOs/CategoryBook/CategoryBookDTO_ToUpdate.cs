using System.ComponentModel.DataAnnotations;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.CategoryBook
{
    public class CategoryBookDTO_ToUpdate
    {
        [Required(ErrorMessage = "CategoryBook Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 50 character")]
        public string Type { get; set; }

        [Required(ErrorMessage = "CodeType Name is required")]
        [StringLength(4, ErrorMessage = "The Name must be 50 character")]
        public string CodeType { get; set; }
    }
}