using System;


using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v3_with_auth.DTOs.Book;
using NetCoreAPI_Template_v3_with_auth.DTOs.Customer;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook
{
    public class BorrowBookDTO_ToCreate
    {


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RecieveDate { get; set; }


        [Range(1, int.MaxValue)]
        public int BookId { get; set; }


        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }



        [Range(1, int.MaxValue)]
        public int AdminId { get; set; }
    }
}