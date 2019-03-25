using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Models
{
    public class Player
    {

        [Key]
        [DisplayName("Account ID")]
        public int PlayerId { get; set; }


        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string PlayerEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(
            "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9]) | " +
            "(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9]) |" +
            "(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", 
            ErrorMessage = "Your password must be classified as at least strong. A good password consist of: <br> " +
            "- 8 or more characters <br> " +
            "- Mixture of letters and numbers <br> " +
            "- Mixure of upper- and lowercase <br> " +
            "- Special character (e.g. !@#$%^&*)")]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PlayerPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PlayerPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string PlayerConfirmedPassword { get; set; }

        public string PlayerRole { get; set; } = "player";
        public string PlayerToken { get; set; }

        public string PlayerProviderKey { get; set; }

        [Required]
        [DisplayName("Username")]
        public string PlayerName { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [ForeignKey("Game")]
        public int? GameId { get; set; }



    }
}
