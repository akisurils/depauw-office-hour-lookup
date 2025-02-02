
// using static Mysqlx.Datatypes.Scalar.Types;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace depauw_officer_hour_lookup.Model {
    public class OfficeHourModel {
        public int Id{get;set;}
        public string Name{get;set;}
    }
    public class Users: IdentityUser{
        public string FullName{get;set;}
    }

    public class UserModel{
        public int Id{get;set;}

        [Required]
        public string Name{get;set;}

        [Required]
        public string Username{get;set;}

        [EmailAddress]
        public string Email{get;set;}

        [PasswordPropertyText]
        [Required]
        public string Password{get;set;}

    
    }
    public class LoginCredentials {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}