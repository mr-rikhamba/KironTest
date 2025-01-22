using System;

namespace KironTest.Logic.Models;

public class UserModel : BaseModel
{
    public long UserId { get; set; }
    public required string Username { get; set; }
    public required string LoginPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
}
