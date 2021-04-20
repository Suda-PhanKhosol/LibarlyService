using System;
using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook
{
    public class BorrowBookDTO_ToReturn
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RecieveDate { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }
        public int TotalPrice { get; set; }
        public int TotalLatePrice { get; set; }
        public int TotalAmount { get; set; }


        [Range(1, int.MaxValue)]
        public int BookId { get; set; }
        public BookDTO_ToReturn Books { get; set; }


        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
        public CustomerDTO_ToRetun Customers { get; set; }


        [Range(1, int.MaxValue)]
        public int AdminId { get; set; }
    }
}