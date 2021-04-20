using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    [Table("BorrowBook")]

    public class BorrowBook
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RecieveDate { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }
        public int TotalPrice { get; set; }

        public int TotalLatePrice { get; set; }

        public int TotalAmount { get; set; }


        //FK Object เพราะเราจะดูแค่ว่ายืมคืนหนังสือเล่มเดียวไปก่อน
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }
        public Book Books { get; set; }


        //FK Object เพราะเราจะดูแค่ว่าลูกค้าคนเดียวยืมไปก่อน
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }


        //RefferenceK Admin คนเดียวที่อ้างไปถึง

        [Range(1, int.MaxValue)]
        public int AdminId { get; set; }
    }
}