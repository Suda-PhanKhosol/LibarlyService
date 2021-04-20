using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    [Table("Customer")]

    public class Customer
    {
        [Key]
        [Range(1, int.MaxValue)] public int Id { get; set; }


        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(50, ErrorMessage = "The Name must be 50 character")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Mobile phone is required")]
        [StringLength(10, ErrorMessage = "The Mobile phone must be 10 character")]
        public string MobilePhone { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email not match type")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Id Card is required")]
        [StringLength(13)]
        public string IdCardNumber { get; set; }
        //ไม่ต้องเก็บ RF เพราะใน transaction มีแล้ว มันก็จะซ้อนๆกันอีก     ลูปซ้อนลูป (Nested Loops)

    }
}