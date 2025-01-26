using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using KironTest.Logic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KironTest.Logic.Services;

public class UserService(ILogger<UserService> _logger, IRepositoryContract _repository, IOptions<AuthConfig> authConfig) : IUserContract
{
    readonly AuthConfig _authConfig = authConfig.Value;
    public async Task<BaseResponseModel<UserModel>> CreateUser(UserModel user)
    {
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@username", user.Username, DbType.String);
            parameters.Add("@password", user.LoginPassword, DbType.String);
            parameters.Add("@firstName", user.FirstName, DbType.String);
            parameters.Add("@lastName", user.LastName, DbType.String);

            var newUser = await _repository.ExecuteSingle<UserModel>("SP_CreateUser", parameters);

            return new BaseResponseModel<UserModel> { ResponseData = newUser };
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Create failed.");
            return new BaseResponseModel<UserModel> { IsSuccessful = false, ResponseMessage = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create failed.");
            throw;
        }
    }

    public async Task<BaseResponseModel<AuthModel>> LoginUser(string username, string password)
    {
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", username, DbType.String);
            parameters.Add("@Password", password, DbType.String);

            var result = await _repository.ExecuteSingle<UserModel>("SP_LoginUser", parameters);
            return new BaseResponseModel<AuthModel>
            {
                ResponseData = new AuthModel
                {
                    Token = GenerateJwtToken(result),
                    User = result
                }
            };
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Login failed.");
            return new BaseResponseModel<AuthModel> { IsSuccessful = false, ResponseMessage = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create failed.");
            throw;
        }
    }

    private string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authConfig.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            }),
            Expires = DateTime.Now.AddMinutes(_authConfig.TokenDurationMins),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}

