using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain.Login;

public class RegisterModel
{
    [Required(ErrorMessage = "User Name is required")]  
    public string Username { get; set; }  
  
    [EmailAddress]  
    [Required(ErrorMessage = "Email is required")]  
    public string Email { get; set; }  
    
    public string Password { get; set; } 
}