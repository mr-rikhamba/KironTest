using System;
using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface IUserContract
{
    Task<BaseResponseModel<UserModel>> CreateUser(UserModel user);
    Task<BaseResponseModel<AuthModel>> LoginUser(string username, string password);
}
