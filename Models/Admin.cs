using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }


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