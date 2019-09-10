using System;
using System.ComponentModel.DataAnnotations;

namespace MovaClubWebApp.Models.Account
{
    public enum UserType
    {
        Teacher,
        Student
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage =  "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Must be between 7 and 15 characters", MinimumLength = 7)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Must be between 7 and 15 characters", MinimumLength = 7)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthday date is required!")]
        public DateTime? BirthdayDate { get; set; }
        [Required(ErrorMessage = "User type is required!")]
        public UserType? UserType { get; set; }


    }
}
