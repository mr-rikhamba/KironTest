using System;
using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface IUserContract
{
    Task<BaseModel> CreateUser(UserModel user);
    Task<BaseModel> LoginUser(string username, string password);
}
