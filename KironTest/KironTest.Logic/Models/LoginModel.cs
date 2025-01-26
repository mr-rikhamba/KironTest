using System;
using System.ComponentModel.DataAnnotations;

namespace KironTest.Logic.Models;

public class LoginModel
{

    [MinLength(0)]
    [MaxLength(60)]
    public required string Username { get; set; }
    [MinLength(0)]
    [MaxLength(60)]
    public required string Password { get; set; }
    public bool IsValid { get { return !string.IsNullOrWhiteSpace(Username) || !string.IsNullOrWhiteSpace(Password); } }
}
