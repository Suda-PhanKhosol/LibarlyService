using System.ComponentModel.DataAnnotations;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.Admin
{
    public class AdminDTO_ToUpdate
    {
        [Required(ErrorMessage = "Admin Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 2 - 50 character")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Mobile phone is required")]
        [StringLength(10, ErrorMessage = "The Mobile phone must be 10 character")]
        public string MobilePhone { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email not match type")]
        public string Email { get; set; }
    }
}