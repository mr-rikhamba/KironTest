using System;

namespace KironTest.Logic.Models;

public class AuthModel : BaseModel
{
    public string Token { get; set; }
    public UserModel User { get; set; }
}
