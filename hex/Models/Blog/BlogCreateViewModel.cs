using hex.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hex.Models.Blog
{
    public class BlogCreateViewModel
    {
        [Required(AllowEmptyStrings=false,ErrorMessage="Name is Required!!")]
        [MaxLength(150,ErrorMessage="Title cannot be made of more than 150 characters ")]
        public string Title { get; set; }

        [MaxLength(150,ErrorMessage  = "Cannot use more than 500 characters in description")]
        public string Description { get; set; }
        public BlogCategories Category { get; set; }


    }
}