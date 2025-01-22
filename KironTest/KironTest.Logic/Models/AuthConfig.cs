using System;

namespace KironTest.Logic.Models;


public class AuthConfig
{
    public string JwtSecret { get; set; }
    public int TokenDurationMins { get; set; }
}
