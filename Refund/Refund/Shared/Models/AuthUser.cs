using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class AuthUser
{

        [Required(ErrorMessage = "Username must not be empty")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password field is empty")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int UserId { get;set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string Group { get; set; }
        public DateTime expiry { get; set; }
    }
}
