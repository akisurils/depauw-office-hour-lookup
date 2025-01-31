
// using static Mysqlx.Datatypes.Scalar.Types;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace depauw_officer_hour_lookup.Model {
    public class OfficeHourModelClass {
        public int Id{get;set;}
        public string Name{get;set;}
    }

    public class UserModelClass {
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
}