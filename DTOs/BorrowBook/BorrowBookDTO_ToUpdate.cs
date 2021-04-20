using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreAPI_Template_v3_with_auth.DTOs.BorrowBook
{
    public class BorrowBookDTO_ToUpdate
    {


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }

        [Range(1, int.MaxValue)]
        public int AdminId { get; set; }

    }
}