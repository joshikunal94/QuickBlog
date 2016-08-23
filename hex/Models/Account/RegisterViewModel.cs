using hex.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace hex.Models.Account
{
    public class RegisterViewModel
    {
        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="Passwords Do not match!!")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}